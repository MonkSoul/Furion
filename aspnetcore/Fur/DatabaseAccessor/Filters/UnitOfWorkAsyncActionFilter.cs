using Fur.DatabaseAccessor.Attributes;
using Fur.DatabaseAccessor.Contexts.Pool;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using StackExchange.Profiling;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Transactions;

namespace Fur.DatabaseAccessor.Filters
{
    /// <summary>
    /// 工作单元异步筛选器
    /// <para>用来实现自动化事务管理</para>
    /// <para>管理步骤：在 请求 Action 之前开启事务，Action执行完毕之后提交事务，如果执行过程中出错，自动回滚事务</para>
    /// </summary>
    public class UnitOfWorkAsyncActionFilter : IAsyncActionFilter
    {
        /// <summary>
        /// 注入数据库上下文线程池
        /// </summary>
        private readonly IDbContextPool _dbContextPool;

        #region 构造函数 + public UnitOfWorkAsyncActionFilter(IDbContextPool dbContextPool)

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbContextPool"></param>
        public UnitOfWorkAsyncActionFilter(IDbContextPool dbContextPool)
        {
            _dbContextPool = dbContextPool;
        }

        #endregion 构造函数 + public UnitOfWorkAsyncActionFilter(IDbContextPool dbContextPool)

        #region Action执行拦截方法 + public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)

        /// <summary>
        /// Action执行拦截方法
        /// </summary>
        /// <param name="context">Action执行上下文</param>
        /// <param name="next">Action执行完成后调用 next() 进行接下来的动作</param>
        /// <returns><see cref="Task"/></returns>
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var miniProfilerName = "transaction";
            var controllerActionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
            var methodInfo = controllerActionDescriptor.MethodInfo;
            var dbContextCount = _dbContextPool.GetDbContexts().Count();

            // 如果贴了 [NonTransaction] 特性，则不开启事务
            if (methodInfo.IsDefined(typeof(NonTransactionAttribute)) || methodInfo.DeclaringType.IsDefined(typeof(NonTransactionAttribute)))
            {
                MiniProfiler.Current.CustomTiming(miniProfilerName, "TransactionScope Disable", "Disable !");
                await next();
                MiniProfiler.Current.CustomTiming(miniProfilerName, $"TransactionScope Complete - DbContexts: Count/{dbContextCount}", "Complete");
                return;
            }

            MiniProfiler.Current.CustomTiming(miniProfilerName, "TransactionScope Enable", "Enable");

            UnitOfWorkAttribute unitOfWorkAttribute = null;
            if (!methodInfo.IsDefined(typeof(UnitOfWorkAttribute)))
            {
                unitOfWorkAttribute ??= new UnitOfWorkAttribute();
            }
            else
            {
                unitOfWorkAttribute = methodInfo.GetCustomAttribute<UnitOfWorkAttribute>();
            }

            using var transaction = new TransactionScope(unitOfWorkAttribute.ScopeOption
                , new TransactionOptions { IsolationLevel = unitOfWorkAttribute.IsolationLevel }
                , unitOfWorkAttribute.AsyncFlowOption);

            var resultContext = await next();

            // 如果没有出错，则提交事务
            if (resultContext.Exception == null)
            {
                var hasChangesCount = await _dbContextPool.SavePoolChangesAsync();
                transaction.Complete();

                MiniProfiler.Current.CustomTiming(miniProfilerName, $"TransactionScope Complete - DbContexts: Count/{ dbContextCount}, Has Changes/{hasChangesCount}", "Complete");
            }
            // 否则回滚
            else
            {
                MiniProfiler.Current.CustomTiming(miniProfilerName, "TransactionScope Rollback", "Rollback !");
            }
        }

        #endregion Action执行拦截方法 + public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    }
}