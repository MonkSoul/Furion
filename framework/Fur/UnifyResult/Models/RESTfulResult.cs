// -----------------------------------------------------------------------------
// Fur 是 .NET 5 平台下企业应用开发最佳实践框架。
// Copyright © 2020 Fur, Baiqian Co.,Ltd.
//
// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0-rc.final.20
// 官方网站：https://chinadot.net
// 源码地址：Gitee：https://gitee.com/monksoul/Fur
// 				    Github：https://github.com/monksoul/Fur
// 开源协议：Apache-2.0（http://www.apache.org/licenses/LICENSE-2.0）
// -----------------------------------------------------------------------------

using Fur.DependencyInjection;

namespace Fur.UnifyResult
{
    /// <summary>
    /// RESTful 风格结果集
    /// </summary>
    [SkipScan]
    public class RESTfulResult
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public int? StatusCode { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// 执行成功
        /// </summary>
        public bool Successed { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public object Errors { get; set; }
    }
}