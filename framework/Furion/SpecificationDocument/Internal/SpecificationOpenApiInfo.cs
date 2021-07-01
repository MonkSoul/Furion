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

namespace Furion.SpecificationDocument
{
    /// <summary>
    /// 规范化文档开放接口信息
    /// </summary>
    [SuppressSniffer]
    public sealed class SpecificationOpenApiInfo : OpenApiInfo
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public SpecificationOpenApiInfo()
        {
            Version = "1.0.0";
        }

        /// <summary>
        /// 分组私有字段
        /// </summary>
        private string _group;

        /// <summary>
        /// 所属组
        /// </summary>
        public string Group
        {
            get => _group;
            set
            {
                _group = value;
                Title ??= string.Join(' ', Penetrates.SplitCamelCase(_group));
            }
        }

        /// <summary>
        /// 排序
        /// </summary>
        public int? Order { get; set; }

        /// <summary>
        /// 是否可见
        /// </summary>
        public bool? Visible { get; set; }
    }
}