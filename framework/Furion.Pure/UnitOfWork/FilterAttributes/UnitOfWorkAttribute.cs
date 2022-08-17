// MIT License
//
// Copyright (c) 2020-2022 百小僧, Baiqian Co.,Ltd and Contributors
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Furion.DatabaseAccessor;

/// <summary>
/// 工作单元配置特性
/// </summary>
[SuppressSniffer, AttributeUsage(AttributeTargets.Method)]
public sealed class UnitOfWorkAttribute : Attribute, IAsyncActionFilter, IAsyncPageFilter, IOrderedFilter
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public UnitOfWorkAttribute()
    {
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="ensureTransaction"></param>
    public UnitOfWorkAttribute(bool ensureTransaction)
    {
        EnsureTransaction = ensureTransaction;
    }

    /// <summary>
    /// 确保事务可用
    /// <para>此方法为了解决静态类方式操作数据库的问题</para>
    /// </summary>
    public bool EnsureTransaction { get; set; } = false;

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

        // 如果没有定义工作单元过滤器，则跳过
        if (!method.IsDefined(typeof(UnitOfWorkAttribute), true))
        {
            // 调用方法
            _ = await next();

            return;
        }

        // 开始事务
        BeginTransaction(context, method, out var _unitOfWork, out var unitOfWorkAttribute);

        // 获取执行 Action 结果
        var resultContext = await next();

        // 提交事务
        CommitTransaction(context, _unitOfWork, unitOfWorkAttribute, resultContext);
    }

    /// <summary>
    /// 模型绑定拦截
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public Task OnPageHandlerSelectionAsync(PageHandlerSelectedContext context)
    {
        return Task.CompletedTask;
    }

    /// <summary>
    /// 拦截请求
    /// </summary>
    /// <param name="context"></param>
    /// <param name="next"></param>
    /// <returns></returns>
    public async Task OnPageHandlerExecutionAsync(PageHandlerExecutingContext context, PageHandlerExecutionDelegate next)
    {
        // 获取动作方法描述器
        var method = context.HandlerMethod?.MethodInfo;
        // 处理 Blazor Server
        if (method == null)
        {
            _ = await next.Invoke();
            return;
        }

        // 如果没有定义工作单元过滤器，则跳过
        if (!method.IsDefined(typeof(UnitOfWorkAttribute), true))
        {
            // 调用方法
            _ = await next.Invoke();
            return;
        }

        // 开始事务
        BeginTransaction(context, method, out var _unitOfWork, out var unitOfWorkAttribute);

        // 获取执行 Action 结果
        var resultContext = await next.Invoke();

        // 提交事务
        CommitTransaction(context, _unitOfWork, unitOfWorkAttribute, resultContext);
    }

    /// <summary>
    /// 开始事务
    /// </summary>
    /// <param name="context"></param>
    /// <param name="method"></param>
    /// <param name="_unitOfWork"></param>
    /// <param name="unitOfWorkAttribute"></param>
    private static void BeginTransaction(FilterContext context, MethodInfo method, out IUnitOfWork _unitOfWork, out UnitOfWorkAttribute unitOfWorkAttribute)
    {
        // 打印工作单元开始消息
        App.PrintToMiniProfiler(MiniProfilerCategory, "Beginning");

        // 解析工作单元服务
        _unitOfWork = context.HttpContext.RequestServices.GetRequiredService<IUnitOfWork>();

        // 获取工作单元特性
        unitOfWorkAttribute = method.GetCustomAttribute<UnitOfWorkAttribute>();

        // 调用开启事务方法
        _unitOfWork.BeginTransaction(context, unitOfWorkAttribute);
    }

    /// <summary>
    /// 提交事务
    /// </summary>
    /// <param name="context"></param>
    /// <param name="_unitOfWork"></param>
    /// <param name="unitOfWorkAttribute"></param>
    /// <param name="resultContext"></param>
    private static void CommitTransaction(FilterContext context, IUnitOfWork _unitOfWork, UnitOfWorkAttribute unitOfWorkAttribute, FilterContext resultContext)
    {
        // 获取动态结果上下文
        dynamic dynamicResultContext = resultContext;

        if (dynamicResultContext.Exception == null)
        {
            // 调用提交事务方法
            _unitOfWork.CommitTransaction(resultContext, unitOfWorkAttribute);
        }
        else
        {
            // 调用回滚事务方法
            _unitOfWork.RollbackTransaction(resultContext, unitOfWorkAttribute);
        }

        // 调用执行完毕方法
        _unitOfWork.OnCompleted(context, resultContext);

        // 打印工作单元结束消息
        App.PrintToMiniProfiler(MiniProfilerCategory, "Ending");
    }
}