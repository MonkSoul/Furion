using Furion.DependencyInjection;
using Furion.JsonSerialization;

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
    }
}