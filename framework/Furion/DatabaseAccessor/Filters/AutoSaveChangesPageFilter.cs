// MIT 许可证
//
// 版权 © 2020-present 百小僧, 百签科技（广东）有限公司 和所有贡献者
//
// 特此免费授予任何获得本软件副本和相关文档文件（下称“软件”）的人不受限制地处置该软件的权利，
// 包括不受限制地使用、复制、修改、合并、发布、分发、转授许可和/或出售该软件副本，
// 以及再授权被配发了本软件的人如上的权利，须在下列条件下：
//
// 上述版权声明和本许可声明应包含在该软件的所有副本或实质成分中。
//
// 本软件是“如此”提供的，没有任何形式的明示或暗示的保证，包括但不限于对适销性、特定用途的适用性和不侵权的保证。
// 在任何情况下，作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace Furion.DatabaseAccessor;

/// <summary>
/// 自动调用 SaveChanges 拦截器（Razor Pages）
/// </summary>
internal sealed class AutoSaveChangesPageFilter : IAsyncPageFilter, IOrderedFilter
{
    /// <summary>
    /// 过滤器排序
    /// </summary>
    private const int FilterOrder = 9999;

    /// <summary>
    /// 排序属性
    /// </summary>
    public int Order => FilterOrder;

    /// <summary>
    /// 模型绑定拦截
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
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
    /// <exception cref="NotImplementedException"></exception>
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

        // 获取请求上下文
        var httpContext = context.HttpContext;

        // 判断是否贴有工作单元特性
        if (method.IsDefined(typeof(UnitOfWorkAttribute), true))
        {
            _ = await next.Invoke();
            return;
        }

        // 调用方法
        var resultContext = await next.Invoke();

        // 判断是否手动提交
        var isManualSaveChanges = method.IsDefined(typeof(ManualCommitAttribute), true);

        // 判断是否异常，并且没有贴 [ManualCommit] 特性
        if (resultContext.Exception == null && !isManualSaveChanges)
        {
            httpContext.RequestServices.GetRequiredService<IDbContextPool>().SavePoolNow();
        }
    }
}