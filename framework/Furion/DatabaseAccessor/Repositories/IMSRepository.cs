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