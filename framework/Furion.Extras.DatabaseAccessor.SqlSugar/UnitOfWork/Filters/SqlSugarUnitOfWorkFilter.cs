// -----------------------------------------------------------------------------
// 让 .NET 开发更简单，更通用，更流行。
// Copyright © 2020-2021 Furion, 百小僧, Baiqian Co.,Ltd.
//
// 框架名称：Furion
// 框架作者：百小僧
// 框架版本：2.10.7
// 源码地址：Gitee： https://gitee.com/dotnetchina/Furion
//          Github：https://github.com/monksoul/Furion
// 开源协议：Apache-2.0（https://gitee.com/dotnetchina/Furion/blob/master/LICENSE）
// -----------------------------------------------------------------------------

using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Threading.Tasks;

namespace SqlSugar
{
    /// <summary>
    /// SqlSugar 工作单元拦截器
    /// </summary>
    public class SqlSugarUnitOfWorkFilter : IAsyncActionFilter, IOrderedFilter
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
        /// SqlSugar 对象
        /// </summary>
        private readonly SqlSugarClient _sqlSugarClient;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sqlSugarClient"></param>

        public SqlSugarUnitOfWorkFilter(ISqlSugarClient sqlSugarClient)
        {
            _sqlSugarClient = (SqlSugarClient)sqlSugarClient;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // 获取动作方法描述器
            var actionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
            var method = actionDescriptor.MethodInfo;

            // 判断是否贴有工作单元特性
            if (!method.IsDefined(typeof(SqlSugarUnitOfWorkAttribute), true))
            {
                // 调用方法
                _ = await next();
            }
            else
            {
                var attribute = (method.GetCustomAttributes(typeof(SqlSugarUnitOfWorkAttribute), true).FirstOrDefault() as SqlSugarUnitOfWorkAttribute);

                // 开启事务
                _sqlSugarClient.Ado.BeginTran(attribute.IsolationLevel);

                // 调用方法
                var resultContext = await next();

                if (resultContext.Exception == null)
                {
                    try
                    {
                        _sqlSugarClient.Ado.CommitTran();
                    }
                    catch
                    {
                        _sqlSugarClient.Ado.RollbackTran();
                    }
                    finally
                    {
                        _sqlSugarClient.Ado.Dispose();
                    }
                }
                else
                {
                    // 回滚事务
                    _sqlSugarClient.Ado.RollbackTran();
                    _sqlSugarClient.Ado.Dispose();
                }
            }
        }
    }
}