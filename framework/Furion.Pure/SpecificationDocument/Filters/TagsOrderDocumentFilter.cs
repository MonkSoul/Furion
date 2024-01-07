// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using Furion.DynamicApiController;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Furion.SpecificationDocument;

/// <summary>
/// 标签文档排序/注释拦截器
/// </summary>
[SuppressSniffer]
public class TagsOrderDocumentFilter : IDocumentFilter
{
    /// <summary>
    /// 配置拦截
    /// </summary>
    /// <param name="swaggerDoc"></param>
    /// <param name="context"></param>
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        swaggerDoc.Tags = Penetrates.ControllerOrderCollection
            .Where(u => SpecificationDocumentBuilder.GetControllerGroups(u.Value.Item3).Any(c => c.Group == context.DocumentName))
            .OrderByDescending(u => u.Value.Item2)
            .ThenBy(u => u.Key)
            .Select(c => new OpenApiTag
            {
                Name = c.Value.Item1,
                Description = swaggerDoc.Tags.FirstOrDefault(m => m.Name == c.Key)?.Description
            }).ToList();
    }
}