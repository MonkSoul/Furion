using Fur.ApplicationBase.Attributes;
using Fur.DatabaseAccessor.Contexts.Identifiers;

namespace Fur.DatabaseAccessor.Models.Entities
{
    /// <summary>
    /// 数据库无键实体泛型抽象类
    /// <para>支持多数据库上下文配置</para>
    /// <para>如果你需要通过仓储方式操作视图、函数、存储过程，就需要继承该类</para>
    /// <para>通常只需要配置视图即可，函数和存储过程建议用切面上下文方式</para>
    /// </summary>
    /// <typeparam name="TDbContextIdentifier1">数据库上下文标识器</typeparam>
    [NonWrapper]
    public abstract class DbNoKeyEntityOfT<TDbContextIdentifier1> : DbNoKeyEntity
        where TDbContextIdentifier1 : IDbContextIdentifier
    {
        #region 构造函数 + public DbNoKeyEntityOfT(string dbDefinedName)
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbDefinedName">数据库定义名称，需包含 schema</param>
        public DbNoKeyEntityOfT(string dbDefinedName)
            : base(dbDefinedName) { }
        #endregion
    }

    /// <summary>
    /// 数据库无键实体泛型抽象类
    /// <para>支持多数据库上下文配置</para>
    /// <para>如果你需要通过仓储方式操作视图、函数、存储过程，就需要继承该类</para>
    /// <para>通常只需要配置视图即可，函数和存储过程建议用切面上下文方式</para>
    /// </summary>
    /// <typeparam name="TDbContextIdentifier1">数据库上下文标识器</typeparam>
    /// <typeparam name="TDbContextIdentifier2">数据库上下文标识器</typeparam>
    [NonWrapper]
    public abstract class DbNoKeyEntityOfT<TDbContextIdentifier1, TDbContextIdentifier2> : DbNoKeyEntity
        where TDbContextIdentifier1 : IDbContextIdentifier
        where TDbContextIdentifier2 : IDbContextIdentifier
    {
        #region 构造函数 + public DbNoKeyEntityOfT(string dbDefinedName)
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbDefinedName">数据库定义名称，需包含 schema</param>
        public DbNoKeyEntityOfT(string dbDefinedName)
            : base(dbDefinedName) { }
        #endregion
    }

    /// <summary>
    /// 数据库无键实体泛型抽象类
    /// <para>支持多数据库上下文配置</para>
    /// <para>如果你需要通过仓储方式操作视图、函数、存储过程，就需要继承该类</para>
    /// <para>通常只需要配置视图即可，函数和存储过程建议用切面上下文方式</para>
    /// </summary>
    /// <typeparam name="TDbContextIdentifier1">数据库上下文标识器</typeparam>
    /// <typeparam name="TDbContextIdentifier2">数据库上下文标识器</typeparam>
    /// <typeparam name="TDbContextIdentifier3">数据库上下文标识器</typeparam>
    [NonWrapper]
    public abstract class DbNoKeyEntityOfT<TDbContextIdentifier1, TDbContextIdentifier2, TDbContextIdentifier3> : DbNoKeyEntity
        where TDbContextIdentifier1 : IDbContextIdentifier
        where TDbContextIdentifier2 : IDbContextIdentifier
        where TDbContextIdentifier3 : IDbContextIdentifier
    {
        #region 构造函数 + public DbNoKeyEntityOfT(string dbDefinedName)
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbDefinedName">数据库定义名称，需包含 schema</param>
        public DbNoKeyEntityOfT(string dbDefinedName)
            : base(dbDefinedName) { }
        #endregion
    }

    /// <summary>
    /// 数据库无键实体泛型抽象类
    /// <para>支持多数据库上下文配置</para>
    /// <para>如果你需要通过仓储方式操作视图、函数、存储过程，就需要继承该类</para>
    /// <para>通常只需要配置视图即可，函数和存储过程建议用切面上下文方式</para>
    /// </summary>
    /// <typeparam name="TDbContextIdentifier1">数据库上下文标识器</typeparam>
    /// <typeparam name="TDbContextIdentifier2">数据库上下文标识器</typeparam>
    /// <typeparam name="TDbContextIdentifier3">数据库上下文标识器</typeparam>
    /// <typeparam name="TDbContextIdentifier4">数据库上下文标识器</typeparam>
    [NonWrapper]
    public abstract class DbNoKeyEntityOfT<TDbContextIdentifier1, TDbContextIdentifier2, TDbContextIdentifier3, TDbContextIdentifier4> : DbNoKeyEntity
        where TDbContextIdentifier1 : IDbContextIdentifier
        where TDbContextIdentifier2 : IDbContextIdentifier
        where TDbContextIdentifier3 : IDbContextIdentifier
        where TDbContextIdentifier4 : IDbContextIdentifier
    {
        #region 构造函数 + public DbNoKeyEntityOfT(string dbDefinedName)
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbDefinedName">数据库定义名称，需包含 schema</param>
        public DbNoKeyEntityOfT(string dbDefinedName)
            : base(dbDefinedName) { }
        #endregion
    }

    /// <summary>
    /// 数据库无键实体泛型抽象类
    /// <para>支持多数据库上下文配置</para>
    /// <para>如果你需要通过仓储方式操作视图、函数、存储过程，就需要继承该类</para>
    /// <para>通常只需要配置视图即可，函数和存储过程建议用切面上下文方式</para>
    /// </summary>
    /// <typeparam name="TDbContextIdentifier1">数据库上下文标识器</typeparam>
    /// <typeparam name="TDbContextIdentifier2">数据库上下文标识器</typeparam>
    /// <typeparam name="TDbContextIdentifier3">数据库上下文标识器</typeparam>
    /// <typeparam name="TDbContextIdentifier4">数据库上下文标识器</typeparam>
    /// <typeparam name="TDbContextIdentifier5">数据库上下文标识器</typeparam>
    [NonWrapper]
    public abstract class DbNoKeyEntityOfT<TDbContextIdentifier1, TDbContextIdentifier2, TDbContextIdentifier3, TDbContextIdentifier4, TDbContextIdentifier5> : DbNoKeyEntity
        where TDbContextIdentifier1 : IDbContextIdentifier
        where TDbContextIdentifier2 : IDbContextIdentifier
        where TDbContextIdentifier3 : IDbContextIdentifier
        where TDbContextIdentifier4 : IDbContextIdentifier
        where TDbContextIdentifier5 : IDbContextIdentifier
    {
        #region 构造函数 + public DbNoKeyEntityOfT(string dbDefinedName)
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbDefinedName">数据库定义名称，需包含 schema</param>
        public DbNoKeyEntityOfT(string dbDefinedName)
            : base(dbDefinedName) { }
        #endregion
    }

    /// <summary>
    /// 数据库无键实体泛型抽象类
    /// <para>支持多数据库上下文配置</para>
    /// <para>如果你需要通过仓储方式操作视图、函数、存储过程，就需要继承该类</para>
    /// <para>通常只需要配置视图即可，函数和存储过程建议用切面上下文方式</para>
    /// </summary>
    /// <typeparam name="TDbContextIdentifier1">数据库上下文标识器</typeparam>
    /// <typeparam name="TDbContextIdentifier2">数据库上下文标识器</typeparam>
    /// <typeparam name="TDbContextIdentifier3">数据库上下文标识器</typeparam>
    /// <typeparam name="TDbContextIdentifier4">数据库上下文标识器</typeparam>
    /// <typeparam name="TDbContextIdentifier5">数据库上下文标识器</typeparam>
    /// <typeparam name="TDbContextIdentifier6">数据库上下文标识器</typeparam>
    [NonWrapper]
    public abstract class DbNoKeyEntityOfT<TDbContextIdentifier1, TDbContextIdentifier2, TDbContextIdentifier3, TDbContextIdentifier4, TDbContextIdentifier5, TDbContextIdentifier6> : DbNoKeyEntity
        where TDbContextIdentifier1 : IDbContextIdentifier
        where TDbContextIdentifier2 : IDbContextIdentifier
        where TDbContextIdentifier3 : IDbContextIdentifier
        where TDbContextIdentifier4 : IDbContextIdentifier
        where TDbContextIdentifier5 : IDbContextIdentifier
        where TDbContextIdentifier6 : IDbContextIdentifier
    {
        #region 构造函数 + public DbNoKeyEntityOfT(string dbDefinedName)
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbDefinedName">数据库定义名称，需包含 schema</param>
        public DbNoKeyEntityOfT(string dbDefinedName)
            : base(dbDefinedName) { }
        #endregion
    }

    /// <summary>
    /// 数据库无键实体泛型抽象类
    /// <para>支持多数据库上下文配置</para>
    /// <para>如果你需要通过仓储方式操作视图、函数、存储过程，就需要继承该类</para>
    /// <para>通常只需要配置视图即可，函数和存储过程建议用切面上下文方式</para>
    /// </summary>
    /// <typeparam name="TDbContextIdentifier1">数据库上下文标识器</typeparam>
    /// <typeparam name="TDbContextIdentifier2">数据库上下文标识器</typeparam>
    /// <typeparam name="TDbContextIdentifier3">数据库上下文标识器</typeparam>
    /// <typeparam name="TDbContextIdentifier4">数据库上下文标识器</typeparam>
    /// <typeparam name="TDbContextIdentifier5">数据库上下文标识器</typeparam>
    /// <typeparam name="TDbContextIdentifier6">数据库上下文标识器</typeparam>
    /// <typeparam name="TDbContextIdentifier7">数据库上下文标识器</typeparam>
    [NonWrapper]
    public abstract class DbNoKeyEntityOfT<TDbContextIdentifier1, TDbContextIdentifier2, TDbContextIdentifier3, TDbContextIdentifier4, TDbContextIdentifier5, TDbContextIdentifier6, TDbContextIdentifier7> : DbNoKeyEntity
        where TDbContextIdentifier1 : IDbContextIdentifier
        where TDbContextIdentifier2 : IDbContextIdentifier
        where TDbContextIdentifier3 : IDbContextIdentifier
        where TDbContextIdentifier4 : IDbContextIdentifier
        where TDbContextIdentifier5 : IDbContextIdentifier
        where TDbContextIdentifier6 : IDbContextIdentifier
        where TDbContextIdentifier7 : IDbContextIdentifier
    {
        #region 构造函数 + public DbNoKeyEntityOfT(string dbDefinedName)
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbDefinedName">数据库定义名称，需包含 schema</param>
        public DbNoKeyEntityOfT(string dbDefinedName)
            : base(dbDefinedName) { }
        #endregion
    }

    /// <summary>
    /// 数据库无键实体泛型抽象类
    /// <para>支持多数据库上下文配置</para>
    /// <para>如果你需要通过仓储方式操作视图、函数、存储过程，就需要继承该类</para>
    /// <para>通常只需要配置视图即可，函数和存储过程建议用切面上下文方式</para>
    /// </summary>
    /// <typeparam name="TDbContextIdentifier1">数据库上下文标识器</typeparam>
    /// <typeparam name="TDbContextIdentifier2">数据库上下文标识器</typeparam>
    /// <typeparam name="TDbContextIdentifier3">数据库上下文标识器</typeparam>
    /// <typeparam name="TDbContextIdentifier4">数据库上下文标识器</typeparam>
    /// <typeparam name="TDbContextIdentifier5">数据库上下文标识器</typeparam>
    /// <typeparam name="TDbContextIdentifier6">数据库上下文标识器</typeparam>
    /// <typeparam name="TDbContextIdentifier7">数据库上下文标识器</typeparam>
    /// <typeparam name="TDbContextIdentifier8">数据库上下文标识器</typeparam>
    [NonWrapper]
    public abstract class DbNoKeyEntityOfT<TDbContextIdentifier1, TDbContextIdentifier2, TDbContextIdentifier3, TDbContextIdentifier4, TDbContextIdentifier5, TDbContextIdentifier6, TDbContextIdentifier7, TDbContextIdentifier8> : DbNoKeyEntity
        where TDbContextIdentifier1 : IDbContextIdentifier
        where TDbContextIdentifier2 : IDbContextIdentifier
        where TDbContextIdentifier3 : IDbContextIdentifier
        where TDbContextIdentifier4 : IDbContextIdentifier
        where TDbContextIdentifier5 : IDbContextIdentifier
        where TDbContextIdentifier6 : IDbContextIdentifier
        where TDbContextIdentifier7 : IDbContextIdentifier
        where TDbContextIdentifier8 : IDbContextIdentifier
    {
        #region 构造函数 + public DbNoKeyEntityOfT(string dbDefinedName)
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbDefinedName">数据库定义名称，需包含 schema</param>
        public DbNoKeyEntityOfT(string dbDefinedName)
            : base(dbDefinedName) { }
        #endregion
    }
}