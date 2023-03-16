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