using System;

namespace Fur.DatabaseVisitor.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class NotTransactionAttribute : Attribute
    {
    }
}
