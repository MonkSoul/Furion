using Autofac;
using Fur.ApplicationBase;
using Fur.DatabaseAccessor.Extensions.ModelCreating;
using Fur.DatabaseAccessor.Models.Entities;
using Fur.DatabaseAccessor.Models.Filters;
using Fur.DatabaseAccessor.Models.Seed;
using Fur.DatabaseAccessor.Models.Tenants;
using Fur.DatabaseAccessor.Providers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Fur.DatabaseAccessor.Contexts.Status
{
    /// <summary>
    /// 框架自定义DbContext 状态器
    /// <para>作于标记作用。避免 <c>OnConfiguring</c> 和 <c>OnModelCreating</c> 重复初始化</para>
    /// </summary>
    internal static class FurDbContextOfTStatus
    {
        /// <summary>
        /// 是否检查过租户提供器状态
        /// <para>避免重复检查</para>
        /// </summary>
        internal static bool IsResolvedTenantProvider = false;

        /// <summary>
        /// 是否已经调用过 <c>OnConfiguring</c>
        /// <para>默认：<c>false</c></para>
        /// </summary>
        private static bool isCallOnConfiguringed = false;

        /// <summary>
        /// 是否已经调用过 <c>OnModelCreating</c>
        /// <para>默认：<c>false</c></para>
        /// </summary>
        private static bool isCallOnModelCreatinged = false;

        /// <summary>
        /// EF Property属性方法
        /// <para>主要用来反射调用 <c>Property</c> 委托，用于 <c>Int32</c></para>
        /// <para>参见：<see cref="EF.Property"/></para>
        /// </summary>
        internal static MethodInfo EFPropertyGenericInt32Method = typeof(EF).GetMethod(nameof(EF.Property)).MakeGenericMethod(typeof(int));

        #region 检查 OnConfiguring 调用情况 + internal static bool CallOnConfiguringed()

        /// <summary>
        /// 检查 <c>OnConfiguring</c> 调用情况
        /// </summary>
        /// <returns>返回 <c>true</c> 表示已经被调用过了</returns>
        internal static bool CallOnConfiguringed()
        {
            if (!isCallOnConfiguringed)
            {
                isCallOnConfiguringed = true;
                return false;
            }
            return true;
        }

        #endregion 检查 OnConfiguring 调用情况 + internal static bool CallOnConfiguringed()

        #region 检查 OnModelCreating 调用情况 + internal static bool CallOnModelCreatinged()

        /// <summary>
        /// 检查 <c>OnModelCreating</c> 调用情况
        /// </summary>
        /// <returns>返回 <c>true</c> 表示已经被调用过了</returns>
        internal static bool CallOnModelCreatinged()
        {
            if (!isCallOnModelCreatinged)
            {
                isCallOnModelCreatinged = true;
                return false;
            }
            return true;
        }

        #endregion 检查 OnModelCreating 调用情况 + internal static bool CallOnModelCreatinged()

        #region 扫描并配置视图/函数 + internal static void ScanToModelCreating(ModelBuilder modelBuilder, ITenantProvider tenantProvider)

        /// <summary>
        /// 扫描并配置视图/函数
        /// <para>数据库编译实体包括：视图、函数、存储过程</para>
        /// </summary>
        /// <param name="modelBuilder">模型构建器</param>
        /// <param name="tenantIdKey">租户Id的键</param>
        /// <param name="tenantId">租户Id</param>
        internal static void ScanDbObjectsToBuilding(ModelBuilder modelBuilder, ITenantProvider tenantProvider, DbContext dbContext)
        {
            var publicClassType = ApplicationCore.ApplicationWrapper.PublicClassTypeWrappers;

            // 配置种子数据
            var dataSeedTypes = publicClassType.Where(u => u.CanBeNew &&
                                                                                            typeof(IDbEntity).IsAssignableFrom(u.Type) &&
                                                                                            !typeof(DbNoKeyEntity).IsAssignableFrom(u.Type) &&
                                                                                            typeof(IDbDataSeedOfT<>).MakeGenericType(u.Type).IsAssignableFrom(u.Type));
            foreach (var seedType in dataSeedTypes)
            {
                var type = seedType.Type;
                if (type == typeof(Tenant) && tenantProvider == null) continue;

                var entityTypeBuilder = modelBuilder.Entity(type);

                var seedTypeInstance = Activator.CreateInstance(type);
                var hasDataMethod = type.GetMethod(nameof(IDbDataSeedOfT<DbEntity>.HasData));
                var seedData = hasDataMethod.Invoke(seedTypeInstance, null) as IEnumerable<object>;

                entityTypeBuilder.HasData(seedData);
            }

            // 配置无键实体
            var noKeyEntityTypes = publicClassType.Where(u => u.CanBeNew && typeof(DbNoKeyEntity).IsAssignableFrom(u.Type));
            foreach (var noKeyEntityType in noKeyEntityTypes)
            {
                var entityTypeBuilder = modelBuilder.Entity(noKeyEntityType.Type);
                entityTypeBuilder.HasNoKey();

                var noKeyEntityInstance = Activator.CreateInstance(noKeyEntityType.Type) as IDbNoKeyEntity;
                entityTypeBuilder.ToView(noKeyEntityInstance.ObjectName);

                // 租户过滤器
                if (noKeyEntityInstance.HasTenantIdFilter && tenantProvider != null)
                {
                    var lambdaExpression = FurDbContextOfTStatus.CreateHasQueryFilterExpression(noKeyEntityType.Type, nameof(DbEntityBase.TenantId), tenantProvider.GetTenantId());
                    entityTypeBuilder.HasTenantIdQueryFilter(lambdaExpression);
                }
            }

            //配置数据过滤器
            var queryFilterTypes = publicClassType.Where(u => u.CanBeNew &&
                                                                                            typeof(IDbEntity).IsAssignableFrom(u.Type) &&
                                                                                            typeof(IDbQueryFilterOfT<>).MakeGenericType(u.Type).IsAssignableFrom(u.Type));
            var lifetimeScope = dbContext.GetService<ILifetimeScope>();
            foreach (var queryFilterType in queryFilterTypes)
            {
                var type = queryFilterType.Type;
                var entityTypeBuilder = modelBuilder.Entity(type);

                var queryFilterTypeInstance = Activator.CreateInstance(type);
                var hasQueryFilterMethod = type.GetMethod(nameof(IDbQueryFilterOfT<object>.HasQueryFilter));
                var queryFilters = hasQueryFilterMethod.Invoke(queryFilterTypeInstance, new object[] { tenantProvider });
            }

            // 配置数据库函数
            var dbFunctionMethods = ApplicationCore.ApplicationWrapper.PublicMethodWrappers
                .Where(u => u.IsStaticMethod && u.Method.IsDefined(typeof(DbFunctionAttribute)) && u.ThisDeclareType.IsAbstract && u.ThisDeclareType.IsSealed);
            foreach (var dbFunction in dbFunctionMethods)
            {
                modelBuilder.HasDbFunction(dbFunction.Method);
            }
        }

        #endregion 扫描并配置视图/函数 + internal static void ScanToModelCreating(ModelBuilder modelBuilder, ITenantProvider tenantProvider)

        #region 创建 HasQueryFilter 表达式参数 + internal static LambdaExpression CreateHasQueryFilterExpression(Type entityType, string propertyName, int propertyValue)

        /// <summary>
        /// 创建 <c>HasQueryFilter</c> 表达式参数
        /// </summary>
        /// <param name="entityType">实体类型</param>
        /// <param name="propertyName">属性名称</param>
        /// <param name="propertyValue">属性值</param>
        /// <returns><see cref="LambdaExpression"/></returns>
        internal static LambdaExpression CreateHasQueryFilterExpression(Type entityType, string propertyName, int propertyValue)
        {
            var leftParameter = Expression.Parameter(entityType, "e");
            var constantKey = Expression.Constant(propertyName);
            var constantValue = Expression.Constant(propertyValue);

            var expressionBody = Expression.Equal(Expression.Call(EFPropertyGenericInt32Method, leftParameter, constantKey), constantValue);
            return Expression.Lambda(expressionBody, leftParameter);
        }

        #endregion 创建 HasQueryFilter 表达式参数 + internal static LambdaExpression CreateHasQueryFilterExpression(Type entityType, string propertyName, int propertyValue)
    }
}