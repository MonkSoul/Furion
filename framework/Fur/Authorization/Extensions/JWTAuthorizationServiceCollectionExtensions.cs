using Fur;
using Fur.Authorization;
using Fur.DataEncryption;
using Fur.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

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
        public static IServiceCollection AddJWTAuthorization<TAuthorizationHandler>(this IServiceCollection services, bool enableGlobalAuthorize = false, TokenValidationParameters tokenValidationParameters = default)
            where TAuthorizationHandler : class, IAuthorizationHandler
        {
            services.AddAppAuthorization<TAuthorizationHandler>(options => options.AddJWTAuthorization(), enableGlobalAuthorize);

            return services;
        }

        /// <summary>
        /// 添加 JWT 授权
        /// </summary>
        /// <param name="services"></param>
        /// <param name="tokenValidationParameters">token 验证参数</param>
        /// <returns></returns>
        public static IServiceCollection AddJWTAuthorization(this IServiceCollection services, TokenValidationParameters tokenValidationParameters = default)
        {
            // 注册 JWT 配置
            services.AddConfigurableOptions<JWTSettingsOptions>();

            // 添加默认授权
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = tokenValidationParameters ?? JWTEncryption.CreateTokenValidationParameters(App.GetOptions<JWTSettingsOptions>());
            });

            return services;
        }
    }
}