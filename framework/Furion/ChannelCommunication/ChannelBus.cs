// Copyright (c) 2020-2021 百小僧, Baiqian Co.,Ltd.
// Furion is licensed under Mulan PSL v2.
// You can use this software according to the terms and conditions of the Mulan PSL v2.
// You may obtain a copy of Mulan PSL v2 at:
//             http://license.coscl.org.cn/MulanPSL2
// THIS SOFTWARE IS PROVIDED ON AN "AS IS" BASIS, WITHOUT WARRANTIES OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO NON-INFRINGEMENT, MERCHANTABILITY OR FIT FOR A PARTICULAR PURPOSE.
// See the Mulan PSL v2 for more details.

using Furion.DependencyInjection;
using Furion.FriendlyException;
using System;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Furion.ChannelCommunication
{
    /// <summary>
    /// 进程管道内通信总线
    /// </summary>
    /// <typeparam name="TMessage"></typeparam>
    /// <typeparam name="THandler"></typeparam>
    [SuppressSniffer]
    public sealed class ChannelBus<TMessage, THandler>
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
        private ChannelBus()
        {
        }

        /// <summary>
        /// 创建一个读写器
        /// </summary>
        /// <param name="channel"></param>
        private static void StartReader(Channel<TMessage> channel)
        {
            var reader = channel.Reader;
            ChannelHandler<TMessage> handler = Activator.CreateInstance<THandler>();

            // 创建长时间线程管道读取器
            _ = Task.Factory.StartNew(async () =>
              {
                  while (await reader.WaitToReadAsync())
                  {
                      if (!reader.TryRead(out var message)) continue;

                      // 默认重试 10 次（每次延迟 500）毫秒
                      await Retry.Invoke(async () => await handler.InvokeAsync(message), 10, 500, finalThrow: false);
                  }
              }, TaskCreationOptions.LongRunning);
        }
    }
}