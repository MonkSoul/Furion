using Fur.ApplicationBase.Attributes;
using Fur.DatabaseAccessor.Contexts.Locators;

namespace Fur.DatabaseAccessor.Models.Entities
{
    /// <summary>
    /// 数据库无键实体泛型抽象类
    /// <para>支持多数据库上下文配置</para>
    /// <para>如果你需要通过仓储方式操作视图、函数、存储过程，就需要继承该类</para>
    /// <para>通常只需要配置视图即可，函数和存储过程建议用切面上下文方式</para>
    /// </summary>
    /// <typeparam name="TDbContextLocator1">数据库上下文定位器</typeparam>
    [NonWrapper]
    public abstract class DbNoKeyEntityOfT<TDbContextLocator1> : DbNoKeyEntity
        where TDbContextLocator1 : IDbContextLocator
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
    /// <typeparam name="TDbContextLocator1">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator2">数据库上下文定位器</typeparam>
    [NonWrapper]
    public abstract class DbNoKeyEntityOfT<TDbContextLocator1, TDbContextLocator2> : DbNoKeyEntity
        where TDbContextLocator1 : IDbContextLocator
        where TDbContextLocator2 : IDbContextLocator
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
    /// <typeparam name="TDbContextLocator1">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator2">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator3">数据库上下文定位器</typeparam>
    [NonWrapper]
    public abstract class DbNoKeyEntityOfT<TDbContextLocator1, TDbContextLocator2, TDbContextLocator3> : DbNoKeyEntity
        where TDbContextLocator1 : IDbContextLocator
        where TDbContextLocator2 : IDbContextLocator
        where TDbContextLocator3 : IDbContextLocator
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
    /// <typeparam name="TDbContextLocator1">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator2">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator3">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator4">数据库上下文定位器</typeparam>
    [NonWrapper]
    public abstract class DbNoKeyEntityOfT<TDbContextLocator1, TDbContextLocator2, TDbContextLocator3, TDbContextLocator4> : DbNoKeyEntity
        where TDbContextLocator1 : IDbContextLocator
        where TDbContextLocator2 : IDbContextLocator
        where TDbContextLocator3 : IDbContextLocator
        where TDbContextLocator4 : IDbContextLocator
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
    /// <typeparam name="TDbContextLocator1">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator2">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator3">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator4">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator5">数据库上下文定位器</typeparam>
    [NonWrapper]
    public abstract class DbNoKeyEntityOfT<TDbContextLocator1, TDbContextLocator2, TDbContextLocator3, TDbContextLocator4, TDbContextLocator5> : DbNoKeyEntity
        where TDbContextLocator1 : IDbContextLocator
        where TDbContextLocator2 : IDbContextLocator
        where TDbContextLocator3 : IDbContextLocator
        where TDbContextLocator4 : IDbContextLocator
        where TDbContextLocator5 : IDbContextLocator
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
    /// <typeparam name="TDbContextLocator1">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator2">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator3">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator4">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator5">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator6">数据库上下文定位器</typeparam>
    [NonWrapper]
    public abstract class DbNoKeyEntityOfT<TDbContextLocator1, TDbContextLocator2, TDbContextLocator3, TDbContextLocator4, TDbContextLocator5, TDbContextLocator6> : DbNoKeyEntity
        where TDbContextLocator1 : IDbContextLocator
        where TDbContextLocator2 : IDbContextLocator
        where TDbContextLocator3 : IDbContextLocator
        where TDbContextLocator4 : IDbContextLocator
        where TDbContextLocator5 : IDbContextLocator
        where TDbContextLocator6 : IDbContextLocator
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
    /// <typeparam name="TDbContextLocator1">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator2">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator3">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator4">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator5">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator6">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator7">数据库上下文定位器</typeparam>
    [NonWrapper]
    public abstract class DbNoKeyEntityOfT<TDbContextLocator1, TDbContextLocator2, TDbContextLocator3, TDbContextLocator4, TDbContextLocator5, TDbContextLocator6, TDbContextLocator7> : DbNoKeyEntity
        where TDbContextLocator1 : IDbContextLocator
        where TDbContextLocator2 : IDbContextLocator
        where TDbContextLocator3 : IDbContextLocator
        where TDbContextLocator4 : IDbContextLocator
        where TDbContextLocator5 : IDbContextLocator
        where TDbContextLocator6 : IDbContextLocator
        where TDbContextLocator7 : IDbContextLocator
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
    /// <typeparam name="TDbContextLocator1">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator2">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator3">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator4">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator5">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator6">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator7">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator8">数据库上下文定位器</typeparam>
    [NonWrapper]
    public abstract class DbNoKeyEntityOfT<TDbContextLocator1, TDbContextLocator2, TDbContextLocator3, TDbContextLocator4, TDbContextLocator5, TDbContextLocator6, TDbContextLocator7, TDbContextLocator8> : DbNoKeyEntity
        where TDbContextLocator1 : IDbContextLocator
        where TDbContextLocator2 : IDbContextLocator
        where TDbContextLocator3 : IDbContextLocator
        where TDbContextLocator4 : IDbContextLocator
        where TDbContextLocator5 : IDbContextLocator
        where TDbContextLocator6 : IDbContextLocator
        where TDbContextLocator7 : IDbContextLocator
        where TDbContextLocator8 : IDbContextLocator
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