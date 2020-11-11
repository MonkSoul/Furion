using Fur;
using Fur.DependencyInjection;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Linq;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// JWT 授权服务拓展类
    /// </summary>
    [SkipScan]
    public static class JwtAuthorizationServiceCollectionExtensions
    {
        /// <summary>
        /// 添加 JWT 授权
        /// </summary>
        /// <typeparam name="TAuthorizationHandler"></typeparam>
        /// <param name="services"></param>
        /// <param name="configureOptions"></param>
        /// <param name="enableGlobalAuthorize"></param>
        /// <param name="tokenValidationParameters"></param>
        /// <returns></returns>
        public static AuthenticationBuilder AddJwt<TAuthorizationHandler>(this IServiceCollection services, Action<AuthenticationOptions> configureOptions = null, object tokenValidationParameters = default, bool enableGlobalAuthorize = false)
            where TAuthorizationHandler : class, IAuthorizationHandler
        {
            // 加载程序集
            var jwtExtraAssembly = Assembly.Load(AppExtra.AUTHENTICATION_JWTBEARER);

            // 加载 jwt 拓展类型和拓展方法
            var jwtAuthorizationServiceCollectionExtensionsType = jwtExtraAssembly.GetType($"Microsoft.Extensions.DependencyInjection.JWTAuthorizationServiceCollectionExtensions");
            var addJwtMethod = jwtAuthorizationServiceCollectionExtensionsType
                .GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Where(u => u.Name == "AddJwt" && u.GetParameters().Length > 0 && u.GetParameters()[0].ParameterType == typeof(IServiceCollection)).First();

            // 添加 JWT 授权
            services.AddAppAuthorization<TAuthorizationHandler>(null, enableGlobalAuthorize);

            return addJwtMethod.Invoke(null, new object[] { services, configureOptions, tokenValidationParameters }) as AuthenticationBuilder;
        }
    }
}