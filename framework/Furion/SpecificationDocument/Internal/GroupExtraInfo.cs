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

namespace Furion.SpecificationDocument
{
    /// <summary>
    /// 分组附加信息
    /// </summary>
    internal sealed class GroupExtraInfo
    {
        /// <summary>
        /// 分组名
        /// </summary>
        public string Group { get; set; }

        /// <summary>
        /// 分组排序
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// 是否可见
        /// </summary>
        public bool Visible { get; set; }
    }
}