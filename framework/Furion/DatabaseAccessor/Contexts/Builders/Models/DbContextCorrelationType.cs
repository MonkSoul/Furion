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

using System.Reflection;

namespace Furion.DatabaseAccessor;

/// <summary>
/// 数据库上下文关联类型
/// </summary>
internal sealed class DbContextCorrelationType
{
    /// <summary>
    /// 构造函数
    /// </summary>
    internal DbContextCorrelationType()
    {
        EntityTypes = new List<Type>();
        EntityNoKeyTypes = new List<Type>();
        EntityTypeBuilderTypes = new List<Type>();
        EntitySeedDataTypes = new List<Type>();
        EntityChangedTypes = new List<Type>();
        ModelBuilderFilterTypes = new List<Type>();
        EntityMutableTableTypes = new List<Type>();
        ModelBuilderFilterInstances = new List<IPrivateModelBuilderFilter>();
        DbFunctionMethods = new List<MethodInfo>();
    }

    /// <summary>
    /// 关联的数据库上下文
    /// </summary>
    internal Type DbContextLocator { get; set; }

    /// <summary>
    /// 所有关联类型
    /// </summary>
    internal List<Type> Types { get; set; }

    /// <summary>
    /// 实体类型集合
    /// </summary>
    internal List<Type> EntityTypes { get; set; }

    /// <summary>
    /// 无键实体类型集合
    /// </summary>
    internal List<Type> EntityNoKeyTypes { get; set; }

    /// <summary>
    /// 实体构建器类型集合
    /// </summary>
    internal List<Type> EntityTypeBuilderTypes { get; set; }

    /// <summary>
    /// 种子数据类型集合
    /// </summary>
    internal List<Type> EntitySeedDataTypes { get; set; }

    /// <summary>
    /// 实体数据改变类型
    /// </summary>
    internal List<Type> EntityChangedTypes { get; set; }

    /// <summary>
    /// 模型构建筛选器类型集合
    /// </summary>
    internal List<Type> ModelBuilderFilterTypes { get; set; }

    /// <summary>
    /// 可变表实体类型集合
    /// </summary>
    internal List<Type> EntityMutableTableTypes { get; set; }

    /// <summary>
    /// 数据库函数方法集合
    /// </summary>
    internal List<MethodInfo> DbFunctionMethods { get; set; }

    /// <summary>
    /// 模型构建器筛选器实例
    /// </summary>
    internal List<IPrivateModelBuilderFilter> ModelBuilderFilterInstances { get; set; }
}