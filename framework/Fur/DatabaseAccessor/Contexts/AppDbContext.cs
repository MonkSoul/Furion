// -----------------------------------------------------------------------------
// Fur 是 .NET 5 平台下企业应用开发最佳实践框架。
// Copyright © 2020 Fur, Baiqian Co.,Ltd.
//
// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0-rc.final.17
// 官方网站：https://chinadot.net
// 源码地址：Gitee：https://gitee.com/monksoul/Fur
// 				    Github：https://github.com/monksoul/Fur
// 开源协议：Apache-2.0（http://www.apache.org/licenses/LICENSE-2.0）
// -----------------------------------------------------------------------------

using Fur.DependencyInjection;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Fur.DatabaseAccessor
{
    /// <summary>
    /// 默认应用数据库上下文
    /// </summary>
    /// <typeparam name="TDbContext">数据库上下文</typeparam>
    [SkipScan]
    public abstract class AppDbContext<TDbContext> : AppDbContext<TDbContext, MasterDbContextLocator>
        where TDbContext : DbContext
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="options"></param>
        public AppDbContext(DbContextOptions<TDbContext> options) : base(options)
        {
        }
    }

    /// <summary>
    /// 应用数据库上下文
    /// </summary>
    /// <typeparam name="TDbContext">数据库上下文</typeparam>
    /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
    [SkipScan]
    public abstract class AppDbContext<TDbContext, TDbContextLocator> : DbContext
        where TDbContext : DbContext
        where TDbContextLocator : class, IDbContextLocator
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="options"></param>
        public AppDbContext(DbContextOptions<TDbContext> options) : base(options)
        {
            // 定义数据库上下文提交更改事件
            this.SavingChanges += SavingChangesEvent;
            this.SavedChanges += SavedChangesEvent;
            this.SaveChangesFailed += SaveChangesFailedEvent;
        }

        /// <summary>
        /// 数据库上下文提交更改之前执行事件
        /// </summary>
        /// <param name="sender">事件对象</param>
        /// <param name="e">事件参数</param>
        protected virtual void SavingChangesEvent(object sender, SavingChangesEventArgs e)
        {
        }

        /// <summary>
        /// 数据库上下文提交更改成功执行事件
        /// </summary>
        /// <param name="sender">事件对象</param>
        /// <param name="e">事件参数</param>
        protected virtual void SavedChangesEvent(object sender, SavedChangesEventArgs e)
        {
        }

        /// <summary>
        /// 数据库上下文提交更改失败执行事件
        /// </summary>
        /// <param name="sender">事件对象</param>
        /// <param name="e">事件参数</param>
        protected virtual void SaveChangesFailedEvent(object sender, SaveChangesFailedEventArgs e)
        {
        }

        /// <summary>
        /// 数据库上下文初始化调用方法
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        /// <summary>
        /// 数据库上下文配置模型调用方法
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 配置数据库上下文实体
            AppDbContextBuilder.ConfigureDbContextEntity(modelBuilder, this, typeof(TDbContextLocator));
        }

        /// <summary>
        /// 获取租户信息
        /// </summary>
        public virtual Tenant Tenant
        {
            get
            {
                // 如果没有实现多租户方式，则无需查询
                if (!typeof(IPrivateMultiTenant).IsAssignableFrom(GetType())) return default;

                // 判断 HttpContext 是否存在
                var httpContextAccessor = App.GetService<IHttpContextAccessor>();
                if (httpContextAccessor == null || httpContextAccessor.HttpContext == null) return default;

                // 获取主机地址
                var host = httpContextAccessor.HttpContext.Request.Host.Value;

                // 从内存缓存中读取或查询数据库
                var memoryCache = App.GetRequestService<IMemoryCache>();
                return memoryCache.GetOrCreate($"{host}:MultiTenants", cache =>
                {
                    // 读取数据库
                    var tenantDbContext = Db.GetDbContext<MultiTenantDbContextLocator>();
                    return tenantDbContext.Set<Tenant>().FirstOrDefault(u => u.Host == host);
                });
            }
        }

        /// <summary>
        /// 构建租户Id 查询过滤器表达式
        /// </summary>
        /// <param name="entityBuilder">实体类型构建器</param>
        /// <param name="tenantId">租户Id</param>
        /// <returns>表达式</returns>
        protected virtual LambdaExpression TenantIdQueryFilterExpression(EntityTypeBuilder entityBuilder, Guid tenantId)
        {
            // 获取实体构建器元数据
            var metadata = entityBuilder.Metadata;
            if (metadata.FindProperty(nameof(Entity.TenantId)) == null) return default;

            // 创建表达式元素
            var parameter = Expression.Parameter(metadata.ClrType, "u");
            var properyName = Expression.Constant(nameof(Entity.TenantId));
            var propertyValue = Expression.Constant(tenantId);

            var expressionBody = Expression.Equal(Expression.Call(typeof(EF), nameof(EF.Property), new[] { typeof(Guid) }, parameter, properyName), propertyValue);
            var expression = Expression.Lambda(expressionBody, parameter);
            return expression;
        }
    }
}