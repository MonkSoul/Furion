using System;

namespace Furion.DatabaseAccessor
{
    /// <summary>
    /// 构建 Sql 字符串执行部件
    /// </summary>
    public sealed partial class SqlStringExecutePart
    {
        /// <summary>
        /// 设置 Sql 字符串
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public SqlStringExecutePart SetSqlString(string sql)
        {
            SqlString = sql;
            return this;
        }

        /// <summary>
        /// 设置数据库执行作用域
        /// </summary>
        /// <param name="scoped"></param>
        /// <returns></returns>
        public SqlStringExecutePart SetContextScoped(IServiceProvider scoped)
        {
            ContextScoped = scoped;
            return this;
        }

        /// <summary>
        /// 设置数据库上下文定位器
        /// </summary>
        /// <typeparam name="TDbContextLocator"></typeparam>
        /// <returns></returns>
        public SqlStringExecutePart Change<TDbContextLocator>()
            where TDbContextLocator : class, IDbContextLocator
        {
            DbContextLocator = typeof(TDbContextLocator) ?? typeof(MasterDbContextLocator);
            return this;
        }

        /// <summary>
        /// 设置数据库上下文定位器
        /// </summary>
        /// <returns></returns>
        public SqlStringExecutePart Change(Type dbContextLocator)
        {
            DbContextLocator = dbContextLocator ?? typeof(MasterDbContextLocator);
            return this;
        }
    }
}