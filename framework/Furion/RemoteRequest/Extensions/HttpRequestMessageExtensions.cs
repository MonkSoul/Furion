// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

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