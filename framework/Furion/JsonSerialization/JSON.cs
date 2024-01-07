// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

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
    /// <param name="jsonString"></param>
    /// <returns></returns>
    public static bool IsValid(string jsonString)
    {
        if (string.IsNullOrWhiteSpace(jsonString)) return false;

        try
        {
            using var document = JsonDocument.Parse(jsonString);
            return true;
        }
        catch (JsonException)
        {
            return false;
        }
    }
}