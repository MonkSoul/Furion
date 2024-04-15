// ------------------------------------------------------------------------
// 版权信息
// 版权归百小僧及百签科技（广东）有限公司所有。
// 所有权利保留。
// 官方网站：https://baiqian.com
//
// 许可证信息
// Furion 项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。
// 许可证的完整文本可以在源代码树根目录中的 LICENSE-APACHE 和 LICENSE-MIT 文件中找到。
// 官方网站：https://furion.net
//
// 使用条款
// 使用本代码应遵守相关法律法规和许可证的要求。
//
// 免责声明
// 对于因使用本代码而产生的任何直接、间接、偶然、特殊或后果性损害，我们不承担任何责任。
//
// 其他重要信息
// Furion 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。
// 有关 Furion 项目的其他详细信息，请参阅位于源代码树根目录中的 COPYRIGHT 和 DISCLAIMER 文件。
//
// 更多信息
// 请访问 https://gitee.com/dotnetchina/Furion 获取更多关于 Furion 项目的许可证和版权信息。
// ------------------------------------------------------------------------

using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Furion.DatabaseAccessor;

/// <summary>
/// 数据库上下文提交拦截器
/// </summary>
[SuppressSniffer]
public class DbContextSaveChangesInterceptor : SaveChangesInterceptor
{
    /// <summary>
    /// 拦截保存数据库之前
    /// </summary>
    /// <param name="eventData"></param>
    /// <param name="result"></param>
    /// <returns></returns>
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        dynamic dbContext = eventData.Context;
        dbContext.SavingChangesEventInner(eventData, result);

        return base.SavingChanges(eventData, result);
    }

    /// <summary>
    /// 拦截保存数据库之前
    /// </summary>
    /// <param name="eventData"></param>
    /// <param name="result"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        dynamic dbContext = eventData.Context;
        dbContext.SavingChangesEventInner(eventData, result);

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    /// <summary>
    /// 拦截保存数据库成功
    /// </summary>
    /// <param name="eventData"></param>
    /// <param name="result"></param>
    /// <returns></returns>
    public override int SavedChanges(SaveChangesCompletedEventData eventData, int result)
    {
        dynamic dbContext = eventData.Context;
        dbContext.SavedChangesEventInner(eventData, result);

        return base.SavedChanges(eventData, result);
    }

    /// <summary>
    /// 拦截保存数据库成功
    /// </summary>
    /// <param name="eventData"></param>
    /// <param name="result"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default)
    {
        dynamic dbContext = eventData.Context;
        dbContext.SavedChangesEventInner(eventData, result);

        return base.SavedChangesAsync(eventData, result, cancellationToken);
    }

    /// <summary>
    /// 拦截保存数据库失败
    /// </summary>
    /// <param name="eventData"></param>
    public override void SaveChangesFailed(DbContextErrorEventData eventData)
    {
        dynamic dbContext = eventData.Context;
        dbContext.SaveChangesFailedEventInner(eventData);

        base.SaveChangesFailed(eventData);
    }

    /// <summary>
    /// 拦截保存数据库失败
    /// </summary>
    /// <param name="eventData"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override Task SaveChangesFailedAsync(DbContextErrorEventData eventData, CancellationToken cancellationToken = default)
    {
        dynamic dbContext = eventData.Context;
        dbContext.SaveChangesFailedEventInner(eventData);

        return base.SaveChangesFailedAsync(eventData, cancellationToken);
    }
}