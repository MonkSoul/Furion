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
        /// 构造函数
        /// </summary>
        static AppDbContextBuilder()
        {
            // 扫描程序集，获取数据库实体相关类型
            EntityCorrelationTypes = App.Assemblies.SelectMany(u => u.GetTypes()
                 .Where(t => t.IsPublic && t.IsClass && !t.IsAbstract && !t.IsGenericType && !t.IsInterface
                     && (typeof(IEntity).IsAssignableFrom(t) || typeof(IModelBuilder).IsAssignableFrom(t))))
                .ToList();

            if (EntityCorrelationTypes.Count > 0)
            {
                // 获取模型构建器 Entity<T> 方法
                ModelBuildEntityMethod = typeof(ModelBuilder).GetMethods(BindingFlags.Public | BindingFlags.Instance).FirstOrDefault(u => u.Name == nameof(ModelBuilder.Entity) && u.GetParameters().Length == 0);
            }
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

            if (dbContextCorrelationType.EntityTypes.Count == 0) return;

            // 初始化所有类型
            foreach (var entityType in dbContextCorrelationType.EntityTypes)
            {
                EntityTypeBuilder entityBuilder = null;
                // 创建实体类型
                CreateEntityTypeBuilder(entityType, modelBuilder, ref entityBuilder);
            }
        }

        /// <summary>
        /// 创建实体类型构建器
        /// </summary>
        /// <param name="type">数据库关联类型</param>
        /// <param name="modelBuilder">模型构建器</param>
        /// <param name="entityBuilder">实体类型构建器</param>
        private static void CreateEntityTypeBuilder(Type type, ModelBuilder modelBuilder, ref EntityTypeBuilder entityBuilder)
        {
            // 如果实体类型构建器不为空，则跳过
            if (entityBuilder != null) return;

            // 如果类型不是实体类型，则跳过
            if (!typeof(IEntity).IsAssignableFrom(type)) return;

            // 反射创建实体类型构建器
            entityBuilder = ModelBuildEntityMethod.MakeGenericMethod(type).Invoke(modelBuilder, null) as EntityTypeBuilder;
        }

        /// <summary>
        /// 判断当前类型是否在数据库上下文中
        /// </summary>
        /// <param name="dbContextLocator"></param>
        /// <param name="entityCorrelationType"></param>
        /// <returns></returns>
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
                    if (typeof(IEntitySeedDataDependency<>).IsAssignableFrom(entityCorrelationType))
                    {
                        result.EntityTypeBuilderTypes.Add(entityCorrelationType);
                    }

                    // 添加全局筛选器
                    if (typeof(IModelBuilderFilterDependency).IsAssignableFrom(entityCorrelationType))
                    {
                        result.ModelBuilderFilterTypes.Add(entityCorrelationType);
                    }

                    // 添加种子数据
                    if (typeof(IEntitySeedDataDependency<>).IsAssignableFrom(entityCorrelationType))
                    {
                        result.EntitySeedDataTypes.Add(entityCorrelationType);
                    }
                }
            }

            return result;
        }
    }
}