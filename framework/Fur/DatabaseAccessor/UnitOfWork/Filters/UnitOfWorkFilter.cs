using Fur.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace Fur.DatabaseAccessor
{
    /// <summary>
    /// 工作单元拦截器
    /// </summary>
    [SkipScan]
    internal sealed class UnitOfWorkFilter : IAsyncActionFilter, IOrderedFilter
    {
        /// <summary>
        /// MiniProfiler 分类名
        /// </summary>
        private const string MiniProfilerCategory = "transaction";

        /// <summary>
        /// 数据库上下文池
        /// </summary>
        private readonly IDbContextPool _dbContextPool;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbContextPool">数据库上下文池</param>
        public UnitOfWorkFilter(IDbContextPool dbContextPool)
        {
            _dbContextPool = dbContextPool;
        }

        /// <summary>
        /// 过滤器排序
        /// </summary>
        internal const int FilterOrder = 9999;

        /// <summary>
        /// 排序属性
        /// </summary>
        public int Order => FilterOrder;

        /// <summary>
        /// 拦截请求
        /// </summary>
        /// <param name="context">动作方法上下文</param>
        /// <param name="next">中间件委托</param>
        /// <returns></returns>
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // 打印事务开始消息
            App.PrintToMiniProfiler(MiniProfilerCategory, "Beginning");

            var resultContext = await next();

            // 判断是否异常
            if (resultContext.Exception == null)
            {
                // 将所有数据库上下文修改 SaveChanges();
                var hasChangesCount = await _dbContextPool.SavePoolNowAsync();

                // 提交共享事务
                if (_dbContextPool.DbContextTransaction == null) return;
                await _dbContextPool.DbContextTransaction.CommitAsync();
                await _dbContextPool.DbContextTransaction.DisposeAsync();

                // 打印事务提交消息
                App.PrintToMiniProfiler(MiniProfilerCategory, "Completed", $"Transaction Completed! Has {hasChangesCount} DbContext Changes.");
            }
            else
            {
                // 回滚事务
                if (_dbContextPool.DbContextTransaction == null) return;
                await _dbContextPool.DbContextTransaction.RollbackAsync();
                await _dbContextPool.DbContextTransaction.DisposeAsync();

                // 打印事务回滚消息
                App.PrintToMiniProfiler(MiniProfilerCategory, "Rollback", isError: true);
            }
        }
    }
}