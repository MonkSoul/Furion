using Furion.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace Furion.DatabaseAccessor
{
    /// <summary>
    /// 工作单元拦截器
    /// </summary>
    [SkipScan]
    internal sealed class UnitOfWorkFilter : IAsyncActionFilter, IOrderedFilter
    {
        /// <summary>
        /// 过滤器排序
        /// </summary>
        internal const int FilterOrder = 9999;

        /// <summary>
        /// 排序属性
        /// </summary>
        public int Order => FilterOrder;

        /// <summary>
        /// 数据库上下文池
        /// </summary>
        private readonly IDbContextPool _dbContextPool;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbContextPool"></param>
        public UnitOfWorkFilter(IDbContextPool dbContextPool)
        {
            _dbContextPool = dbContextPool;
        }

        /// <summary>
        /// 拦截请求
        /// </summary>
        /// <param name="context">动作方法上下文</param>
        /// <param name="next">中间件委托</param>
        /// <returns></returns>
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // 获取动作方法描述器
            var actionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
            var method = actionDescriptor.MethodInfo;

            // 判断是否手动提交
            var isManualSaveChanges = method.IsDefined(typeof(ManualSaveChangesAttribute), true);

            // 判断是否贴有工作单元特性
            if (!method.IsDefined(typeof(UnitOfWorkAttribute), true))
            {
                // 调用方法
                var resultContext = await next();

                // 判断是否异常，并且没有贴 [ManualSaveChanges] 特性
                if (resultContext.Exception == null && !isManualSaveChanges) _dbContextPool.SavePoolNow();
            }
            else
            {
                // 打印事务开始消息
                App.PrintToMiniProfiler("unitOfWork", "Beginning");

                // 开启事务
                _dbContextPool.BeginTransaction();

                // 调用方法
                var resultContext = await next();

                // 提交事务
                _dbContextPool.CommitTransaction(isManualSaveChanges, resultContext.Exception);
            }

            // 手动关闭
            _dbContextPool.CloseAll();
        }
    }
}