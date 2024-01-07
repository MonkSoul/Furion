// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.Logging;

/// <summary>
/// LogContext 拓展
/// </summary>
[SuppressSniffer]
public static class LogContextExtensions
{
    /// <summary>
    /// 设置上下文数据
    /// </summary>
    /// <param name="logContext"></param>
    /// <param name="key">键</param>
    /// <param name="value">值</param>
    /// <returns></returns>
    public static LogContext Set(this LogContext logContext, object key, object value)
    {
        if (logContext == null || key == null) return logContext;

        logContext.Properties ??= new Dictionary<object, object>();

        if (logContext.Properties.ContainsKey(key)) logContext.Properties.Remove(key);
        logContext.Properties.Add(key, value);
        return logContext;
    }

    /// <summary>
    /// 批量设置上下文数据
    /// </summary>
    /// <param name="logContext"></param>
    /// <param name="properties"></param>
    /// <returns></returns>
    public static LogContext SetRange(this LogContext logContext, IDictionary<object, object> properties)
    {
        if (logContext == null
            || properties == null
            || properties.Count == 0) return logContext;

        foreach (var (key, value) in properties)
        {
            logContext.Set(key, value);
        }

        return logContext;
    }

    /// <summary>
    /// 获取上下文数据
    /// </summary>
    /// <param name="logContext"></param>
    /// <param name="key">键</param>
    /// <returns></returns>
    public static object Get(this LogContext logContext, object key)
    {
        if (logContext == null
            || key == null
            || logContext.Properties == null
            || logContext.Properties.Count == 0) return default;

        var isExists = logContext.Properties.TryGetValue(key, out var value);
        return isExists ? value : null;
    }

    /// <summary>
    /// 获取上下文数据
    /// </summary>
    /// <param name="logContext"></param>
    /// <param name="key">键</param>
    /// <returns></returns>
    public static object Get<T>(this LogContext logContext, object key)
    {
        var value = logContext.Get(key);
        return (T)value;
    }
}