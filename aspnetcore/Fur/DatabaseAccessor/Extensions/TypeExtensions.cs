using Fur.ApplicationBase.Attributes;
using Fur.DatabaseAccessor.Options;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Fur.DatabaseAccessor.Extensions
{
    /// <summary>
    /// 类型拓展类
    /// </summary>
    [NonWrapper]
    public static class TypeExtensions
    {
        #region 获取类型的父类型或接口类型泛型参数 + internal static Type[] GetTypeGenericArguments(this Type type, Type filterType, GenericArgumentSourceOptions genericArgumentSourceOptions)
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
        #endregion

        #region 创建查询筛选器表达式 + public static LambdaExpression QueryFilterExpression<TProperty>(this Type dbEntityType, string propertyName, int propertyValue)
        /// <summary>
        /// 创建查询筛选器表达式
        /// </summary>
        /// <typeparam name="TProperty">属性类型</typeparam>
        /// <param name="dbEntityType">数据库实体类型</param>
        /// <param name="propertyName">属性名</param>
        /// <param name="propertyValue">属性值</param>
        /// <returns><see cref="LambdaExpression"/></returns>
        public static LambdaExpression QueryFilterExpression<TProperty>(this Type dbEntityType, string propertyName, int propertyValue)
        {
            var leftParameter = Expression.Parameter(dbEntityType, "e");
            var constantKey = Expression.Constant(propertyName);
            var constantValue = Expression.Constant(propertyValue);

            var expressionBody = Expression.Equal(Expression.Call(typeof(EF).GetMethod("Property").MakeGenericMethod(typeof(TProperty)), leftParameter, constantKey), constantValue);
            return Expression.Lambda(expressionBody, leftParameter);
        }
        #endregion
    }
}
