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