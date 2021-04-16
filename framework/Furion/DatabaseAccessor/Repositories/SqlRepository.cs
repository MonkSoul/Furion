using Furion.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Furion.DatabaseAccessor
{
    /// <summary>
    /// Sql 操作仓储实现
    /// </summary>
    [SkipScan]
    public partial class SqlRepository : PrivateSqlRepository, ISqlRepository
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="scoped">服务提供器</param>
        public SqlRepository(IServiceProvider scoped) : base(typeof(MasterDbContextLocator), scoped)
        {
        }
    }

    /// <summary>
    /// Sql 操作仓储实现
    /// </summary>
    [SkipScan]
    public partial class SqlRepository<TDbContextLocator> : PrivateSqlRepository, ISqlRepository<TDbContextLocator>
        where TDbContextLocator : class, IDbContextLocator
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="scoped">服务提供器</param>
        public SqlRepository(IServiceProvider scoped) : base(typeof(TDbContextLocator), scoped)
        {
        }
    }

    /// <summary>
    /// 私有 Sql 仓储
    /// </summary>
    public partial class PrivateSqlRepository : IPrivateSqlRepository
    {
        /// <summary>
        /// 服务提供器
        /// </summary>
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbContextLocator"></param>
        /// <param name="scoped">服务提供器</param>
        public PrivateSqlRepository(Type dbContextLocator, IServiceProvider scoped)
        {
            _serviceProvider = scoped;

            // 解析数据库上下文
            var dbContextResolve = scoped.GetService<Func<Type, IScoped, DbContext>>();
            var dbContext = dbContextResolve(dbContextLocator, default);
            DynamicContext = Context = dbContext;

            // 初始化数据库相关数据
            Database = Context.Database;
        }

        /// <summary>
        /// 数据库上下文
        /// </summary>
        public virtual DbContext Context { get; }

        /// <summary>
        /// 动态数据库上下文
        /// </summary>
        public virtual dynamic DynamicContext { get; }

        /// <summary>
        /// 数据库操作对象
        /// </summary>
        public virtual DatabaseFacade Database { get; }

        /// <summary>
        /// 切换仓储
        /// </summary>
        /// <typeparam name="TChangeDbContextLocator">数据库上下文定位器</typeparam>
        /// <returns>仓储</returns>
        public virtual ISqlRepository<TChangeDbContextLocator> Change<TChangeDbContextLocator>()
             where TChangeDbContextLocator : class, IDbContextLocator
        {
            return _serviceProvider.GetService<ISqlRepository<TChangeDbContextLocator>>();
        }

        /// <summary>
        /// 解析服务
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <returns></returns>
        public virtual TService GetService<TService>()
            where TService : class
        {
            return _serviceProvider.GetService<TService>();
        }

        /// <summary>
        /// 解析服务
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <returns></returns>
        public virtual TService GetRequiredService<TService>()
            where TService : class
        {
            return _serviceProvider.GetRequiredService<TService>();
        }

        /// <summary>
        /// 将仓储约束为特定仓储
        /// </summary>
        /// <typeparam name="TRestrainRepository">特定仓储</typeparam>
        /// <returns>TRestrainRepository</returns>
        public virtual TRestrainRepository Constraint<TRestrainRepository>()
            where TRestrainRepository : class, IPrivateRootRepository
        {
            var type = typeof(TRestrainRepository);
            if (!type.IsInterface || typeof(IPrivateRootRepository) == type || type.Name.Equals(nameof(IRepository)) || (type.IsGenericType && type.GetGenericTypeDefinition().Name.Equals(nameof(IRepository))))
            {
                throw new InvalidCastException("Invalid type conversion.");
            }

            return this as TRestrainRepository;
        }
    }
}