using Fur.DependencyInjection;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Text;

namespace Fur.SpecificationDocument
{
    /// <summary>
    /// 修正 规范化文档 Enum 提示
    /// </summary>
    /// <remarks>
    /// 参考https://github.com/domaindrivendev/Swashbuckle.AspNetCore/issues/1269#issuecomment-577182931
    /// </remarks>
    [SkipScan]
    public class EnumSchemaFilter : ISchemaFilter
    {
        /// <summary>
        /// 实现过滤器方法
        /// </summary>
        /// <param name="model"></param>
        /// <param name="context"></param>
        public void Apply(OpenApiSchema model, SchemaFilterContext context)
        {
            var type = context.Type;
            if (type.IsEnum)
            {
                model.Enum.Clear();
                var stringBuilder = new StringBuilder();

                var enumValues = Enum.GetValues(type);
                foreach (var option in enumValues)
                {
                    var value = (int)option;
                    model.Enum.Add(new OpenApiInteger(value));
                    stringBuilder.Append($"{option} = {value}<br />");
                }
                model.Description = stringBuilder.ToString();
            }
        }
    }
}