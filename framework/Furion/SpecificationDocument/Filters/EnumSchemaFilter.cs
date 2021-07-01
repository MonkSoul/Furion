// -----------------------------------------------------------------------------
// 让 .NET 开发更简单，更通用，更流行。
// Copyright © 2020-2021 Furion, 百小僧, Baiqian Co.,Ltd.
//
// 框架名称：Furion
// 框架作者：百小僧
// 框架版本：2.11.1
// 源码地址：Gitee： https://gitee.com/dotnetchina/Furion
//          Github：https://github.com/monksoul/Furion
// 开源协议：Apache-2.0（https://gitee.com/dotnetchina/Furion/blob/master/LICENSE）
// -----------------------------------------------------------------------------

using Furion.DependencyInjection;
using Furion.JsonSerialization;
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
        /// <param name="serializerProvider"></param>
        public EnumSchemaFilter(IJsonSerializerProvider serializerProvider)
        {
            _serializerProvider = serializerProvider;
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