using Furion.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Furion.Reflection.Extensions
{
    /// <summary>
    /// Method Info 拓展
    /// </summary>
    [SkipScan]
    public static class MethodInfoExtensions
    {
        /// <summary>
        /// 获取真实方法的特性集合
        /// </summary>
        /// <param name="method"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static IEnumerable<Attribute> GetActualCustomAttributes(this MethodInfo method, object target)
        {
            return GetActualMethodInfo(method, target)?.GetCustomAttributes();
        }

        /// <summary>
        /// 获取真实方法的特性集合
        /// </summary>
        /// <param name="method"></param>
        /// <param name="target"></param>
        /// <param name="inherit"></param>
        /// <returns></returns>
        public static object[] GetActualCustomAttributes(this MethodInfo method, object target, bool inherit)
        {
            return GetActualMethodInfo(method, target)?.GetCustomAttributes(inherit);
        }

        /// <summary>
        /// 获取真实方法的特性集合
        /// </summary>
        /// <param name="method"></param>
        /// <param name="target"></param>
        /// <param name="attributeType"></param>
        /// <returns></returns>
        public static IEnumerable<Attribute> GetActualCustomAttributes(this MethodInfo method, object target, Type attributeType)
        {
            return GetActualMethodInfo(method, target)?.GetCustomAttributes(attributeType);
        }

        /// <summary>
        /// 获取真实方法的特性集合
        /// </summary>
        /// <param name="method"></param>
        /// <param name="target"></param>
        /// <param name="attributeType"></param>
        /// <param name="inherit"></param>
        /// <returns></returns>
        public static object[] GetActualCustomAttributes(this MethodInfo method, object target, Type attributeType, bool inherit)
        {
            return GetActualMethodInfo(method, target)?.GetCustomAttributes(attributeType, inherit);
        }

        /// <summary>
        /// 获取真实方法的特性集合
        /// </summary>
        /// <param name="method"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static IEnumerable<TAttribute> GetActualCustomAttributes<TAttribute>(this MethodInfo method, object target)
            where TAttribute : Attribute
        {
            return GetActualMethodInfo(method, target)?.GetCustomAttributes<TAttribute>();
        }

        /// <summary>
        /// 获取真实方法的特性集合
        /// </summary>
        /// <param name="method"></param>
        /// <param name="target"></param>
        /// <param name="inherit"></param>
        /// <returns></returns>
        public static IEnumerable<TAttribute> GetActualCustomAttributes<TAttribute>(this MethodInfo method, object target, bool inherit)
            where TAttribute : Attribute
        {
            return GetActualMethodInfo(method, target)?.GetCustomAttributes<TAttribute>(inherit);
        }

        /// <summary>
        /// 获取真实方法的特性
        /// </summary>
        /// <param name="method"></param>
        /// <param name="target"></param>
        /// <param name="attributeType"></param>
        /// <returns></returns>
        public static Attribute GetActualCustomAttribute(this MethodInfo method, object target, Type attributeType)
        {
            return GetActualMethodInfo(method, target)?.GetCustomAttribute(attributeType);
        }

        /// <summary>
        /// 获取真实方法的特性
        /// </summary>
        /// <param name="method"></param>
        /// <param name="target"></param>
        /// <param name="attributeType"></param>
        /// <param name="inherit"></param>
        /// <returns></returns>
        public static Attribute GetActualCustomAttribute(this MethodInfo method, object target, Type attributeType, bool inherit)
        {
            return GetActualMethodInfo(method, target)?.GetCustomAttribute(attributeType, inherit);
        }

        /// <summary>
        /// 获取真实方法的特性
        /// </summary>
        /// <param name="method"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static TAttribute GetActualCustomAttribute<TAttribute>(this MethodInfo method, object target)
            where TAttribute : Attribute
        {
            return GetActualMethodInfo(method, target)?.GetCustomAttribute<TAttribute>();
        }

        /// <summary>
        /// 获取真实方法的特性
        /// </summary>
        /// <param name="method"></param>
        /// <param name="target"></param>
        /// <param name="inherit"></param>
        /// <returns></returns>
        public static TAttribute GetActualCustomAttribute<TAttribute>(this MethodInfo method, object target, bool inherit)
            where TAttribute : Attribute
        {
            return GetActualMethodInfo(method, target)?.GetCustomAttribute<TAttribute>(inherit);
        }

        /// <summary>
        /// 获取实际方法对象
        /// </summary>
        /// <param name="method"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        private static MethodInfo GetActualMethodInfo(MethodInfo method, object target)
        {
            if (target == null) return default;

            var targetType = target.GetType();
            var actualMethod = targetType.GetMethods()
                                                 .FirstOrDefault(u => u.ToString().Equals(method.ToString()));

            if (actualMethod == null) return default;

            return actualMethod;
        }
    }
}