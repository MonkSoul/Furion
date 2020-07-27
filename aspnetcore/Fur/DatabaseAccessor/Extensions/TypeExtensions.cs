using Fur.ApplicationBase.Attributes;
using Fur.DatabaseAccessor.Options;
using System;
using System.Linq;

namespace Fur.DatabaseAccessor.Extensions
{
    /// <summary>
    /// 类型拓展类
    /// </summary>
    [NonWrapper]
    internal static class TypeExtensions
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
    }
}
