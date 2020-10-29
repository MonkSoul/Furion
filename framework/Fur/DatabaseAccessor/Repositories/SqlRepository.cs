using Fur.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Fur.DatabaseAccessor
{
    /// <summary>
    /// Sql 操作仓储实现
    /// </summary>
    [SkipScan]
    public partial class SqlRepository : SqlRepository<MasterDbContextLocator>, ISqlRepository
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbContextResolve">数据库上下文解析器</param>
        /// <param name="dbContextPool">数据库上下文池</param>
        /// <param name="serviceProvider">服务提供器</param>
        public SqlRepository(
            Func<Type, IScoped, DbContext> dbContextResolve
            , IDbContextPool dbContextPool
            , IServiceProvider serviceProvider) : base(dbContextResolve, dbContextPool, serviceProvider)
        {
        }
    }

    /// <summary>
    /// Sql 操作仓储实现
    /// </summary>
    [SkipScan]
    public partial class SqlRepository<TDbContextLocator> : ISqlRepository<TDbContextLocator>
        where TDbContextLocator : class, IDbContextLocator
    {
        /// <summary>
        /// 服务提供器
        /// </summary>
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbContextResolve">数据库上下文解析器</param>
        /// <param name="dbContextPool">数据库上下文池</param>
        /// <param name="serviceProvider">服务提供器</param>
        public SqlRepository(
            Func<Type, IScoped, DbContext> dbContextResolve
            , IDbContextPool dbContextPool
            , IServiceProvider serviceProvider)
        {
            // 解析数据库上下文
            var dbContext = dbContextResolve(typeof(TDbContextLocator), default);

            // 保存当前数据库上下文到池中
            dbContextPool.AddToPool(dbContext);

            // 初始化数据库相关数据
            Database = dbContext.Database;

            _serviceProvider = serviceProvider;
        }

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
        {
            return _serviceProvider.GetService<TService>();
        }

        /// <summary>
        /// 解析服务
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <returns></returns>
        public virtual TService GetRequiredService<TService>()
        {
            return _serviceProvider.GetRequiredService<TService>();
        }
    }
}