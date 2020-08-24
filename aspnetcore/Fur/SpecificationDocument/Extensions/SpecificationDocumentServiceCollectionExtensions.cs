using Fur.SpecificationDocument;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class SpecificationDocumentServiceCollectionExtensions
    {
        /// <summary>
        /// 添加规范化文档
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddSpecificationDocuments(this IServiceCollection services)
        {
            // 添加配置
            services.AddAppOptions<SpecificationDocumentSettingsOptions>();

            // 添加Swagger生成器服务
            services.AddSwaggerGen(options => SpecificationDocumentBuilder.BuildGen(options));

            return services;
        }
    }
}