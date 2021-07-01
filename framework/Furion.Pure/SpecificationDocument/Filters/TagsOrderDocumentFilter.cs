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
using Furion.DynamicApiController;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;

namespace Furion.SpecificationDocument
{
    /// <summary>
    /// 标签文档排序拦截器
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
            if (Penetrates.ControllerOrderCollection.ContainsKey(tag))
            {
                return Penetrates.ControllerOrderCollection[tag];
            }
            return default;
        }
    }
}