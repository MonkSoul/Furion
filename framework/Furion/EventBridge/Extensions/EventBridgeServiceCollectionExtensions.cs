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
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;

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
        /// 添加事件总线服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddEventBridge(this IServiceCollection services)
        {
            return services.AddEventBridge<DefaultEventStoreProvider>();
        }

        /// <summary>
        /// 添加事件总线服务
        /// </summary>
        /// <typeparam name="TEventStoreProvider"></typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddEventBridge<TEventStoreProvider>(this IServiceCollection services)
            where TEventStoreProvider : class, IEventStoreProvider
        {
            // 查找所有事件处理程序
            var eventHandlerTypes = App.EffectiveTypes.Where(u => u.IsClass && !u.IsInterface && !u.IsAbstract && typeof(IEventHandler).IsAssignableFrom(u));
            if (!eventHandlerTypes.Any()) return services;

            // 注册事件存储提供器
            services.AddTransient<IEventStoreProvider, TEventStoreProvider>();

            using var serviceProvider = InternalApp.InternalServices.BuildServiceProvider();

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

                // 添加注册
                var eventStoreProvider = serviceProvider.GetService<IEventStoreProvider>();
                eventStoreProvider.RegisterEventAsync(new EventMetadata
                {
                    AssemblyName = type.Assembly.GetName().Name,
                    Category = eventCategory,
                    TypeFullName = type.FullName,
                    CreatedTime = DateTimeOffset.UtcNow
                }).GetAwaiter().GetResult();
            }

            // 注册解析事件处理程序委托
            services.TryAddTransient(provider =>
            {
                IEventHandler eventHandlerResolve(EventIdMetadata eventIdMetadata)
                {
                    // 加载程序集
                    var assembly = AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(eventIdMetadata.AssemblyName));

                    // 解析服务
                    return provider.GetService(assembly.GetType(eventIdMetadata.TypeFullName)) as IEventHandler;
                }
                return (Func<EventIdMetadata, IEventHandler>)eventHandlerResolve;
            });

            return services;
        }
    }
}