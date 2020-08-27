using Fur.DataValidation;

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
        /// <param name="mvcBuilder"></param>
        /// <returns></returns>
        public static IMvcBuilder AddDataValidation(this IMvcBuilder mvcBuilder)
        {
            var services = mvcBuilder.Services;

            // 使用自定义验证
            mvcBuilder.ConfigureApiBehaviorOptions(options =>
             {
                 options.SuppressModelStateInvalidFilter = true;
             });

            // 添加全局数据验证
            mvcBuilder.AddMvcOptions(options => options.Filters.Add<DataValidationFilter>());

            return mvcBuilder;
        }
    }
}