using Fur;
using Fur.Authorization;
using Fur.Core;
using Fur.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;

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
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddJWTAuthorization(this IServiceCollection services)
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
                options.TokenValidationParameters = JWTEncryption.CreateTokenValidationParameters(App.GetOptions<JWTSettingsOptions>());
            });

            return services;
        }
    }
}