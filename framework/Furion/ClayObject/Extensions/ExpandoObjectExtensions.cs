using Furion.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace Furion.ClayObject.Extensions
{
    /// <summary>
    /// ExpandoObject 对象拓展
    /// </summary>
    [SkipScan]
    public static class ExpandoObjectExtensions
    {
        /// <summary>
        /// 将对象转 ExpandoObject 类型
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static ExpandoObject ToExpando(this object value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            if (value is not ExpandoObject expando)
            {
                expando = new ExpandoObject();
                var dict = (IDictionary<string, object>)expando;

                foreach (var kvp in DictionaryMaker.Make(value))
                    dict.Add(kvp);
            }

            return expando;
        }

        /// <summary>
        /// 移除 ExpandoObject 对象属性
        /// </summary>
        /// <param name="expando"></param>
        /// <param name="propertyName"></param>
        public static void RemoveProperty(this ExpandoObject expando, string propertyName)
        {
            if (expando == null)
                throw new ArgumentNullException(nameof(expando));

            if (propertyName == null)
                throw new ArgumentNullException(nameof(propertyName));

            ((IDictionary<string, object>)expando).Remove(propertyName);
        }

        /// <summary>
        /// 判断 ExpandoObject 是否为空
        /// </summary>
        /// <param name="expando"></param>
        /// <returns></returns>
        public static bool Empty(this ExpandoObject expando)
        {
            return !((IDictionary<string, object>)expando).Any();
        }

        /// <summary>
        /// 判断 ExpandoObject 是否拥有某属性
        /// </summary>
        /// <param name="expando"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static bool HasProperty(this ExpandoObject expando, string propertyName)
        {
            if (expando == null)
                throw new ArgumentNullException(nameof(expando));

            if (propertyName == null)
                throw new ArgumentNullException(nameof(propertyName));

            return ((IDictionary<string, object>)expando).ContainsKey(propertyName);
        }

        /// <summary>
        /// 实现 ExpandoObject 浅拷贝
        /// </summary>
        /// <param name="expando"></param>
        /// <returns></returns>
        public static ExpandoObject ShallowCopy(this ExpandoObject expando)
        {
            return Copy(expando, false);
        }

        /// <summary>
        /// 实现 ExpandoObject 深度拷贝
        /// </summary>
        /// <param name="expando"></param>
        /// <returns></returns>
        public static ExpandoObject DeepCopy(this ExpandoObject expando)
        {
            return Copy(expando, true);
        }

        /// <summary>
        /// 拷贝 ExpandoObject 对象
        /// </summary>
        /// <param name="original"></param>
        /// <param name="deep"></param>
        /// <returns></returns>
        private static ExpandoObject Copy(ExpandoObject original, bool deep)
        {
            var clone = new ExpandoObject();

            var _original = (IDictionary<string, object>)original;
            var _clone = (IDictionary<string, object>)clone;

            foreach (var kvp in _original)
                _clone.Add(
                    kvp.Key,
                    deep && kvp.Value is ExpandoObject eObject ? DeepCopy(eObject) : kvp.Value
                );

            return clone;
        }
    }
}