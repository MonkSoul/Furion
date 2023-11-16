// MIT 许可证
//
// 版权 © 2020-present 百小僧, 百签科技（广东）有限公司
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
        var dbContext = Db.CreateDbContext<TDbContextLocator>();
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