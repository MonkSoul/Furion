// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.RemoteRequest;

/// <summary>
/// 远程请求参数拦截器
/// </summary>
/// <remarks>如果贴在静态方法中，则为全局拦截</remarks>
[SuppressSniffer, AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Method)]
public class InterceptorAttribute : Attribute
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="type"></param>
    public InterceptorAttribute(InterceptorTypes type)
    {
        Type = type;
    }

    /// <summary>
    /// 拦截类型
    /// </summary>
    public InterceptorTypes Type { get; set; }
}