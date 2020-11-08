using Fur.DependencyInjection;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Mvc 拓展类
    /// </summary>
    [SkipScan]
    public static class MvcBuilderServiceCollectionExtensions
    {
        /// <summary>
        /// 添加 JsonSerializer 序列化
        /// </summary>
        /// <param name="mvcBuilder">Mvc构建器</param>
        /// <returns>Mvc构建器</returns>
        public static IMvcBuilder AddJsonSerializerPascalPropertyNaming(this IMvcBuilder mvcBuilder)
        {
            mvcBuilder.AddJsonOptions(options =>
             {
                 options.JsonSerializerOptions.PropertyNamingPolicy = null;
             });

            return mvcBuilder;
        }
    }
}