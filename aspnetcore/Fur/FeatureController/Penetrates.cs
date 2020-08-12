using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Concurrent;
using System.Reflection;
using System.Text;

namespace Fur.FeatureController
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
        /// 静态构造函数
        /// </summary>
        static Penetrates()
        {
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
            // 不能是非公开、抽象类、接口、泛型类、值类型、枚举类型
            if (!type.IsPublic || type.IsAbstract || type.IsInterface || type.IsGenericType || type.IsValueType || type.IsEnum) return false;

            // 不能是定义了 [ApiExplorerSettings] 特性且 IgnoreApi 为 true
            if (type.IsDefined(typeof(ApiExplorerSettingsAttribute), true) && type.GetCustomAttribute<ApiExplorerSettingsAttribute>(true).IgnoreApi) return false;

            // 是 ControllerBase 子类型，且贴有 [Route] 特性
            if (typeof(ControllerBase).IsAssignableFrom(type) && type.IsDefined(typeof(RouteAttribute), true)) return true;

            // 实现了 IFeatureController 子类型
            if (typeof(IFeatureController).IsAssignableFrom(type)) return true;

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

            var stringBuilder = new StringBuilder();
            foreach (var affix in affixes)
            {
                if (pos != 1 && !startCleared && str.StartsWith(affix))
                {
                    stringBuilder.Insert(0, str[affix.Length..]);
                    startCleared = true;
                }
                if (pos != -1 && !endCleared && str.EndsWith(affix))
                {
                    var _tempStr = stringBuilder.Length > 0 ? stringBuilder.ToString() : str;
                    stringBuilder.Append(_tempStr.Substring(0, _tempStr.Length - affix.Length));
                    endCleared = true;
                }
                if (startCleared && endCleared) break;
            }

            var newStr = stringBuilder.ToString();
            return newStr.Length > 0 ? newStr : str;
        }
    }
}