using Furion.DependencyInjection;
using Furion.JsonConverters;
using Furion.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Mvc 拓展类
    /// </summary>
    [SkipScan]
    public static class MvcBuilderServiceCollectionExtensions
    {
        /// <summary>
        /// 配置 Json 序列化属性名大写
        /// </summary>
        /// <param name="mvcBuilder">Mvc构建器</param>
        /// <returns>Mvc构建器</returns>
        public static IMvcBuilder AddJsonSerializerPascalPropertyNaming(this IMvcBuilder mvcBuilder)
        {
            // 存储是否启动大写序列化
            JsonSerializerUtility.EnabledPascalPropertyNaming = true;

            mvcBuilder.AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
            });

            return mvcBuilder;
        }

        /// <summary>
        /// 添加时间格式化
        /// </summary>
        /// <param name="mvcBuilder"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static IMvcBuilder AddDateTimeJsonConverter(this IMvcBuilder mvcBuilder, string format)
        {
            mvcBuilder.AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new DateTimeJsonConverter(format));
                options.JsonSerializerOptions.Converters.Add(new DateTimeOffsetJsonConverter(format));
            });

            return mvcBuilder;
        }

        /// <summary>
        /// 注册 Mvc 过滤器
        /// </summary>
        /// <typeparam name="TFilter"></typeparam>
        /// <param name="mvcBuilder"></param>
        /// <returns></returns>
        public static IMvcBuilder AddMvcFilter<TFilter>(this IMvcBuilder mvcBuilder)
            where TFilter : IFilterMetadata
        {
            mvcBuilder.AddMvcOptions(options => options.Filters.Add<TFilter>());

            return mvcBuilder;
        }

        /// <summary>
        /// 注册 Mvc 过滤器
        /// </summary>
        /// <typeparam name="TFilter"></typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddMvcFilter<TFilter>(this IServiceCollection services)
            where TFilter : IFilterMetadata
        {
            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add<TFilter>();
            });

            return services;
        }
    }
}