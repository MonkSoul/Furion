// MIT License
//
// Copyright (c) 2020-2022 百小僧, Baiqian Co.,Ltd and Contributors
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;
using System.Reflection;

namespace Furion.DynamicApiController;

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
    internal static ConcurrentDictionary<string, string> VerbToHttpMethods { get; private set; }

    /// <summary>
    /// 控制器排序集合
    /// </summary>
    internal static ConcurrentDictionary<string, int> ControllerOrderCollection { get; set; }

    /// <summary>
    /// 构造函数
    /// </summary>
    static Penetrates()
    {
        ControllerOrderCollection = new ConcurrentDictionary<string, int>();

        VerbToHttpMethods = new ConcurrentDictionary<string, string>
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
}