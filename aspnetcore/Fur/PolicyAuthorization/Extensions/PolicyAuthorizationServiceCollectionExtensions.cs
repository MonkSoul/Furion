using Fur.PolicyAuthorization;
using Microsoft.AspNetCore.Authorization;

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
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddPolicyAuthorizations<TAuthorizationHandler>(this IServiceCollection services)
            where TAuthorizationHandler : class, IAuthorizationHandler
        {
            // 注册授权策略提供器
            services.AddSingleton<IAuthorizationPolicyProvider, AuthorizePolicyProvider>();

            // 注册策略授权处理程序
            services.AddSingleton<IAuthorizationHandler, TAuthorizationHandler>();

            return services;
        }
    }
}