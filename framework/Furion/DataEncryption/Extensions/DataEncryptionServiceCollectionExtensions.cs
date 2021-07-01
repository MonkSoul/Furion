// -----------------------------------------------------------------------------
// 让 .NET 开发更简单，更通用，更流行。
// Copyright © 2020-2021 Furion, 百小僧, Baiqian Co.,Ltd.
//
// 框架名称：Furion
// 框架作者：百小僧
// 框架版本：2.11.1
// 源码地址：Gitee： https://gitee.com/dotnetchina/Furion
//          Github：https://github.com/monksoul/Furion
// 开源协议：Apache-2.0（https://gitee.com/dotnetchina/Furion/blob/master/LICENSE）
// -----------------------------------------------------------------------------

using Furion.DataEncryption;
using Furion.DependencyInjection;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// PBKDF2 加密服务拓展
    /// </summary>
    [SuppressSniffer]
    public static class DataEncryptionServiceCollectionExtensions
    {
        /// <summary>
        /// 注册 PBKDF2 加密服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddPBKDF2EncryptionOptions(this IServiceCollection services)
        {
            // 添加默认配置
            services.AddConfigurableOptions<PBKDF2EncryptionSettingsOptions>();

            return services;
        }
    }
}