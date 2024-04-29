// ------------------------------------------------------------------------
// 版权信息
// 版权归百小僧及百签科技（广东）有限公司所有。
// 所有权利保留。
// 官方网站：https://baiqian.com
//
// 许可证信息
// Furion 项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。
// 许可证的完整文本可以在源代码树根目录中的 LICENSE-APACHE 和 LICENSE-MIT 文件中找到。
// 官方网站：https://furion.net
//
// 使用条款
// 使用本代码应遵守相关法律法规和许可证的要求。
//
// 免责声明
// 对于因使用本代码而产生的任何直接、间接、偶然、特殊或后果性损害，我们不承担任何责任。
//
// 其他重要信息
// Furion 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。
// 有关 Furion 项目的其他详细信息，请参阅位于源代码树根目录中的 COPYRIGHT 和 DISCLAIMER 文件。
//
// 更多信息
// 请访问 https://gitee.com/dotnetchina/Furion 获取更多关于 Furion 项目的许可证和版权信息。
// ------------------------------------------------------------------------

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
                  await Retry.InvokeAsync(async () => await Activator.CreateInstance<THandler>().InvokeAsync(message), 3, 1000, finalThrow: false);
              }
          }, TaskCreationOptions.LongRunning);
    }
}