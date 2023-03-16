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

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Concurrent;
using System.Data.Common;
using System.Linq.Expressions;

namespace Furion.DatabaseAccessor;

/// <summary>
/// 非泛型仓储
/// </summary>
public partial interface IRepository
{
    /// <summary>
    /// 切换仓储
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <returns>仓储</returns>
    IRepository<TEntity> Change<TEntity>()
        where TEntity : class, IPrivateEntity, new();

    /// <summary>
    /// 切换多数据库上下文仓储
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
    /// <returns>仓储</returns>
    IRepository<TEntity, TDbContextLocator> Change<TEntity, TDbContextLocator>()
        where TEntity : class, IPrivateEntity, new()
        where TDbContextLocator : class, IDbContextLocator;

    /// <summary>
    /// 重新构建并切换仓储
    /// </summary>
    /// <remarks>特别注意，Scoped 必须手动释放</remarks>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <returns>仓储</returns>
    (IRepository<TEntity> Repository, IServiceScope Scoped) BuildChange<TEntity>()
        where TEntity : class, IPrivateEntity, new();

    /// <summary>
    /// 重新构建并切换多数据库上下文仓储
    /// </summary>
    /// <remarks>特别注意，Scoped 必须手动释放</remarks>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
    /// <returns>仓储</returns>
    (IRepository<TEntity, TDbContextLocator> Repository, IServiceScope Scoped) BuildChange<TEntity, TDbContextLocator>()
        where TEntity : class, IPrivateEntity, new()
        where TDbContextLocator : class, IDbContextLocator;

    /// <summary>
    /// 获取 Sql 操作仓储
    /// </summary>
    /// <returns>ISqlRepository</returns>
    ISqlRepository Sql();

    /// <summary>
    /// 获取多数据库上下文 Sql 操作仓储
    /// </summary>
    /// <returns>ISqlRepository{TDbContextLocator}</returns>
    ISqlRepository<TDbContextLocator> Sql<TDbContextLocator>()
         where TDbContextLocator : class, IDbContextLocator;

    /// <summary>
    /// 解析服务
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    /// <returns></returns>
    TService GetService<TService>();

    /// <summary>
    /// 解析服务
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    /// <returns></returns>
    TService GetRequiredService<TService>();
}

/// <summary>
/// 仓储接口
/// </summary>
/// <typeparam name="TEntity">实体类型</typeparam>
public partial interface IRepository<TEntity> : IRepository<TEntity, MasterDbContextLocator>
    , IWritableRepository<TEntity>
    , IReadableRepository<TEntity>
    , ISqlRepository
    where TEntity : class, IPrivateEntity, new()
{
}

/// <summary>
/// 多数据库上下文仓储
/// </summary>
/// <typeparam name="TEntity">实体类型</typeparam>
/// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
public partial interface IRepository<TEntity, TDbContextLocator> : IPrivateRepository<TEntity>
    , IWritableRepository<TEntity, TDbContextLocator>
    , IReadableRepository<TEntity, TDbContextLocator>
    , ISqlRepository<TDbContextLocator>
    where TEntity : class, IPrivateEntity, new()
    where TDbContextLocator : class, IDbContextLocator
{
}

/// <summary>
/// 私有公共实体
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public interface IPrivateRepository<TEntity>
    : IPrivateWritableRepository<TEntity>
    , IPrivateReadableRepository<TEntity>
    , IPrivateSqlRepository
    where TEntity : class, IPrivateEntity, new()
{
    /// <summary>
    /// 实体集合
    /// </summary>
    DbSet<TEntity> Entities { get; }

    /// <summary>
    /// 不跟踪的（脱轨）实体
    /// </summary>
    IQueryable<TEntity> DetachedEntities { get; }

    /// <summary>
    /// 查看实体类型
    /// </summary>
    IEntityType EntityType { get; }

    /// <summary>
    /// 数据库连接对象
    /// </summary>
    DbConnection DbConnection { get; }

    /// <summary>
    /// 实体追综器
    /// </summary>
    ChangeTracker ChangeTracker { get; }

    /// <summary>
    /// 实体模型
    /// </summary>
    IModel Model { get; }

    /// <summary>
    /// 租户信息
    /// </summary>
    Tenant Tenant { get; }

    /// <summary>
    /// 数据库提供器名
    /// </summary>
    string ProviderName { get; }

    /// <summary>
    /// 服务提供器
    /// </summary>
    IServiceProvider ServiceProvider { get; }

    /// <summary>
    /// 租户Id
    /// </summary>
    Guid? TenantId { get; }

    /// <summary>
    /// 判断上下文是否更改
    /// </summary>
    /// <returns>bool</returns>
    bool HasChanges();

    /// <summary>
    /// 将实体加入数据上下文托管
    /// </summary>
    /// <param name="entity">实体</param>
    /// <returns>EntityEntry</returns>
    EntityEntry Entry(object entity);

    /// <summary>
    /// 将实体加入数据上下文托管
    /// </summary>
    /// <param name="entity">实体</param>
    /// <returns></returns>
    EntityEntry<TEntity> Entry(TEntity entity);

    /// <summary>
    /// 获取实体状态
    /// </summary>
    /// <param name="entity">实体</param>
    /// <returns></returns>
    EntityState EntityEntryState(object entity);

    /// <summary>
    /// 获取实体状态
    /// </summary>
    /// <param name="entity">实体</param>
    /// <returns>EntityState</returns>
    EntityState EntityEntryState(TEntity entity);

    /// <summary>
    /// 将实体属性加入托管
    /// </summary>
    /// <param name="entity">实体</param>
    /// <param name="propertyName">属性名</param>
    /// <returns>PropertyEntry</returns>
    PropertyEntry EntityPropertyEntry(object entity, string propertyName);

    /// <summary>
    /// 将实体属性加入托管
    /// </summary>
    /// <param name="entity">实体</param>
    /// <param name="propertyName">属性名</param>
    /// <returns>PropertyEntry</returns>
    PropertyEntry EntityPropertyEntry(TEntity entity, string propertyName);

    /// <summary>
    /// 将实体属性加入托管
    /// </summary>
    /// <typeparam name="TProperty">属性类型</typeparam>
    /// <param name="entity">实体</param>
    /// <param name="propertyPredicate">属性表达式</param>
    /// <returns>PropertyEntry</returns>
    PropertyEntry<TEntity, TProperty> EntityPropertyEntry<TProperty>(TEntity entity, Expression<Func<TEntity, TProperty>> propertyPredicate);

    /// <summary>
    /// 改变实体状态
    /// </summary>
    /// <param name="entity">实体</param>
    /// <param name="entityState">实体状态</param>
    /// <returns>EntityEntry</returns>
    EntityEntry ChangeEntityState(object entity, EntityState entityState);

    /// <summary>
    /// 改变实体状态
    /// </summary>
    /// <param name="entity">实体</param>
    /// <param name="entityState">实体状态</param>
    /// <returns>EntityEntry{TEntity}</returns>
    EntityEntry<TEntity> ChangeEntityState(TEntity entity, EntityState entityState);

    /// <summary>
    /// 改变实体状态
    /// </summary>
    /// <param name="entityEntry">实体条目</param>
    /// <param name="entityState">实体状态</param>
    /// <returns>EntityEntry</returns>
    EntityEntry ChangeEntityState(EntityEntry entityEntry, EntityState entityState);

    /// <summary>
    /// 改变实体状态
    /// </summary>
    /// <param name="entityEntry">实体条目</param>
    /// <param name="entityState">实体状态</param>
    /// <returns>EntityEntry{TEntity}</returns>
    EntityEntry<TEntity> ChangeEntityState(EntityEntry<TEntity> entityEntry, EntityState entityState);

    /// <summary>
    /// 检查实体跟踪状态
    /// </summary>
    /// <param name="id"></param>
    /// <param name="entityEntry"></param>
    /// <param name="keyName"></param>
    /// <returns></returns>
    bool CheckTrackState(object id, out EntityEntry entityEntry, string keyName = default);

    /// <summary>
    /// 检查实体跟踪状态
    /// </summary>
    /// <typeparam name="TTrackEntity"></typeparam>
    /// <param name="id"></param>
    /// <param name="entityEntry"></param>
    /// <param name="keyName"></param>
    /// <returns></returns>
    bool CheckTrackState<TTrackEntity>(object id, out EntityEntry entityEntry, string keyName = default)
         where TTrackEntity : class, IPrivateEntity, new();

    /// <summary>
    /// 判断是否被附加
    /// </summary>
    /// <param name="entity">实体</param>
    /// <returns>bool</returns>
    bool IsAttached(object entity);

    /// <summary>
    /// 判断是否被附加
    /// </summary>
    /// <param name="entity">实体</param>
    /// <returns>bool</returns>
    bool IsAttached(TEntity entity);

    /// <summary>
    /// 附加实体
    /// </summary>
    /// <param name="entity">实体</param>
    /// <returns>EntityEntry</returns>
    EntityEntry Attach(object entity);

    /// <summary>
    /// 附加实体
    /// </summary>
    /// <param name="entity">实体</param>
    /// <returns>EntityEntry</returns>
    EntityEntry<TEntity> Attach(TEntity entity);

    /// <summary>
    /// 附加多个实体
    /// </summary>
    /// <param name="entities">多个实体</param>
    void AttachRange(params object[] entities);

    /// <summary>
    /// 附加多个实体
    /// </summary>
    /// <param name="entities">多个实体</param>
    void AttachRange(IEnumerable<TEntity> entities);

    /// <summary>
    /// 取消附加实体
    /// </summary>
    /// <param name="entity">实体</param>
    void Detach(object entity);

    /// <summary>
    /// 取消附加实体
    /// </summary>
    /// <param name="entity">实体</param>
    void Detach(TEntity entity);

    /// <summary>
    /// 取消附加实体
    /// </summary>
    /// <param name="entityEntry">实体条目</param>
    void Detach(EntityEntry entityEntry);

    /// <summary>
    /// 取消附加实体
    /// </summary>
    /// <param name="entityEntry">实体条目</param>
    void Detach(EntityEntry<TEntity> entityEntry);

    /// <summary>
    /// 获取所有数据库上下文
    /// </summary>
    /// <returns>ConcurrentBag{DbContext}</returns>
    public ConcurrentDictionary<Guid, DbContext> GetDbContexts();

    /// <summary>
    /// 判断实体是否设置了主键
    /// </summary>
    /// <param name="entity">实体</param>
    /// <returns>bool</returns>
    bool IsKeySet(TEntity entity);

    /// <summary>
    /// 删除数据库
    /// </summary>
    void EnsureDeleted();

    /// <summary>
    /// 删除数据库
    /// </summary>
    Task EnsureDeletedAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// 创建数据库
    /// </summary>
    void EnsureCreated();

    /// <summary>
    /// 创建数据库
    /// </summary>
    Task EnsureCreatedAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// 动态改变表名
    /// </summary>
    /// <param name="tableName">表名</param>
    [Obsolete("该方法已过时，请调用 BuildChange<TEntity> 方法代替。")]
    void ChangeTable(string tableName);

    /// <summary>
    /// 动态改变数据库
    /// </summary>
    /// <param name="connectionString">连接字符串</param>
    void ChangeDatabase(string connectionString);

    /// <summary>
    /// 动态改变数据库
    /// </summary>
    /// <param name="connectionString">连接字符串</param>
    /// <param name="cancellationToken">异步取消令牌</param>
    Task ChangeDatabaseAsync(string connectionString, CancellationToken cancellationToken = default);

    /// <summary>
    /// 判断是否是 SqlServer 数据库
    /// </summary>
    /// <returns>bool</returns>
    bool IsSqlServer();

    /// <summary>
    /// 判断是否是 Sqlite 数据库
    /// </summary>
    /// <returns>bool</returns>
    bool IsSqlite();

    /// <summary>
    /// 判断是否是 Cosmos 数据库
    /// </summary>
    /// <returns>bool</returns>
    bool IsCosmos();

    /// <summary>
    /// 判断是否是 内存中 数据库
    /// </summary>
    /// <returns>bool</returns>
    bool InMemoryDatabase();

    /// <summary>
    /// 判断是否是 MySql 数据库
    /// </summary>
    /// <returns>bool</returns>
    bool IsMySql();

    /// <summary>
    /// 判断是否是 MySql 数据库 官方包（更新不及时，只支持 8.0.23+ 版本， 所以单独弄一个分类）
    /// </summary>
    /// <returns>bool</returns>
    bool IsMySqlOfficial();

    /// <summary>
    /// 判断是否是 PostgreSQL 数据库
    /// </summary>
    /// <returns>bool</returns>
    bool IsNpgsql();

    /// <summary>
    /// 判断是否是 Oracle 数据库
    /// </summary>
    /// <returns>bool</returns>
    bool IsOracle();

    /// <summary>
    /// 判断是否是 Firebird 数据库
    /// </summary>
    /// <returns>bool</returns>
    bool IsFirebird();

    /// <summary>
    /// 判断是否是 Dm 数据库
    /// </summary>
    /// <returns>bool</returns>
    bool IsDm();

    /// <summary>
    /// 判断是否是关系型数据库
    /// </summary>
    /// <returns>bool</returns>
    bool IsRelational();

    /// <summary>
    /// 切换仓储
    /// </summary>
    /// <typeparam name="TChangeEntity">实体类型</typeparam>
    /// <returns>仓储</returns>
    new IRepository<TChangeEntity> Change<TChangeEntity>()
            where TChangeEntity : class, IPrivateEntity, new();

    /// <summary>
    /// 切换多数据库上下文仓储
    /// </summary>
    /// <typeparam name="TChangeEntity">实体类型</typeparam>
    /// <typeparam name="TChangeDbContextLocator">数据库上下文定位器</typeparam>
    /// <returns>仓储</returns>
    IRepository<TChangeEntity, TChangeDbContextLocator> Change<TChangeEntity, TChangeDbContextLocator>()
        where TChangeEntity : class, IPrivateEntity, new()
        where TChangeDbContextLocator : class, IDbContextLocator;

    /// <summary>
    /// 重新构建并切换仓储
    /// </summary>
    /// <remarks>特别注意，Scoped 必须手动释放</remarks>
    /// <typeparam name="TChangeEntity">实体类型</typeparam>
    /// <returns>仓储</returns>
    (IRepository<TChangeEntity> Repository, IServiceScope Scoped) BuildChange<TChangeEntity>()
        where TChangeEntity : class, IPrivateEntity, new();

    /// <summary>
    /// 重新构建并切换多数据库上下文仓储
    /// </summary>
    /// <remarks>特别注意，Scoped 必须手动释放</remarks>
    /// <typeparam name="TChangeEntity">实体类型</typeparam>
    /// <typeparam name="TChangeDbContextLocator">数据库上下文定位器</typeparam>
    /// <returns>仓储</returns>
    (IRepository<TChangeEntity, TChangeDbContextLocator> Repository, IServiceScope Scoped) BuildChange<TChangeEntity, TChangeDbContextLocator>()
        where TChangeEntity : class, IPrivateEntity, new()
        where TChangeDbContextLocator : class, IDbContextLocator;
}