using Fur.Authorization;
using Microsoft.AspNetCore.Authorization;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 策略授权服务拓展类
    /// </summary>
    public static class PolicyAuthorizationServiceCollectionExtensions
    {
        /// <summary>
        /// 添加策略授权服务
        /// </summary>
        /// <typeparam name="TAuthorizationHandler">策略授权处理程序</typeparam>
        /// <param name="services">服务集合</param>
        /// <param name="configure">自定义配置</param>
        /// <returns>服务集合</returns>
        public static IServiceCollection AddPolicyAuthorizations<TAuthorizationHandler>(this IServiceCollection services, Action<IServiceCollection> configure = null)
            where TAuthorizationHandler : class, IAuthorizationHandler
        {
            // 注册授权策略提供器
            services.AddSingleton<IAuthorizationPolicyProvider, AuthorizePolicyProvider>();

            // 注册策略授权处理程序
            services.AddSingleton<IAuthorizationHandler, TAuthorizationHandler>();

            configure?.Invoke(services);
            return services;
        }
    }
}