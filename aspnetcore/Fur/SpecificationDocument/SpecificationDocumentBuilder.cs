using Fur.DynamicApiController;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Fur.SpecificationDocument
{
    /// <summary>
    /// 规范化文档构建器
    /// </summary>
    internal static class SpecificationDocumentBuilder
    {
        /// <summary>
        /// 规范化文档配置
        /// </summary>
        private static readonly SpecificationDocumentSettingsOptions _specificationDocumentSettings;

        /// <summary>
        /// 文档默认分组
        /// </summary>
        private static readonly IEnumerable<GroupOrder> _defaultGroups;

        /// <summary>
        /// 文档分组列表
        /// </summary>
        private static readonly IEnumerable<string> _groups;

        /// <summary>
        /// 带排序的分组名
        /// </summary>
        private static readonly Regex _groupOrderRegex;

        /// <summary>
        /// 构造函数
        /// </summary>
        static SpecificationDocumentBuilder()
        {
            _specificationDocumentSettings = App.GetOptions<SpecificationDocumentSettingsOptions>();
            _groupOrderRegex = new Regex(@"@(?<order>[0-9]+$)");

            // 默认分组，支持多个逗号分割
            _defaultGroups = new List<GroupOrder> { ResolveGroupOrder(_specificationDocumentSettings.DefaultGroupName) };

            GetActionGroupsCached = new ConcurrentDictionary<MethodInfo, IEnumerable<GroupOrder>>();
            // 加载所有分组
            _groups = ReadGroups();
        }

        internal static void Build()
        {
        }

        /// <summary>
        /// 读取所有分组信息
        /// </summary>
        /// <returns></returns>
        private static IEnumerable<string> ReadGroups()
        {
            // 获取所有的控制器和动作方法
            var controllers = App.Assemblies.SelectMany(a => a.GetTypes().Where(u => Penetrates.IsController(u)));
            var actions = controllers.SelectMany(c => c.GetMethods().Where(u => IsAction(u, false)));

            // 合并所有分组
            var groupOrders = controllers.SelectMany(u => GetControllerGroups(u))
                .Union(
                    actions.SelectMany(u => GetActionGroups(u))
                )
                .Where(u => u != null)
                .Distinct();

            // 分组排序
            return groupOrders
                .OrderBy(u => u.Order)
                .ThenBy(u => u.Group)
                .Select(u => u.Group)
                .Distinct();
        }

        /// <summary>
        /// 获取控制器分组列表
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static IEnumerable<GroupOrder> GetControllerGroups(Type type)
        {
            // 如果控制器没有定义 [ApiDescriptionSettings] 特性，则返回默认分组
            if (!type.IsDefined(typeof(ApiDescriptionSettingsAttribute), true)) return _defaultGroups;

            // 读取分组
            var apiDescriptionSettings = type.GetCustomAttribute<ApiDescriptionSettingsAttribute>(true);
            if (apiDescriptionSettings.Groups == null || apiDescriptionSettings.Groups.Length == 0) return _defaultGroups;

            // 处理排序
            var groupOrders = new List<GroupOrder>();
            foreach (var group in apiDescriptionSettings.Groups)
            {
                groupOrders.Add(ResolveGroupOrder(group));
            }

            return groupOrders;
        }

        /// <summary>
        /// <see cref="GetActionGroups(MethodInfo)"/> 缓存集合
        /// </summary>
        private static readonly ConcurrentDictionary<MethodInfo, IEnumerable<GroupOrder>> GetActionGroupsCached;

        /// <summary>
        /// 获取动作方法分组列表
        /// </summary>
        /// <param name="method">方法</param>
        /// <returns></returns>
        internal static IEnumerable<GroupOrder> GetActionGroups(MethodInfo method)
        {
            var isCached = GetActionGroupsCached.TryGetValue(method, out IEnumerable<GroupOrder> groups);
            if (isCached) return groups;

            // 本地静态方法
            static IEnumerable<GroupOrder> Function(MethodInfo method)
            {
                // 如果动作方法没有定义 [ApiDescriptionSettings] 特性，则返回所在控制器分组
                if (!method.IsDefined(typeof(ApiDescriptionSettingsAttribute), true)) return GetControllerGroups(method.DeclaringType);

                // 读取分组
                var apiDescriptionSettings = method.GetCustomAttribute<ApiDescriptionSettingsAttribute>(true);
                if (apiDescriptionSettings.Groups == null || apiDescriptionSettings.Groups.Length == 0) return GetControllerGroups(method.DeclaringType);

                // 处理排序
                var groupOrders = new List<GroupOrder>();
                foreach (var group in apiDescriptionSettings.Groups)
                {
                    groupOrders.Add(ResolveGroupOrder(group));
                }

                return groupOrders;
            }

            // 调用本地静态方法
            groups = Function(method);
            GetActionGroupsCached.TryAdd(method, groups);
            return groups;
        }

        /// <summary>
        /// 是否是动作方法
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        private static bool IsAction(MethodInfo method, bool checkDeclaringType = true)
        {
            // 不是是非公开、抽象、静态、泛型方法
            if (!method.IsPublic || method.IsAbstract || method.IsStatic || method.IsGenericMethod) return false;

            // 如果所在类型不是控制器，则该行为也被忽略
            if (checkDeclaringType && !Penetrates.IsController(method.DeclaringType)) return false;

            // 不是能被导出忽略的接方法
            if (method.IsDefined(typeof(ApiExplorerSettingsAttribute), true) && method.GetCustomAttribute<ApiExplorerSettingsAttribute>(true).IgnoreApi) return false;

            return true;
        }

        /// <summary>
        /// 解析分组名称中的排序
        /// </summary>
        /// <param name="name">分组名</param>
        /// <returns></returns>
        private static GroupOrder ResolveGroupOrder(string group)
        {
            if (!_groupOrderRegex.IsMatch(group)) return new GroupOrder { Group = group };

            var order = int.Parse(_groupOrderRegex.Match(group).Groups["order"].Value);
            return new GroupOrder
            {
                Group = _groupOrderRegex.Replace(group, ""),
                Order = order
            };
        }
    }
}