using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Data.Common;
using System.Linq;

namespace Fur.DatabaseAccessor.Repositories
{
    /// <summary>
    /// EF Core仓储实现
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public partial class EFCoreRepository<TEntity> : IRepository<TEntity>
         where TEntity : class, IDbEntityBase, new()
    {
        /// <summary>
        /// 数据库上下文池
        /// </summary>
        private readonly IDbContextPool _dbContextPool;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbContextPool"></param>
        /// <param name="dbContext"></param>
        public EFCoreRepository(
            IDbContextPool dbContextPool
            , DbContext dbContext)
        {
            _dbContextPool = dbContextPool;
            // 保存当前数据库上下文到池中
            _dbContextPool.SaveDbContext(dbContext);

            // 初始化数据库相关数据
            DbContext = dbContext;
            Database = dbContext.Database;
            DbConnection = Database.GetDbConnection();

            //初始化实体
            Entities = dbContext.Set<TEntity>();
            DerailEntities = Entities.AsNoTracking();
        }

        /// <summary>
        /// 数据库上下文
        /// </summary>
        public DbContext DbContext { get; }

        /// <summary>
        /// 实体集合
        /// </summary>
        public DbSet<TEntity> Entities { get; }

        /// <summary>
        /// 不跟踪的（脱轨）实体
        /// </summary>
        public IQueryable<TEntity> DerailEntities { get; }

        /// <summary>
        /// 数据库操作对象
        /// </summary>
        public DatabaseFacade Database { get; }

        /// <summary>
        /// 数据库连接对象
        /// </summary>
        public DbConnection DbConnection { get; }

        /// <summary>
        /// 租户Id
        /// </summary>
        public Guid? TenantId { get; }
    }
}