// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Microsoft.AspNetCore.Mvc;

/// <summary>
/// 接口参数约束
/// </summary>
[SuppressSniffer, AttributeUsage(AttributeTargets.Parameter)]
public class RouteConstraintAttribute : Attribute
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="constraint"></param>
    public RouteConstraintAttribute(string constraint)
    {
        Constraint = constraint;
    }

    /// <summary>
    /// 约束表达式
    /// </summary>
    public string Constraint { get; set; }
}