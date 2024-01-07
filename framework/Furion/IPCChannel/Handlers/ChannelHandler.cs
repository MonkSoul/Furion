// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.IPCChannel;

/// <summary>
/// 进程管道内通信处理程序
/// </summary>
/// <typeparam name="TMessage"></typeparam>
public abstract class ChannelHandler<TMessage>
{
    /// <summary>
    /// 管道执行器
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    public abstract Task InvokeAsync(TMessage message);
}