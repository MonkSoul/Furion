// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

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