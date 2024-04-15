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
/// 随机主从库仓储（主库是默认数据库）
/// </summary>
public partial interface IMSRepository : IMSRepository<MasterDbContextLocator>
{
}

/// <summary>
/// 随机主从库仓储
/// </summary>
/// <typeparam name="TMasterDbContextLocator">主库</typeparam>
public partial interface IMSRepository<TMasterDbContextLocator>
    where TMasterDbContextLocator : class, IDbContextLocator
{
    /// <summary>
    /// 获取主库仓储
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <returns></returns>
    IRepository<TEntity, TMasterDbContextLocator> Master<TEntity>()
        where TEntity : class, IPrivateEntity, new();

    /// <summary>
    /// 动态获取从库（随机）
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <returns></returns>
    IPrivateReadableRepository<TEntity> Slave<TEntity>()
        where TEntity : class, IPrivateEntity, new();

    /// <summary>
    /// 动态获取从库（自定义）
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <returns></returns>
    IPrivateReadableRepository<TEntity> Slave<TEntity>(Func<Type> locatorHandle)
        where TEntity : class, IPrivateEntity, new();
}

/// <summary>
/// 主从库仓储
/// </summary>
/// <typeparam name="TMasterDbContextLocator">主库</typeparam>
/// <typeparam name="TSlaveDbContextLocator1">从库</typeparam>
public partial interface IMSRepository<TMasterDbContextLocator, TSlaveDbContextLocator1>
    where TMasterDbContextLocator : class, IDbContextLocator
    where TSlaveDbContextLocator1 : class, IDbContextLocator
{
    /// <summary>
    /// 获取主库仓储
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <returns></returns>
    IRepository<TEntity, TMasterDbContextLocator> Master<TEntity>()
         where TEntity : class, IPrivateEntity, new();

    /// <summary>
    /// 获取从库仓储
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <returns></returns>
    IReadableRepository<TEntity, TSlaveDbContextLocator1> Slave1<TEntity>()
        where TEntity : class, IPrivateEntity, new();
}

/// <summary>
/// 主从库仓储
/// </summary>
/// <typeparam name="TMasterDbContextLocator">主库</typeparam>
/// <typeparam name="TSlaveDbContextLocator1">从库</typeparam>
/// <typeparam name="TSlaveDbContextLocator2">从库</typeparam>
public partial interface IMSRepository<TMasterDbContextLocator, TSlaveDbContextLocator1, TSlaveDbContextLocator2>
    : IMSRepository<TMasterDbContextLocator, TSlaveDbContextLocator1>
    where TMasterDbContextLocator : class, IDbContextLocator
    where TSlaveDbContextLocator1 : class, IDbContextLocator
    where TSlaveDbContextLocator2 : class, IDbContextLocator
{
    /// <summary>
    /// 获取从库仓储2
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <returns></returns>
    IReadableRepository<TEntity, TSlaveDbContextLocator2> Slave2<TEntity>()
        where TEntity : class, IPrivateEntity, new();
}

/// <summary>
/// 主从库仓储
/// </summary>
/// <typeparam name="TMasterDbContextLocator">主库</typeparam>
/// <typeparam name="TSlaveDbContextLocator1">从库</typeparam>
/// <typeparam name="TSlaveDbContextLocator2">从库</typeparam>
/// <typeparam name="TSlaveDbContextLocator3">从库</typeparam>
public partial interface IMSRepository<TMasterDbContextLocator, TSlaveDbContextLocator1, TSlaveDbContextLocator2, TSlaveDbContextLocator3>
    : IMSRepository<TMasterDbContextLocator, TSlaveDbContextLocator1, TSlaveDbContextLocator2>
    where TMasterDbContextLocator : class, IDbContextLocator
    where TSlaveDbContextLocator1 : class, IDbContextLocator
    where TSlaveDbContextLocator2 : class, IDbContextLocator
    where TSlaveDbContextLocator3 : class, IDbContextLocator
{
    /// <summary>
    /// 获取从库仓储3
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <returns></returns>
    IReadableRepository<TEntity, TSlaveDbContextLocator3> Slave3<TEntity>()
        where TEntity : class, IPrivateEntity, new();
}

/// <summary>
/// 主从库仓储
/// </summary>
/// <typeparam name="TMasterDbContextLocator">主库</typeparam>
/// <typeparam name="TSlaveDbContextLocator1">从库</typeparam>
/// <typeparam name="TSlaveDbContextLocator2">从库</typeparam>
/// <typeparam name="TSlaveDbContextLocator3">从库</typeparam>
/// <typeparam name="TSlaveDbContextLocator4">从库</typeparam>
public partial interface IMSRepository<TMasterDbContextLocator, TSlaveDbContextLocator1, TSlaveDbContextLocator2, TSlaveDbContextLocator3, TSlaveDbContextLocator4>
    : IMSRepository<TMasterDbContextLocator, TSlaveDbContextLocator1, TSlaveDbContextLocator2, TSlaveDbContextLocator3>
    where TMasterDbContextLocator : class, IDbContextLocator
    where TSlaveDbContextLocator1 : class, IDbContextLocator
    where TSlaveDbContextLocator2 : class, IDbContextLocator
    where TSlaveDbContextLocator3 : class, IDbContextLocator
    where TSlaveDbContextLocator4 : class, IDbContextLocator
{
    /// <summary>
    /// 获取从库仓储4
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <returns></returns>
    IReadableRepository<TEntity, TSlaveDbContextLocator4> Slave4<TEntity>()
        where TEntity : class, IPrivateEntity, new();
}

/// <summary>
/// 主从库仓储
/// </summary>
/// <typeparam name="TMasterDbContextLocator">主库</typeparam>
/// <typeparam name="TSlaveDbContextLocator1">从库</typeparam>
/// <typeparam name="TSlaveDbContextLocator2">从库</typeparam>
/// <typeparam name="TSlaveDbContextLocator3">从库</typeparam>
/// <typeparam name="TSlaveDbContextLocator4">从库</typeparam>
/// <typeparam name="TSlaveDbContextLocator5">从库</typeparam>
public partial interface IMSRepository<TMasterDbContextLocator, TSlaveDbContextLocator1, TSlaveDbContextLocator2, TSlaveDbContextLocator3, TSlaveDbContextLocator4, TSlaveDbContextLocator5>
    : IMSRepository<TMasterDbContextLocator, TSlaveDbContextLocator1, TSlaveDbContextLocator2, TSlaveDbContextLocator3, TSlaveDbContextLocator4>
    where TMasterDbContextLocator : class, IDbContextLocator
    where TSlaveDbContextLocator1 : class, IDbContextLocator
    where TSlaveDbContextLocator2 : class, IDbContextLocator
    where TSlaveDbContextLocator3 : class, IDbContextLocator
    where TSlaveDbContextLocator4 : class, IDbContextLocator
    where TSlaveDbContextLocator5 : class, IDbContextLocator
{
    /// <summary>
    /// 获取从库仓储5
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <returns></returns>
    IReadableRepository<TEntity, TSlaveDbContextLocator5> Slave5<TEntity>()
        where TEntity : class, IPrivateEntity, new();
}

/// <summary>
/// 主从库仓储
/// </summary>
/// <typeparam name="TMasterDbContextLocator">主库</typeparam>
/// <typeparam name="TSlaveDbContextLocator1">从库</typeparam>
/// <typeparam name="TSlaveDbContextLocator2">从库</typeparam>
/// <typeparam name="TSlaveDbContextLocator3">从库</typeparam>
/// <typeparam name="TSlaveDbContextLocator4">从库</typeparam>
/// <typeparam name="TSlaveDbContextLocator5">从库</typeparam>
/// <typeparam name="TSlaveDbContextLocator6">从库</typeparam>
public partial interface IMSRepository<TMasterDbContextLocator, TSlaveDbContextLocator1, TSlaveDbContextLocator2, TSlaveDbContextLocator3, TSlaveDbContextLocator4, TSlaveDbContextLocator5, TSlaveDbContextLocator6>
    : IMSRepository<TMasterDbContextLocator, TSlaveDbContextLocator1, TSlaveDbContextLocator2, TSlaveDbContextLocator3, TSlaveDbContextLocator4, TSlaveDbContextLocator5>
    where TMasterDbContextLocator : class, IDbContextLocator
    where TSlaveDbContextLocator1 : class, IDbContextLocator
    where TSlaveDbContextLocator2 : class, IDbContextLocator
    where TSlaveDbContextLocator3 : class, IDbContextLocator
    where TSlaveDbContextLocator4 : class, IDbContextLocator
    where TSlaveDbContextLocator5 : class, IDbContextLocator
    where TSlaveDbContextLocator6 : class, IDbContextLocator
{
    /// <summary>
    /// 获取从库仓储6
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <returns></returns>
    IReadableRepository<TEntity, TSlaveDbContextLocator6> Slave6<TEntity>()
        where TEntity : class, IPrivateEntity, new();
}

/// <summary>
/// 主从库仓储
/// </summary>
/// <typeparam name="TMasterDbContextLocator">主库</typeparam>
/// <typeparam name="TSlaveDbContextLocator1">从库</typeparam>
/// <typeparam name="TSlaveDbContextLocator2">从库</typeparam>
/// <typeparam name="TSlaveDbContextLocator3">从库</typeparam>
/// <typeparam name="TSlaveDbContextLocator4">从库</typeparam>
/// <typeparam name="TSlaveDbContextLocator5">从库</typeparam>
/// <typeparam name="TSlaveDbContextLocator6">从库</typeparam>
/// <typeparam name="TSlaveDbContextLocator7">从库</typeparam>
public partial interface IMSRepository<TMasterDbContextLocator, TSlaveDbContextLocator1, TSlaveDbContextLocator2, TSlaveDbContextLocator3, TSlaveDbContextLocator4, TSlaveDbContextLocator5, TSlaveDbContextLocator6, TSlaveDbContextLocator7>
    : IMSRepository<TMasterDbContextLocator, TSlaveDbContextLocator1, TSlaveDbContextLocator2, TSlaveDbContextLocator3, TSlaveDbContextLocator4, TSlaveDbContextLocator5, TSlaveDbContextLocator6>
    where TMasterDbContextLocator : class, IDbContextLocator
    where TSlaveDbContextLocator1 : class, IDbContextLocator
    where TSlaveDbContextLocator2 : class, IDbContextLocator
    where TSlaveDbContextLocator3 : class, IDbContextLocator
    where TSlaveDbContextLocator4 : class, IDbContextLocator
    where TSlaveDbContextLocator5 : class, IDbContextLocator
    where TSlaveDbContextLocator6 : class, IDbContextLocator
    where TSlaveDbContextLocator7 : class, IDbContextLocator
{
    /// <summary>
    /// 获取从库仓储7
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <returns></returns>
    IReadableRepository<TEntity, TSlaveDbContextLocator7> Slave7<TEntity>()
        where TEntity : class, IPrivateEntity, new();
}