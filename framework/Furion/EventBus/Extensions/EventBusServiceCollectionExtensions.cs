// Copyright (c) 2020-2021 ��Сɮ, Baiqian Co.,Ltd.
// Furion is licensed under Mulan PSL v2.
// You can use this software according to the terms and conditions of the Mulan PSL v2.
// You may obtain a copy of Mulan PSL v2 at:
//             https://gitee.com/dotnetchina/Furion/blob/master/LICENSE
// THIS SOFTWARE IS PROVIDED ON AN "AS IS" BASIS, WITHOUT WARRANTIES OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO NON-INFRINGEMENT, MERCHANTABILITY OR FIT FOR A PARTICULAR PURPOSE.
// See the Mulan PSL v2 for more details.

using Furion.EventBus;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// EventBus ģ�������չ
    /// </summary>
    public static class EventBusServiceCollectionExtensions
    {
        /// <summary>
        /// ��� EventBus ģ��ע��
        /// </summary>
        /// <param name="services">���񼯺϶���</param>
        /// <param name="configureOptionsBuilder">�¼���������ѡ�����ί��</param>
        /// <returns>���񼯺�ʵ��</returns>
        public static IServiceCollection AddEventBus(this IServiceCollection services, Action<EventBusOptionsBuilder> configureOptionsBuilder)
        {
            // ������ʼ�¼���������ѡ�����
            var eventBusOptionsBuilder = new EventBusOptionsBuilder();
            configureOptionsBuilder.Invoke(eventBusOptionsBuilder);

            return services.AddEventBus(eventBusOptionsBuilder);
        }

        /// <summary>
        /// ��� EventBus ģ��ע��
        /// </summary>
        /// <param name="services">���񼯺϶���</param>
        /// <param name="eventBusOptionsBuilder">�¼���������ѡ�����</param>
        /// <returns>���񼯺�ʵ��</returns>
        public static IServiceCollection AddEventBus(this IServiceCollection services, EventBusOptionsBuilder eventBusOptionsBuilder = default)
        {
            // ��ʼ���¼�����������
            eventBusOptionsBuilder ??= new EventBusOptionsBuilder();

            // ע���ڲ�����
            services.AddInternalService(eventBusOptionsBuilder);

            // ͨ������ģʽ����
            services.AddHostedService(serviceProvider =>
            {
                // �����¼����ߺ�̨�������
                var eventBusHostedService = ActivatorUtilities.CreateInstance<EventBusHostedService>(serviceProvider);

                // ����δ��������쳣�¼�
                var unobservedTaskExceptionHandler = eventBusOptionsBuilder.UnobservedTaskExceptionHandler;
                if (unobservedTaskExceptionHandler != default)
                {
                    eventBusHostedService.UnobservedTaskException += unobservedTaskExceptionHandler;
                }

                return eventBusHostedService;
            });

            // �����¼����߷���
            eventBusOptionsBuilder.Build(services);

            return services;
        }

        /// <summary>
        /// ע���ڲ�����
        /// </summary>
        /// <param name="services">���񼯺϶���</param>
        /// <param name="eventBusOptions">�¼���������ѡ��</param>
        /// <returns>���񼯺�ʵ��</returns>
        private static IServiceCollection AddInternalService(this IServiceCollection services, EventBusOptionsBuilder eventBusOptions)
        {
            // ע���̨������нӿ�/ʵ��Ϊ���������ù�����ʽ����
            services.AddSingleton<IEventSourceStorer>(_ =>
            {
                // ����Ĭ���ڴ�ͨ���¼�Դ����
                return new ChannelEventSourceStorer(eventBusOptions.ChannelCapacity);
            });

            // ע��Ĭ���ڴ�ͨ���¼�������
            services.AddSingleton<IEventPublisher, ChannelEventPublisher>();

            return services;
        }
    }
}