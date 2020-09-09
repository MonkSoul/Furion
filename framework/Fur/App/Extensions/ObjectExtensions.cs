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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Threading.Tasks;

namespace Fur.Extensions
{
    /// <summary>
    /// 对象拓展类
    /// </summary>
    internal static class ObjectExtensions
    {
        /// <summary>
        /// 判断是否是富基元类型
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns></returns>
        internal static bool IsRichPrimitive(this Type type)
        {
            // 处理元组类型
            if (type.IsValueTuple()) return false;

            // 基元类型或值类型或字符串类型
            if (type.IsPrimitive || type.IsValueType || type == typeof(string)) return true;

            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>)) return type.GenericTypeArguments[0].IsRichPrimitive();

            return false;
        }

        /// <summary>
        /// 合并两个字典
        /// </summary>
        /// <param name="dic">字典</param>
        /// <param name="newDic">新字典</param>
        /// <returns></returns>
        internal static Dictionary<string, string> AddOrUpdate(this Dictionary<string, string> dic, Dictionary<string, string> newDic)
        {
            foreach (var key in newDic.Keys)
            {
                if (dic.ContainsKey(key))
                    dic[key] = newDic[key];
                else
                    dic.Add(key, newDic[key]);
            }

            return dic;
        }

        /// <summary>
        /// 转换值到为指定类型
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="type">最终类型</param>
        /// <returns>object</returns>
        internal static object ChangeType(this object value, Type type)
        {
            // 如果值为默认值，则返回默认值
            if (value == null || (value.GetType().IsValueType && (value.Equals(0) || value.Equals(Guid.Empty)))) return default;

            // 属性真实类型
            Type underlyingType = null;

            // 判断属性类型是否是可空类型
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                underlyingType = new NullableConverter(type).UnderlyingType;
            }
            underlyingType ??= type;

            // 枚举类型处理
            if (typeof(Enum).IsAssignableFrom(underlyingType))
            {
                return Enum.Parse(underlyingType, value.ToString());
            }

            // 执行转换
            return Convert.ChangeType(value, underlyingType);
        }

        /// <summary>
        /// 判断是否是元组类型
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns></returns>
        internal static bool IsValueTuple(this Type type)
        {
            return type.ToString().StartsWith(typeof(ValueTuple).FullName);
        }

        /// <summary>
        /// 判断方法是否是异步
        /// </summary>
        /// <param name="method">方法</param>
        /// <returns></returns>
        internal static bool IsAsync(this MethodInfo method)
        {
            return method.ReturnType.IsAsync();
        }

        /// <summary>
        /// 判断类型是否是异步类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        internal static bool IsAsync(this Type type)
        {
            return type.ToString().StartsWith(typeof(Task).FullName);
        }
    }
}