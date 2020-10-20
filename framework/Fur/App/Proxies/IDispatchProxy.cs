// -----------------------------------------------------------------------------
// Fur 是 .NET 5 平台下企业应用开发最佳实践框架。
// Copyright © 2020 Fur, Baiqian Co.,Ltd.
//
// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0-rc.final.17
// 官方网站：https://chinadot.net
// 源码地址：Gitee：https://gitee.com/monksoul/Fur
// 				    Github：https://github.com/monksoul/Fur
// 开源协议：Apache-2.0（http://www.apache.org/licenses/LICENSE-2.0）
// -----------------------------------------------------------------------------

using System;

namespace Fur
{
    /// <summary>
    /// 代理拦截依赖接口
    /// </summary>
    public interface IDispatchProxy
    {
        /// <summary>
        /// 实例
        /// </summary>
        object Target { get; set; }

        /// <summary>
        /// 服务提供器
        /// </summary>
        IServiceProvider Services { get; set; }
    }
}