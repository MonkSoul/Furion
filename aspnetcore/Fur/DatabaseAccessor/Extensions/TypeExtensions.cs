using Fur.AppBasic.Attributes;
using Fur.DatabaseAccessor.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Fur.DatabaseAccessor.Extensions
{
    /// <summary>
    /// 类型拓展类
    /// </summary>
    [NonWrapper]
    public static class TypeExtensions
    {
        /// <summary>
        /// 获取类型的父类型或接口类型泛型参数
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="filterType">特定类型</param>
        /// <param name="genericArgumentSourceOptions">泛型参数来源，参见：<see cref="GenericArgumentSourceOptions"/></param>
        /// <returns></returns>
        internal static Type[] GetTypeGenericArguments(this Type type, Type filterType, GenericArgumentSourceOptions genericArgumentSourceOptions)
        {
            if (genericArgumentSourceOptions == GenericArgumentSourceOptions.Interface)
            {
                return type.GetInterfaces()
                    .FirstOrDefault(c => c.IsGenericType && filterType.IsAssignableFrom(c.GetGenericTypeDefinition()))
                    ?.GetGenericArguments();
            }
            else
            {
                var baseType = type.BaseType;
                if (baseType.IsGenericType && filterType.IsAssignableFrom(baseType.GetGenericTypeDefinition()))
                {
                    return baseType.GetGenericArguments();
                }
                return default;
            }
        }

        /// <summary>
        /// 创建查询筛选器表达式
        /// </summary>
        /// <typeparam name="TProperty">属性类型</typeparam>
        /// <param name="dbEntityType">数据库实体类型</param>
        /// <param name="propertyName">属性名</param>
        /// <param name="propertyValue">属性值</param>
        /// <returns><see cref="LambdaExpression"/></returns>
        public static LambdaExpression QueryFilterExpression<TProperty>(this Type dbEntityType, string propertyName, TProperty propertyValue)
        {
            var leftParameter = Expression.Parameter(dbEntityType, "e");
            var constantKey = Expression.Constant(propertyName);
            var constantValue = Expression.Constant(propertyValue);

            var expressionBody = Expression.Equal(Expression.Call(EFPropertyMethod.MakeGenericMethod(typeof(TProperty)), leftParameter, constantKey), constantValue);
            return Expression.Lambda(expressionBody, leftParameter);
        }

        private static readonly MethodInfo EFPropertyMethod = typeof(EF).GetMethod("Property");

        /// <summary>
        /// 获取实体属性
        /// </summary>
        /// <param name="entityEntry">实体跟踪器</param>
        /// <param name="propertyName">属性名称</param>
        /// <returns><see cref="PropertyEntry"/></returns>
        public static PropertyEntry GetProperty(this EntityEntry entityEntry, string propertyName)
        {
            var entityType = entityEntry.Entity.GetType();
            var isSet = EntityEntryProperties.TryGetValue((entityType, propertyName), out PropertyEntry propertyEntry);
            if (isSet) return propertyEntry;
            else
            {
                if (entityEntry.Metadata.FindProperty(propertyName) != null)
                {
                    propertyEntry = entityEntry.Property(propertyName);
                }

                EntityEntryProperties.TryAdd((entityType, propertyName), propertyEntry);
                return propertyEntry;
            }
        }

        private static readonly ConcurrentDictionary<(Type, string), PropertyEntry> EntityEntryProperties;

        static TypeExtensions()
        {
            EntityEntryProperties = new ConcurrentDictionary<(Type, string), PropertyEntry>();
        }
    }
}