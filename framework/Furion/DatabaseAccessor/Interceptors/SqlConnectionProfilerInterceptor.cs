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