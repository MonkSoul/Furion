// MIT License
//
// Copyright (c) 2020-2022 百小僧, Baiqian Co.,Ltd.
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using Furion.Extensions;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace Furion.SpecificationDocument;

/// <summary>
/// 修正 规范化文档 Enum 提示
/// </summary>
[SuppressSniffer]
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

        // 排除其他程序集的枚举
        if (type.IsEnum && App.Assemblies.Contains(type.Assembly))
        {
            model.Enum.Clear();
            var stringBuilder = new StringBuilder();
            stringBuilder.Append($"{model.Description}<br />");

            var enumValues = Enum.GetValues(type);
            // 获取枚举实际值类型
            var enumValueType = type.GetField("value__").FieldType;

            foreach (var value in enumValues)
            {
                var numValue = value.ChangeType(enumValueType);

                // 获取枚举成员特性
                var fieldinfo = type.GetField(Enum.GetName(type, value));
                var descriptionAttribute = fieldinfo.GetCustomAttribute<DescriptionAttribute>(true);
                model.Enum.Add(OpenApiAnyFactory.CreateFromJson($"{numValue}"));

                stringBuilder.Append($"&nbsp;{descriptionAttribute?.Description} {value} = {numValue}<br />");
            }
            model.Description = stringBuilder.ToString();
        }
    }
}