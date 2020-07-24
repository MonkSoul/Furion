using Fur.ApplicationBase.Attributes;
using System;
using System.Transactions;

namespace Fur.DatabaseAccessor.Attributes
{
    /// <summary>
    /// 工作单元配置特性
    /// <para>支持配置事务范围、隔离级别、跨线程异步流</para>
    /// <para>说明：只能在方法中贴此特性，通常贴在最外层的方法中，如果在子方法中贴该特性，将启用嵌套子事务</para>
    /// </summary>
    [AttributeUsage(AttributeTargets.Method), NonWrapper]
    public class UnitOfWorkAttribute : Attribute
    {
        #region 构造函数 + public UnitOfWorkAttribute()
        /// <summary>
        /// 构造函数
        /// </summary>
        public UnitOfWorkAttribute() { }
        #endregion

        #region 构造函数 + public UnitOfWorkAttribute(IsolationLevel isolationLevel)
        /// <summary>
        /// 构造函数
        /// <para>支持传入事务隔离级别 <see cref="IsolationLevel"/> 参数值</para>
        /// </summary>
        /// <param name="isolationLevel">事务隔离级别</param>
        public UnitOfWorkAttribute(IsolationLevel isolationLevel)
        {
            this.IsolationLevel = isolationLevel;
        }
        #endregion

        #region 构造函数 + public UnitOfWorkAttribute(TransactionScopeOption scopeOption, IsolationLevel isolationLevel)
        /// <summary>
        /// 构造函数
        /// <para>支持传入 事务范围 <see cref="TransactionScope"/>，事务级别 <see cref="IsolationLevel"/> 参数值</para>
        /// </summary>
        /// <param name="scopeOption">事务范围</param>
        /// <param name="isolationLevel">事务隔离级别</param>
        public UnitOfWorkAttribute(TransactionScopeOption scopeOption, IsolationLevel isolationLevel)
        {
            this.ScopeOption = scopeOption;
            this.IsolationLevel = isolationLevel;
        }
        #endregion

        /// <summary>
        /// 事务范围
        /// <para>默认：<see cref="TransactionScopeOption.Required"/>，参见：<see cref="TransactionScope"/></para>
        /// <para>说明：如果当前上下文已存在事务，那么这个事务范围将加入已有的事务。否则，它将创建自己的事务</para>
        /// </summary>
        public TransactionScopeOption ScopeOption { get; set; } = TransactionScopeOption.Required;

        /// <summary>
        /// 事务隔离级别
        /// <para>默认：<see cref="IsolationLevel.ReadCommitted"/>，参见：<see cref="IsolationLevel"/></para>
        /// <para>说明：当事务A更新某条数据的时候，不容许其他事务来更新该数据，但可以进行读取操作</para>
        /// </summary>
        public IsolationLevel IsolationLevel { get; set; } = IsolationLevel.ReadCommitted;

        /// <summary>
        /// 跨线程异步流
        /// <para>默认：<see cref="TransactionScopeAsyncFlowOption.Enabled"/>，参见：<see cref="TransactionScopeAsyncFlowOption"/></para>
        /// <para>说明：允许跨线程连续任务的事务流，如有异步操作需开启该选项</para>
        /// </summary>
        public TransactionScopeAsyncFlowOption AsyncFlowOption { get; set; } = TransactionScopeAsyncFlowOption.Enabled;
    }
}