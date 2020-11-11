using Fur.DependencyInjection;
using System;
using System.Transactions;

namespace Fur.DatabaseAccessor
{
    /// <summary>
    /// 工作单元配置特性
    /// </summary>
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
        /// <remarks>
        /// <para>支持传入事务隔离级别 <see cref="IsolationLevel"/> 参数值</para>
        /// </remarks>
        /// <param name="isolationLevel">事务隔离级别</param>
        public UnitOfWorkAttribute(IsolationLevel isolationLevel)
        {
            IsolationLevel = isolationLevel;
        }

        /// <summary>
        /// 事务隔离级别
        /// </summary>
        /// <remarks>
        /// <para>默认：<see cref="IsolationLevel.ReadCommitted"/>，参见：<see cref="IsolationLevel"/></para>
        /// <para>说明：当事务A更新某条数据的时候，不容许其他事务来更新该数据，但可以进行读取操作</para>
        /// </remarks>
        public IsolationLevel IsolationLevel { get; set; } = IsolationLevel.ReadCommitted;
    }
}