using Fur.DatabaseVisitor.Dependencies;
using Fur.DependencyInjection.Lifetimes;
using Fur.Unmanaged;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Data.Common;

namespace Fur.DatabaseVisitor.Repositories
{
    public partial class EFCoreRepositoryOfT<TEntity> : UnmanagedDispose, IRepositoryOfT<TEntity>, IScopedLifetimeOfT<TEntity> where TEntity : class, IEntity, new()
    {
        private readonly DbContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;
        public EFCoreRepositoryOfT(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }

        /// <summary>
        /// 数据库操作上下文
        /// </summary>
        public DbContext DbContext => _dbContext;
        /// <summary>
        /// 实体对象
        /// </summary>
        public DbSet<TEntity> Entity => _dbSet;
        /// <summary>
        /// 数据库操作对象
        /// </summary>
        public DatabaseFacade Database => _dbContext.Database;
        /// <summary>
        /// 数据库链接对象
        /// </summary>
        public DbConnection DbConnection => _dbContext.Database.GetDbConnection();
    }
}
