using Fur.DependencyInjection;
using System.Text.Encodings.Web;

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
        public static IMvcBuilder AddBaseJsonOptions(this IMvcBuilder mvcBuilder)
        {
            mvcBuilder.AddJsonOptions(options =>
            {
                //options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
                options.JsonSerializerOptions.WriteIndented = true;
                options.JsonSerializerOptions.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
                options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
            });

            return mvcBuilder;
        }

        /// <summary>
        /// 配置 Json 序列化属性名大写
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