// -----------------------------------------------------------------------------
// Fur 是 .NET 5 平台下极易入门、极速开发的 Web 应用框架。
// Copyright © 2020 Fur, Baiqian Co.,Ltd.
//
// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0
// 源码地址：https://gitee.com/monksoul/Fur
// 开源协议：Apache-2.0（http://www.apache.org/licenses/LICENSE-2.0）
// -----------------------------------------------------------------------------

using Fur.DependencyInjection;
using Fur.Extensions;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Fur.DatabaseAccessor
{
    /// <summary>
    /// 数据库上下文构建器
    /// </summary>
    [NonBeScan]
    internal static class AppDbContextBuilder
    {
        /// <summary>
        /// 数据库实体相关类型
        /// </summary>
        private static readonly List<Type> EntityCorrelationTypes;

        /// <summary>
        /// 数据库函数方法集合
        /// </summary>
        private static readonly List<MethodInfo> DbFunctionMethods;

        /// <summary>
        /// 模型构建器 Entity<TEntity> 方法 <see cref="ModelBuilder.Entity{TEntity}"/>
        /// </summary>
        private static readonly MethodInfo ModelBuildEntityMethod;

        /// <summary>
        /// 数据库配置
        /// </summary>
        private static readonly DatabaseAccessorSettingsOptions databaseAccessorSettings;

        /// <summary>
        /// 构造函数
        /// </summary>
        static AppDbContextBuilder()
        {
            // 扫描程序集，获取数据库实体相关类型
            EntityCorrelationTypes = App.CanBeScanTypes.Where(t => (typeof(IEntity).IsAssignableFrom(t) || typeof(IModelBuilder).IsAssignableFrom(t))
                && t.IsClass && !t.IsAbstract && !t.IsGenericType && !t.IsInterface && !t.IsDefined(typeof(NonAutomaticAttribute), true))
                .ToList();

            if (EntityCorrelationTypes.Count > 0)
            {
                // 获取模型构建器 Entity<T> 方法
                ModelBuildEntityMethod = typeof(ModelBuilder).GetMethods(BindingFlags.Public | BindingFlags.Instance).FirstOrDefault(u => u.Name == nameof(ModelBuilder.Entity) && u.GetParameters().Length == 0);

                // 加载数据库配置
                databaseAccessorSettings = App.GetOptions<DatabaseAccessorSettingsOptions>();
            }

            // 查找所有数据库函数，必须是公开静态方法，且所在父类也必须是公开静态方法
            DbFunctionMethods = App.CanBeScanTypes
                .Where(t => t.IsAbstract && t.IsSealed && t.IsClass && !t.IsDefined(typeof(NonAutomaticAttribute), true))
                .SelectMany(t => t.GetMethods(BindingFlags.Public | BindingFlags.Static).Where(m => !m.IsDefined(typeof(NonBeScanAttribute), false) && m.IsDefined(typeof(QueryableFunctionAttribute), true))).ToList();
        }

        /// <summary>
        /// 配置数据库上下文实体
        /// </summary>
        /// <param name="modelBuilder">模型构建器</param>
        /// <param name="dbContext">数据库上下文</param>
        /// <param name="dbContextLocator">数据库上下文定位器</param>
        internal static void ConfigureDbContextEntity(ModelBuilder modelBuilder, DbContext dbContext, Type dbContextLocator)
        {
            // 获取当前数据库上下文关联的类型
            var dbContextCorrelationType = GetDbContextCorrelationType(dbContextLocator);

            // 如果没有数据，则跳过
            if (!dbContextCorrelationType.EntityTypes.Any()) goto EntityFunctions;

            // 初始化所有类型
            foreach (var entityType in dbContextCorrelationType.EntityTypes)
            {
                // 创建实体类型
                var entityBuilder = CreateEntityTypeBuilder(entityType, modelBuilder);
                if (entityBuilder == null) continue;

                // 配置无键实体构建器
                ConfigureEntityNoKeyType(entityType, entityBuilder, dbContextCorrelationType.EntityNoKeyTypes);

                // 实体构建成功注入拦截
                LoadModelBuilderOnCreating(entityBuilder, dbContext, dbContextLocator, dbContextCorrelationType.ModelBuilderFilterInstances);

                // 配置数据库实体类型构建器
                ConfigureEntityTypeBuilder(entityType, entityBuilder, dbContext, dbContextLocator, dbContextCorrelationType);

                // 配置数据库实体种子数据
                ConfigureEntitySeedData(entityType, entityBuilder, dbContext, dbContextLocator, dbContextCorrelationType);

                // 实体完成配置注入拦截
                LoadModelBuilderOnCreated(entityBuilder, dbContext, dbContextLocator, dbContextCorrelationType.ModelBuilderFilterInstances);
            }

        // 配置数据库函数
        EntityFunctions: ConfigureDbFunctions(modelBuilder, dbContextLocator);
        }

        /// <summary>
        /// 创建实体类型构建器
        /// </summary>
        /// <param name="type">数据库关联类型</param>
        /// <param name="modelBuilder">模型构建器</param>
        /// <returns>EntityTypeBuilder</returns>
        private static EntityTypeBuilder CreateEntityTypeBuilder(Type type, ModelBuilder modelBuilder)
        {
            // 判断是否启用多租户支持
            var enabledMultiTenant = !(databaseAccessorSettings.EnabledMultiTenant == false || databaseAccessorSettings.MultiTenantOptions == MultiTenantPattern.None);
            if (type == typeof(Tenant) && !enabledMultiTenant) return default;

            // 反射创建实体类型构建器
            var entityTypeBuilder = ModelBuildEntityMethod.MakeGenericMethod(type).Invoke(modelBuilder, null) as EntityTypeBuilder;

            // 如果未启用多租户支持，则忽略
            if (!enabledMultiTenant)
            {
                entityTypeBuilder.Ignore(nameof(Tenant.TenantId));
            }

            return entityTypeBuilder;
        }

        /// <summary>
        /// 配置无键实体类型
        /// </summary>
        /// <param name="entityType">实体类型</param>
        /// <param name="entityBuilder">实体类型构建器</param>
        /// <param name="EntityNoKeyTypes">无键实体列表</param>
        private static void ConfigureEntityNoKeyType(Type entityType, EntityTypeBuilder entityBuilder, List<Type> EntityNoKeyTypes)
        {
            if (!EntityNoKeyTypes.Contains(entityType)) return;

            // 配置视图、存储过程、函数无键实体
            entityBuilder.HasNoKey();
            entityBuilder.ToView((Activator.CreateInstance(entityType) as IEntityNotKey).DEFINED_NAME);
        }

        /// <summary>
        /// 加载模型构建筛选器创建之前拦截
        /// </summary>
        /// <param name="entityBuilder">实体类型构建器</param>
        /// <param name="dbContext">数据库上下文</param>
        /// <param name="dbContextLocator">数据库上下文定位器</param>
        /// <param name="modelBuilderFilterInstances">模型构建器筛选器实例</param>
        private static void LoadModelBuilderOnCreating(EntityTypeBuilder entityBuilder, DbContext dbContext, Type dbContextLocator, List<IModelBuilderFilterDependency> modelBuilderFilterInstances)
        {
            if (modelBuilderFilterInstances.Count == 0) return;

            // 创建过滤器
            foreach (var filterInstance in modelBuilderFilterInstances)
            {
                // 执行构建之后
                filterInstance.OnCreating(entityBuilder, dbContext, dbContextLocator);
            }
        }

        /// <summary>
        /// 加载模型构建筛选器创建之后拦截
        /// </summary>
        /// <param name="entityBuilder">实体类型构建器</param>
        /// <param name="dbContext">数据库上下文</param>
        /// <param name="dbContextLocator">数据库上下文定位器</param>
        /// <param name="modelBuilderFilterInstances">模型构建器筛选器实例</param>
        private static void LoadModelBuilderOnCreated(EntityTypeBuilder entityBuilder, DbContext dbContext, Type dbContextLocator, List<IModelBuilderFilterDependency> modelBuilderFilterInstances)
        {
            if (modelBuilderFilterInstances.Count == 0) return;

            // 创建过滤器
            foreach (var filterInstance in modelBuilderFilterInstances)
            {
                // 执行构建之后
                filterInstance.OnCreated(entityBuilder, dbContext, dbContextLocator);
            }
        }

        /// <summary>
        /// 配置数据库实体类型构建器
        /// </summary>
        /// <param name="entityType">实体类型</param>
        /// <param name="entityBuilder">实体类型构建器</param>
        /// <param name="dbContext">数据库上下文</param>
        /// <param name="dbContextLocator">数据库上下文定位器</param>
        /// <param name="dbContextCorrelationType">数据库实体关联类型</param>
        private static void ConfigureEntityTypeBuilder(Type entityType, EntityTypeBuilder entityBuilder, DbContext dbContext, Type dbContextLocator, DbContextCorrelationType dbContextCorrelationType)
        {
            // 获取该实体类型的配置类型
            var entityTypeBuilderTypes = dbContextCorrelationType.EntityTypeBuilderTypes
                .Where(u => u.GetInterfaces()
                    .Any(i => i.HasImplementedRawGeneric(typeof(IEntityTypeBuilderDependency<>)) && i.GenericTypeArguments.Contains(entityType)));

            if (!entityTypeBuilderTypes.Any()) return;

            // 调用数据库实体自定义配置
            foreach (var entityTypeBuilderType in entityTypeBuilderTypes)
            {
                var instance = Activator.CreateInstance(entityTypeBuilderType);
                var configureMethod = entityTypeBuilderType.GetMethod("Configure");
                configureMethod.Invoke(instance, new object[] { entityBuilder, dbContext, dbContextLocator });
            }
        }

        /// <summary>
        /// 配置数据库实体种子数据
        /// </summary>
        /// <param name="entityType">实体类型</param>
        /// <param name="entityBuilder">实体类型构建器</param>
        /// <param name="dbContext">数据库上下文</param>
        /// <param name="dbContextLocator">数据库上下文定位器</param>
        /// <param name="dbContextCorrelationType">数据库实体关联类型</param>
        private static void ConfigureEntitySeedData(Type entityType, EntityTypeBuilder entityBuilder, DbContext dbContext, Type dbContextLocator, DbContextCorrelationType dbContextCorrelationType)
        {
            // 获取该实体类型的种子配置
            var entitySeedDataTypes = dbContextCorrelationType.EntitySeedDataTypes
                .Where(u => u.GetInterfaces()
                    .Any(i => i.HasImplementedRawGeneric(typeof(IEntitySeedDataDependency<>)) && i.GenericTypeArguments.Contains(entityType)));

            if (!entitySeedDataTypes.Any()) return;

            var datas = new List<object>();

            // 调用数据库实体自定义配置
            foreach (var entitySeedDataType in entitySeedDataTypes)
            {
                var instance = Activator.CreateInstance(entitySeedDataType);
                var hasDataMethod = entitySeedDataType.GetMethod("HasData");
                datas.AddRange(hasDataMethod.Invoke(instance, new object[] { dbContext, dbContextLocator }).Adapt<IEnumerable<object>>());
            }

            entityBuilder.HasData(datas.ToArray());
        }

        /// <summary>
        /// 配置数据库函数
        /// </summary>
        /// <param name="modelBuilder">模型构建起</param>
        /// <param name="dbContextLocator">数据库上下文定位器</param>
        private static void ConfigureDbFunctions(ModelBuilder modelBuilder, Type dbContextLocator)
        {
            var dbContextFunctionMethods = DbFunctionMethods.Where(u => IsInThisDbContext(dbContextLocator, u));
            if (!dbContextFunctionMethods.Any()) return;

            foreach (var method in dbContextFunctionMethods)
            {
                modelBuilder.HasDbFunction(method);
            }
        }

        /// <summary>
        /// 判断当前类型是否在数据库上下文中
        /// </summary>
        /// <param name="dbContextLocator">数据库上下文定位器</param>
        /// <param name="entityCorrelationType">数据库实体关联类型</param>
        /// <returns>bool</returns>
        private static bool IsInThisDbContext(Type dbContextLocator, Type entityCorrelationType)
        {
            // 获取类型父类型及接口
            var baseType = entityCorrelationType.BaseType;
            var interfaces = entityCorrelationType.GetInterfaces().Where(u => typeof(IModelBuilder).IsAssignableFrom(u));

            // 默认数据库上下文情况
            if (dbContextLocator == typeof(DbContextLocator))
            {
                // 父类继承 IEntity 类型且不是泛型
                if (typeof(IEntity).IsAssignableFrom(baseType) && !baseType.IsGenericType) return true;

                // 接口等于 IModelBuilderFilter 类型或 只有一个泛型参数
                if (interfaces.Any(u => u == typeof(IModelBuilderFilter) || (u.IsGenericType && u.GenericTypeArguments.Length == 1))) return true;
            }

            // 父类是泛型且泛型参数包含数据库上下文定位器
            if (baseType.IsGenericType && baseType.GenericTypeArguments.Contains(dbContextLocator)) return true;

            // 接口是泛型且泛型参数包含数据库上下文定位器
            if (interfaces.Any(u => u.IsGenericType && u.GenericTypeArguments.Contains(dbContextLocator))) return true;

            return false;
        }

        /// <summary>
        /// 判断当前函数是否在数据库上下文中
        /// </summary>
        /// <param name="dbContextLocator">数据库上下文定位器</param>
        /// <param name="method">标识为数据库的函数</param>
        /// <returns>bool</returns>
        private static bool IsInThisDbContext(Type dbContextLocator, MethodInfo method)
        {
            var queryableFunctionAttribute = method.GetCustomAttribute<QueryableFunctionAttribute>(true);

            // 如果数据库上下文定位器为默认定位器且该函数没有定义数据库上下文定位器，则返回 true
            if (dbContextLocator == typeof(DbContextLocator) && queryableFunctionAttribute.DbContextLocators.Length == 0) return true;

            // 判断是否保护
            if (queryableFunctionAttribute.DbContextLocators.Contains(dbContextLocator)) return true;

            return false;
        }

        /// <summary>
        /// 获取当前数据库上下文关联类型
        /// </summary>
        /// <param name="dbContextLocator">数据库上下文定位器</param>
        /// <returns>DbContextCorrelationType</returns>
        private static DbContextCorrelationType GetDbContextCorrelationType(Type dbContextLocator)
        {
            var result = new DbContextCorrelationType { DbContextLocator = dbContextLocator };

            // 获取当前数据库上下文关联类型
            var dbContextEntityCorrelationTypes = EntityCorrelationTypes.Where(u => IsInThisDbContext(dbContextLocator, u));

            // 组装对象
            foreach (var entityCorrelationType in dbContextEntityCorrelationTypes)
            {
                // 只要继承 IEntity 接口，都是实体
                if (typeof(IEntity).IsAssignableFrom(entityCorrelationType))
                {
                    // 添加实体
                    result.EntityTypes.Add(entityCorrelationType);

                    // 添加无键实体
                    if (typeof(IEntityNotKey).IsAssignableFrom(entityCorrelationType))
                    {
                        result.EntityNoKeyTypes.Add(entityCorrelationType);
                    }
                }
                else if (typeof(IModelBuilder).IsAssignableFrom(entityCorrelationType))
                {
                    // 添加模型构建器
                    if (entityCorrelationType.HasImplementedRawGeneric(typeof(IEntityTypeBuilderDependency<>)))
                    {
                        result.EntityTypeBuilderTypes.Add(entityCorrelationType);
                    }

                    // 添加全局筛选器
                    if (entityCorrelationType.HasImplementedRawGeneric(typeof(IModelBuilderFilterDependency)))
                    {
                        result.ModelBuilderFilterTypes.Add(entityCorrelationType);
                        result.ModelBuilderFilterInstances.Add(Activator.CreateInstance(entityCorrelationType) as IModelBuilderFilterDependency);
                    }

                    // 添加种子数据
                    if (entityCorrelationType.HasImplementedRawGeneric(typeof(IEntitySeedDataDependency<>)))
                    {
                        result.EntitySeedDataTypes.Add(entityCorrelationType);
                    }
                }
            }

            return result;
        }
    }
}