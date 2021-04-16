using Furion.DependencyInjection;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;

namespace Furion.DatabaseAccessor.Extensions
{
    /// <summary>
    /// 构建 Sql 执行部分
    /// </summary>
    [SkipScan]
    public sealed partial class SqlBuilderPart
    {
        /// <summary>
        /// Sql 字符串
        /// </summary>
        public string SqlString { get; private set; }

        /// <summary>
        /// 数据库上下文定位器
        /// </summary>
        public Type DbContextLocator { get; private set; } = typeof(MasterDbContextLocator);

        /// <summary>
        /// 设置服务提供器
        /// </summary>
        public IServiceProvider ServiceProvider { get; private set; }

        /// <summary>
        /// 设置 Sql 字符串
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public SqlBuilderPart SetSqlString(string sql)
        {
            SqlString = sql;
            return this;
        }

        /// <summary>
        /// 设置服务提供器
        /// </summary>
        /// <param name="scoped"></param>
        /// <returns></returns>
        public SqlBuilderPart SetServiceProvider(IServiceProvider scoped)
        {
            ServiceProvider = scoped;
            return this;
        }

        /// <summary>
        /// 设置数据库上下文定位器
        /// </summary>
        /// <typeparam name="TDbContextLocator"></typeparam>
        /// <returns></returns>
        public SqlBuilderPart Change<TDbContextLocator>()
            where TDbContextLocator : class, IDbContextLocator
        {
            DbContextLocator = typeof(TDbContextLocator) ?? typeof(MasterDbContextLocator);
            return this;
        }

        /// <summary>
        /// 设置数据库上下文定位器
        /// </summary>
        /// <returns></returns>
        public SqlBuilderPart Change(Type dbContextLocator)
        {
            DbContextLocator = dbContextLocator ?? typeof(MasterDbContextLocator);
            return this;
        }

        /// <summary>
        /// 构建数据库对象
        /// </summary>
        /// <returns></returns>
        private DatabaseFacade Build()
        {
            var sqlRepositoryType = typeof(ISqlRepository<>).MakeGenericType(DbContextLocator);
            var sqlRepository = App.GetService(sqlRepositoryType, ServiceProvider);

            // 反射读取值
            return sqlRepositoryType.GetProperty(nameof(ISqlRepository.Database)).GetValue(sqlRepository) as DatabaseFacade;
        }
    }
}