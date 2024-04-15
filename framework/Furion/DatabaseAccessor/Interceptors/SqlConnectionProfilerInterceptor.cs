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
using System.Data.Common;

namespace Furion.DatabaseAccessor;

/// <summary>
/// 数据库连接拦截分析器
/// </summary>
internal sealed class SqlConnectionProfilerInterceptor : DbConnectionInterceptor
{
    /// <summary>
    /// 是否打印数据库连接信息
    /// </summary>
    private readonly bool IsPrintDbConnectionInfo;

    /// <summary>
    /// 构造函数
    /// </summary>
    public SqlConnectionProfilerInterceptor()
    {
        IsPrintDbConnectionInfo = App.Settings.PrintDbConnectionInfo.Value;
    }

    /// <summary>
    /// 拦截数据库连接
    /// </summary>
    /// <param name="connection">数据库连接对象</param>
    /// <param name="eventData">数据库连接事件数据</param>
    /// <param name="result">拦截结果</param>
    /// <returns></returns>
    public override InterceptionResult ConnectionOpening(DbConnection connection, ConnectionEventData eventData, InterceptionResult result)
    {
        // 打印数据库连接信息到 MiniProfiler
        PrintConnectionToMiniProfiler(connection, eventData);

        return base.ConnectionOpening(connection, eventData, result);
    }

    /// <summary>
    /// 拦截数据库连接
    /// </summary>
    /// <param name="connection">数据库连接对象</param>
    /// <param name="eventData">数据库连接事件数据</param>
    /// <param name="result">拦截器结果</param>
    /// <param name="cancellationToken">取消异步Token</param>
    /// <returns></returns>
    public override ValueTask<InterceptionResult> ConnectionOpeningAsync(DbConnection connection, ConnectionEventData eventData, InterceptionResult result, CancellationToken cancellationToken = default)
    {
        // 打印数据库连接信息到 MiniProfiler
        PrintConnectionToMiniProfiler(connection, eventData);

        return base.ConnectionOpeningAsync(connection, eventData, result, cancellationToken);
    }

    /// <summary>
    /// 打印数据库连接信息到 MiniProfiler
    /// </summary>
    /// <param name="connection">数据库连接对象</param>
    /// <param name="eventData">数据库连接事件数据</param>
    private void PrintConnectionToMiniProfiler(DbConnection connection, ConnectionEventData eventData)
    {
        // 打印连接信息消息
        App.PrintToMiniProfiler("connection", "Information", $"[Connection Id: {eventData.ConnectionId}] / [Database: {connection.Database}]{(IsPrintDbConnectionInfo ? $" / [Connection String: {connection.ConnectionString}]" : string.Empty)}");
    }
}