// ------------------------------------------------------------------------
// 版权信息
// 版权归百小僧及百签科技（广东）有限公司所有。
// 所有权利保留。
// 官方网站：https://baiqian.com
//
// 许可证信息
// Furion 项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。
// 许可证的完整文本可以在源代码树根目录中的 LICENSE-APACHE 和 LICENSE-MIT 文件中找到。
// 官方网站：https://furion.net
//
// 使用条款
// 使用本代码应遵守相关法律法规和许可证的要求。
//
// 免责声明
// 对于因使用本代码而产生的任何直接、间接、偶然、特殊或后果性损害，我们不承担任何责任。
//
// 其他重要信息
// Furion 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。
// 有关 Furion 项目的其他详细信息，请参阅位于源代码树根目录中的 COPYRIGHT 和 DISCLAIMER 文件。
//
// 更多信息
// 请访问 https://gitee.com/dotnetchina/Furion 获取更多关于 Furion 项目的许可证和版权信息。
// ------------------------------------------------------------------------

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