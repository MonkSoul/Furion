using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Hosting;
using StackExchange.Profiling;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Fur.DatabaseAccessor.Interceptors
{
    /// <summary>
    /// 数据库连接/关闭拦截器
    /// <para>用来拦截数据库连接/关闭的每一个生命周期</para>
    /// </summary>

    public sealed class SqlConnectionProfilerInterceptor : DbConnectionInterceptor
    {
        /// <summary>
        /// 性能分析器类别
        /// </summary>
        private static readonly string miniProfilerName = "connection";

        /// <summary>
        /// 拦截数据库连接
        /// </summary>
        /// <param name="connection">数据库连接对象</param>
        /// <param name="eventData">数据库连接事件数据</param>
        /// <param name="result">拦截结果</param>
        /// <returns><see cref="InterceptionResult"/></returns>
        public override InterceptionResult ConnectionOpening(DbConnection connection, ConnectionEventData eventData, InterceptionResult result)
        {
            if (App.WebHostEnvironment.IsDevelopment())
            {
                MiniProfiler.Current.CustomTiming(miniProfilerName, $"Connection: [Id: {eventData.ConnectionId}] / [Database: {connection.Database}] / [Connection String: {connection.ConnectionString}]", "String");
            }
            return base.ConnectionOpening(connection, eventData, result);
        }

        /// <summary>
        /// 拦截数据库连接（异步）
        /// </summary>
        /// <param name="connection">数据库连接对象</param>
        /// <param name="eventData">数据库连接事件数据</param>
        /// <param name="result">拦截结果</param>
        /// <param name="cancellationToken">取消异步Token</param>
        /// <returns><see cref="ValueTask{TResult}"/></returns>
        public override ValueTask<InterceptionResult> ConnectionOpeningAsync(DbConnection connection, ConnectionEventData eventData, InterceptionResult result, CancellationToken cancellationToken = default)
        {
            if (App.WebHostEnvironment.IsDevelopment())
            {
                MiniProfiler.Current.CustomTiming(miniProfilerName, $"Connection: [Id: {eventData.ConnectionId}] / [Database: {connection.Database}] / [Connection String: {connection.ConnectionString}]", "String (Async)");
            }
            return base.ConnectionOpeningAsync(connection, eventData, result, cancellationToken);
        }
    }
}