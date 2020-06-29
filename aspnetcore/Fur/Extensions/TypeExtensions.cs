using System;
using System.Reflection;

namespace Fur.Extensions
{
    /// <summary>
    /// 类型拓展类
    /// </summary>
    public static class TypeExtensions
    {
        #region 递归获取特性 +/* public static TAttribute GetDeepAttribute<TAttribute>(this TypeInfo typeInfo) where TAttribute : Attribute
        /// <summary>
        /// 递归获取特性
        /// </summary>
        /// <typeparam name="TAttribute">特性泛型类型</typeparam>
        /// <param name="typeInfo">类型对象</param>
        /// <returns>特性对象</returns>
        public static TAttribute GetDeepAttribute<TAttribute>(this TypeInfo typeInfo) where TAttribute : Attribute
        {
            var attributeType = typeof(TAttribute);
            if (typeInfo.IsDefined(attributeType, true)) return typeInfo.GetCustomAttribute<TAttribute>(true);
            else
            {
                var implementedInterfaces = typeInfo.ImplementedInterfaces;
                foreach (var impl in implementedInterfaces)
                {
                    var tAttribute = GetDeepAttribute<TAttribute>(impl.GetTypeInfo());
                    if (tAttribute != null) return tAttribute;
                }
            }

            return null;
        }
        #endregion

        #region 递归获取特性 +/* public static TAttribute GetDeepAttribute<TAttribute>(this Type type) where TAttribute : Attribute
        /// <summary>
        /// 递归获取特性
        /// </summary>
        /// <typeparam name="TAttribute">特性泛型类型</typeparam>
        /// <param name="type">类型对象</param>
        /// <returns>特性对象</returns>
        public static TAttribute GetDeepAttribute<TAttribute>(this Type type) where TAttribute : Attribute
            => GetDeepAttribute<TAttribute>(type.GetTypeInfo());
        #endregion
    }
}
