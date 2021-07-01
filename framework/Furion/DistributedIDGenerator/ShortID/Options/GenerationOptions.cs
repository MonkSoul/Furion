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

namespace Furion.DistributedIDGenerator
{
    /// <summary>
    /// 短 ID 生成配置选项
    /// </summary>
    [SuppressSniffer]
    public class GenerationOptions
    {
        /// <summary>
        /// 是否使用数字
        /// <para>默认 false</para>
        /// </summary>
        public bool UseNumbers { get; set; }

        /// <summary>
        /// 是否使用特殊字符
        /// <para>默认 true</para>
        /// </summary>
        public bool UseSpecialCharacters { get; set; } = true;

        /// <summary>
        /// 设置短 ID 长度
        /// </summary>
        public int Length { get; set; } = RandomHelpers.GenerateNumberInRange(Constants.MinimumAutoLength, Constants.MaximumAutoLength);
    }
}