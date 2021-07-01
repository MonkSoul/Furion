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

using Furion.ConfigurableOptions;

namespace Furion.FriendlyException
{
    /// <summary>
    /// 异常配置选项，最优的方式是采用后期配置，也就是所有异常状态码先不设置（推荐）
    /// </summary>
    public sealed class ErrorCodeMessageSettingsOptions : IConfigurableOptions
    {
        /// <summary>
        /// 异常状态码配置列表
        /// </summary>
        public object[][] Definitions { get; set; }
    }
}