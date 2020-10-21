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

namespace Fur.DatabaseAccessor
{
    /// <summary>
    /// 数据库无键实体依赖基接口
    /// </summary>
    [SkipScan]
    public abstract class EntityNotKey : EntityNotKey<MasterDbContextLocator>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">数据库中定义名</param>
        public EntityNotKey(string name) : base(name)
        {
        }
    }

    /// <summary>
    /// 数据库无键实体依赖基接口
    /// </summary>
    /// <typeparam name="TDbContextLocator1">数据库上下文定位器</typeparam>
    [SkipScan]
    public abstract class EntityNotKey<TDbContextLocator1> : PrivateEntityNotKey
        where TDbContextLocator1 : class, IDbContextLocator
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">数据库中定义名</param>
        public EntityNotKey(string name) : base(name)
        {
        }
    }

    /// <summary>
    /// 数据库无键实体依赖基接口
    /// </summary>
    /// <typeparam name="TDbContextLocator1">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator2">数据库上下文定位器</typeparam>
    [SkipScan]
    public abstract class EntityNotKey<TDbContextLocator1, TDbContextLocator2> : PrivateEntityNotKey
        where TDbContextLocator2 : class, IDbContextLocator
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">数据库中定义名</param>
        public EntityNotKey(string name) : base(name)
        {
        }
    }

    /// <summary>
    /// 数据库无键实体依赖基接口
    /// </summary>
    /// <typeparam name="TDbContextLocator1">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator2">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator3">数据库上下文定位器</typeparam>
    [SkipScan]
    public abstract class EntityNotKey<TDbContextLocator1, TDbContextLocator2, TDbContextLocator3> : PrivateEntityNotKey
        where TDbContextLocator1 : class, IDbContextLocator
        where TDbContextLocator2 : class, IDbContextLocator
        where TDbContextLocator3 : class, IDbContextLocator
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">数据库中定义名</param>
        public EntityNotKey(string name) : base(name)
        {
        }
    }

    /// <summary>
    /// 数据库无键实体依赖基接口
    /// </summary>
    /// <typeparam name="TDbContextLocator1">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator2">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator3">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator4">数据库上下文定位器</typeparam>
    [SkipScan]
    public abstract class EntityNotKey<TDbContextLocator1, TDbContextLocator2, TDbContextLocator3, TDbContextLocator4> : PrivateEntityNotKey
        where TDbContextLocator1 : class, IDbContextLocator
        where TDbContextLocator2 : class, IDbContextLocator
        where TDbContextLocator3 : class, IDbContextLocator
        where TDbContextLocator4 : class, IDbContextLocator
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">数据库中定义名</param>
        public EntityNotKey(string name) : base(name)
        {
        }
    }

    /// <summary>
    /// 数据库无键实体依赖基接口
    /// </summary>
    /// <typeparam name="TDbContextLocator1">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator2">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator3">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator4">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator5">数据库上下文定位器</typeparam>
    [SkipScan]
    public abstract class EntityNotKey<TDbContextLocator1, TDbContextLocator2, TDbContextLocator3, TDbContextLocator4, TDbContextLocator5> : PrivateEntityNotKey
        where TDbContextLocator1 : class, IDbContextLocator
        where TDbContextLocator2 : class, IDbContextLocator
        where TDbContextLocator3 : class, IDbContextLocator
        where TDbContextLocator4 : class, IDbContextLocator
        where TDbContextLocator5 : class, IDbContextLocator
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">数据库中定义名</param>
        public EntityNotKey(string name) : base(name)
        {
        }
    }

    /// <summary>
    /// 数据库无键实体依赖基接口
    /// </summary>
    /// <typeparam name="TDbContextLocator1">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator2">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator3">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator4">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator5">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator6">数据库上下文定位器</typeparam>
    [SkipScan]
    public abstract class EntityNotKey<TDbContextLocator1, TDbContextLocator2, TDbContextLocator3, TDbContextLocator4, TDbContextLocator5, TDbContextLocator6> : PrivateEntityNotKey
        where TDbContextLocator1 : class, IDbContextLocator
        where TDbContextLocator2 : class, IDbContextLocator
        where TDbContextLocator3 : class, IDbContextLocator
        where TDbContextLocator4 : class, IDbContextLocator
        where TDbContextLocator5 : class, IDbContextLocator
        where TDbContextLocator6 : class, IDbContextLocator
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">数据库中定义名</param>
        public EntityNotKey(string name) : base(name)
        {
        }
    }

    /// <summary>
    /// 数据库无键实体依赖基接口
    /// </summary>
    /// <typeparam name="TDbContextLocator1">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator2">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator3">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator4">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator5">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator6">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator7">数据库上下文定位器</typeparam>
    [SkipScan]
    public abstract class EntityNotKey<TDbContextLocator1, TDbContextLocator2, TDbContextLocator3, TDbContextLocator4, TDbContextLocator5, TDbContextLocator6, TDbContextLocator7> : PrivateEntityNotKey
        where TDbContextLocator1 : class, IDbContextLocator
        where TDbContextLocator2 : class, IDbContextLocator
        where TDbContextLocator3 : class, IDbContextLocator
        where TDbContextLocator4 : class, IDbContextLocator
        where TDbContextLocator5 : class, IDbContextLocator
        where TDbContextLocator6 : class, IDbContextLocator
        where TDbContextLocator7 : class, IDbContextLocator
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">数据库中定义名</param>
        public EntityNotKey(string name) : base(name)
        {
        }
    }

    /// <summary>
    /// 数据库无键实体依赖基接口
    /// </summary>
    /// <typeparam name="TDbContextLocator1">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator2">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator3">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator4">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator5">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator6">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator7">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator8">数据库上下文定位器</typeparam>
    [SkipScan]
    public abstract class EntityNotKey<TDbContextLocator1, TDbContextLocator2, TDbContextLocator3, TDbContextLocator4, TDbContextLocator5, TDbContextLocator6, TDbContextLocator7, TDbContextLocator8> : PrivateEntityNotKey
        where TDbContextLocator1 : class, IDbContextLocator
        where TDbContextLocator2 : class, IDbContextLocator
        where TDbContextLocator3 : class, IDbContextLocator
        where TDbContextLocator4 : class, IDbContextLocator
        where TDbContextLocator5 : class, IDbContextLocator
        where TDbContextLocator6 : class, IDbContextLocator
        where TDbContextLocator7 : class, IDbContextLocator
        where TDbContextLocator8 : class, IDbContextLocator
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">数据库中定义名</param>
        public EntityNotKey(string name) : base(name)
        {
        }
    }

    /// <summary>
    /// 数据库无键实体基类（禁止外部继承）
    /// </summary>
    [SkipScan]
    public abstract class PrivateEntityNotKey : IPrivateEntityNotKey
    {
        /// <summary>
        /// 无键实体名
        /// </summary>
        private readonly string _name;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">数据库中定义名</param>
        public PrivateEntityNotKey(string name)
        {
            _name = name;
        }

        /// <summary>
        /// 获取视图名称
        /// </summary>
        /// <returns></returns>
        public string GetName()
        {
            return _name;
        }
    }
}