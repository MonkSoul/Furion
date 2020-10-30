using Fur.Authorization;
using Fur.DataEncryption;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// JWT 授权服务拓展类
    /// </summary>
    public static class JWTAuthorizationServiceCollectionExtensions
    {
        /// <summary>
        /// 添加 JWT 授权
        /// </summary>
        /// <param name="services"></param>
        /// <param name="tokenValidationParameters">token 验证参数</param>
        /// <returns></returns>
        public static IServiceCollection AddJWTAuthorization(this IServiceCollection services, object tokenValidationParameters = default)
        {
            // 获取配置节点
            var jwtSettingsConfiguration = services.BuildServiceProvider()
                        .GetService<IConfiguration>()
                        .GetSection("JWTSettings");

            // 配置验证
            services.AddOptions<JWTSettingsOptions>()
                        .Bind(jwtSettingsConfiguration)
                        .ValidateDataAnnotations();

            // 选项后期配置
            services.PostConfigure<JWTSettingsOptions>(options =>
            {
                options.ValidateIssuerSigningKey ??= true;
                if (options.ValidateIssuerSigningKey == true)
                {
                    options.IssuerSigningKey ??= "U2FsdGVkX1+6H3D8Q//yQMhInzTdRZI9DbUGetbyaag=";
                }
                options.ValidateIssuer ??= true;
                if (options.ValidateIssuer == true)
                {
                    options.ValidIssuer = "dotnetchina";
                }
                options.ValidateAudience ??= true;
                if (options.ValidateAudience == true)
                {
                    options.ValidAudience = "powerby Fur";
                }
                options.ValidateLifetime ??= true;
                if (options.ValidateLifetime == true)
                {
                    options.ClockSkew ??= 10;
                }
                options.ExpiredTime ??= 20;
            });

            var jwtSettings = services.BuildServiceProvider().GetService<IOptions<JWTSettingsOptions>>().Value;

            // 添加默认授权
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = (tokenValidationParameters as TokenValidationParameters) ?? JWTEncryption.CreateTokenValidationParameters(jwtSettings);
            });

            return services;
        }
    }
}