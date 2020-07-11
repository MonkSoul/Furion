using Fur.DatabaseVisitor.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Fur.DatabaseVisitor.TenantSaaS
{
    /// <summary>
    /// 租户数据库操作上下文
    /// </summary>
    public class FurTenantDbContext : FurDbContextOfT<FurTenantDbContext>
    {
        #region 构造函数 + public FurTenantDbContext(DbContextOptions<FurTenantDbContext> options) : base(options)
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="options">数据库上下文配置选项</param>
        public FurTenantDbContext(DbContextOptions<FurTenantDbContext> options) : base(options)
        {
        }
        #endregion

        #region DbContext上下文初始化配置时调用的方法 + protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        /// <summary>
        /// DbContext上下文初始化配置时调用的方法
        /// <para>可在这里配置数据库连接字符串，数据库提供器等</para>
        /// </summary>
        /// <param name="optionsBuilder">DbContext上下文配置选项构建器</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        #endregion

        #region DbContext 上下文初始化模型时调用的方法 + protected override void OnModelCreating(ModelBuilder modelBuilder)
        /// <summary>
        /// DbContext 上下文初始化模型时调用的方法
        /// </summary>
        /// <param name="modelBuilder">DbContext 上下文模型构建器</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        #endregion
    }
}