// Copyright (c) 2020-2021 百小僧, Baiqian Co.,Ltd.
// Furion is licensed under Mulan PSL v2.
// You can use this software according to the terms and conditions of the Mulan PSL v2.
// You may obtain a copy of Mulan PSL v2 at:
//             http://license.coscl.org.cn/MulanPSL2
// THIS SOFTWARE IS PROVIDED ON AN "AS IS" BASIS, WITHOUT WARRANTIES OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO NON-INFRINGEMENT, MERCHANTABILITY OR FIT FOR A PARTICULAR PURPOSE.
// See the Mulan PSL v2 for more details.

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Furion.DynamicApiController
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
        /// 请求动词映射字典
        /// </summary>
        internal static Dictionary<string, string> VerbToHttpMethods { get; private set; }

        /// <summary>
        /// 控制器排序集合
        /// </summary>
        internal static Dictionary<string, int> ControllerOrderCollection { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        static Penetrates()
        {
            ControllerOrderCollection = new Dictionary<string, int>();

            VerbToHttpMethods = new Dictionary<string, string>
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
                //["getlist"] = "GET",
                //["getall"] = "GET",

                ["put"] = "PUT",
                ["update"] = "PUT",

                ["delete"] = "DELETE",
                ["remove"] = "DELETE",
                ["clear"] = "DELETE",

                ["patch"] = "PATCH"
            };

            IsApiControllerCached = new ConcurrentDictionary<Type, bool>();
        }

        /// <summary>
        /// <see cref="IsApiController(Type)"/> 缓存集合
        /// </summary>
        private static readonly ConcurrentDictionary<Type, bool> IsApiControllerCached;

        /// <summary>
        /// 是否是Api控制器
        /// </summary>
        /// <param name="type">type</param>
        /// <returns></returns>
        internal static bool IsApiController(Type type)
        {
            return IsApiControllerCached.GetOrAdd(type, Function);

            // 本地静态方法
            static bool Function(Type type)
            {
                // 不能是非公开、基元类型、值类型、抽象类、接口、泛型类
                if (!type.IsPublic || type.IsPrimitive || type.IsValueType || type.IsAbstract || type.IsInterface || type.IsGenericType) return false;

                // 继承 ControllerBase 或 实现 IDynamicApiController 的类型 或 贴了 [DynamicApiController] 特性
                if ((!typeof(Controller).IsAssignableFrom(type) && typeof(ControllerBase).IsAssignableFrom(type)) || typeof(IDynamicApiController).IsAssignableFrom(type) || type.IsDefined(typeof(DynamicApiControllerAttribute), true))
                {
                    // 不是能被导出忽略的接口
                    if (type.IsDefined(typeof(ApiExplorerSettingsAttribute), true) && type.GetCustomAttribute<ApiExplorerSettingsAttribute>(true).IgnoreApi) return false;

                    return true;
                }
                return false;
            }
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
            if (string.IsNullOrWhiteSpace(str)) return str;

            // 空前后缀集合直接返回
            if (affixes == null || affixes.Length == 0) return str;

            var startCleared = false;
            var endCleared = false;

            string tempStr = null;
            foreach (var affix in affixes)
            {
                if (string.IsNullOrWhiteSpace(affix)) continue;

                if (pos != 1 && !startCleared && str.StartsWith(affix, StringComparison.OrdinalIgnoreCase))
                {
                    tempStr = str[affix.Length..];
                    startCleared = true;
                }
                if (pos != -1 && !endCleared && str.EndsWith(affix, StringComparison.OrdinalIgnoreCase))
                {
                    var _tempStr = !string.IsNullOrWhiteSpace(tempStr) ? tempStr : str;
                    tempStr = _tempStr.Substring(0, _tempStr.Length - affix.Length);
                    endCleared = true;
                }
                if (startCleared && endCleared) break;
            }

            return !string.IsNullOrWhiteSpace(tempStr) ? tempStr : str;
        }

        /// <summary>
        /// 切割骆驼命名式字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        internal static string[] SplitCamelCase(string str)
        {
            if (string.IsNullOrWhiteSpace(str)) return new string[] { str };
            if (str.Length == 1) return new string[] { str };

            return Regex.Split(str, @"(?=\p{Lu}\p{Ll})|(?<=\p{Ll})(?=\p{Lu})")
                .Where(u => u.Length > 0)
                .ToArray();
        }
    }
}