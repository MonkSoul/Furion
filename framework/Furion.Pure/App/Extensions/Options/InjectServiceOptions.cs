// -----------------------------------------------------------------------------
// 让 .NET 开发更简单，更通用，更流行。
// Copyright © 2020-2021 Furion, 百小僧, Baiqian Co.,Ltd.
//
// 框架名称：Furion
// 框架作者：百小僧
// 框架版本：2.14.1
// 源码地址：Gitee： https://gitee.com/dotnetchina/Furion
//          Github：https://github.com/monksoul/Furion
// 开源协议：Apache-2.0（https://gitee.com/dotnetchina/Furion/blob/master/LICENSE）
// -----------------------------------------------------------------------------

using Furion.DataValidation;
using Furion.FriendlyException;
using Furion.SpecificationDocument;
using System;

namespace Furion
{
    /// <summary>
    /// Inject 服务配置选项
    /// </summary>
    public sealed class InjectServiceOptions
    {
        /// <summary>
        /// 规范化结果配置
        /// </summary>
        public Action<SpecificationDocumentServiceOptions> SpecificationDocumentConfigure { get; set; }

        /// <summary>
        /// 数据校验配置
        /// </summary>
        public Action<DataValidationServiceOptions> DataValidationConfigure { get; set; }

        /// <summary>
        /// 友好异常配置
        /// </summary>
        public Action<FriendlyExceptionServiceOptions> FriendlyExceptionConfigure { get; set; }
    }
}