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

namespace Furion.FriendlyException
{
    /// <summary>
    /// 友好异常服务配置选项
    /// </summary>
    public sealed class FriendlyExceptionServiceOptions
    {
        /// <summary>
        /// 是否启用全局友好异常
        /// </summary>
        public bool EnabledGlobalFriendlyException { get; set; } = true;
    }
}