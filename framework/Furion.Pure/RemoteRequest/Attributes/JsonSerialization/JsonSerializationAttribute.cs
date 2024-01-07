// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.RemoteRequest;

/// <summary>
/// JSON 序列化提供器
/// </summary>
[SuppressSniffer, AttributeUsage(AttributeTargets.Interface | AttributeTargets.Method)]
public class JsonSerializationAttribute : Attribute
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="providerType"></param>
    public JsonSerializationAttribute(Type providerType)
    {
        ProviderType = providerType;
    }

    /// <summary>
    /// 提供器类型
    /// </summary>
    public Type ProviderType { get; set; }
}