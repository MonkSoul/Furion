// Copyright (c) 2020-2021 百小僧, Baiqian Co.,Ltd.
// Furion is licensed under Mulan PSL v2.
// You can use this software according to the terms and conditions of the Mulan PSL v2.
// You may obtain a copy of Mulan PSL v2 at:
//             http://license.coscl.org.cn/MulanPSL2
// THIS SOFTWARE IS PROVIDED ON AN "AS IS" BASIS, WITHOUT WARRANTIES OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO NON-INFRINGEMENT, MERCHANTABILITY OR FIT FOR A PARTICULAR PURPOSE.
// See the Mulan PSL v2 for more details.

using Furion.Extensions;
using Furion.FriendlyException;
using Furion.IPCChannel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Furion.EventBridge
{
    /// <summary>
    /// 事件分发调度器
    /// </summary>
    internal sealed class EventDispatcher : ChannelHandler<EventPayload>
    {
        /// <summary>
        /// 调度核心代码
        /// </summary>
        /// <param name="eventPayload"></param>
        /// <returns></returns>
        public async override Task InvokeAsync(EventPayload eventPayload)
        {
            // 解析服务工厂
            var serviceScopeFactory = App.GetService<IServiceScopeFactory>(App.RootServices);

            // 创建服务作用域
            using var scoped = serviceScopeFactory.CreateScope();

            // 解析事件存储提供器
            var eventStoreProvider = scoped.ServiceProvider.GetService<IEventStoreProvider>();
            var eventIdMetadata = await eventStoreProvider?.GetEventIdAsync(eventPayload.Category, eventPayload.EventId);
            if (eventIdMetadata == null) return;

            // 获取解析事件处理程序服务委托
            var eventHandlerResolve = scoped.ServiceProvider.GetService<Func<EventIdMetadata, IEventHandler>>();
            var eventHandler = eventHandlerResolve(eventIdMetadata);
            if (eventHandler == null) return;

            // 查找所有符合的处理方法，贴了 [EventId] 或 方法名相等的
            var methods = eventHandler.GetType().GetTypeInfo().DeclaredMethods
                                   .Where(u => !u.IsStatic)
                                   .Where(u => u.Name == eventPayload.EventId
                                       || (u.IsDefined(typeof(EventIdAttribute), false) && u.GetCustomAttributes<EventIdAttribute>(false).Any(e => e.Id == eventPayload.EventId)))
                                   .Where(u => u.ReturnType == typeof(void) || u.ReturnType == typeof(Task))
                                   .Where(u => u.GetParameters().Length > 0 && u.GetParameters()[0].ParameterType.HasImplementedRawGeneric(typeof(EventPayload<>)));

            if (!methods.Any()) return;

            // 调用方法
            await InvokeMethods(eventPayload, eventIdMetadata, scoped, eventStoreProvider, eventHandler, methods);
        }

        /// <summary>
        /// 调用符合规则的方法
        /// </summary>
        /// <param name="eventPayload"></param>
        /// <param name="eventIdMetadata"></param>
        /// <param name="scoped"></param>
        /// <param name="eventStoreProvider"></param>
        /// <param name="eventHandler"></param>
        /// <param name="methods"></param>
        /// <returns></returns>
        private static async Task InvokeMethods(EventPayload eventPayload, EventIdMetadata eventIdMetadata, IServiceScope scoped, IEventStoreProvider eventStoreProvider, IEventHandler eventHandler, IEnumerable<MethodInfo> methods)
        {
            foreach (var method in methods)
            {
                // 转换承载数据类型（支持泛型）
                var payload = ConvertPayloadType(eventPayload, method);
                var parameters = new List<object> { payload };

                // 解析贴了 [FromServices] 特性的服务
                var otherParameters = method.GetParameters().Skip(1);
                foreach (var parameterInfo in otherParameters)
                {
                    if (!parameterInfo.IsDefined(typeof(FromServicesAttribute), false)) parameters.Add(default);
                    else parameters.Add(scoped.ServiceProvider.GetService(parameterInfo.ParameterType));
                }

                try
                {
                    // 默认重试 3 次（每次间隔 1s）
                    await Retry.Invoke(async () =>
                    {
                        var result = method.Invoke(eventHandler, parameters.ToArray());
                        if (method.IsAsync()) await (Task)result;
                    }, 3, 1000, finalThrow: true);

                    // 调用成功
                    await eventStoreProvider.ExecuteEventIdSuccessfully(eventIdMetadata);
                }
                catch (Exception ex)
                {
                    // 调用失败
                    await eventStoreProvider.ExecuteEventIdFaildAsync(eventIdMetadata, ex);
                }
            }
        }

        /// <summary>
        /// 转换承载数据类型
        /// </summary>
        /// <param name="eventPayload"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        private static object ConvertPayloadType(EventPayload eventPayload, MethodInfo method)
        {
            object payload;
            if (method.GetParameters()[0].ParameterType.IsGenericType)
            {
                var payloadType = method.GetParameters()[0].ParameterType.GetGenericArguments().First();
                payload = Activator.CreateInstance(method.GetParameters()[0].ParameterType, new object[] { eventPayload.Category, eventPayload.EventId, eventPayload.Payload.ChangeType(payloadType) });
            }
            else payload = eventPayload;
            return payload;
        }
    }
}
