// -----------------------------------------------------------------------------
// Fur 是 .NET 5 平台下企业应用开发最佳实践框架。
// Copyright © 2020 Fur, Baiqian Co.,Ltd.
//
// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0-rc.final.20
// 官方网站：https://chinadot.net
// 源码地址：Gitee：https://gitee.com/monksoul/Fur
// 				    Github：https://github.com/monksoul/Fur
// 开源协议：Apache-2.0（http://www.apache.org/licenses/LICENSE-2.0）
// -----------------------------------------------------------------------------

using Fur.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Transactions;

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
            // 获取动作方法描述器
            var actionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
            var method = actionDescriptor.MethodInfo;

            TransactionScope transaction = null;
            var isSupportTransactionScope = true;
            var disabledTransact = false;

            // 如果没有在构造函数中初始化上下文，则跳过
            var dbContexts = _dbContextPool.GetDbContexts();
            if (!dbContexts.Any()) goto Continue;

            // 工作单元特性
            UnitOfWorkAttribute unitOfWorkAttribute = null;

            // 判断是否禁用了工作单元
            if (method.IsDefined(typeof(UnitOfWorkAttribute), true))
            {
                unitOfWorkAttribute = method.GetCustomAttribute<UnitOfWorkAttribute>(true);
                if (!unitOfWorkAttribute.Enabled)
                {
                    App.PrintToMiniProfiler("UnitOfWork", "Disabled !");
                    await next();
                    return;
                }
            }

            // 如果方法贴了 [NonTransact] 则跳过事务
            disabledTransact = method.IsDefined(typeof(NonTransactAttribute), true);

            // 打印验禁止事务信息
            if (disabledTransact) App.PrintToMiniProfiler(MiniProfilerCategory, "Disabled !");

            // 判断是否支持环境事务
            isSupportTransactionScope = !dbContexts.Any(u => DbProvider.NotSupportTransactionScopeDatabase.Contains(u.Database.ProviderName));

            if (isSupportTransactionScope && !disabledTransact)
            {
                // 打印事务开始消息
                App.PrintToMiniProfiler(MiniProfilerCategory, "Beginning");

                // 获取工作单元特性
                unitOfWorkAttribute ??= method.GetCustomAttribute<UnitOfWorkAttribute>() ?? new UnitOfWorkAttribute();

                // 开启分布式事务
                transaction = new TransactionScope(unitOfWorkAttribute.ScopeOption
              , new TransactionOptions { IsolationLevel = unitOfWorkAttribute.IsolationLevel }
              , unitOfWorkAttribute.AsyncFlowOption);
            }
            // 打印不支持事务
            else if (!isSupportTransactionScope && !disabledTransact) { App.PrintToMiniProfiler(MiniProfilerCategory, "NotSupported !"); }

        // 继续执行
        Continue: var resultContext = await next();

            // 判断是否出现异常
            if (resultContext.Exception == null)
            {
                // 将所有上下文提交事务
                var hasChangesCount = await _dbContextPool.SavePoolNowAsync();

                if (isSupportTransactionScope && !disabledTransact)
                {
                    transaction?.Complete();
                    transaction?.Dispose();

                    // 打印事务提交消息
                    App.PrintToMiniProfiler(MiniProfilerCategory, "Completed", $"Transaction Completed! Has {hasChangesCount} DbContext Changes.");
                }
            }
            else
            {
                // 打印事务回滚消息
                if (isSupportTransactionScope && !disabledTransact) App.PrintToMiniProfiler(MiniProfilerCategory, "Rollback", isError: true);
            }
        }
    }
}