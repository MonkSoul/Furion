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

using Fur.DependencyInjection;
using System;
using System.Transactions;

namespace Fur.DatabaseAccessor
{
    /// <summary>
    /// 工作单元配置特性
    /// </summary>
    /// <remarks>
    /// <para>支持配置事务范围、隔离级别、跨线程异步流</para>
    /// <para>只能在方法中贴此特性，通常贴在最外层的方法中，如果在子方法中贴该特性，将启用嵌套子事务</para>
    /// <para>注意：只对请求中的起始方法起作用</para>
    /// </remarks>
    [SkipScan, AttributeUsage(AttributeTargets.Method)]
    public sealed class UnitOfWorkAttribute : Attribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public UnitOfWorkAttribute() { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="enabled">是否启用工作单元</param>
        public UnitOfWorkAttribute(bool enabled)
        {
            Enabled = enabled;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <remarks>
        /// <para>支持传入事务隔离级别 <see cref="IsolationLevel"/> 参数值</para>
        /// </remarks>
        /// <param name="isolationLevel">事务隔离级别</param>
        public UnitOfWorkAttribute(IsolationLevel isolationLevel)
        {
            IsolationLevel = isolationLevel;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <remarks>
        /// <para>支持传入 事务范围 <see cref="TransactionScope"/>，事务级别 <see cref="IsolationLevel"/> 参数值</para>
        /// </remarks>
        /// <param name="scopeOption">事务范围</param>
        /// <param name="isolationLevel">事务隔离级别</param>
        public UnitOfWorkAttribute(TransactionScopeOption scopeOption, IsolationLevel isolationLevel)
        {
            ScopeOption = scopeOption;
            IsolationLevel = isolationLevel;
        }

        /// <summary>
        /// 是否启用工作单元
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// 事务范围
        /// </summary>
        /// <remarks>
        /// <para>默认：<see cref="TransactionScopeOption.Required"/>，参见：<see cref="TransactionScope"/></para>
        /// <para>说明：如果当前上下文已存在事务，那么这个事务范围将加入已有的事务。否则，它将创建自己的事务</para>
        /// </remarks>
        public TransactionScopeOption ScopeOption { get; set; } = TransactionScopeOption.Required;

        /// <summary>
        /// 事务隔离级别
        /// </summary>
        /// <remarks>
        /// <para>默认：<see cref="IsolationLevel.ReadCommitted"/>，参见：<see cref="IsolationLevel"/></para>
        /// <para>说明：当事务A更新某条数据的时候，不容许其他事务来更新该数据，但可以进行读取操作</para>
        /// </remarks>
        public IsolationLevel IsolationLevel { get; set; } = IsolationLevel.ReadCommitted;

        /// <summary>
        /// 跨线程异步流
        /// </summary>
        /// <remarks>
        /// <para>默认：<see cref="TransactionScopeAsyncFlowOption.Enabled"/>，参见：<see cref="TransactionScopeAsyncFlowOption"/></para>
        /// <para>说明：允许跨线程连续任务的事务流，如有异步操作需开启该选项</para>
        /// </remarks>
        public TransactionScopeAsyncFlowOption AsyncFlowOption { get; set; } = TransactionScopeAsyncFlowOption.Enabled;
    }
}