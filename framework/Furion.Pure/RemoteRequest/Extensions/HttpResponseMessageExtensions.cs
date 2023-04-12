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
            // 根据 ; 切割
            var cookieParts = setCookie.Split(';', StringSplitOptions.RemoveEmptyEntries);
            if (cookieParts.Length > 0)
            {
                // 根据 = 切割
                var cookiePart = cookieParts[0].Split('=');
                var cookieKey = cookiePart[0];
                var cookieValue = Uri.UnescapeDataString(cookiePart[1]);

                cookies.Add(cookieKey, cookieValue);
            }
        }

        return cookies;
    }
}