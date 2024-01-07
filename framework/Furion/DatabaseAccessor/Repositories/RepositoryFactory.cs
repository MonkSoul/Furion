// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Furion.DatabaseAccessor;

/// <summary>
/// 仓储工厂实现
/// </summary>
internal sealed class RepositoryFactory<TEntity> : RepositoryFactory<TEntity, MasterDbContextLocator>
    , IRepositoryFactory<TEntity>
    where TEntity : class, IPrivateEntity, new()
{
    public RepositoryFactory()
        : base()
    {
    }
}


/// <summary>
/// 仓储工厂实现
/// </summary>
internal class RepositoryFactory<TEntity, TDbContextLocator> : IRepositoryFactory<TEntity, TDbContextLocator>
    where TEntity : class, IPrivateEntity, new()
    where TDbContextLocator : class, IDbContextLocator
{
    /// <summary>
    /// 创建实体仓储（需要手动 using）
    /// </summary>
    /// <returns></returns>
    public IRepository<TEntity, TDbContextLocator> CreateRepository()
    {
        // 初始化新的数据库上下文
        var dbContext = Db.CreateDbContext<TDbContextLocator>();

        // 创建 EFCore 仓储
        var efcoreRepository = new EFCoreRepository<TEntity, TDbContextLocator>(dbContext.GetService<IServiceProvider>())
        {
            UndisposedContext = true,
            Context = dbContext,
            DynamicContext = dbContext,
            Database = dbContext.Database,
            ProviderName = dbContext.Database.ProviderName,
            ChangeTracker = dbContext.ChangeTracker,
            Model = dbContext.Model,
            Tenant = ((dynamic)dbContext).Tenant,
            Entities = dbContext.Set<TEntity>(),
            DetachedEntities = dbContext.Set<TEntity>().AsNoTracking(),
            EntityType = dbContext.Set<TEntity>().EntityType
        };

        if (dbContext.Database.IsRelational())
        {
            efcoreRepository.DbConnection = dbContext.Database.GetDbConnection();
        }

        return efcoreRepository;
    }
}