using Microsoft.AspNetCore.Http;

namespace Furion.Logging;

/// <summary>
/// LoggingMonitor 上下文
/// </summary>
[SuppressSniffer]
public static class LoggingMonitorContext
{
    internal const string KEY = nameof(LoggingMonitorContext);

    /// <summary>
    /// 追加附加信息
    /// </summary>
    /// <param name="items"></param>
    public static void Append(Dictionary<string, object> items)
    {
        var httpContextItems = App.HttpContext?.Items;
        if (httpContextItems == null)
        {
            return;
        }

        if (httpContextItems.ContainsKey(KEY))
        {
            httpContextItems.Remove(KEY);
        }

        httpContextItems.Add(KEY, items);
    }

    /// <summary>
    /// 追加附加信息
    /// </summary>
    /// <param name="action"></param>
    public static void Append(Action<Dictionary<string, object>, HttpContext> action)
    {
        if (action == null)
        {
            throw new ArgumentNullException(nameof(action));
        }

        var httpContext = App.HttpContext;
        if (httpContext == null)
        {
            return;
        }

        var items = new Dictionary<string, object>();
        action?.Invoke(items, httpContext);

        Append(items);
    }
}