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

using Furion.ClayObject.Extensions;
using Furion.Extensions;
using System.Text;

namespace System.Net.Http;

/// <summary>
/// HttpRequestMessage 拓展
/// </summary>
[SuppressSniffer]
public static class HttpRequestMessageExtensions
{
    /// <summary>
    /// 追加查询参数
    /// </summary>
    /// <param name="httpRequest"></param>
    /// <param name="queries"></param>
    /// <param name="isEncode"></param>
    /// <param name="ignoreNullValue"></param>
    public static void AppendQueries(this HttpRequestMessage httpRequest, IDictionary<string, object> queries, bool isEncode = true, bool ignoreNullValue = false)
    {
        if (queries == null || queries.Count == 0) return;

        // 获取原始地址
        var finalRequestUrl = httpRequest.RequestUri?.OriginalString ?? string.Empty;

        // 拼接
        var urlParameters = ExpandQueries(queries, isEncode, ignoreNullValue);
        finalRequestUrl += $"{(finalRequestUrl.IndexOf("?") > -1 ? "&" : "?")}{string.Join("&", urlParameters)}";

        // 重新设置地址
        httpRequest.RequestUri = new Uri(finalRequestUrl, UriKind.RelativeOrAbsolute);
    }

    /// <summary>
    /// 追加查询参数
    /// </summary>
    /// <param name="httpRequest"></param>
    /// <param name="queries"></param>
    /// <param name="isEncode"></param>
    public static void AppendQueries(this HttpRequestMessage httpRequest, object queries, bool isEncode = true)
    {
        if (queries == null) return;

        httpRequest.AppendQueries(queries.ToDictionary(), isEncode);
    }

    /// <summary>
    /// 展开 Url 参数
    /// </summary>
    /// <param name="queries"></param>
    /// <param name="isEncode"></param>
    /// <param name="ignoreNullValue"></param>
    /// <returns></returns>
    private static IEnumerable<string> ExpandQueries(IDictionary<string, object> queries, bool isEncode = true, bool ignoreNullValue = false)
    {
        var items = new List<string>();

        foreach (var (key, value) in queries)
        {
            // 处理忽略 null 值问题
            if (ignoreNullValue && value == null) continue;

            var paramBuilder = new StringBuilder();
            paramBuilder.Append(key);
            paramBuilder.Append('=');

            var type = value?.GetType();
            if (type == null) items.Add(paramBuilder.ToString());
            // 处理基元类型
            else if (type.IsRichPrimitive()
                && (!type.IsArray || type == typeof(string)))
            {
                paramBuilder.Append(isEncode ? Uri.EscapeDataString(value.ToString()) : value.ToString());
                items.Add(paramBuilder.ToString());
            }
            // 处理集合类型
            else if (type.IsArray
                || (typeof(IEnumerable).IsAssignableFrom(type)
                    && type.IsGenericType && type.GenericTypeArguments.Length == 1))
            {
                var valueList = ((IEnumerable)value)?.Cast<object>();

                // 这里不进行递归，只处理一级
                foreach (var val in valueList)
                {
                    var childBuilder = new StringBuilder();
                    childBuilder.Append(key);
                    childBuilder.Append('=');
                    childBuilder.Append(isEncode ? Uri.EscapeDataString(val.ToString()) : val.ToString());
                    items.Add(childBuilder.ToString());
                }
            }
            else throw new InvalidOperationException("Unsupported type.");
        }

        return items;
    }

    /// <summary>
    /// 追加请求报文头
    /// </summary>
    /// <param name="httpRequest"></param>
    /// <param name="headers"></param>
    public static void AppendHeaders(this HttpRequestMessage httpRequest, IDictionary<string, object> headers)
    {
        if (headers == null) return;

        foreach (var header in headers)
        {
            if (header.Value != null) httpRequest.Headers.TryAddWithoutValidation(header.Key, header.Value.ToString());
        }
    }

    /// <summary>
    /// 追加请求报文头
    /// </summary>
    /// <param name="httpRequest"></param>
    /// <param name="headers"></param>
    public static void AppendHeaders(this HttpRequestMessage httpRequest, object headers)
    {
        httpRequest.AppendHeaders(headers.ToDictionary());
    }
}