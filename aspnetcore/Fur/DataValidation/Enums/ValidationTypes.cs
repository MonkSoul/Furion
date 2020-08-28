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
        Number,

        /// <summary>
        /// 正数
        /// </summary>
        PositiveNumber,

        /// <summary>
        /// 负数
        /// </summary>
        NegativeNumber,

        /// <summary>
        /// 整数
        /// </summary>
        Integer,

        /// <summary>
        /// 金钱类型
        /// </summary>
        Money,

        /// <summary>
        /// 日期类型
        /// </summary>
        Date,

        /// <summary>
        /// 时间类型
        /// </summary>
        Time,

        /// <summary>
        /// 身份证号码
        /// </summary>
        IdCard,

        /// <summary>
        /// 邮政编码
        /// </summary>
        PostCode,

        /// <summary>
        /// 电话类型
        /// </summary>
        PhoneNumaber,

        /// <summary>
        /// 固话格式
        /// </summary>
        Telephone,

        /// <summary>
        /// 手机或固话类型
        /// </summary>
        PhoneTelNumber,

        /// <summary>
        /// 邮件类型
        /// </summary>
        EmailAddress,

        /// <summary>
        /// 网址类型
        /// </summary>
        Url,

        /// <summary>
        /// 颜色类型
        /// </summary>
        Color,

        /// <summary>
        /// 中文
        /// </summary>
        Chinese,

        /// <summary>
        /// IPv4 类型
        /// </summary>
        IPv4,

        /// <summary>
        /// IPv6类型
        /// </summary>
        IPv6,

        /// <summary>
        /// 年龄
        /// </summary>
        Age,

        /// <summary>
        /// 中文名
        /// </summary>
        ChineseName,

        /// <summary>
        /// 英文名
        /// </summary>
        EnglishName,

        /// <summary>
        /// 纯大写
        /// </summary>
        Capital,

        /// <summary>
        /// 纯小写
        /// </summary>
        Lowercase,

        /// <summary>
        /// Ascii 编码
        /// </summary>
        Ascii,

        /// <summary>
        /// 是否是Md5加密字符串
        /// </summary>
        Md5,

        /// <summary>
        /// 是否是压缩文件
        /// </summary>
        Zip,

        /// <summary>
        /// 图片格式
        /// </summary>
        Image,

        /// <summary>
        /// 文档格式
        /// </summary>
        Document,

        /// <summary>
        /// MP3 格式
        /// </summary>
        MP3,

        /// <summary>
        /// Flash 格式
        /// </summary>
        Flash,

        /// <summary>
        /// 视频文件
        /// </summary>
        Video,
    }
}