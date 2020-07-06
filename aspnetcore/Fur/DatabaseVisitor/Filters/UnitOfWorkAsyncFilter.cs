using Fur.DatabaseVisitor.Attributes;
using Fur.DatabaseVisitor.DbContextPool;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using StackExchange.Profiling;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Transactions;

namespace Fur.DatabaseVisitor.Filters
{
    public class UnitOfWorkAsyncFilter : IAsyncActionFilter
    {
        private readonly IDbContextPool _dbContextPool;
        public UnitOfWorkAsyncFilter(IDbContextPool dbContextPool)
        {
            _dbContextPool = dbContextPool;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var controllerActionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
            var methodInfo = controllerActionDescriptor.MethodInfo;

            if (methodInfo.IsDefined(typeof(NotTransactionAttribute)) || methodInfo.DeclaringType.IsDefined(typeof(NotTransactionAttribute)))
            {
                MiniProfiler.Current.CustomTiming("transaction", "TransactionScope Disable", "Disable !");
                await next();
                return;
            }

            MiniProfiler.Current.CustomTiming("transaction", "TransactionScope Enable", "Enable");

            UnitOfWorkAttribute unitOfWorkAttribute = null;
            if (!controllerActionDescriptor.MethodInfo.IsDefined(typeof(UnitOfWorkAttribute)))
            {
                unitOfWorkAttribute ??= new UnitOfWorkAttribute();
            }
            else
            {
                unitOfWorkAttribute = methodInfo.GetCustomAttributes(typeof(UnitOfWorkAttribute), false).FirstOrDefault() as UnitOfWorkAttribute ?? new UnitOfWorkAttribute();
            }


            unitOfWorkAttribute ??= new UnitOfWorkAttribute();
            using var transaction = new TransactionScope(unitOfWorkAttribute.TransactionScopeOption, new TransactionOptions { IsolationLevel = unitOfWorkAttribute.IsolationLevel }, unitOfWorkAttribute.AsyncFlowOption);

            var resultContext = await next();

            if (resultContext.Exception == null)
            {
                var hasChangesCount = await _dbContextPool.SavePoolChangesAsync();
                transaction.Complete();

                MiniProfiler.Current.CustomTiming("transaction", $"TransactionScope Complete - DbContexts: Count/{ _dbContextPool.GetDbContexts().Count()}, Has Changes/{hasChangesCount}", "Complete");
            }
            else
            {
                MiniProfiler.Current.CustomTiming("transaction", "TransactionScope Rollback", "Rollback !");
            }
        }
    }
}
