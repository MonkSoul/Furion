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

namespace Furion.DatabaseAccessor;

/// <summary>
/// 数据库上下文配置特性
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class AppDbContextAttribute : Attribute
{
    /// <summary>
    /// 默认构造函数
    /// </summary>
    /// <param name="slaveDbContextLocators"></param>
    public AppDbContextAttribute(params Type[] slaveDbContextLocators)
    {
        SlaveDbContextLocators = slaveDbContextLocators;
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="connectionMetadata">支持数据库连接字符串，配置文件的 ConnectionStrings 中的Key或配置文件的完整的配置路径，如果是内存数据库，则为数据库名称</param>
    /// <param name="slaveDbContextLocators"></param>
    public AppDbContextAttribute(string connectionMetadata, params Type[] slaveDbContextLocators)
    {
        ConnectionMetadata = connectionMetadata;
        SlaveDbContextLocators = slaveDbContextLocators;
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="connectionMetadata">支持数据库连接字符串，配置文件的 ConnectionStrings 中的Key或配置文件的完整的配置路径，如果是内存数据库，则为数据库名称</param>
    /// <param name="providerName"></param>
    /// <param name="slaveDbContextLocators"></param>
    public AppDbContextAttribute(string connectionMetadata, string providerName, params Type[] slaveDbContextLocators)
    {
        ConnectionMetadata = connectionMetadata;
        ProviderName = providerName;
        SlaveDbContextLocators = slaveDbContextLocators;
    }

    /// <summary>
    /// 数据库连接元数据
    /// </summary>
    /// <remarks>支持数据库连接字符串，配置文件的 ConnectionStrings 中的Key或配置文件的完整的配置路径，如果是内存数据库，则为数据库名称</remarks>
    public string ConnectionMetadata { get; set; }

    /// <summary>
    /// 数据库提供器名称
    /// </summary>
    public string ProviderName { get; set; }

    /// <summary>
    /// 数据库上下文模式
    /// </summary>
    public DbContextMode Mode { get; set; } = DbContextMode.Cached;

    /// <summary>
    /// 表统一前缀
    /// </summary>
    /// <remarks>前缀不能包含 . 和特殊符号，可使用下划线或短杆线</remarks>
    public string TablePrefix { get; set; }

    /// <summary>
    /// 表统一后缀
    /// </summary>
    /// <remarks>后缀不能包含 . 和特殊符号，可使用下划线或短杆线</remarks>
    public string TableSuffix { get; set; }

    /// <summary>
    /// 指定从库定位器
    /// </summary>
    public Type[] SlaveDbContextLocators { get; set; }
}