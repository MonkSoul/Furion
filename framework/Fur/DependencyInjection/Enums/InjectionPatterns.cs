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

namespace Fur.DependencyInjection
{
    /// <summary>
    /// 注册范围
    /// </summary>
    [SkipScan]
    public enum InjectionPatterns
    {
        /// <summary>
        /// 只注册自己
        /// </summary>
        Self,

        /// <summary>
        /// 第一个接口，默认值
        /// </summary>
        FirstInterface,

        /// <summary>
        /// 自己和第一个接口，默认值
        /// </summary>
        SelfWithFirstInterface,

        /// <summary>
        /// 所有接口
        /// </summary>
        ImplementedInterfaces,

        /// <summary>
        /// 注册自己包括所有接口
        /// </summary>
        All
    }
}