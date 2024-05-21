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

using Furion.JsonSerialization;
using System.Text.Json.Serialization;

namespace System.Text.Json;

/// <summary>
/// System.Text.Json 拓展
/// </summary>
[SuppressSniffer]
public static class SystemTextJsonExtensions
{
    /// <summary>
    /// 添加 DateTime/DateTime?/DateTimeOffset/DateTimeOffset? 类型序列化处理
    /// </summary>
    /// <param name="converters"></param>
    /// <param name="outputFormat"></param>
    /// <param name="localized">自动转换 DateTimeOffset 为当地时间</param>
    /// <returns></returns>
    public static IList<JsonConverter> AddDateTimeTypeConverters(this IList<JsonConverter> converters, string outputFormat = "yyyy-MM-dd HH:mm:ss", bool localized = false)
    {
        converters.Add(new SystemTextJsonDateTimeJsonConverter(outputFormat));
        converters.Add(new SystemTextJsonNullableDateTimeJsonConverter(outputFormat));

        converters.Add(new SystemTextJsonDateTimeOffsetJsonConverter(outputFormat, localized));
        converters.Add(new SystemTextJsonNullableDateTimeOffsetJsonConverter(outputFormat, localized));

        return converters;
    }

    /// <summary>
    /// 添加 long/long? 类型序列化处理
    /// </summary>
    /// <param name="converters"></param>
    /// <param name="overMaxLengthOf17">是否超过最大长度 17 再处理</param>
    /// <remarks></remarks>
    public static IList<JsonConverter> AddLongTypeConverters(this IList<JsonConverter> converters, bool overMaxLengthOf17 = false)
    {
        converters.Add(new SystemTextJsonLongToStringJsonConverter(overMaxLengthOf17));
        converters.Add(new SystemTextJsonNullableLongToStringJsonConverter(overMaxLengthOf17));

        return converters;
    }

    /// <summary>
    /// 添加 Clay 类型序列化处理
    /// </summary>
    /// <remarks></remarks>
    public static IList<JsonConverter> AddClayConverters(this IList<JsonConverter> converters)
    {
        converters.Add(new SystemTextJsonClayJsonConverter());

        return converters;
    }

    /// <summary>
    /// 添加 DateOnly/DateOnly? 类型序列化处理
    /// </summary>
    /// <param name="converters"></param>
    /// <param name="outputFormat"></param>
    /// <returns></returns>
    public static IList<JsonConverter> AddDateOnlyConverters(this IList<JsonConverter> converters, string outputFormat = "yyyy-MM-dd")
    {
#if !NET5_0
        converters.Add(new SystemTextJsonDateOnlyJsonConverter(outputFormat));
        converters.Add(new SystemTextJsonNullableDateOnlyJsonConverter(outputFormat));
#endif
        return converters;
    }

    /// <summary>
    /// 添加 TimeOnly/TimeOnly? 类型序列化处理
    /// </summary>
    /// <param name="converters"></param>
    /// <param name="outputFormat"></param>
    /// <returns></returns>
    public static IList<JsonConverter> AddTimeOnlyConverters(this IList<JsonConverter> converters, string outputFormat = "HH:mm:ss")
    {
#if !NET5_0
        converters.Add(new SystemTextJsonTimeOnlyJsonConverter(outputFormat));
        converters.Add(new SystemTextJsonNullableTimeOnlyJsonConverter(outputFormat));
#endif
        return converters;
    }
}