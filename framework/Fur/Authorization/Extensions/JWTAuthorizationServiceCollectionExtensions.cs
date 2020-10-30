using Fur;
using Fur.DependencyInjection;
using Microsoft.AspNetCore.Authorization;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// JWT 授权服务拓展类
    /// </summary>
    [SkipScan]
    public static class JWTAuthorizationServiceCollectionExtensions
    {
        /// <summary>
        /// 添加 JWT 授权
        /// </summary>
        /// <typeparam name="TAuthorizationHandler"></typeparam>
        /// <param name="services"></param>
        /// <param name="enableGlobalAuthorize"></param>
        /// <param name="tokenValidationParameters"></param>
        /// <returns></returns>
        public static IServiceCollection AddJWTAuthorization<TAuthorizationHandler>(this IServiceCollection services, bool enableGlobalAuthorize = false, object tokenValidationParameters = default)
            where TAuthorizationHandler : class, IAuthorizationHandler
        {
            // 加载程序集
            var jwtExtraAssembly = Assembly.Load(AppExtra.AUTHENTICATION_JWTBEARER);

            // 加载 jwt 拓展类型和拓展方法
            var jwtAuthorizationServiceCollectionExtensionsType = jwtExtraAssembly.GetType($"Microsoft.Extensions.DependencyInjection.JWTAuthorizationServiceCollectionExtensions");
            var addJWTAuthorizationMethod = jwtAuthorizationServiceCollectionExtensionsType.GetMethod("AddJWTAuthorization", BindingFlags.Public | BindingFlags.Static);

            // 添加 JWT 授权
            services.AddAppAuthorization<TAuthorizationHandler>(options =>
            {
                addJWTAuthorizationMethod.Invoke(null, new object[] { services, tokenValidationParameters });
            }, enableGlobalAuthorize);

            return services;
        }
    }
}