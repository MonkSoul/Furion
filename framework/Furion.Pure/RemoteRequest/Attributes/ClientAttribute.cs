// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.RemoteRequest;

/// <summary>
/// 配置请求客户端
/// </summary>
[SuppressSniffer, AttributeUsage(AttributeTargets.Interface | AttributeTargets.Method)]
public class ClientAttribute : Attribute
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="name"></param>
    public ClientAttribute(string name)
    {
        Name = name;
    }

    /// <summary>
    /// 客户端名称
    /// </summary>
    public string Name { get; set; }
}