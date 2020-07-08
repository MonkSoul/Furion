using System;

namespace Fur.DatabaseVisitor.Attributes
{
    /// <summary>
    /// 禁止事务特性
    /// <para>框架默认拦截了所有 API 方法并启动了环境事务</para>
    /// <para>贴了此特性后，将关闭环境事务</para>
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class NonTransactionAttribute : Attribute
    {
    }
}