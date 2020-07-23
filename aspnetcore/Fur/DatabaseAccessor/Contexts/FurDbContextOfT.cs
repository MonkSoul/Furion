using Autofac;
using Fur.DatabaseAccessor.Contexts.Status;
using Fur.DatabaseAccessor.Providers;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Fur.DatabaseAccessor.Contexts
{
    /// <summary>
    /// Fur 数据库上下文
    /// <para>抽象类，不能实例化</para>
    /// <para>区别于 EF Core 提供的 <see cref="DbContext"/>，<see cref="FurDbContextOfT{TDbContext}"/> 数据库上下文默认继承 <see cref="DbContext"/>，但在此基础上拓展了租户模式，数据库视图自动注册 <see cref="DbSet{TEntity}"/>等功能</para>
    /// <para>建议所有数据库上下文都应继承 <see cref="FurDbContextOfT{TDbContext}"/></para>
    /// </summary>
    /// <typeparam name="TDbContext"><see cref="DbContext"/> 衍生类</typeparam>
    public abstract class FurDbContextOfT<TDbContext> : DbContext
        where TDbContext : DbContext
    {
        #region 构造函数 + public FurDbContextOfT(DbContextOptions<TDbContext> options)
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="options">数据库上下文选择配置，<see cref="DbContextOptions{TContext}"/></param>
        public FurDbContextOfT(DbContextOptions<TDbContext> options)
            : base(options)
        {
        }
        #endregion

        #region 数据库上下文初始化调用方法 + protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        /// <summary>
        /// 数据库上下文初始化调用方法
        /// <para>通常配置数据库连接字符串，数据库类型等等</para>
        /// </summary>
        /// <param name="optionsBuilder">数据库上下文选项配置构建器，参见：<see cref="DbContextOptionsBuilder"/></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            // 数据库上下文状态器用来避免子类多次调用该方法进行初始化
            if (FurDbContextOfTStatus.CallOnConfiguringed()) return;

            // 如需添加其他配置，应写在以下位置
        }
        #endregion

        #region 数据库上下文创建模型调用方法 + protected override void OnModelCreating(ModelBuilder modelBuilder)
        /// <summary>
        /// 数据库上下文创建模型调用方法
        /// </summary>
        /// <param name="modelBuilder">模型构建器，参见：<see cref="ModelBuilder"/></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // 数据库上下文状态器用来避免子类多次调用该方法进行初始化
            if (FurDbContextOfTStatus.CallOnModelCreatinged()) return;

            // 扫描数据库对象类型加入模型构建器中，其中包括视图、存储过程、函数（标量函数/表值函数）
            FurDbContextOfTStatus.ScanDbObjectsToBuilding(modelBuilder, TenantProvider, this);

            // 如需添加其他配置，应写在以下位置
        }
        #endregion

        /// <summary>
        /// 租户提供器
        /// </summary>
        private ITenantProvider _tenantProvider;
        public ITenantProvider TenantProvider
        {
            get
            {
                // 判断是否解析过租户提供器，如果解析过，则跳过，主要是避免子类重复解析
                if (!FurDbContextOfTStatus.IsResolvedTenantProvider)
                {
                    FurDbContextOfTStatus.IsResolvedTenantProvider = true;

                    var lifetimeScope = this.GetService<ILifetimeScope>();
                    if (!lifetimeScope.IsRegistered<ITenantProvider>()) return _tenantProvider = null;

                    _tenantProvider = lifetimeScope.Resolve<ITenantProvider>();
                }
                return _tenantProvider;
            }
        }
    }
}