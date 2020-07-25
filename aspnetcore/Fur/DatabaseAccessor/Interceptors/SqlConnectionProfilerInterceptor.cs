using Microsoft.EntityFrameworkCore.Diagnostics;
using StackExchange.Profiling;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Fur.DatabaseAccessor.Interceptors
{
    public class SqlConnectionProfilerInterceptor : DbConnectionInterceptor
    {
        private static readonly string miniProfilerName = "connection";

        public override InterceptionResult ConnectionOpening(DbConnection connection, ConnectionEventData eventData, InterceptionResult result)
        {
            MiniProfiler.Current.CustomTiming(miniProfilerName, connection.ConnectionString, "String");
            return base.ConnectionOpening(connection, eventData, result);
        }
        public override ValueTask<InterceptionResult> ConnectionOpeningAsync(DbConnection connection, ConnectionEventData eventData, InterceptionResult result, CancellationToken cancellationToken = default)
        {
            MiniProfiler.Current.CustomTiming(miniProfilerName, connection.ConnectionString, "String (Async)");
            return base.ConnectionOpeningAsync(connection, eventData, result, cancellationToken);
        }
    }
}