// MIT License
//
// Copyright (c) 2020-2023 百小僧, Baiqian Co.,Ltd and Contributors
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

        var cookies = new Dictionary<string, string>();

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