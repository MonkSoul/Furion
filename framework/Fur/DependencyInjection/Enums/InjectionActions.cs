// -----------------------------------------------------------------------------
// Fur 是 .NET 5 平台下极易入门、极速开发的 Web 应用框架。
// Copyright © 2020 Fur, Baiqian Co.,Ltd.
//
// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0-rc1
// 官方网站：https://chinadot.net
// 源码地址：Gitee：https://gitee.com/monksoul/Fur 
// 				    Github：https://github.com/monksoul/Fur 
// 开源协议：Apache-2.0（http://www.apache.org/licenses/LICENSE-2.0）
// -----------------------------------------------------------------------------

namespace Fur.DependencyInjection
{
    /// <summary>
    /// 服务注册方式
    /// </summary>
    [SkipScan]
    public enum InjectionActions
    {
        /// <summary>
        /// 如果存在则覆盖
        /// </summary>
        Add,

        /// <summary>
        /// 如果存在则跳过，默认方式
        /// </summary>
        TryAdd
    }
}