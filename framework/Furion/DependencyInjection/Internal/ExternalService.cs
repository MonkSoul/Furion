// MIT 许可证
//
// 版权 © 2020-present 百小僧, 百签科技（广东）有限公司 和所有贡献者
//
// 特此免费授予任何获得本软件副本和相关文档文件（下称“软件”）的人不受限制地处置该软件的权利，
// 包括不受限制地使用、复制、修改、合并、发布、分发、转授许可和/或出售该软件副本，
// 以及再授权被配发了本软件的人如上的权利，须在下列条件下：
//
// 上述版权声明和本许可声明应包含在该软件的所有副本或实质成分中。
//
// 本软件是“如此”提供的，没有任何形式的明示或暗示的保证，包括但不限于对适销性、特定用途的适用性和不侵权的保证。
// 在任何情况下，作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

namespace Furion.DependencyInjection;

/// <summary>
/// 外部注册类型模型
/// </summary>
[SuppressSniffer]
public sealed class ExternalService
{
    /// <summary>
    /// 接口类型，格式："程序集名称;接口完整名称"
    /// </summary>
    public string Interface { get; set; }

    /// <summary>
    /// 实例类型，格式："程序集名称;接口完整名称"
    /// </summary>
    public string Service { get; set; }

    /// <summary>
    /// 注册类型
    /// </summary>
    public RegisterType RegisterType { get; set; }

    /// <summary>
    /// 添加服务方式，存在不添加，或继续添加
    /// </summary>
    public InjectionActions Action { get; set; } = InjectionActions.Add;

    /// <summary>
    /// 注册选项
    /// </summary>
    public InjectionPatterns Pattern { get; set; } = InjectionPatterns.All;

    /// <summary>
    /// 注册别名
    /// </summary>
    /// <remarks>多服务时使用</remarks>
    public string Named { get; set; }

    /// <summary>
    /// 排序，排序越大，则在后面注册
    /// </summary>
    public int Order { get; set; }

    /// <summary>
    /// 代理类型，格式："程序集名称;接口完整名称"
    /// </summary>
    public string Proxy { get; set; }
}