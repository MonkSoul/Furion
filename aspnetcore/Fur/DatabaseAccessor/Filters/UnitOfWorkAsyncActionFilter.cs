using Fur.ApplicationBase.Attributes;
using Fur.DatabaseAccessor.Attributes;
using Fur.DatabaseAccessor.Contexts.Pools;
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
    /// <para>说明：在 请求 Action 之前开启事务，Action执行完毕之后提交事务，如果执行过程中出错，自动回滚事务</para>
    /// </summary>
    [NonWrapper]
    public class UnitOfWorkAsyncActionFilter : IAsyncActionFilter
    {
        /// <summary>
        /// 性能分析器类别
        /// </summary>
        private static readonly string miniProfilerName = "transaction";

        /// <summary>
        /// 数据库上下文池
        /// </summary>
        private readonly IDbContextPool _dbContextPool;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbContextPool">数据库上下文池</param>
        public UnitOfWorkAsyncActionFilter(IDbContextPool dbContextPool)
        {
            _dbContextPool = dbContextPool;
        }

        /// <summary>
        /// Action执行拦截方法
        /// </summary>
        /// <param name="context">Action执行上下文</param>
        /// <param name="next">Action执行委托</param>
        /// <returns><see cref="Task"/></returns>
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var controllerActionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
            var methodInfo = controllerActionDescriptor.MethodInfo;
            var dbContextCount = _dbContextPool.GetDbContexts().Count();

            // 如果贴了 [NonUnitOfWork] 特性，则不开启事务
            if (methodInfo.IsDefined(typeof(NonUnitOfWorkAttribute)) || methodInfo.DeclaringType.IsDefined(typeof(NonUnitOfWorkAttribute)))
            {
                MiniProfiler.Current.CustomTiming(miniProfilerName, "TransactionScope Disabled", "Disabled !");
                await next();
                MiniProfiler.Current.CustomTiming(miniProfilerName, $"TransactionScope Completed - DbContexts: Count/{dbContextCount}", "Completed");
                return;
            }

            MiniProfiler.Current.CustomTiming(miniProfilerName, "TransactionScope Enabled", "Enabled");

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

                MiniProfiler.Current.CustomTiming(miniProfilerName, $"TransactionScope Completed - DbContexts: Count/{ dbContextCount}, Has Changes/{hasChangesCount}", "Completed");
            }
            // 否则回滚
            else
            {
                MiniProfiler.Current.CustomTiming(miniProfilerName, "TransactionScope Rollback", "Rollback !");
            }
        }
    }
}