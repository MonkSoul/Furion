using System;
using System.ComponentModel;
using System.Reflection;

namespace Fur.TypeExtensions
{
    /// <summary>
    /// 类型拓展类
    /// </summary>
    public static class TypeExtensions
    {
        #region 递归获取特性 + public static TAttribute GetDeepAttribute<TAttribute>(this TypeInfo typeInfo) where TAttribute : Attribute

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

        #region 递归获取特性 + public static TAttribute GetDeepAttribute<TAttribute>(this Type type) where TAttribute : Attribute

        /// <summary>
        /// 递归获取特性
        /// </summary>
        /// <typeparam name="TAttribute">特性泛型类型</typeparam>
        /// <param name="type">类型对象</param>
        /// <returns>特性对象</returns>
        public static TAttribute GetDeepAttribute<TAttribute>(this Type type) where TAttribute : Attribute
            => GetDeepAttribute<TAttribute>(type.GetTypeInfo());

        internal static bool IsPrimitivePlus(this Type type, bool includeEnum = true)
        {
            if (type.IsPrimitive) return true;
            if (includeEnum && type.IsEnum) return true;

            return type == typeof(string) ||
                   type == typeof(decimal) ||
                   type == typeof(float) ||
                   type == typeof(DateTime) ||
                   type == typeof(DateTimeOffset) ||
                   type == typeof(TimeSpan) ||
                   type == typeof(Guid);
        }

        internal static bool IsPrimitivePlusIncludeNullable(this Type type, bool includeEnum = true)
        {
            if (IsPrimitivePlus(type, includeEnum)) return true;

            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                return IsPrimitivePlus(type.GenericTypeArguments[0], includeEnum);

            return false;
        }

        #endregion

        #region 是否是可空类型 + public static bool IsNullable(this Type type) => type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>)
        /// <summary>
        /// 是否是可空类型
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>是/否</returns>
        public static bool IsNullable(this Type type)
            => type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);

        #endregion

        public static void SetPropertyValue(this PropertyInfo property, object obj, object value)
            => property.SetValue(obj, SetPropertyValue(value, property.PropertyType));

        private static object SetPropertyValue(object value, Type conversionType)
        {
            if (conversionType.IsGenericType && conversionType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                if (value == null) return null;

                NullableConverter nullableConverter = new NullableConverter(conversionType);
                conversionType = nullableConverter.UnderlyingType;
            }
            if (typeof(System.Enum).IsAssignableFrom(conversionType)) return Enum.Parse(conversionType, value.ToString());

            return Convert.ChangeType(value, conversionType);
        }

        internal static object CallMethod(this Type type, string methodName, object instance, params object[] parameters)
        {
            return type.GetMethod(methodName).Invoke(instance, parameters);
        }
    }
}