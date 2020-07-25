using Fur.DatabaseAccessor.Contexts;
using Fur.DatabaseAccessor.MultipleTenants.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Fur.DatabaseAccessor.MultipleTenants
{
    /// <summary>
    /// 多租户数据库上下文
    /// </summary>
    public class FurMultipleTenantDbContext : FurDbContextOfT<FurMultipleTenantDbContext, FurMultipleTenantDbContextIdentifier>
    {
        #region 构造函数 + public FurMultiTenantDbContext(DbContextOptions<FurMultiTenantDbContext> options)
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="options">数据库上下文选项配置，<see cref="DbContextOptions{TContext}"/></param>
        public FurMultipleTenantDbContext(DbContextOptions<FurMultipleTenantDbContext> options)
            : base(options)
        {
        }
        #endregion

        /// <summary>
        /// 租户实体信息
        /// </summary>
        public virtual DbSet<Tenant> Tenants { get; set; }

        #region 数据库上下文初始化调用方法 + protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        /// <summary>
        /// 数据库上下文初始化调用方法
        /// <para>通常配置数据库连接字符串，数据库类型等等</para>
        /// </summary>
        /// <param name="optionsBuilder">数据库上下文选项配置构建器，参见：<see cref="DbContextOptionsBuilder"/></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
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
        }
        #endregion

        #region 获取租户Id + public virtual int GetTenantId(string host)
        /// <summary>
        /// 获取租户Id
        /// </summary>
        /// <param name="host">请求来源主机地址</param>
        /// <returns>租户Id</returns>
        public virtual int GetTenantId(string host)
        {
            var tenant = Tenants.FirstOrDefault(t => t.Host == host);
            return tenant?.Id ?? 0;
        }
        #endregion
    }
}