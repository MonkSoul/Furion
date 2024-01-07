// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using Microsoft.AspNetCore.Mvc.Controllers;
using System.Reflection;

namespace Furion.DynamicApiController;

/// <summary>
/// 动态接口控制器特性提供器
/// </summary>
[SuppressSniffer]
public sealed class DynamicApiControllerFeatureProvider : ControllerFeatureProvider
{
    /// <summary>
    /// 扫描控制器
    /// </summary>
    /// <param name="typeInfo">类型</param>
    /// <returns>bool</returns>
    protected override bool IsController(TypeInfo typeInfo)
    {
        return Penetrates.IsApiController(typeInfo);
    }
}