using System;

namespace Fur.DatabaseAccessor.Attributes
{
    /// <summary>
    /// 禁用工作单元特性
    /// <para>慎用！一旦贴了此特性，单次请求中有任何异常代码，已经增删改的数据库操作将不会回滚。</para>
    /// <para>说明：支持方法、方法所在类中贴此特性</para>
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class NonTransactionAttribute : Attribute { }
}