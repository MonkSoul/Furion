namespace Fur.DatabaseAccessor
{
    /// <summary>
    /// 数据库提供器选项
    /// </summary>
    internal static class DatabaseProviderOptions
    {
        /// <summary>
        /// SqlServer 提供器程序集
        /// </summary>
        internal const string SqlServer = "Microsoft.EntityFrameworkCore.SqlServer";

        /// <summary>
        /// Sqlite 提供器程序集
        /// </summary>
        internal const string Sqlite = "Microsoft.EntityFrameworkCore.Sqlite";

        /// <summary>
        /// Cosmos 提供器程序集
        /// </summary>
        internal const string Cosmos = "Microsoft.EntityFrameworkCore.Cosmos";

        /// <summary>
        /// 内存数据库 提供器程序集
        /// </summary>
        internal const string InMemory = "Microsoft.EntityFrameworkCore.InMemory";

        /// <summary>
        /// MySql 提供器程序集
        /// </summary>
        internal const string MySql = "Pomelo.EntityFrameworkCore.MySql";

        /// <summary>
        /// PostgreSQL 提供器程序集
        /// </summary>
        internal const string PostgreSQL = "Npgsql.EntityFrameworkCore.PostgreSQL";
    }
}