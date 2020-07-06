using System;
using System.Transactions;

namespace Fur.DatabaseVisitor.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class UnitOfWorkAttribute : Attribute
    {
        public UnitOfWorkAttribute()
        {
        }

        public UnitOfWorkAttribute(IsolationLevel isolationLevel)
        {
            this.IsolationLevel = isolationLevel;
        }

        public UnitOfWorkAttribute(TransactionScopeOption transactionScopeOption, IsolationLevel isolationLevel)
        {
            this.TransactionScopeOption = transactionScopeOption;
            this.IsolationLevel = isolationLevel;
        }

        public TransactionScopeOption TransactionScopeOption { get; set; } = TransactionScopeOption.Required;

        public IsolationLevel IsolationLevel { get; set; } = IsolationLevel.ReadCommitted;

        public TransactionScopeAsyncFlowOption AsyncFlowOption { get; set; } = TransactionScopeAsyncFlowOption.Enabled;
    }
}
