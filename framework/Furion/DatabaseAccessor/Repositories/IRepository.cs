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
    /// 获取完整的数据库表名
    /// </summary>
    /// <returns></returns>
    string GetFullTableName();

    /// <summary>
    /// 从分部表中构建查询对象
    /// </summary>
    /// <param name="tableNamesAction"></param>
    /// <param name="tracking">是否跟踪实体</param>
    /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    IQueryable<TEntity> FromSegments(Func<string, IEnumerable<string>> tableNamesAction, bool? tracking = null, bool ignoreQueryFilters = false);

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