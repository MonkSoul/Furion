// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

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

    /// <summary>
    /// 表名使用蛇形命名
    /// </summary>
    public bool UseSnakeCaseNaming { get; set; }
}