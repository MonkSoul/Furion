// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.DatabaseAccessor;

/// <summary>
/// 构建 Sql 字符串执行部件
/// </summary>
[SuppressSniffer]
public sealed partial class SqlExecutePart
{
    /// <summary>
    /// 静态缺省 Sql 部件
    /// </summary>
    public static SqlExecutePart Default()
    {
        return new();
    }

    /// <summary>
    /// Sql 字符串
    /// </summary>
    public string SqlString { get; private set; }

    /// <summary>
    /// 设置超时时间
    /// </summary>
    public int Timeout { get; private set; }

    /// <summary>
    /// 数据库上下文定位器
    /// </summary>
    public Type DbContextLocator { get; private set; } = typeof(MasterDbContextLocator);

    /// <summary>
    /// 设置服务提供器
    /// </summary>
    public IServiceProvider ContextScoped { get; private set; }
}