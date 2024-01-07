// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.RemoteRequest;

/// <summary>
/// 配置请求报文头
/// </summary>
[SuppressSniffer, AttributeUsage(AttributeTargets.Interface | AttributeTargets.Method | AttributeTargets.Parameter, AllowMultiple = true)]
public class HeadersAttribute : Attribute
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public HeadersAttribute(string key, object value)
    {
        Key = key;
        Value = value;
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <remarks>用于将参数添加到请求报文头中，如 Token </remarks>
    public HeadersAttribute()
    {
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <remarks>用于将参数添加到请求报文头中，如 Token </remarks>
    /// <param name="alias">别名</param>
    public HeadersAttribute(string alias)
    {
        Key = alias;
    }

    /// <summary>
    /// 键
    /// </summary>
    public string Key { get; set; }

    /// <summary>
    /// 值
    /// </summary>
    public object Value { get; set; }
}