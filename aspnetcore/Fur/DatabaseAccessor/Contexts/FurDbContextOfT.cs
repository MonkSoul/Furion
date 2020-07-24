using Autofac;
using Fur.ApplicationBase.Attributes;
using Fur.DatabaseAccessor.Contexts.Staters;
using Fur.DatabaseAccessor.Identifiers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Fur.DatabaseAccessor.Contexts
{
    /// <summary>
    /// Fur 数据库上下文
    /// <para>抽象类，不能实例化</para>
    /// <para>区别于 EF Core 提供的 <see cref="DbContext"/>，<see cref="FurDbContextOfT{TDbContext, TDbContextIdentifier}"/> 数据库上下文默认继承 <see cref="DbContext"/>，但在此基础上拓展了租户模式，数据库对象类型自动注册 <see cref="DbSet{TEntity}"/>等功能</para>
    /// <para>建议所有数据库上下文都应继承 <see cref="FurDbContextOfT{TDbContext}"/></para>
    /// </summary>
    /// <typeparam name="TDbContext"><see cref="DbContext"/> 衍生类</typeparam>
    /// <typeparam name="TDbContextIdentifier">数据库上下文标识器</typeparam>
    [NonWrapper]
    public abstract class FurDbContextOfT<TDbContext, TDbContextIdentifier> : DbContext
        where TDbContext : DbContext
        where TDbContextIdentifier : IDbContextIdentifier
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

            // 如需添加其他配置，应写在以下位置，但是要注意基类多次调用问题，建议通过 TDbContextIdentifier 来区分当前数据库上下文
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

            // 扫描数据库对象类型加入模型构建器中，包括视图、存储过程、函数（标量函数/表值函数）初始化、及种子数据、查询筛选器配置
            FurDbContextOfTStater.ScanDbObjectsToBuilding(modelBuilder, typeof(TDbContextIdentifier), this.GetService<ILifetimeScope>());

            // 如需添加其他配置，应写在以下位置，但是要注意基类多次调用问题，建议通过 TDbContextIdentifier 来区分当前数据库上下文
        }
        #endregion
    }
}