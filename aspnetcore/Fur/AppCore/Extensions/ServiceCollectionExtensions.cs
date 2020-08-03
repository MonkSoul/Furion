using Fur.AppCore.Attributes;
using Fur.AppCore.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Fur.AppCore.Extensions
{
    /// <summary>
    /// 选项服务拓展类
    /// </summary>
    [NonInflated]
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 配置选项
        /// </summary>
        /// <typeparam name="TOptions">选项类型</typeparam>
        /// <param name="services">服务集合</param>
        /// <param name="success">配置成功回调</param>
        /// <param name="configuration">应用配置对象</param>
        /// <returns>新的服务集合</returns>
        public static IServiceCollection AddFurOptions<TOptions>(this IServiceCollection services, Action<IFurOptions> success, IConfiguration configuration)
            where TOptions : class, IFurOptions
        {
            var settings = configuration.GetSection(typeof(TOptions).Name);
            services.AddOptions<TOptions>().Bind(settings).ValidateDataAnnotations();
            success?.Invoke(settings.Get<AppOptions>());
            return services;
        }
    }
}