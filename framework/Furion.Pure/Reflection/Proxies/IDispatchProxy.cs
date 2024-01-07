// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.Reflection;

/// <summary>
/// 代理拦截依赖接口
/// </summary>
public interface IDispatchProxy
{
    /// <summary>
    /// 实例
    /// </summary>
    object Target { get; set; }

    /// <summary>
    /// 服务提供器
    /// </summary>
    IServiceProvider Services { get; set; }
}