// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using Furion.Templates.Extensions;
using Microsoft.Extensions.Logging;

namespace Furion.Logging;

/// <summary>
/// 构建字符串日志部分类
/// </summary>
public sealed partial class StringLoggingPart
{
    /// <summary>
    /// 设置消息
    /// </summary>
    /// <param name="message"></param>
    public StringLoggingPart SetMessage(string message)
    {
        // 支持读取配置渲染
        if (message != null) Message = message.Render();
        return this;
    }

    /// <summary>
    /// 设置日志级别
    /// </summary>
    /// <param name="level"></param>
    public StringLoggingPart SetLevel(LogLevel level)
    {
        Level = level;
        return this;
    }

    /// <summary>
    /// 设置消息格式化参数
    /// </summary>
    /// <param name="args"></param>
    public StringLoggingPart SetArgs(params object[] args)
    {
        if (args != null && args.Length > 0) Args = args;
        return this;
    }

    /// <summary>
    /// 设置事件 Id
    /// </summary>
    /// <param name="eventId"></param>
    public StringLoggingPart SetEventId(EventId eventId)
    {
        EventId = eventId;
        return this;
    }

    /// <summary>
    /// 设置日志分类
    /// </summary>
    /// <typeparam name="TClass"></typeparam>
    public StringLoggingPart SetCategory<TClass>()
    {
        CategoryType = typeof(TClass);
        return this;
    }

    /// <summary>
    /// 设置异常对象
    /// </summary>
    public StringLoggingPart SetException(Exception exception)
    {
        if (exception != null) Exception = exception;
        return this;
    }

    /// <summary>
    /// 设置日志服务作用域
    /// </summary>
    /// <param name="serviceProvider"></param>
    /// <returns></returns>
    public StringLoggingPart SetLoggerScoped(IServiceProvider serviceProvider)
    {
        if (serviceProvider != null) LoggerScoped = serviceProvider;
        return this;
    }

    /// <summary>
    /// 配置日志上下文
    /// </summary>
    /// <param name="properties">建议使用 ConcurrentDictionary 类型</param>
    /// <returns></returns>
    public StringLoggingPart ScopeContext(IDictionary<object, object> properties)
    {
        if (properties == null) return this;
        LogContext = new LogContext { Properties = properties };

        return this;
    }

    /// <summary>
    /// 配置日志上下文
    /// </summary>
    /// <param name="configure"></param>
    /// <returns></returns>
    public StringLoggingPart ScopeContext(Action<LogContext> configure)
    {
        var logContext = new LogContext();
        configure?.Invoke(logContext);

        LogContext = logContext;

        return this;
    }

    /// <summary>
    /// 配置日志上下文
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public StringLoggingPart ScopeContext(LogContext context)
    {
        if (context == null) return this;
        LogContext = context;

        return this;
    }
}