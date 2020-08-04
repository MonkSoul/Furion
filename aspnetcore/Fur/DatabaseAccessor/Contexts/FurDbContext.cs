using Fur.AppCore;
using Fur.AppCore.Attributes;
using Fur.DatabaseAccessor.Contexts.Builders;
using Fur.DatabaseAccessor.Contexts.Locators;
using Microsoft.EntityFrameworkCore;

namespace Fur.DatabaseAccessor.Contexts
{
    /// <summary>
    /// Fur 数据库上下文
    /// </summary>
    /// <remarks>
    /// <para>区别于 EF Core 提供的 <see cref="DbContext"/>，<see cref="FurDbContext{TDbContext, TDbContextLocator}"/> 数据库上下文默认继承 <see cref="DbContext"/>，但在此基础上拓展了租户模式，数据库对象类型自动注册 <see cref="DbSet{TEntity}"/>等功能</para>
    /// <para>建议所有数据库上下文都应继承 <see cref="FurDbContext{TDbContext}"/></para>
    /// </remarks>
    /// <typeparam name="TDbContext"><see cref="DbContext"/> 衍生类</typeparam>
    /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
    [NonInflated]
    public abstract class FurDbContext<TDbContext, TDbContextLocator> : DbContext
        where TDbContext : DbContext
        where TDbContextLocator : IDbContextLocator
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="options">数据库上下文选项配置，<see cref="DbContextOptions{TContext}"/></param>
        public FurDbContext(DbContextOptions<TDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// 数据库上下文初始化调用方法
        /// </summary>
        /// <remarks>
        /// <para>通常配置数据库连接字符串，数据库类型等等</para>
        /// </remarks>
        /// <param name="optionsBuilder">数据库上下文选项配置构建器，参见：<see cref="DbContextOptionsBuilder"/></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            // 如需添加其他配置，应写在以下位置，但是要注意基类多次调用问题，建议通过 TDbContextLocator 来区分当前数据库上下文
        }

        /// <summary>
        /// 数据库上下文创建模型调用方法
        /// </summary>
        /// <param name="modelBuilder">模型构建器，参见：<see cref="ModelBuilder"/></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            if (!App.AppOptions.AutoConfigureDbObjects) return;

            // 扫描数据库对象类型加入模型构建器中，包括视图、存储过程、函数（标量函数/表值函数）初始化、及种子数据、查询筛选器配置
            FurDbContextBuilder.ScanDbObjectsToBuilding(modelBuilder, typeof(TDbContextLocator), this);

            // 如需添加其他配置，应写在以下位置，但是要注意基类多次调用问题，建议通过 TDbContextLocator 来区分当前数据库上下文
        }
    }
}