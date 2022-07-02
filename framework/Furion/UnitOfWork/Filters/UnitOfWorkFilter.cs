// Copyright (c) 2020-2022 百小僧, Baiqian Co.,Ltd.
// Furion is licensed under Mulan PSL v2.
// You can use this software according to the terms and conditions of the Mulan PSL v2.
// You may obtain a copy of Mulan PSL v2 at:
//             https://gitee.com/dotnetchina/Furion/blob/master/LICENSE
// THIS SOFTWARE IS PROVIDED ON AN "AS IS" BASIS, WITHOUT WARRANTIES OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO NON-INFRINGEMENT, MERCHANTABILITY OR FIT FOR A PARTICULAR PURPOSE.
// See the Mulan PSL v2 for more details.

using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Reflection;

namespace Furion.DatabaseAccessor;

/// <summary>
/// 工作单元拦截器
/// </summary>
internal sealed class UnitOfWorkFilter : IAsyncActionFilter, IOrderedFilter
{
    /// <summary>
    ///  MiniProfiler 分类名
    /// </summary>
    private const string MiniProfilerCategory = "unitOfWork";

    /// <summary>
    /// 过滤器排序
    /// </summary>
    internal const int FilterOrder = 9999;

    /// <summary>
    /// 排序属性
    /// </summary>
    public int Order => FilterOrder;

    /// <summary>
    /// 数据库工作单元对象
    /// </summary>
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="unitOfWork"></param>
    public UnitOfWorkFilter(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
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
        var isManualSaveChanges = method.IsDefined(typeof(ManualCommitAttribute), true);

        ActionExecutedContext resultContext;

        // 判断是否贴有工作单元特性
        if (!method.IsDefined(typeof(UnitOfWorkAttribute), true))
        {
            // 调用方法
            resultContext = await next();

            // 调用未标记方法
            _unitOfWork.OnUnmark(resultContext, isManualSaveChanges);
        }
        else
        {
            // 打印工作单元开始消息
            App.PrintToMiniProfiler(MiniProfilerCategory, "Beginning");

            // 获取工作单元特性
            var unitOfWorkAttribute = method.GetCustomAttribute<UnitOfWorkAttribute>();

            // 调用开启事务方法
            _unitOfWork.BeginTransaction(context, unitOfWorkAttribute, isManualSaveChanges);

            // 获取执行 Action 结果
            resultContext = await next();

            if (resultContext.Exception == null)
            {
                // 调用提交事务方法
                _unitOfWork.CommitTransaction(resultContext, unitOfWorkAttribute, isManualSaveChanges);
            }
            else
            {
                // 调用回滚事务方法
                _unitOfWork.RollbackTransaction(resultContext, unitOfWorkAttribute, isManualSaveChanges);
            }

            // 打印工作单元结束消息
            App.PrintToMiniProfiler(MiniProfilerCategory, "Ending");
        }

        // 调用执行完毕方法
        _unitOfWork.OnCompleted(context, resultContext);
    }
}