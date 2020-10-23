// sdfdsfdsdfsd

using Fur;
using Fur.CorsAccessor;
using Fur.DependencyInjection;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 跨域访问服务拓展类
    /// </summary>
    [SkipScan]
    public static class CorsAccessorServiceCollectionExtensions
    {
        /// <summary>
        /// 配置跨域
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <returns>服务集合</returns>
        public static IServiceCollection AddCorsAccessor(this IServiceCollection services)
        {
            // 添加跨域配置选项
            services.AddConfigurableOptions<CorsAccessorSettingsOptions>();

            // 获取选项
            var corsAccessorSettings = App.GetOptions<CorsAccessorSettingsOptions>();

            // 添加跨域服务
            services.AddCors(options =>
            {
                // 添加策略跨域
                options.AddPolicy(name: corsAccessorSettings.PolicyName, builder =>
                {
                    // 判断是否设置了来源，因为 AllowAnyOrigin 不能和 AllowCredentials一起公用
                    var isNotSetOrigins = corsAccessorSettings.WithOrigins == null || corsAccessorSettings.WithOrigins.Length == 0;

                    // 如果没有配置来源，则允许所有来源
                    if (isNotSetOrigins) builder.AllowAnyOrigin();
                    else builder.WithOrigins(corsAccessorSettings.WithOrigins)
                                      .SetIsOriginAllowedToAllowWildcardSubdomains();

                    // 如果没有配置请求标头，则允许所有表头
                    if (corsAccessorSettings.WithHeaders == null || corsAccessorSettings.WithHeaders.Length == 0) builder.AllowAnyHeader();
                    else builder.WithHeaders(corsAccessorSettings.WithHeaders);

                    // 如果没有配置任何请求谓词，则允许所有请求谓词
                    if (corsAccessorSettings.WithMethods == null || corsAccessorSettings.WithMethods.Length == 0) builder.AllowAnyMethod();
                    else builder.WithMethods(corsAccessorSettings.WithMethods);

                    // 配置跨域凭据
                    if (corsAccessorSettings.AllowCredentials == true && !isNotSetOrigins) builder.AllowCredentials();

                    // 配置响应头
                    if (corsAccessorSettings.WithExposedHeaders != null && corsAccessorSettings.WithExposedHeaders.Length > 0) builder.WithExposedHeaders();

                    // 设置预检过期时间
                    if (corsAccessorSettings.SetPreflightMaxAge.HasValue) builder.SetPreflightMaxAge(TimeSpan.FromSeconds(corsAccessorSettings.SetPreflightMaxAge.Value));
                });
            });

            // 添加响应压缩
            services.AddResponseCaching();

            return services;
        }
    }
}