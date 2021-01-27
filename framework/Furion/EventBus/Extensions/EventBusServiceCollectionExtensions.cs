using Furion;
using Furion.DependencyInjection;
using Furion.EventBus;
using System;
using System.Linq;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 轻量级事件总线服务拓展
    /// </summary>
    [SkipScan]
    public static class EventBusServiceCollectionExtensions
    {
        /// <summary>
        /// 添加轻量级事件总线服务拓展
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddSimpleEventBus(this IServiceCollection services)
        {
            // 查找所有订阅处理程序
            var subscribeHandlerTypes = App.EffectiveTypes
                .Where(u => typeof(ISubscribeHandler).IsAssignableFrom(u) && u.IsClass && !u.IsInterface && !u.IsAbstract);

            // 查找所有贴了 [SubscribeMessage] 特性的方法，并且含有两个参数，第一个参数为 string messageId，第二个参数为 object payload
            var methods = subscribeHandlerTypes
                .SelectMany(u => u.GetMethods(BindingFlags.Public | BindingFlags.Instance)
                    .Where(m => m.IsDefined(typeof(SubscribeMessageAttribute), false)
                                                && m.GetParameters().Length == 2
                                                && m.GetParameters()[0].ParameterType == typeof(string)
                                                && m.GetParameters()[1].ParameterType == typeof(object)
                                                && m.ReturnType == typeof(void)));

            // 遍历所有订阅类型
            foreach (var handlerType in subscribeHandlerTypes)
            {
                // 获取所有符合定义的消息定义方法
                var subscribeMethods = handlerType.GetMethods(BindingFlags.Public | BindingFlags.Instance)
                                                                    .Where(m => m.IsDefined(typeof(SubscribeMessageAttribute), false)
                                                                                                && m.GetParameters().Length == 2
                                                                                                && m.GetParameters()[0].ParameterType == typeof(string)
                                                                                                && m.GetParameters()[1].ParameterType == typeof(object)
                                                                                                && m.ReturnType == typeof(void));

                if (!subscribeMethods.Any()) continue;

                // 创建订阅类型实例
                var instance = Activator.CreateInstance(handlerType);

                // 批量注册
                foreach (var method in methods)
                {
                    // 创建委托类型
                    var action = (Action<string, object>)Delegate.CreateDelegate(typeof(Action<string, object>), instance, method.Name);

                    // 获取所有消息特性
                    var subscribeMessageAttributes = method.GetCustomAttributes<SubscribeMessageAttribute>();

                    foreach (var subscribeMessageAttribute in subscribeMessageAttributes)
                    {
                        if (string.IsNullOrEmpty(subscribeMessageAttribute.MessageId)) continue;

                        InternalMessageCenter.Instance.Subscribe(handlerType, subscribeMessageAttribute.MessageId, action);
                    }
                }
            }

            return services;
        }
    }
}