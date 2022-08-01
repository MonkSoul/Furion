// MIT License
//
// Copyright (c) 2020-2022 百小僧, Baiqian Co.,Ltd.
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using Furion.FriendlyException;
using System.Threading.Channels;

namespace Furion.IPCChannel;

/// <summary>
/// 进程管道内通信上下文
/// </summary>
/// <typeparam name="TMessage"></typeparam>
/// <typeparam name="THandler"></typeparam>
/// <remarks>后续将通过 MemoryMapperFile 共享内存实现 IPC 通信：https://docs.microsoft.com/zh-cn/dotnet/api/system.io.memorymappedfiles.memorymappedfile?view=net-5.0 </remarks>
[SuppressSniffer]
public sealed class ChannelContext<TMessage, THandler>
    where THandler : ChannelHandler<TMessage>
{
    /// <summary>
    /// 通过懒加载创建无限容量通道
    /// </summary>
    private static readonly Lazy<Channel<TMessage>> _unBoundedChannel = new(() =>
    {
        var channel = Channel.CreateUnbounded<TMessage>(new UnboundedChannelOptions
        {
            SingleReader = false,   // 允许多个管道读写，提供管道吞吐量（无序操作）
            SingleWriter = false
        });

        StartReader(channel);
        return channel;
    });

    /// <summary>
    /// 通过懒加载创建有限容量通道
    /// </summary>
    /// <remarks>默认容量为 1000</remarks>
    private static readonly Lazy<Channel<TMessage>> _boundedChannel = new(() =>
    {
        var channel = Channel.CreateBounded<TMessage>(new BoundedChannelOptions(1000)
        {
            FullMode = BoundedChannelFullMode.Wait,
            SingleReader = false,   // 允许多个管道读写，提供管道吞吐量（无序操作）
            SingleWriter = false
        });

        StartReader(channel);
        return channel;
    });

    /// <summary>
    /// 无限容量通道
    /// </summary>
    public static Channel<TMessage> UnBoundedChannel => _unBoundedChannel.Value;

    /// <summary>
    /// 有限容量通道
    /// </summary>
    public static Channel<TMessage> BoundedChannel => _boundedChannel.Value;

    /// <summary>
    /// 私有构造函数
    /// </summary>
    private ChannelContext()
    {
    }

    /// <summary>
    /// 创建一个读取器
    /// </summary>
    /// <param name="channel"></param>
    private static void StartReader(Channel<TMessage> channel)
    {
        var reader = channel.Reader;

        // 创建长时间线程管道读取器
        _ = Task.Factory.StartNew(async () =>
          {
              while (await reader.WaitToReadAsync())
              {
                  if (!reader.TryRead(out var message)) continue;

                  // 并行执行（非等待）
                  var task = new Task(async () =>
              {
                  // 默认重试 3 次（每次间隔 1s）
                  await Retry.InvokeAsync(async () => await Activator.CreateInstance<THandler>().InvokeAsync(message), 3, 1000, finalThrow: false);
              });
                  task.Start();
              }
          }, TaskCreationOptions.LongRunning);
    }
}