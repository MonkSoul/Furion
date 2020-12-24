using Furion.DependencyInjection;
using Furion.DynamicApiController;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;

namespace Furion.SpecificationDocument
{
    /// <summary>
    /// 标签文档排序拦截器
    /// </summary>
    [SkipScan]
    public class TagsOrderDocumentFilter : IDocumentFilter
    {
        /// <summary>
        /// 配置拦截
        /// </summary>
        /// <param name="swaggerDoc"></param>
        /// <param name="context"></param>
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            swaggerDoc.Tags = swaggerDoc.Tags
                                        .OrderByDescending(u => GetTagOrder(u.Name))
                                        .ThenBy(u => u.Name).ToArray();
        }

        /// <summary>
        /// 获取标签排序
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        private static int GetTagOrder(string tag)
        {
            return Penetrates.ControllerOrderCollection[tag];
        }
    }
}