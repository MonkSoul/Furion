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
        [ValidationRegularExpressionAttribute(@"^[\w\W]+$", "The Value is required.")]
        Required,

        /// <summary>
        /// 数值类型
        /// </summary>
        [ValidationRegularExpression(@"^(\d+[\s,]*)+\.?\d*$", "The Value is not a numeric type.")]
        Numeric,

        /// <summary>
        /// 正数
        /// </summary>
        [ValidationRegularExpression(@"^[+]?\d+(\.\d+)?$", "The Value is not a positive number type.")]
        PositiveNumber,

        /// <summary>
        /// 负数
        /// </summary>
        [ValidationRegularExpression(@"^-[1-9]\d*\.\d*|-0\.\d*[1-9]\d*$", "The Value is not a negative number type.")]
        NegativeNumber,

        /// <summary>
        /// 整数
        /// </summary>
        [ValidationRegularExpression(@"^-?\d+$", "The Value is not a integer type.")]
        Integer,

        /// <summary>
        /// 金钱类型
        /// </summary>
        [ValidationRegularExpression(@"^(([0-9]|([1-9][0-9]{0,9}))((\.[0-9]{1,2})?))$", "The Value is not a money type.")]
        Money,

        /// <summary>
        /// 日期类型
        /// </summary>
        [ValidationRegularExpression(@"^(?:(?:1[6-9]|[2-9][0-9])[0-9]{2}([-/.]?)(?:(?:0?[1-9]|1[0-2])\1(?:0?[1-9]|1[0-9]|2[0-8])|(?:0?[13-9]|1[0-2])\1(?:29|30)|(?:0?[13578]|1[02])\1(?:31))|(?:(?:1[6-9]|[2-9][0-9])(?:0[48]|[2468][048]|[13579][26])|(?:16|[2468][048]|[3579][26])00)([-/.]?)0?2\2(?:29))(\s+([01][0-9]:|2[0-3]:)?[0-5][0-9]:[0-5][0-9])?$", "The Value is not a date type.")]
        Date,

        /// <summary>
        /// 时间类型
        /// </summary>
        [ValidationRegularExpression(@"^(\d{1,2})(:)?(\d{1,2})\2(\d{1,2})$", "The Value is not a time type.")]
        Time,

        /// <summary>
        /// 身份证号码
        /// </summary>
        [ValidationRegularExpression(@"(^\d{15}$)|(^\d{18}$)|(^\d{17}(\d|X|x)$)", "The Value is not a idcard type.")]
        IdCard,

        /// <summary>
        /// 邮政编码
        /// </summary>
        [ValidationRegularExpression(@"^[0-9]{6}$", "The Value is not a postcode type.")]
        PostCode,

        /// <summary>
        /// 电话类型
        /// </summary>
        [ValidationRegularExpression(@"^13[0-9]{9}$|14[0-9]{9}|15[0-9]{9}$|16[0-9]{9}$|17[0-9]{9}$|18[0-9]{9}$|19[0-9]{9}$", "The Value is not a phone number type.")]
        PhoneNumaber,

        /// <summary>
        /// 固话格式
        /// </summary>
        [ValidationRegularExpression(@"(^[0-9]{3,4}\-[0-9]{3,8}$)|(^[0-9]{3,8}$)|(^\([0-9]{3,4}\)[0-9]{3,8}$)|(^0{0,1}13[0-9]{9}$)", "The Value is not a telephone type.")]
        Telephone,

        /// <summary>
        /// 手机或固话类型
        /// </summary>
        [ValidationRegularExpression(@"((13)\d{9})|((15)\d{9})|(16)\d{9})|(17)\d{9})|((18)\d{9})|(19)\d{9})|(0[1-9]{2,3}\-?[1-9]{6,7})", "The Value is not a phone number or telephone type.", RegexOptions.IgnoreCase)]
        PhoneOrTelNumber,

        /// <summary>
        /// 邮件类型
        /// </summary>
        [ValidationRegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", "The Value is not a email address type.")]
        EmailAddress,

        /// <summary>
        /// 网址类型
        /// </summary>
        [ValidationRegularExpression(@"^(\w+:\/\/)?\w+(\.\w+)+.*$", "The Value is not a url address type.")]
        Url,

        /// <summary>
        /// 颜色类型
        /// </summary>
        [ValidationRegularExpression(@"(^#([0-9a-f]{6}|[0-9a-f]{3})$)|(^rgb\(([0-9]|[0-9][0-9]|25[0-5]|2[0-4][0-9]|[0-1][0-9][0-9])\,([0-9]|[0-9][0-9]|25[0-5]|2[0-4][0-9]|[0-1][0-9][0-9])\,([0-9]|[0-9][0-9]|25[0-5]|2[0-4][0-9]|[0-1][0-9][0-9])\)$)|(^rgba\(([0-9]|[0-9][0-9]|25[0-5]|2[0-4][0-9]|[0-1][0-9][0-9])\,([0-9]|[0-9][0-9]|25[0-5]|2[0-4][0-9]|[0-1][0-9][0-9])\,([0-9]|[0-9][0-9]|25[0-5]|2[0-4][0-9]|[0-1][0-9][0-9])\,(1|1.0|0.[0-9])\)$)", "The Value is not a color type.", RegexOptions.IgnoreCase)]
        Color,

        /// <summary>
        /// 中文
        /// </summary>
        [ValidationRegularExpression(@"^[\u4e00-\u9fa5]+$", "The Value is not a chinese type.")]
        Chinese,

        /// <summary>
        /// IPv4 类型
        /// </summary>
        [ValidationRegularExpression(@"^((25[0-5]|2[0-4][0-9]|1[0-9]{2}|[0-9]{1,2})\.){3}(25[0-5]|2[0-4][0-9]|1[0-9]{2}|[0-9]{1,2})$", "The Value is not a IPv4 type.")]
        IPv4,

        /// <summary>
        /// IPv6类型
        /// </summary>
        [ValidationRegularExpression(@"/^\s*((([0-9A-Fa-f]{1,4}:){7}([0-9A-Fa-f]{1,4}|:))|(([0-9A-Fa-f]{1,4}:){6}(:[0-9A-Fa-f]{1,4}|((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3})|:))|(([0-9A-Fa-f]{1,4}:){5}(((:[0-9A-Fa-f]{1,4}){1,2})|:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3})|:))|(([0-9A-Fa-f]{1,4}:){4}(((:[0-9A-Fa-f]{1,4}){1,3})|((:[0-9A-Fa-f]{1,4})?:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3}))|:))|(([0-9A-Fa-f]{1,4}:){3}(((:[0-9A-Fa-f]{1,4}){1,4})|((:[0-9A-Fa-f]{1,4}){0,2}:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3}))|:))|(([0-9A-Fa-f]{1,4}:){2}(((:[0-9A-Fa-f]{1,4}){1,5})|((:[0-9A-Fa-f]{1,4}){0,3}:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3}))|:))|(([0-9A-Fa-f]{1,4}:){1}(((:[0-9A-Fa-f]{1,4}){1,6})|((:[0-9A-Fa-f]{1,4}){0,4}:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3}))|:))|(:(((:[0-9A-Fa-f]{1,4}){1,7})|((:[0-9A-Fa-f]{1,4}){0,5}:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3}))|:)))(%.+)?\s*$", "The Value is not a IPv6 type.")]
        IPv6,

        /// <summary>
        /// 年龄
        /// </summary>
        [ValidationRegularExpression(@"^[1-99]?\d*$", "The Value is not a age type.")]
        Age,

        /// <summary>
        /// 中文名
        /// </summary>
        [ValidationRegularExpression(@"^[\u0391-\uFFE5]{2,15}$", "The Value is not a chinese name type.")]
        ChineseName,

        /// <summary>
        /// 英文名
        /// </summary>
        [ValidationRegularExpression(@"^[A-Za-z]{1,161}$", "The Value is not a english name type.")]
        EnglishName,

        /// <summary>
        /// 纯大写
        /// </summary>
        [ValidationRegularExpression(@"^[A-Z]+$", "The Value is not a capital type.")]
        Capital,

        /// <summary>
        /// 纯小写
        /// </summary>
        [ValidationRegularExpression(@"^[a-z]+$", "The Value is not a lowercase type.")]
        Lowercase,

        /// <summary>
        /// Ascii 编码
        /// </summary>
        [ValidationRegularExpression(@"^[\x00-\xFF]+$", "The Value is not a ascii type.")]
        Ascii,

        /// <summary>
        /// 是否是Md5加密字符串
        /// </summary>
        [ValidationRegularExpression(@"^([a-fA-F0-9]{32})$", "The Value is not a md5 type.")]
        Md5,

        /// <summary>
        /// 是否是压缩文件
        /// </summary>
        [ValidationRegularExpression(@"(.*)\.(rar|zip|7zip|tgz)$", "The Value is not a zip type.")]
        Zip,

        /// <summary>
        /// 图片格式
        /// </summary>
        [ValidationRegularExpression(@"(.*)\.(jpg|gif|ico|jpeg|png)$", "The Value is not a image type.")]
        Image,

        /// <summary>
        /// 文档格式
        /// </summary>
        [ValidationRegularExpression(@"(.*)\.(doc|xls|docx|xlsx|pdf|md)$", "The Value is not a document type.")]
        Document,

        /// <summary>
        /// MP3 格式
        /// </summary>
        [ValidationRegularExpression(@"(.*)\.(mp3)$", "The Value is not a mp3 type.")]
        MP3,

        /// <summary>
        /// Flash 格式
        /// </summary>
        [ValidationRegularExpression(@"(.*)\.(swf|fla|flv)$", "The Value is not a flash type.")]
        Flash,

        /// <summary>
        /// 视频文件
        /// </summary>
        [ValidationRegularExpression(@"(.*)\.(rm|rmvb|wmv|avi|mp4|3gp|mkv)$", "The Value is not a video type.")]
        Video,
    }
}