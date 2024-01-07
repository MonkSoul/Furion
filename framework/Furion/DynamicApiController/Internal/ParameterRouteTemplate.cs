// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.DynamicApiController;

/// <summary>
/// 参数路由模板
/// </summary>
internal class ParameterRouteTemplate
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public ParameterRouteTemplate()
    {
        ControllerStartTemplates = new List<string>();
        ControllerEndTemplates = new List<string>();
        ActionStartTemplates = new List<string>();
        ActionEndTemplates = new List<string>();
    }

    /// <summary>
    /// 控制器之前的参数
    /// </summary>
    public IList<string> ControllerStartTemplates { get; set; }

    /// <summary>
    /// 控制器之后的参数
    /// </summary>
    public IList<string> ControllerEndTemplates { get; set; }

    /// <summary>
    /// 行为之前的参数
    /// </summary>
    public IList<string> ActionStartTemplates { get; set; }

    /// <summary>
    /// 行为之后的参数
    /// </summary>
    public IList<string> ActionEndTemplates { get; set; }
}