using Furion.DataEncryption;
using Furion.DependencyInjection;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// PBKDF2 加密服务拓展
    /// </summary>
    [SkipScan]
    public static class DataEncryptionServiceCollectionExtensions
    {
        /// <summary>
        /// 注册 PBKDF2 加密服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddPBKDF2Options(this IServiceCollection services)
        {
            // 添加默认配置
            services.AddConfigurableOptions<PBKDF2SettingsOptions>();

            return services;
        }
    }
}