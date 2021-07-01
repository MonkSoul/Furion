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

namespace Furion.DatabaseAccessor
{
    /// <summary>
    /// 存储过程输出值模型
    /// </summary>
    [SuppressSniffer]
    public sealed class ProcedureOutputValue
    {
        /// <summary>
        /// 输出参数名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 输出参数值
        /// </summary>
        public object Value { get; set; }
    }
}