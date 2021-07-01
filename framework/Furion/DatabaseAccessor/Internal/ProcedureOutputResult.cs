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
using System.Collections.Generic;
using System.Data;

namespace Furion.DatabaseAccessor
{
    /// <summary>
    /// 存储过程输出返回值
    /// </summary>
    [SuppressSniffer]
    public sealed class ProcedureOutputResult : ProcedureOutputResult<DataSet>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ProcedureOutputResult() : base()
        {
        }
    }

    /// <summary>
    /// 存储过程输出返回值
    /// </summary>
    /// <typeparam name="TResult">泛型版本</typeparam>
    [SuppressSniffer]
    public class ProcedureOutputResult<TResult>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ProcedureOutputResult()
        {
            OutputValues = new List<ProcedureOutputValue>();
        }

        /// <summary>
        /// 输出值
        /// </summary>
        public IEnumerable<ProcedureOutputValue> OutputValues { get; set; }

        /// <summary>
        /// 返回值
        /// </summary>
        public object ReturnValue { get; set; }

        /// <summary>
        /// 结果集
        /// </summary>
        public TResult Result { get; set; }
    }
}