using System;
using System.Transactions;

namespace Fur.DatabaseVisitor.Attributes
{
    /// <summary>
    /// 工作单元特性
    /// <para>所谓的工作单元就是将一系列的操作包裹在一个事务中，只要任何地方出错，所有受影响的数据库操作都将回滚</para>
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class UnitOfWorkAttribute : Attribute
    {
        #region 默认无参构造函数 + public UnitOfWorkAttribute()
        /// <summary>
        /// 默认无参构造函数
        /// </summary>
        public UnitOfWorkAttribute() { }
        #endregion

        #region 带事务隔离级别的构造函数 + public UnitOfWorkAttribute(IsolationLevel isolationLevel)
        /// <summary>
        /// 带事务隔离级别的构造函数
        /// </summary>
        /// <param name="isolationLevel">事务隔离级别</param>
        public UnitOfWorkAttribute(IsolationLevel isolationLevel)
        {
            this.IsolationLevel = isolationLevel;
        }
        #endregion

        #region 带事务范围，事务隔离级别的构造函数 + public UnitOfWorkAttribute(TransactionScopeOption transactionScopeOption, IsolationLevel isolationLevel)
        /// <summary>
        /// 带事务范围，事务隔离级别的构造函数
        /// </summary>
        /// <param name="transactionScopeOption"></param>
        /// <param name="isolationLevel">事务隔离级别</param>
        public UnitOfWorkAttribute(TransactionScopeOption transactionScopeOption, IsolationLevel isolationLevel)
        {
            this.TransactionScopeOption = transactionScopeOption;
            this.IsolationLevel = isolationLevel;
        }
        #endregion

        /// <summary>
        /// 事务范围
        /// <para>默认为：<see cref="TransactionScopeOption.Required"/></para>
        /// <para>表示：如果已经存在一个事务，那么这个事务范围将加入已有的事务。否则，它将创建自己的事务</para>
        /// </summary>
        public TransactionScopeOption TransactionScopeOption { get; set; } = TransactionScopeOption.Required;

        /// <summary>
        /// 事务隔离级别
        /// <para>默认为：<see cref="IsolationLevel.ReadCommitted"/></para>
        /// <para>表示：未提交读。当事务A更新某条数据的时候，不容许其他事务来更新该数据，但可以进行读取操作</para>
        /// </summary>
        public IsolationLevel IsolationLevel { get; set; } = IsolationLevel.ReadCommitted;

        /// <summary>
        /// 跨线程连续任务事务流
        /// <para>默认为：<see cref="TransactionScopeAsyncFlowOption.Enabled"/></para>
        /// <para>表示：允许跨线程连续任务的事务流，通常异步操作需开启该选项</para>
        /// </summary>
        public TransactionScopeAsyncFlowOption AsyncFlowOption { get; set; } = TransactionScopeAsyncFlowOption.Enabled;
    }
}
