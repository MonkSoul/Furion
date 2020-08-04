using Fur.Attributes;
using System;
using System.ComponentModel;
using System.Reflection;

namespace Fur.Extensions
{
    /// <summary>
    /// 类型拓展类
    /// </summary>
    [NonInflated]
    public static class TypeExtensions
    {
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

        /// <summary>
        /// 是否是可空类型
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>是/否</returns>
        public static bool IsNullable(this Type type)
            => type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);

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