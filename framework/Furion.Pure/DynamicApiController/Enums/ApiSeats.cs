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
using System.ComponentModel;

namespace Microsoft.AspNetCore.Mvc
{
    /// <summary>
    /// 接口参数位置
    /// </summary>
    [SuppressSniffer]
    public enum ApiSeats
    {
        /// <summary>
        /// 控制器之前
        /// </summary>
        [Description("控制器之前")]
        ControllerStart,

        /// <summary>
        /// 控制器之后
        /// </summary>
        [Description("控制器之后")]
        ControllerEnd,

        /// <summary>
        /// 行为之前
        /// </summary>
        [Description("行为之前")]
        ActionStart,

        /// <summary>
        /// 行为之后
        /// </summary>
        [Description("行为之后")]
        ActionEnd
    }
}