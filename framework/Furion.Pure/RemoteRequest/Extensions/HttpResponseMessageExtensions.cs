// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using Furion.RemoteRequest;

namespace System.Net.Http;

/// <summary>
/// HttpResponseMessage 拓展
/// </summary>
[SuppressSniffer]
public static class HttpResponseMessageExtensions
{
    /// <summary>
    /// 获取 Set-Cookie 数据
    /// </summary>
    /// <param name="httpResponse"></param>
    /// <returns></returns>
    public static IEnumerable<string> GetSetCookies(this HttpResponseMessage httpResponse)
    {
        // 读取响应报文中的 Set-Cookie 数据
        return httpResponse.Headers.SingleOrDefault(header => header.Key.Equals("Set-Cookie", StringComparison.OrdinalIgnoreCase)).Value;
    }

    /// <summary>
    /// 获取 Cookies
    /// </summary>
    /// <param name="httpResponse"></param>
    public static Dictionary<string, string> GetCookies(this HttpResponseMessage httpResponse)
    {
        // 读取响应报文中的 Set-Cookie 数据
        var setCookies = httpResponse.GetSetCookies();

        // 支持重复 Key
        var cookies = new Dictionary<string, string>(new RepeatKeyEqualityComparer());

        // 遍历所有 Set-Cookie 键
        foreach (var setCookie in setCookies)
        {
            if (string.IsNullOrWhiteSpace(setCookie)) continue;

            // 根据 ; 切割
            var cookieParts = setCookie.Split(';', StringSplitOptions.RemoveEmptyEntries);
            if (cookieParts.Length > 0)
            {
                // 根据第一个 = 切割
                var cookieString = cookieParts[0];
                var index = cookieString.IndexOf('=');

                var cookieKey = cookieString[..index];
                var cookieValue = Uri.UnescapeDataString(cookieString[(index + 1)..]);

                cookies.Add(cookieKey, cookieValue);
            }
        }

        return cookies;
    }
}