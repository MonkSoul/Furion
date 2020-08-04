using Fur.Attributes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Concurrent;
using System.Linq.Expressions;
using System.Reflection;

namespace Fur.DatabaseAccessor.Extensions
{
    /// <summary>
    /// 类型拓展类
    /// </summary>
    [NonInflated]
    public static class TypeExtensions
    {
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