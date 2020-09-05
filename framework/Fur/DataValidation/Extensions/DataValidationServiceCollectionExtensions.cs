// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0
// 源码地址：https://gitee.com/monksoul/Fur 
// 开源协议：Apache-2.0（https://gitee.com/monksoul/Fur/blob/alpha/LICENSE）

using Fur.DataValidation;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 友好异常服务拓展类
    /// </summary>
    public static class DataValidationServiceCollectionExtensions
    {
        /// <summary>
        /// 添加全局数据验证
        /// </summary>
        /// <typeparam name="TValidationMessageTypeProvider">验证类型消息提供器</typeparam>
        /// <param name="mvcBuilder"></param>
        /// <param name="enabledGlobalDataValidationFilter">启用全局验证过滤器</param>
        /// <returns></returns>
        public static IMvcBuilder AddDataValidation<TValidationMessageTypeProvider>(this IMvcBuilder mvcBuilder, bool enabledGlobalDataValidationFilter = true)
            where TValidationMessageTypeProvider : class, IValidationMessageTypeProvider
        {
            var services = mvcBuilder.Services;

            // 添加全局数据验证
            mvcBuilder.AddDataValidation(enabledGlobalDataValidationFilter);

            // 单例注册验证消息提供器
            services.TryAddSingleton<IValidationMessageTypeProvider, TValidationMessageTypeProvider>();

            return mvcBuilder;
        }

        /// <summary>
        /// 添加全局数据验证
        /// </summary>
        /// <param name="mvcBuilder"></param>
        /// <param name="enabledGlobalDataValidationFilter">启用全局验证过滤器</param>
        /// <returns></returns>
        public static IMvcBuilder AddDataValidation(this IMvcBuilder mvcBuilder, bool enabledGlobalDataValidationFilter = true)
        {
            var services = mvcBuilder.Services;

            // 添加验证配置文件支持
            services.AddConfigurableOptions<ValidationTypeMessageSettingsOptions>();

            // 判断是否启用全局
            if (enabledGlobalDataValidationFilter)
            {
                // 使用自定义验证
                mvcBuilder.ConfigureApiBehaviorOptions(options =>
                {
                    options.SuppressModelStateInvalidFilter = true;
                });

                // 添加全局数据验证
                mvcBuilder.AddMvcOptions(options => options.Filters.Add<DataValidationFilter>());
            }

            return mvcBuilder;
        }
    }
}