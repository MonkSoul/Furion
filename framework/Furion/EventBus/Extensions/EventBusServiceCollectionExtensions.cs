// Copyright (c) 2020-2021 百小僧, Baiqian Co.,Ltd.
// Furion is licensed under Mulan PSL v2.
// You can use this software according to the terms and conditions of the Mulan PSL v2.
// You may obtain a copy of Mulan PSL v2 at:
//             http://license.coscl.org.cn/MulanPSL2
// THIS SOFTWARE IS PROVIDED ON AN "AS IS" BASIS, WITHOUT WARRANTIES OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO NON-INFRINGEMENT, MERCHANTABILITY OR FIT FOR A PARTICULAR PURPOSE.
// See the Mulan PSL v2 for more details.

using Furion;
using Furion.DependencyInjection;
using Furion.EventBus;
using Furion.Extensions;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 轻量级事件总线服务拓展
    /// </summary>
    [SuppressSniffer]
    public static class EventBusServiceCollectionExtensions
    {
        /// <summary>
        /// 添加轻量级事件总线服务拓展
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddSimpleEventBus(this IServiceCollection services)
        {
            // 查找所有贴了 [SubscribeMessage] 特性的方法，并且含有两个参数，第一个参数为 string messageId，第二个参数为 object payload
            var typeMethods = App.EffectiveTypes
                    // 查询符合条件的订阅类型
                    .Where(u => u.IsClass && !u.IsInterface && !u.IsAbstract && typeof(ISubscribeHandler).IsAssignableFrom(u))
                    // 查询符合条件的订阅方法
                    .SelectMany(u =>
                        u.GetMethods(BindingFlags.Public | BindingFlags.Instance)
                         .Where(m => m.IsDefined(typeof(SubscribeMessageAttribute), false)
                                                    && m.GetParameters().Length == 2
                                                    && m.GetParameters()[0].ParameterType == typeof(string)
                                                    && m.GetParameters()[1].ParameterType == typeof(object))
                         .GroupBy(m => m.DeclaringType));

            if (!typeMethods.Any()) return services;

            // 遍历所有订阅类型
            foreach (var item in typeMethods)
            {
                if (!item.Any()) continue;

                // 创建订阅类对象
                var typeInstance = Activator.CreateInstance(item.Key);

                foreach (var method in item)
                {
                    // 判断是否是异步方法
                    var isAsyncMethod = method.IsAsync();

                    // 创建委托类型
                    var action = Delegate.CreateDelegate(isAsyncMethod ? typeof(Func<string, object, Task>) : typeof(Action<string, object>), typeInstance, method.Name);

                    // 获取所有消息特性
                    var subscribeMessageAttributes = method.GetCustomAttributes<SubscribeMessageAttribute>();

                    // 注册订阅
                    foreach (var subscribeMessageAttribute in subscribeMessageAttributes)
                    {
                        if (string.IsNullOrWhiteSpace(subscribeMessageAttribute.MessageId)) continue;

                        if (isAsyncMethod)
                        {
                            InternalMessageCenter.Instance.Subscribe(item.Key, subscribeMessageAttribute.MessageId, (Func<string, object, Task>)action);
                        }
                        else
                        {
                            InternalMessageCenter.Instance.Subscribe(item.Key, subscribeMessageAttribute.MessageId, (Action<string, object>)action);
                        }
                    }
                }
            }

            return services;
        }
    }
}