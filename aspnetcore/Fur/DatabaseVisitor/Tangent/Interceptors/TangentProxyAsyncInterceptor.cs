using Autofac;
using Castle.DynamicProxy;
using Fur.DatabaseVisitor.Tangent.Attributes;
using System;

namespace Fur.DatabaseVisitor.Tangent.Interceptors
{
    /// <summary>
    /// 切面代理异步拦截器
    /// </summary>
    public class TangentProxyAsyncInterceptor : IAsyncInterceptor
    {
        private readonly ILifetimeScope _lifetimeScope;

        public TangentProxyAsyncInterceptor(ILifetimeScope lifetimeScope)
        {
            _lifetimeScope = lifetimeScope;
        }

        /// <summary>
        /// 同步拦截器
        /// </summary>
        /// <param name="invocation">拦截器对象</param>
        public void InterceptSynchronous(IInvocation invocation)
        {
            invocation.ReturnValue = SynchronousInvoke(invocation);
        }

        public void InterceptAsynchronous(IInvocation invocation)
        {
            //invocation.ReturnValue = AsynchronousInvoke(invocation);
        }

        public void InterceptAsynchronous<TResult>(IInvocation invocation)
        {
            //var result = AsynchronousOfTInvoke<TResult>(invocation);
            //invocation.ReturnValue = result;
        }

        #region 同步执行 + private object SynchronousInvoke(IInvocation invocation)
        /// <summary>
        /// 同步执行
        /// </summary>
        /// <param name="invocation">拦截器对象</param>
        /// <returns>object</returns>
        private object SynchronousInvoke(IInvocation invocation)
        {
            var (tangentMethod, tangentAttribute) = TangentDbContextUtilities.GetTangentMethodInfo(invocation, _lifetimeScope);

            if (tangentAttribute is DbQueryAttribute dbQueryAttribute)
            {
                return TangentDbContextUtilities.DbQueryExecute(tangentMethod, dbQueryAttribute);
            }
            else if (tangentAttribute is DbNonQueryAttribute dbNonQueryAttribute)
            {
                return TangentDbContextUtilities.DbNonQueryExecute(tangentMethod, dbNonQueryAttribute);
            }
            else if (tangentAttribute is DbFunctionAttribute dbFunctionAttribute)
            {
                return TangentDbContextUtilities.DbFunctionExecute(tangentMethod, dbFunctionAttribute);
            }
            else if (tangentAttribute is DbProcedureAttribute dbProcedureAttribute)
            {
                return TangentDbContextUtilities.DbProcedureExecute(tangentMethod, dbProcedureAttribute);
            }
            else
            {
                throw new NotSupportedException($"{tangentAttribute.GetType().Name}");
            }
        }
        #endregion
    }
}