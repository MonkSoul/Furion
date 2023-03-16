// MIT 许可证
//
// 版权 © 2020-present 百小僧, 百签科技（广东）有限公司 和所有贡献者
//
// 特此免费授予任何获得本软件副本和相关文档文件（下称“软件”）的人不受限制地处置该软件的权利，
// 包括不受限制地使用、复制、修改、合并、发布、分发、转授许可和/或出售该软件副本，
// 以及再授权被配发了本软件的人如上的权利，须在下列条件下：
//
// 上述版权声明和本许可声明应包含在该软件的所有副本或实质成分中。
//
// 本软件是“如此”提供的，没有任何形式的明示或暗示的保证，包括但不限于对适销性、特定用途的适用性和不侵权的保证。
// 在任何情况下，作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

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