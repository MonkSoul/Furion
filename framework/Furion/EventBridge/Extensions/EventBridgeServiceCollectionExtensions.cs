// Copyright (c) 2020-2021 百小僧, Baiqian Co.,Ltd.
// Furion is licensed under Mulan PSL v2.
// You can use this software according to the terms and conditions of the Mulan PSL v2.
// You may obtain a copy of Mulan PSL v2 at:
//             http://license.coscl.org.cn/MulanPSL2
// THIS SOFTWARE IS PROVIDED ON AN "AS IS" BASIS, WITHOUT WARRANTIES OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO NON-INFRINGEMENT, MERCHANTABILITY OR FIT FOR A PARTICULAR PURPOSE.
// See the Mulan PSL v2 for more details.

using Furion;
using Furion.DependencyInjection;
using Furion.EventBridge;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 事件总线服务拓展
    /// </summary>
    [SuppressSniffer]
    public static class EventBridgeServiceCollectionExtensions
    {
        /// <summary>
        /// 默认移除事件命名后缀
        /// </summary>
        private const string eventTypeNameSuffix = "EventHandler";

        /// <summary>
        /// 事件处理程序集合
        /// </summary>
        private static readonly ConcurrentDictionary<string, Type> EventHandlerCollection;

        /// <summary>
        /// 添加事件总线服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddEventBridge(this IServiceCollection services)
        {
            // 查找所有事件处理程序
            var eventHandlerTypes = App.EffectiveTypes.Where(u => u.IsClass && !u.IsInterface && !u.IsAbstract && typeof(IEventHandler).IsAssignableFrom(u));

            // 批量注册
            foreach (var type in eventHandlerTypes)
            {
                // 注册事件类型
                services.AddTransient(type);
                services.AddTransient(typeof(IEventHandler), type);

                // 如果定义了 [EventCategory] 特性，使用 Category，否则使用类型名（默认去除 EventHandler）结尾
                var eventCategory = type.IsDefined(typeof(EventCategoryAttribute), false)
                                                     ? type.GetCustomAttribute<EventCategoryAttribute>(false).Category
                                                     : (type.Name.EndsWith(eventTypeNameSuffix) ? type.Name[0..^eventTypeNameSuffix.Length] : type.Name);
                // 存储事件和类型关系
                EventHandlerCollection.TryAdd(eventCategory, type);
            }

            // 注册解析事件处理程序委托
            services.TryAddTransient(provider =>
            {
                IEventHandler eventHandlerResolve(string category, IEventHandler _)
                {
                    if (!EventHandlerCollection.TryGetValue(category, out var eventHandlerType)) return default;

                    // 解析服务
                    return provider.GetService(eventHandlerType) as IEventHandler;
                }
                return (Func<string, IEventHandler, IEventHandler>)eventHandlerResolve;
            });

            return services;
        }

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static EventBridgeServiceCollectionExtensions()
        {
            EventHandlerCollection = new();
        }
    }
}