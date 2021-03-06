using Furion.DependencyInjection;
using Furion.JsonSerialization;
using Furion.Utilities;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Json 序列化服务拓展类
    /// </summary>
    [SkipScan]
    public static class JsonSerializationServiceCollectionExtensions
    {
        /// <summary>
        /// 配置 Json 序列化提供器
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddJsonSerialization(this IServiceCollection services)
        {
            services.AddSingleton<IJsonSerializerProvider, SystemTextJsonSerializerProvider>();
            return services;
        }

        /// <summary>
        /// 配置 Json 序列化提供器
        /// </summary>
        /// <typeparam name="TJsonSerializerProvider"></typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddJsonSerialization<TJsonSerializerProvider>(this IServiceCollection services)
            where TJsonSerializerProvider : class, IJsonSerializerProvider
        {
            services.AddSingleton<IJsonSerializerProvider, TJsonSerializerProvider>();
            return services;
        }

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
    }
}