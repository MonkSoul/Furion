// -----------------------------------------------------------------------------
// Fur 是 .NET 5 平台下企业应用开发最佳实践框架。
// Copyright © 2020 Fur, Baiqian Co.,Ltd.
//
// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0-rc.final.20
// 官方网站：https://chinadot.net
// 源码地址：Gitee：https://gitee.com/monksoul/Fur
// 				    Github：https://github.com/monksoul/Fur
// 开源协议：Apache-2.0（http://www.apache.org/licenses/LICENSE-2.0）
// -----------------------------------------------------------------------------

using Fur.DependencyInjection;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Hosting;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Fur.DatabaseAccessor
{
    /// <summary>
    /// 数据库连接拦截分析器
    /// </summary>
    [SkipScan]
    internal sealed class SqlConnectionProfilerInterceptor : DbConnectionInterceptor
    {
        /// <summary>
        /// MiniProfiler 分类名
        /// </summary>
        private const string MiniProfilerCategory = "connection";

        /// <summary>
        /// 是否是开发环境
        /// </summary>
        private readonly bool IsDevelopment;

        /// <summary>
        /// 构造函数
        /// </summary>
        public SqlConnectionProfilerInterceptor()
        {
            IsDevelopment = App.HostEnvironment.IsDevelopment();
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
            if (IsDevelopment)
            {
                // 打印连接信息消息
                App.PrintToMiniProfiler(MiniProfilerCategory, "Information", $"[Connection Id: {eventData.ConnectionId}] / [Database: {connection.Database}] / [Connection String: {connection.ConnectionString}]");
            }
        }
    }
}