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

using System.Text.Json;

namespace Furion.JsonSerialization;

/// <summary>
/// JSON 静态帮助类
/// </summary>
[SuppressSniffer]
public static class JSON
{
    /// <summary>
    /// 获取 JSON 序列化提供器
    /// </summary>
    /// <returns></returns>
    public static IJsonSerializerProvider GetJsonSerializer()
    {
        return App.GetService<IJsonSerializerProvider>(App.RootServices);
    }

    /// <summary>
    /// 序列化对象
    /// </summary>
    /// <param name="value"></param>
    /// <param name="jsonSerializerOptions"></param>
    /// <returns></returns>
    public static string Serialize(object value, object jsonSerializerOptions = default)
    {
        return GetJsonSerializer().Serialize(value, jsonSerializerOptions);
    }

    /// <summary>
    /// 反序列化字符串
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="json"></param>
    /// <param name="jsonSerializerOptions"></param>
    /// <returns></returns>
    public static T Deserialize<T>(string json, object jsonSerializerOptions = default)
    {
        return GetJsonSerializer().Deserialize<T>(json, jsonSerializerOptions);
    }

    /// <summary>
    /// 获取 JSON 配置选项
    /// </summary>
    /// <typeparam name="TOptions"></typeparam>
    /// <returns></returns>
    public static TOptions GetSerializerOptions<TOptions>()
        where TOptions : class
    {
        return GetJsonSerializer().GetSerializerOptions() as TOptions;
    }

    /// <summary>
    /// 检查 JSON 字符串是否有效
    /// </summary>
    /// <param name="jsonString">JSON 字符串</param>
    /// <param name="standard">标准 JSON</param>
    /// <returns></returns>
    public static bool IsValid(string jsonString, bool standard = false)
    {
        if (string.IsNullOrWhiteSpace(jsonString)) return false;

        try
        {
            using var document = JsonDocument.Parse(jsonString);
            return !standard || document.RootElement.ValueKind == JsonValueKind.Object || document.RootElement.ValueKind == JsonValueKind.Array;
        }
        catch (JsonException)
        {
            return false;
        }
    }
}