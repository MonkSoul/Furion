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
    internal sealed class EventDispatcher : ChannelHandler<EventMessage>
    {
        /// <summary>
        /// 调度核心代码
        /// </summary>
        /// <param name="eventMessage"></param>
        /// <returns></returns>
        public async override Task InvokeAsync(EventMessage eventMessage)
        {
            // 解析服务工厂及创建作用域
            var serviceScopeFactory = App.GetService<IServiceScopeFactory>(App.RootServices);
            using var scoped = serviceScopeFactory.CreateScope();

            // 解析事件消息元数据
            var eventStoreProvider = scoped.ServiceProvider.GetService<IEventStoreProvider>();
            var eventMessageMetadata = await eventStoreProvider?.GetEventMessageAsync(eventMessage.Category, eventMessage.EventId);
            if (eventMessageMetadata == null) return;

            // 获取解析事件处理程序服务委托并创建事件处理程序
            var eventHandlerResolve = scoped.ServiceProvider.GetService<Func<EventMessageMetadata, IEventHandler>>();
            var eventHandler = eventHandlerResolve(eventMessageMetadata);
            if (eventHandler == null) return;

            // 查找所有符合的处理方法，贴了 [EventId] 或 方法名相等的
            var methods = eventHandler.GetType().GetTypeInfo().DeclaredMethods
                                   .Where(u => !u.IsStatic)
                                   .Where(u => u.Name.ClearStringAffixes(1, "Async") == eventMessage.EventId
                                       || (u.IsDefined(typeof(EventMessageAttribute), false) && u.GetCustomAttributes<EventMessageAttribute>(false).Any(e => e.EventId == eventMessage.EventId)))
                                   .Where(u => u.ReturnType == typeof(void) || u.ReturnType == typeof(Task))
                                   .Where(u => u.GetParameters().Length > 0 && u.GetParameters()[0].ParameterType.HasImplementedRawGeneric(typeof(EventMessage<>)));

            if (!methods.Any()) return;

            // 调用方法
            await InvokeAsync(methods, eventMessage, eventMessageMetadata
                , scoped, eventStoreProvider, eventHandler);
        }

        /// <summary>
        /// 调用符合规则的方法
        /// </summary>
        /// <param name="methods"></param>
        /// <param name="eventPayload"></param>
        /// <param name="eventMessageMetadata"></param>
        /// <param name="scoped"></param>
        /// <param name="eventStoreProvider"></param>
        /// <param name="eventHandler"></param>
        /// <returns></returns>
        private static async Task InvokeAsync(IEnumerable<MethodInfo> methods
            , EventMessage eventPayload
            , EventMessageMetadata eventMessageMetadata
            , IServiceScope scoped
            , IEventStoreProvider eventStoreProvider
            , IEventHandler eventHandler)
        {
            foreach (var method in methods)
            {
                // 处理泛型事件消息承载数据
                var parameters = new List<object> { ConvertGenericPayload(eventPayload, method) };

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

                    // 触发调用成功处理方法
                    await eventStoreProvider.ExecuteSuccessfullyAsync(eventMessageMetadata);
                }
                catch (Exception exception)
                {
                    // 触发调用失败处理方法
                    await eventStoreProvider.ExecuteFaildedAsync(eventMessageMetadata, exception);
                }
            }
        }

        /// <summary>
        /// 处理泛型消息承载数据
        /// </summary>
        /// <param name="eventMessage"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        private static object ConvertGenericPayload(EventMessage eventMessage, MethodInfo method)
        {
            object payload;
            if (method.GetParameters()[0].ParameterType.IsGenericType)
            {
                var payloadType = method.GetParameters()[0].ParameterType.GetGenericArguments().First();
                payload = Activator.CreateInstance(method.GetParameters()[0].ParameterType
                    , new object[] { eventMessage.Category, eventMessage.EventId, eventMessage.Payload.ChangeType(payloadType) });
            }
            else payload = eventMessage;
            return payload;
        }
    }
}
