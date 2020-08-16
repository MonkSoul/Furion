using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Fur.LazyController
{
    /// <summary>
    /// 常量、公共方法配置类
    /// </summary>
    internal static class Penetrates
    {
        /// <summary>
        /// 分组分隔符
        /// </summary>
        internal const string GroupSeparator = "@";

        /// <summary>
        /// 不能被FromBody绑定的请求动词
        /// </summary>
        internal static string[] HttpMethodOfCanNotBindFromBody;

        /// <summary>
        /// 请求动词映射字典
        /// </summary>
        internal static Dictionary<string, string> HttpVerbSetters { get; private set; }

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static Penetrates()
        {
            HttpMethodOfCanNotBindFromBody = new string[] { "GET", "DELETE", "TRACE", "HEAD" };

            HttpVerbSetters = new Dictionary<string, string>
            {
                ["post"] = "POST",
                ["add"] = "POST",
                ["create"] = "POST",
                ["insert"] = "POST",
                ["submit"] = "POST",

                ["get"] = "GET",
                ["find"] = "GET",
                ["fetch"] = "GET",
                ["query"] = "GET",
                ["search"] = "GET",

                ["put"] = "PUT",
                ["update"] = "PUT",

                ["delete"] = "DELETE",
                ["remove"] = "DELETE",
                ["clear"] = "DELETE"
            };

            IsControllerCached = new ConcurrentDictionary<Type, bool>();
            IsActionCached = new ConcurrentDictionary<MethodInfo, bool>();
        }

        /// <summary>
        /// <see cref="IsController(Type)"/> 缓存集合
        /// </summary>
        private static readonly ConcurrentDictionary<Type, bool> IsControllerCached;

        /// <summary>
        /// 是否是控制器
        /// </summary>
        /// <param name="type">type</param>
        /// <returns></returns>
        internal static bool IsController(Type type)
        {
            var isCached = IsControllerCached.TryGetValue(type, out bool isControllerType);
            if (isCached) return isControllerType;

            isControllerType = IsControllerType(type);
            IsControllerCached.TryAdd(type, isControllerType);
            return isControllerType;
        }

        /// <summary>
        /// 是否是控制器类型
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>bool</returns>
        private static bool IsControllerType(Type type)
        {
            // 不能是非公开、基元类型、值类型、抽象类、接口、泛型类
            if (!type.IsPublic || type.IsPrimitive || type.IsValueType || type.IsAbstract || type.IsInterface || type.IsGenericType) return false;

            // 继承 ControllerBase 或 实现 ILazyController 的类型
            if (typeof(ILazyController).IsAssignableFrom(type) || typeof(ControllerBase).IsAssignableFrom(type)) return true;

            return false;
        }

        /// <summary>
        /// <see cref="IsAction(MethodInfo)"/> 缓存集合
        /// </summary>
        private static readonly ConcurrentDictionary<MethodInfo, bool> IsActionCached;

        /// <summary>
        /// 是否是行为
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        internal static bool IsAction(MethodInfo method)
        {
            var isCached = IsActionCached.TryGetValue(method, out bool isActionMethod);
            if (isCached) return isActionMethod;

            isActionMethod = IsActionMethod(method);
            IsActionCached.TryAdd(method, isActionMethod);
            return isActionMethod;
        }

        /// <summary>
        /// 是否是行为方法
        /// </summary>
        /// <param name="methodInfo"></param>
        /// <returns></returns>
        private static bool IsActionMethod(MethodInfo method)
        {
            // 如果所在类型不是控制器，则该行为也被忽略
            if (!IsController(method.DeclaringType)) return false;

            // 不是是非公开、抽象、静态、泛型方法
            if (!method.IsPublic || method.IsAbstract || method.IsStatic || method.IsGenericMethod) return false;

            // 不能是定义了 [ApiExplorerSettings] 特性且 IgnoreApi 为 true
            if (method.IsDefined(typeof(ApiExplorerSettingsAttribute), true) && method.GetCustomAttribute<ApiExplorerSettingsAttribute>(true).IgnoreApi) return false;

            return true;
        }

        /// <summary>
        /// 清除字符串前后缀
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="pos">0：前后缀，1：后缀，-1：前缀</param>
        /// <param name="affixes">前后缀集合</param>
        /// <returns></returns>
        internal static string ClearStringAffixes(string str, int pos = 0, params string[] affixes)
        {
            // 空字符串直接返回
            if (string.IsNullOrEmpty(str)) return str;

            // 空前后缀集合直接返回
            if (affixes == null || affixes.Length == 0) return str;

            bool startCleared = false;
            bool endCleared = false;

            string tempStr = null;
            foreach (var affix in affixes)
            {
                if (pos != 1 && !startCleared && str.StartsWith(affix))
                {
                    tempStr = str[affix.Length..];
                    startCleared = true;
                }
                if (pos != -1 && !endCleared && str.EndsWith(affix))
                {
                    var _tempStr = !string.IsNullOrEmpty(tempStr) ? tempStr : str;
                    tempStr = _tempStr.Substring(0, _tempStr.Length - affix.Length);
                    endCleared = true;
                }
                if (startCleared && endCleared) break;
            }

            return !string.IsNullOrEmpty(tempStr) ? tempStr : str;
        }

        /// <summary>
        /// 切割骆驼命名式字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        internal static string[] SplitToWords(string str)
        {
            if (string.IsNullOrEmpty(str)) throw new ArgumentNullException(nameof(str));
            if (str.Length == 1) return new string[] { str };

            return Regex.Split(str, @"(?=\p{Lu}\p{Ll})|(?<=\p{Ll})(?=\p{Lu})")
                .Where(u => u.Length > 0)
                .ToArray();
        }
    }
}