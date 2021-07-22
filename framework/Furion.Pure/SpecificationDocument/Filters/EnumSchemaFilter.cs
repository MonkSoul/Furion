// Copyright (c) 2020-2021 百小僧, Baiqian Co.,Ltd.
// Furion is licensed under Mulan PSL v2.
// You can use this software according to the terms and conditions of the Mulan PSL v2.
// You may obtain a copy of Mulan PSL v2 at:
//             http://license.coscl.org.cn/MulanPSL2
// THIS SOFTWARE IS PROVIDED ON AN "AS IS" BASIS, WITHOUT WARRANTIES OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO NON-INFRINGEMENT, MERCHANTABILITY OR FIT FOR A PARTICULAR PURPOSE.
// See the Mulan PSL v2 for more details.

using Furion.DependencyInjection;
using Furion.JsonSerialization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Furion.SpecificationDocument
{
    /// <summary>
    /// 修正 规范化文档 Enum 提示
    /// </summary>
    [SuppressSniffer]
    public class EnumSchemaFilter : ISchemaFilter
    {
        /// <summary>
        /// JSON 序列化
        /// </summary>
        private readonly IJsonSerializerProvider _serializerProvider;

        /// <summary>
        /// 构造函数
        /// </summary>
        public EnumSchemaFilter()
        {
            _serializerProvider = App.RootServices.GetService<IJsonSerializerProvider>();
        }

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
                foreach (var value in enumValues)
                {
                    // 获取枚举成员特性
                    var fieldinfo = type.GetField(Enum.GetName(type, value));
                    var descriptionAttribute = fieldinfo.GetCustomAttribute<DescriptionAttribute>(true);
                    model.Enum.Add(OpenApiAnyFactory.CreateFromJson(_serializerProvider.Serialize(value)));

                    stringBuilder.Append($"&nbsp;{descriptionAttribute?.Description} {value} = {value}<br />");
                }
                model.Description = stringBuilder.ToString();
            }
        }
    }
}