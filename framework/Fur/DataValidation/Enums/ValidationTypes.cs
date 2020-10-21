// -----------------------------------------------------------------------------
// Fur 是 .NET 5 平台下企业应用开发最佳实践框架。
// Copyright © 2020 Fur, Baiqian Co.,Ltd.
//
// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0-rc.final.20
// 官方网站：https://chinadot.net
// 源码地址：Gitee：https://gitee.com/monksoul/Fur
// 				    Github：https://github.com/monksoul/Fur
// 开源协议：Apache-2.0（http://www.apache.org/licenses/LICENSE-2.0）
// -----------------------------------------------------------------------------

using System.Text.RegularExpressions;

namespace Fur.DataValidation
{
    /// <summary>
    /// 验证类型
    /// </summary>
    [ValidationType]
    public enum ValidationTypes
    {
        /// <summary>
        /// 非空非Null
        /// </summary>
        [ValidationItemMetadata(@"^[\w\W]+$", "The Value is required")]
        Required,

        /// <summary>
        /// 数值类型
        /// </summary>
        [ValidationItemMetadata(@"^(\d+[\s,]*)+\.?\d*$", "The Value is not a numeric type")]
        Numeric,

        /// <summary>
        /// 正数
        /// </summary>
        [ValidationItemMetadata(@"^[+]?\d+(\.\d+)?$", "The Value is not a positive number type")]
        PositiveNumber,

        /// <summary>
        /// 负数
        /// </summary>
        [ValidationItemMetadata(@"^-[1-9]\d*\.\d*|-0\.\d*[1-9]\d*$", "The Value is not a negative number type")]
        NegativeNumber,

        /// <summary>
        /// 整数
        /// </summary>
        [ValidationItemMetadata(@"^-?\d+$", "The Value is not a integer type")]
        Integer,

        /// <summary>
        /// 金钱类型
        /// </summary>
        [ValidationItemMetadata(@"^(([0-9]|([1-9][0-9]{0,9}))((\.[0-9]{1,2})?))$", "The Value is not a money type")]
        Money,

        /// <summary>
        /// 日期类型
        /// </summary>
        [ValidationItemMetadata(@"^(?:(?:1[6-9]|[2-9][0-9])[0-9]{2}([-/.]?)(?:(?:0?[1-9]|1[0-2])\1(?:0?[1-9]|1[0-9]|2[0-8])|(?:0?[13-9]|1[0-2])\1(?:29|30)|(?:0?[13578]|1[02])\1(?:31))|(?:(?:1[6-9]|[2-9][0-9])(?:0[48]|[2468][048]|[13579][26])|(?:16|[2468][048]|[3579][26])00)([-/.]?)0?2\2(?:29))(\s+([01][0-9]:|2[0-3]:)?[0-5][0-9]:[0-5][0-9])?$", "The Value is not a date type")]
        Date,

        /// <summary>
        /// 时间类型
        /// </summary>
        [ValidationItemMetadata(@"^(\d{1,2})(:)?(\d{1,2})\2(\d{1,2})$", "The Value is not a time type")]
        Time,

        /// <summary>
        /// 身份证号码
        /// </summary>
        [ValidationItemMetadata(@"(^\d{15}$)|(^\d{18}$)|(^\d{17}(\d|X|x)$)", "The Value is not a idcard type")]
        IDCard,

        /// <summary>
        /// 邮政编码
        /// </summary>
        [ValidationItemMetadata(@"^[0-9]{6}$", "The Value is not a postcode type")]
        PostCode,

        /// <summary>
        /// 手机号码
        /// </summary>
        [ValidationItemMetadata(@"^13[0-9]{9}$|14[0-9]{9}|15[0-9]{9}$|16[0-9]{9}$|17[0-9]{9}$|18[0-9]{9}$|19[0-9]{9}$", "The Value is not a phone number type")]
        PhoneNumber,

        /// <summary>
        /// 固话格式
        /// </summary>
        [ValidationItemMetadata(@"(^[0-9]{3,4}\-[0-9]{3,8}$)|(^[0-9]{3,8}$)|(^\([0-9]{3,4}\)[0-9]{3,8}$)|(^0{0,1}13[0-9]{9}$)", "The Value is not a telephone type")]
        Telephone,

        /// <summary>
        /// 手机或固话类型
        /// </summary>
        [ValidationItemMetadata(@"((13)\d{9})|((15)\d{9})|(16)\d{9})|(17)\d{9})|((18)\d{9})|(19)\d{9})|(0[1-9]{2,3}\-?[1-9]{6,7})", "The Value is not a phone number or telephone type", RegexOptions.IgnoreCase)]
        PhoneOrTelNumber,

        /// <summary>
        /// 邮件类型
        /// </summary>
        [ValidationItemMetadata(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", "The Value is not a email address type")]
        EmailAddress,

        /// <summary>
        /// 网址类型
        /// </summary>
        [ValidationItemMetadata(@"^(\w+:\/\/)?\w+(\.\w+)+.*$", "The Value is not a url address type")]
        Url,

        /// <summary>
        /// 颜色类型
        /// </summary>
        [ValidationItemMetadata(@"(^#([0-9a-f]{6}|[0-9a-f]{3})$)|(^rgb\(([0-9]|[0-9][0-9]|25[0-5]|2[0-4][0-9]|[0-1][0-9][0-9])\,([0-9]|[0-9][0-9]|25[0-5]|2[0-4][0-9]|[0-1][0-9][0-9])\,([0-9]|[0-9][0-9]|25[0-5]|2[0-4][0-9]|[0-1][0-9][0-9])\)$)|(^rgba\(([0-9]|[0-9][0-9]|25[0-5]|2[0-4][0-9]|[0-1][0-9][0-9])\,([0-9]|[0-9][0-9]|25[0-5]|2[0-4][0-9]|[0-1][0-9][0-9])\,([0-9]|[0-9][0-9]|25[0-5]|2[0-4][0-9]|[0-1][0-9][0-9])\,(1|1.0|0.[0-9])\)$)", "The Value is not a color type", RegexOptions.IgnoreCase)]
        Color,

        /// <summary>
        /// 中文
        /// </summary>
        [ValidationItemMetadata(@"^[\u4e00-\u9fa5]+$", "The Value is not a chinese type")]
        Chinese,

        /// <summary>
        /// IPv4 类型
        /// </summary>
        [ValidationItemMetadata(@"^((25[0-5]|2[0-4][0-9]|1[0-9]{2}|[0-9]{1,2})\.){3}(25[0-5]|2[0-4][0-9]|1[0-9]{2}|[0-9]{1,2})$", "The Value is not a IPv4 type")]
        IPv4,

        /// <summary>
        /// IPv6类型
        /// </summary>
        [ValidationItemMetadata(@"/^\s*((([0-9A-Fa-f]{1,4}:){7}([0-9A-Fa-f]{1,4}|:))|(([0-9A-Fa-f]{1,4}:){6}(:[0-9A-Fa-f]{1,4}|((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3})|:))|(([0-9A-Fa-f]{1,4}:){5}(((:[0-9A-Fa-f]{1,4}){1,2})|:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3})|:))|(([0-9A-Fa-f]{1,4}:){4}(((:[0-9A-Fa-f]{1,4}){1,3})|((:[0-9A-Fa-f]{1,4})?:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3}))|:))|(([0-9A-Fa-f]{1,4}:){3}(((:[0-9A-Fa-f]{1,4}){1,4})|((:[0-9A-Fa-f]{1,4}){0,2}:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3}))|:))|(([0-9A-Fa-f]{1,4}:){2}(((:[0-9A-Fa-f]{1,4}){1,5})|((:[0-9A-Fa-f]{1,4}){0,3}:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3}))|:))|(([0-9A-Fa-f]{1,4}:){1}(((:[0-9A-Fa-f]{1,4}){1,6})|((:[0-9A-Fa-f]{1,4}){0,4}:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3}))|:))|(:(((:[0-9A-Fa-f]{1,4}){1,7})|((:[0-9A-Fa-f]{1,4}){0,5}:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3}))|:)))(%.+)?\s*$", "The Value is not a IPv6 type")]
        IPv6,

        /// <summary>
        /// 年龄
        /// </summary>
        [ValidationItemMetadata(@"^[1-99]?\d*$", "The Value is not a age type")]
        Age,

        /// <summary>
        /// 中文名
        /// </summary>
        [ValidationItemMetadata(@"^[\u0391-\uFFE5]{2,15}$", "The Value is not a chinese name type")]
        ChineseName,

        /// <summary>
        /// 英文名
        /// </summary>
        [ValidationItemMetadata(@"^[A-Za-z]{1,161}$", "The Value is not a english name type")]
        EnglishName,

        /// <summary>
        /// 纯大写
        /// </summary>
        [ValidationItemMetadata(@"^[A-Z]+$", "The Value is not a capital type")]
        Capital,

        /// <summary>
        /// 纯小写
        /// </summary>
        [ValidationItemMetadata(@"^[a-z]+$", "The Value is not a lowercase type")]
        Lowercase,

        /// <summary>
        /// Ascii 编码
        /// </summary>
        [ValidationItemMetadata(@"^[\x00-\xFF]+$", "The Value is not a ascii type")]
        Ascii,

        /// <summary>
        /// 是否是Md5加密字符串
        /// </summary>
        [ValidationItemMetadata(@"^([a-fA-F0-9]{32})$", "The Value is not a md5 type")]
        Md5,

        /// <summary>
        /// 是否是压缩文件
        /// </summary>
        [ValidationItemMetadata(@"(.*)\.(rar|zip|7zip|tgz)$", "The Value is not a zip type")]
        Zip,

        /// <summary>
        /// 图片格式
        /// </summary>
        [ValidationItemMetadata(@"(.*)\.(jpg|gif|ico|jpeg|png)$", "The Value is not a image type")]
        Image,

        /// <summary>
        /// 文档格式
        /// </summary>
        [ValidationItemMetadata(@"(.*)\.(doc|xls|docx|xlsx|pdf|md)$", "The Value is not a document type")]
        Document,

        /// <summary>
        /// MP3 格式
        /// </summary>
        [ValidationItemMetadata(@"(.*)\.(mp3)$", "The Value is not a mp3 type")]
        Mp3,

        /// <summary>
        /// Flash 格式
        /// </summary>
        [ValidationItemMetadata(@"(.*)\.(swf|fla|flv)$", "The Value is not a flash type")]
        Flash,

        /// <summary>
        /// 视频文件格式
        /// </summary>
        [ValidationItemMetadata(@"(.*)\.(rm|rmvb|wmv|avi|mp4|3gp|mkv)$", "The Value is not a video type")]
        Video,
    }
}