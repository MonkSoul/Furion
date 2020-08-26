using Fur.FriendlyException;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 友好异常服务拓展类
    /// </summary>
    public static class FriendlyExceptionServiceCollectionExtensions
    {
        /// <summary>
        /// 添加友好异常服务拓展服务
        /// </summary>
        /// <typeparam name="ExceptionErrorCodeProvider">异常错误码提供器</typeparam>
        /// <param name="services">服务集合</param>
        /// <returns></returns>
        public static IServiceCollection AddFriendlyException<ExceptionErrorCodeProvider>(this IServiceCollection services)
            where ExceptionErrorCodeProvider : class, IExceptionErrorCodeProvider
        {
            // 单例注册异常状态码提供器
            services.TryAddSingleton<IExceptionErrorCodeProvider, ExceptionErrorCodeProvider>();

            return services;
        }
    }
}