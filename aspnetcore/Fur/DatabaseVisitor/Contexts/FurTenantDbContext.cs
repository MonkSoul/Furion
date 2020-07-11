using Fur.DatabaseVisitor.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Fur.DatabaseVisitor.Contexts
{
    /// <summary>
    /// 租户数据库操作上下文
    /// </summary>
    public class FurTenantDbContext : FurDbContextOfT<FurTenantDbContext>
    {
        /// <summary>
        /// 租户实体表
        /// <para>框架默认启用了租户模式。参见：<see cref="Tenant"/></para>
        /// </summary>
        public virtual DbSet<Tenant> Tenants { get; set; }

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

        #region DbContext 上下文获取租户Id + public virtual int GetTenantId(string host)
        /// <summary>
        /// DbContext 上下文获取租户Id
        /// </summary>
        /// <param name="host">主机地址</param>
        /// <returns>int</returns>
        public virtual int GetTenantId(string host)
        {
            var tenant = Tenants.FirstOrDefault(t => t.Host == host);
            return tenant?.Id ?? 0;
        }
        #endregion
    }
}