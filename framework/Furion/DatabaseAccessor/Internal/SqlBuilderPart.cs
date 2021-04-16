using Furion.DependencyInjection;
using System;

namespace Furion.DatabaseAccessor
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
        public IServiceProvider DbScoped { get; private set; }
    }
}