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

using System.ComponentModel;
using System.Text.RegularExpressions;

namespace Furion.DataValidation;

/// <summary>
/// 验证类型
/// </summary>
[ValidationType]
public enum ValidationTypes
{
    /// <summary>
    /// 数值类型
    /// <para>
    /// 表达式：^\+?(:?(:?\d+\.\d+)?$|(:?\d+))?$|(-?\d+)(\.\d+)?$
    /// </para>
    /// </summary>
    [Description("数值类型"), ValidationItemMetadata(@"^\+?(:?(:?\d+\.\d+)?$|(:?\d+))?$|(-?\d+)(\.\d+)?$", "The value is not a numeric type.")]
    Numeric,

    /// <summary>
    /// 正数
    /// <para>
    /// 表达式：^(0\.0*[1-9]+[0-9]*$|[1-9]+[0-9]*\.[0-9]*[0-9]$|[1-9]+[0-9]*$)
    /// </para>
    /// </summary>
    [Description("正数"), ValidationItemMetadata(@"^(0\.0*[1-9]+[0-9]*$|[1-9]+[0-9]*\.[0-9]*[0-9]$|[1-9]+[0-9]*$)", "The value is not a positive number type.")]
    PositiveNumber,

    /// <summary>
    /// 负数
    /// <para>
    /// 表达式：^-(0\.0*[1-9]+[0-9]*$|[1-9]+[0-9]*\.[0-9]*[0-9]$|[1-9]+[0-9]*$)
    /// </para>
    /// </summary>
    [Description("负数"), ValidationItemMetadata(@"^-(0\.0*[1-9]+[0-9]*$|[1-9]+[0-9]*\.[0-9]*[0-9]$|[1-9]+[0-9]*$)", "The value is not a negative number type.")]
    NegativeNumber,

    /// <summary>
    /// 整数
    /// <para>
    /// 表达式：^-?[1-9]+[0-9]*$|^0$
    /// </para>
    /// </summary>
    [Description("整数"), ValidationItemMetadata(@"^-?[1-9]+[0-9]*$|^0$", "The value is not a integer type.")]
    Integer,

    /// <summary>
    /// 金钱类型
    /// <para>
    /// 表达式：^(([0-9]|([1-9][0-9]{0,9}))((\.[0-9]{1,2})?))$
    /// </para>
    /// </summary>
    [Description("金钱类型"), ValidationItemMetadata(@"^(([0-9]|([1-9][0-9]{0,9}))((\.[0-9]{1,2})?))$", "The value is not a money type.")]
    Money,

    /// <summary>
    /// 日期类型
    /// <para>
    /// 表达式：^(?:(?:1[6-9]|[2-9][0-9])[0-9]{2}([-/.]?)(?:(?:0?[1-9]|1[0-2])\1(?:0?[1-9]|1[0-9]|2[0-8])|(?:0?[13-9]|1[0-2])\1(?:29|30)|(?:0?[13578]|1[02])\1(?:31))|(?:(?:1[6-9]|[2-9][0-9])(?:0[48]|[2468][048]|[13579][26])|(?:16|[2468][048]|[3579][26])00)([-/.]?)0?2\2(?:29))(\s+([01][0-9]:|2[0-3]:)?[0-5][0-9]:[0-5][0-9])?$
    /// </para>
    /// </summary>
    [Description("日期类型"), ValidationItemMetadata(@"^(?:(?:1[6-9]|[2-9][0-9])[0-9]{2}([-/.]?)(?:(?:0?[1-9]|1[0-2])\1(?:0?[1-9]|1[0-9]|2[0-8])|(?:0?[13-9]|1[0-2])\1(?:29|30)|(?:0?[13578]|1[02])\1(?:31))|(?:(?:1[6-9]|[2-9][0-9])(?:0[48]|[2468][048]|[13579][26])|(?:16|[2468][048]|[3579][26])00)([-/.]?)0?2\2(?:29))(\s+([01][0-9]?:|2[0-3]:)?[0-5][0-9]:[0-5][0-9])?$", "The value is not a date type.")]
    Date,

    /// <summary>
    /// 时间类型
    /// <para>
    /// 表达式：^(\d{1,2})(:)?(\d{1,2})\2(\d{1,2})$
    /// </para>
    /// </summary>
    [Description("时间类型"), ValidationItemMetadata(@"^(\d{1,2})(:)?(\d{1,2})\2(\d{1,2})$", "The value is not a time type.")]
    Time,

    /// <summary>
    /// 身份证号码
    /// <para>
    /// 表达式：(^\d{15}$)|(^\d{18}$)|(^\d{17}(\d|X|x)$)
    /// </para>
    /// </summary>
    [Description("身份证号码"), ValidationItemMetadata(@"(^\d{15}$)|(^\d{18}$)|(^\d{17}(\d|X|x)$)", "The value is not a idcard type.")]
    IDCard,

    /// <summary>
    /// 邮政编码
    /// <para>
    /// 表达式：^[0-9]{6}$
    /// </para>
    /// </summary>
    [Description("邮政编码"), ValidationItemMetadata(@"^[0-9]{6}$", "The value is not a postcode type.")]
    PostCode,

    /// <summary>
    /// 手机号码
    /// <para>
    /// 表达式：^1[3456789][0-9]{9}$
    /// </para>
    /// </summary>
    [Description("手机号码"), ValidationItemMetadata(@"^1[3456789][0-9]{9}$", "The value is not a phone number type.")]
    PhoneNumber,

    /// <summary>
    /// 固话格式
    /// <para>
    /// 表达式：(^[0-9]{3,4}\-[0-9]{3,8}$)|(^[0-9]{3,8}$)|(^\([0-9]{3,4}\)[0-9]{3,8}$)|(^0{0,1}13[0-9]{9}$)
    /// </para>
    /// </summary>
    [Description("固话格式"), ValidationItemMetadata(@"(^[0-9]{3,4}\-[0-9]{3,8}$)|(^[0-9]{3,8}$)|(^\([0-9]{3,4}\)[0-9]{3,8}$)|(^0{0,1}13[0-9]{9}$)", "The value is not a telephone type.")]
    Telephone,

    /// <summary>
    /// 手机或固话类型
    /// <para>
    /// 表达式：(^1[3456789][0-9]{9}$)|((^[0-9]{3,4}\-[0-9]{3,8}$)|(^[0-9]{3,8}$)|(^\([0-9]{3,4}\)[0-9]{3,8}$)|(^0{0,1}13[0-9]{9}$))
    /// </para>
    /// </summary>
    [Description("手机或固话类型"), ValidationItemMetadata(@"(^1[3456789][0-9]{9}$)|((^[0-9]{3,4}\-[0-9]{3,8}$)|(^[0-9]{3,8}$)|(^\([0-9]{3,4}\)[0-9]{3,8}$)|(^0{0,1}13[0-9]{9}$))", "The value is not a phone number or telephone type.", RegexOptions.IgnoreCase)]
    PhoneOrTelNumber,

    /// <summary>
    /// 邮件类型
    /// <para>
    /// 表达式：^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$
    /// </para>
    /// </summary>
    [Description("邮件类型"), ValidationItemMetadata(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", "The value is not a email address type.")]
    EmailAddress,

    /// <summary>
    /// 网址类型
    /// <para>
    /// 表达式：^(((ht|f)tps?):\/\/)?([^!@#$%^与*?.\s-]([^!@#$%^与*?.\s]{0,63}[^!@#$%^与*?.\s])?\.)+[a-z]{2,6}\/?
    /// </para>
    /// </summary>
    [Description("网址类型"), ValidationItemMetadata(@"^(((ht|f)tps?):\/\/)?([^!@#$%^&*?.\s-]([^!@#$%^&*?.\s]{0,63}[^!@#$%^&*?.\s])?\.)+[a-z]{2,6}\/?", "The value is not a url address type")]
    Url,

    /// <summary>
    /// 颜色类型
    /// <para>
    /// 表达式：(^#([0-9a-f]{6}|[0-9a-f]{3})$)|(^rgb\(([0-9]|[0-9][0-9]|25[0-5]|2[0-4][0-9]|[0-1][0-9][0-9])\,([0-9]|[0-9][0-9]|25[0-5]|2[0-4][0-9]|[0-1][0-9][0-9])\,([0-9]|[0-9][0-9]|25[0-5]|2[0-4][0-9]|[0-1][0-9][0-9])\)$)|(^rgba\(([0-9]|[0-9][0-9]|25[0-5]|2[0-4][0-9]|[0-1][0-9][0-9])\,([0-9]|[0-9][0-9]|25[0-5]|2[0-4][0-9]|[0-1][0-9][0-9])\,([0-9]|[0-9][0-9]|25[0-5]|2[0-4][0-9]|[0-1][0-9][0-9])\,(1|1.0|0.[0-9])\)$)
    /// </para>
    /// </summary>
    [Description("颜色类型"), ValidationItemMetadata(@"(^#([0-9a-f]{6}|[0-9a-f]{3})$)|(^rgb\(([0-9]|[0-9][0-9]|25[0-5]|2[0-4][0-9]|[0-1][0-9][0-9])\,([0-9]|[0-9][0-9]|25[0-5]|2[0-4][0-9]|[0-1][0-9][0-9])\,([0-9]|[0-9][0-9]|25[0-5]|2[0-4][0-9]|[0-1][0-9][0-9])\)$)|(^rgba\(([0-9]|[0-9][0-9]|25[0-5]|2[0-4][0-9]|[0-1][0-9][0-9])\,([0-9]|[0-9][0-9]|25[0-5]|2[0-4][0-9]|[0-1][0-9][0-9])\,([0-9]|[0-9][0-9]|25[0-5]|2[0-4][0-9]|[0-1][0-9][0-9])\,(1|1.0|0.[0-9])\)$)", "The value is not a color type.", RegexOptions.IgnoreCase)]
    Color,

    /// <summary>
    /// 中文
    /// <para>
    /// 表达式：^[\u4e00-\u9fa5]+$
    /// </para>
    /// </summary>
    [Description("中文"), ValidationItemMetadata(@"^[\u4e00-\u9fa5]+$", "The value is not a chinese type.")]
    Chinese,

    /// <summary>
    /// IPv4 类型
    /// <para>
    /// 表达式：^((25[0-5]|2[0-4][0-9]|1[0-9]{2}|[0-9]{1,2})\.){3}(25[0-5]|2[0-4][0-9]|1[0-9]{2}|[0-9]{1,2})$
    /// </para>
    /// </summary>
    [Description("IPv4 类型"), ValidationItemMetadata(@"^((25[0-5]|2[0-4][0-9]|1[0-9]{2}|[0-9]{1,2})\.){3}(25[0-5]|2[0-4][0-9]|1[0-9]{2}|[0-9]{1,2})$", "The value is not a IPv4 type.")]
    IPv4,

    /// <summary>
    /// IPv6 类型
    /// <para>
    /// 表达式：/^\s*((([0-9A-Fa-f]{1,4}:){7}([0-9A-Fa-f]{1,4}|:))|(([0-9A-Fa-f]{1,4}:){6}(:[0-9A-Fa-f]{1,4}|((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3})|:))|(([0-9A-Fa-f]{1,4}:){5}(((:[0-9A-Fa-f]{1,4}){1,2})|:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3})|:))|(([0-9A-Fa-f]{1,4}:){4}(((:[0-9A-Fa-f]{1,4}){1,3})|((:[0-9A-Fa-f]{1,4})?:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3}))|:))|(([0-9A-Fa-f]{1,4}:){3}(((:[0-9A-Fa-f]{1,4}){1,4})|((:[0-9A-Fa-f]{1,4}){0,2}:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3}))|:))|(([0-9A-Fa-f]{1,4}:){2}(((:[0-9A-Fa-f]{1,4}){1,5})|((:[0-9A-Fa-f]{1,4}){0,3}:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3}))|:))|(([0-9A-Fa-f]{1,4}:){1}(((:[0-9A-Fa-f]{1,4}){1,6})|((:[0-9A-Fa-f]{1,4}){0,4}:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3}))|:))|(:(((:[0-9A-Fa-f]{1,4}){1,7})|((:[0-9A-Fa-f]{1,4}){0,5}:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3}))|:)))(%.+)?\s*$
    /// </para>
    /// </summary>
    [Description("IPv6 类型"), ValidationItemMetadata(@"/^\s*((([0-9A-Fa-f]{1,4}:){7}([0-9A-Fa-f]{1,4}|:))|(([0-9A-Fa-f]{1,4}:){6}(:[0-9A-Fa-f]{1,4}|((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3})|:))|(([0-9A-Fa-f]{1,4}:){5}(((:[0-9A-Fa-f]{1,4}){1,2})|:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3})|:))|(([0-9A-Fa-f]{1,4}:){4}(((:[0-9A-Fa-f]{1,4}){1,3})|((:[0-9A-Fa-f]{1,4})?:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3}))|:))|(([0-9A-Fa-f]{1,4}:){3}(((:[0-9A-Fa-f]{1,4}){1,4})|((:[0-9A-Fa-f]{1,4}){0,2}:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3}))|:))|(([0-9A-Fa-f]{1,4}:){2}(((:[0-9A-Fa-f]{1,4}){1,5})|((:[0-9A-Fa-f]{1,4}){0,3}:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3}))|:))|(([0-9A-Fa-f]{1,4}:){1}(((:[0-9A-Fa-f]{1,4}){1,6})|((:[0-9A-Fa-f]{1,4}){0,4}:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3}))|:))|(:(((:[0-9A-Fa-f]{1,4}){1,7})|((:[0-9A-Fa-f]{1,4}){0,5}:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3}))|:)))(%.+)?\s*$", "The value is not a IPv6 type.")]
    IPv6,

    /// <summary>
    /// 年龄
    /// <para>
    /// 表达式：^[1-99]?\d*$
    /// </para>
    /// </summary>
    [Description("年龄"), ValidationItemMetadata(@"^[1-99]?\d*$", "The value is not a age type.")]
    Age,

    /// <summary>
    /// 中文名
    /// <para>
    /// 表达式：^[\u0391-\uFFE5]{2,15}$
    /// </para>
    /// </summary>
    [Description("中文名"), ValidationItemMetadata(@"^[\u0391-\uFFE5]{2,15}$", "The value is not a chinese name type.")]
    ChineseName,

    /// <summary>
    /// 英文名
    /// <para>
    /// 表达式：^[A-Za-z]{1,161}$
    /// </para>
    /// </summary>
    [Description("英文名"), ValidationItemMetadata(@"^[A-Za-z]{1,161}$", "The value is not a english name type.")]
    EnglishName,

    /// <summary>
    /// 纯大写
    /// <para>
    /// 表达式：^[A-Z]+$
    /// </para>
    /// </summary>
    [Description("纯大写"), ValidationItemMetadata(@"^[A-Z]+$", "The value is not a capital type.")]
    Capital,

    /// <summary>
    /// 纯小写
    /// <para>
    /// 表达式：^[a-z]+$
    /// </para>
    /// </summary>
    [Description("纯小写"), ValidationItemMetadata(@"^[a-z]+$", "The value is not a lowercase type.")]
    Lowercase,

    /// <summary>
    /// ASCII 编码
    /// <para>
    /// 表达式：^[\x00-\xFF]+$
    /// </para>
    /// </summary>
    [Description("ASCII 编码"), ValidationItemMetadata(@"^[\x00-\xFF]+$", "The value is not a ascii type.")]
    Ascii,

    /// <summary>
    /// MD5 加密字符串
    /// <para>
    /// 表达式：^([a-fA-F0-9]{32})$
    /// </para>
    /// </summary>
    [Description("MD5 加密字符串"), ValidationItemMetadata(@"^([a-fA-F0-9]{32})$", "The value is not a md5 type.")]
    Md5,

    /// <summary>
    /// 压缩文件格式
    /// <para>
    /// 表达式：(.*)\.(rar|zip|7zip|tgz)$
    /// </para>
    /// </summary>
    [Description("压缩文件格式"), ValidationItemMetadata(@"(.*)\.(rar|zip|7zip|tgz)$", "The value is not a zip type.")]
    Zip,

    /// <summary>
    /// 图片格式
    /// <para>
    /// 表达式：(.*)\.(jpg|gif|ico|jpeg|png)$
    /// </para>
    /// </summary>
    [Description("图片格式"), ValidationItemMetadata(@"(.*)\.(jpg|gif|ico|jpeg|png)$", "The value is not a image type.")]
    Image,

    /// <summary>
    /// 文档格式
    /// <para>
    /// 表达式：(.*)\.(doc|xls|docx|xlsx|pdf|md)$
    /// </para>
    /// </summary>
    [Description("文档格式"), ValidationItemMetadata(@"(.*)\.(doc|xls|docx|xlsx|pdf|md)$", "The value is not a document type.")]
    Document,

    /// <summary>
    /// MP3 格式
    /// <para>
    /// 表达式：(.*)\.(mp3)$
    /// </para>
    /// </summary>
    [Description("MP3 格式"), ValidationItemMetadata(@"(.*)\.(mp3)$", "The value is not a mp3 type.")]
    Mp3,

    /// <summary>
    /// Flash 格式
    /// <para>
    /// 表达式：(.*)\.(swf|fla|flv)$
    /// </para>
    /// </summary>
    [Description("Flash 格式"), ValidationItemMetadata(@"(.*)\.(swf|fla|flv)$", "The value is not a flash type.")]
    Flash,

    /// <summary>
    /// 视频文件格式
    /// <para>
    /// 表达式：(.*)\.(rm|rmvb|wmv|avi|mp4|3gp|mkv)$
    /// </para>
    /// </summary>
    [Description("视频文件格式"), ValidationItemMetadata(@"(.*)\.(rm|rmvb|wmv|avi|mp4|3gp|mkv)$", "The value is not a video type.")]
    Video,

    /// <summary>
    /// 字母加数字组合
    /// <para>
    /// 表达式：^(?![0-9]+$)(?![a-zA-Z]+$)[0-9A-Za-z]*$
    /// </para>
    /// </summary>
    [Description("字母和数字组合"), ValidationItemMetadata(@"^(?![0-9]+$)(?![a-zA-Z]+$)[0-9A-Za-z]*$", "The value is not a combination of letters and numbers.")]
    WordWithNumber,

    /// <summary>
    /// Html 标签格式
    /// <para>
    /// 表达式：lt(\w+)[^gt]*>(.*?lt\/\1gt)?
    /// </para>
    /// </summary>
    [Description("Html 标签格式"), ValidationItemMetadata(@"<(\w+)[^>]*>(.*?<\/\1>)?", "The value is not a html tag.")]
    Html,

    /// <summary>
    /// 手机机身码
    /// </summary>
    [Description("手机机身码"), ValidationItemMetadata(@"^\d{15,17}$", "The value is not a IMEI type.")]
    IMEI,

    /// <summary>
    /// 统一社会信用代码
    /// </summary>
    [Description("统一社会信用代码"), ValidationItemMetadata(@"^[0-9A-HJ-NPQRTUWXY]{2}\d{6}[0-9A-HJ-NPQRTUWXY]{10}$", "The value is not a social credit code type.")]
    SocialCreditCode,

    /// <summary>
    /// GUID 或者 UUID
    /// </summary>
    [Description("GUID 或者 UUID"), ValidationItemMetadata(@"^[a-fA-F\d]{4}(?:[a-fA-F\d]{4}-){4}[a-fA-F\d]{12}$", "The value is not a GUID or UUID type.")]
    GUID_OR_UUID,

    /// <summary>
    /// base64 格式
    /// </summary>
    [Description("base64 格式"), ValidationItemMetadata(@"^\s*data:(?:[a-z]+\/[a-z0-9-+.]+(?:;[a-z-]+=[a-z0-9-]+)?)?(?:;base64)?,([a-z0-9!$&',()*+;=\-._~:@/?%\s]*?)\s*$", "The value is not a base64 type.")]
    Base64
}