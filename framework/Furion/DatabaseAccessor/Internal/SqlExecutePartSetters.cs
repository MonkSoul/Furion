// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.DatabaseAccessor;

/// <summary>
/// 构建 Sql 字符串执行部件
/// </summary>
public sealed partial class SqlExecutePart
{
    /// <summary>
    /// 设置 Sql 字符串
    /// </summary>
    /// <param name="sql"></param>
    /// <returns></returns>
    public SqlExecutePart SetSqlString(string sql)
    {
        if (!string.IsNullOrWhiteSpace(sql)) SqlString = sql;
        return this;
    }

    /// <summary>
    /// 设置 ADO.NET 超时时间
    /// </summary>
    /// <param name="timeout">单位秒</param>
    /// <returns></returns>
    public SqlExecutePart SetCommandTimeout(int timeout)
    {
        if (timeout > 0) Timeout = timeout;
        return this;
    }

    /// <summary>
    /// 设置数据库执行作用域
    /// </summary>
    /// <param name="serviceProvider"></param>
    /// <returns></returns>
    public SqlExecutePart SetContextScoped(IServiceProvider serviceProvider)
    {
        if (serviceProvider != null) ContextScoped = serviceProvider;
        return this;
    }

    /// <summary>
    /// 设置数据库上下文定位器
    /// </summary>
    /// <typeparam name="TDbContextLocator"></typeparam>
    /// <returns></returns>
    public SqlExecutePart Change<TDbContextLocator>()
        where TDbContextLocator : class, IDbContextLocator
    {
        return Change(typeof(TDbContextLocator));
    }

    /// <summary>
    /// 设置数据库上下文定位器
    /// </summary>
    /// <returns></returns>
    public SqlExecutePart Change(Type dbContextLocator)
    {
        if (dbContextLocator != null) DbContextLocator = dbContextLocator;
        return this;
    }
}