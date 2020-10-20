// -----------------------------------------------------------------------------
// Fur 是 .NET 5 平台下企业应用开发最佳实践框架。
// Copyright © 2020 Fur, Baiqian Co.,Ltd.
//
// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0-rc.final.17
// 官方网站：https://chinadot.net
// 源码地址：Gitee：https://gitee.com/monksoul/Fur
// 				    Github：https://github.com/monksoul/Fur
// 开源协议：Apache-2.0（http://www.apache.org/licenses/LICENSE-2.0）
// -----------------------------------------------------------------------------

using Fur.DependencyInjection;

namespace Fur.DatabaseAccessor
{
    /// <summary>
    /// 主从库仓储
    /// </summary>
    /// <typeparam name="TMasterDbContextLocator">主库</typeparam>
    /// <typeparam name="TSlaveDbContextLocator1">从库</typeparam>
    [SkipScan]
    public class MSRepository<TMasterDbContextLocator, TSlaveDbContextLocator1> : IMSRepository<TMasterDbContextLocator, TSlaveDbContextLocator1>
        where TMasterDbContextLocator : class, IDbContextLocator
        where TSlaveDbContextLocator1 : class, IDbContextLocator
    {
        /// <summary>
        /// 非泛型仓储
        /// </summary>
        private readonly IRepository _repository;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repository">非泛型仓储</param>
        public MSRepository(IRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// 获取主库仓储
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <returns></returns>
        public virtual IRepository<TEntity, TMasterDbContextLocator> Master<TEntity>()
            where TEntity : class, IPrivateEntity, new()
        {
            return _repository.Change<TEntity, TMasterDbContextLocator>();
        }

        /// <summary>
        /// 获取从库仓储
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <returns></returns>
        public virtual IReadableRepository<TEntity, TSlaveDbContextLocator1> Slave1<TEntity>()
            where TEntity : class, IPrivateEntity, new()
        {
            return _repository.Change<TEntity, TSlaveDbContextLocator1>().Constraint<IReadableRepository<TEntity, TSlaveDbContextLocator1>>();
        }
    }

    /// <summary>
    /// 主从库仓储
    /// </summary>
    /// <typeparam name="TMasterDbContextLocator">主库</typeparam>
    /// <typeparam name="TSlaveDbContextLocator1">从库</typeparam>
    /// <typeparam name="TSlaveDbContextLocator2">从库</typeparam>
    [SkipScan]
    public class MSRepository<TMasterDbContextLocator, TSlaveDbContextLocator1, TSlaveDbContextLocator2>
        : MSRepository<TMasterDbContextLocator, TSlaveDbContextLocator1>
        , IMSRepository<TMasterDbContextLocator, TSlaveDbContextLocator1, TSlaveDbContextLocator2>
        where TMasterDbContextLocator : class, IDbContextLocator
        where TSlaveDbContextLocator1 : class, IDbContextLocator
        where TSlaveDbContextLocator2 : class, IDbContextLocator
    {
        /// <summary>
        /// 非泛型仓储
        /// </summary>
        private readonly IRepository _repository;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repository">非泛型仓储</param>
        public MSRepository(IRepository repository) : base(repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// 获取从库仓储2
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <returns></returns>
        public virtual IReadableRepository<TEntity, TSlaveDbContextLocator2> Slave2<TEntity>()
            where TEntity : class, IPrivateEntity, new()
        {
            return _repository.Change<TEntity, TSlaveDbContextLocator2>().Constraint<IReadableRepository<TEntity, TSlaveDbContextLocator2>>();
        }
    }

    /// <summary>
    /// 主从库仓储
    /// </summary>
    /// <typeparam name="TMasterDbContextLocator">主库</typeparam>
    /// <typeparam name="TSlaveDbContextLocator1">从库</typeparam>
    /// <typeparam name="TSlaveDbContextLocator2">从库</typeparam>
    /// <typeparam name="TSlaveDbContextLocator3">从库</typeparam>
    [SkipScan]
    public class MSRepository<TMasterDbContextLocator, TSlaveDbContextLocator1, TSlaveDbContextLocator2, TSlaveDbContextLocator3>
        : MSRepository<TMasterDbContextLocator, TSlaveDbContextLocator1, TSlaveDbContextLocator2>
        , IMSRepository<TMasterDbContextLocator, TSlaveDbContextLocator1, TSlaveDbContextLocator2, TSlaveDbContextLocator3>
        where TMasterDbContextLocator : class, IDbContextLocator
        where TSlaveDbContextLocator1 : class, IDbContextLocator
        where TSlaveDbContextLocator2 : class, IDbContextLocator
        where TSlaveDbContextLocator3 : class, IDbContextLocator
    {
        /// <summary>
        /// 非泛型仓储
        /// </summary>
        private readonly IRepository _repository;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repository">非泛型仓储</param>
        public MSRepository(IRepository repository) : base(repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// 获取从库仓储3
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <returns></returns>
        public virtual IReadableRepository<TEntity, TSlaveDbContextLocator3> Slave3<TEntity>()
            where TEntity : class, IPrivateEntity, new()
        {
            return _repository.Change<TEntity, TSlaveDbContextLocator3>().Constraint<IReadableRepository<TEntity, TSlaveDbContextLocator3>>();
        }
    }

    /// <summary>
    /// 主从库仓储
    /// </summary>
    /// <typeparam name="TMasterDbContextLocator">主库</typeparam>
    /// <typeparam name="TSlaveDbContextLocator1">从库</typeparam>
    /// <typeparam name="TSlaveDbContextLocator2">从库</typeparam>
    /// <typeparam name="TSlaveDbContextLocator3">从库</typeparam>
    /// <typeparam name="TSlaveDbContextLocator4">从库</typeparam>
    [SkipScan]
    public class MSRepository<TMasterDbContextLocator, TSlaveDbContextLocator1, TSlaveDbContextLocator2, TSlaveDbContextLocator3, TSlaveDbContextLocator4>
        : MSRepository<TMasterDbContextLocator, TSlaveDbContextLocator1, TSlaveDbContextLocator2, TSlaveDbContextLocator3>
        , IMSRepository<TMasterDbContextLocator, TSlaveDbContextLocator1, TSlaveDbContextLocator2, TSlaveDbContextLocator3, TSlaveDbContextLocator4>
        where TMasterDbContextLocator : class, IDbContextLocator
        where TSlaveDbContextLocator1 : class, IDbContextLocator
        where TSlaveDbContextLocator2 : class, IDbContextLocator
        where TSlaveDbContextLocator3 : class, IDbContextLocator
        where TSlaveDbContextLocator4 : class, IDbContextLocator
    {
        /// <summary>
        /// 非泛型仓储
        /// </summary>
        private readonly IRepository _repository;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repository">非泛型仓储</param>
        public MSRepository(IRepository repository) : base(repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// 获取从库仓储4
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <returns></returns>
        public virtual IReadableRepository<TEntity, TSlaveDbContextLocator4> Slave4<TEntity>()
            where TEntity : class, IPrivateEntity, new()
        {
            return _repository.Change<TEntity, TSlaveDbContextLocator4>().Constraint<IReadableRepository<TEntity, TSlaveDbContextLocator4>>();
        }
    }

    /// <summary>
    /// 主从库仓储
    /// </summary>
    /// <typeparam name="TMasterDbContextLocator">主库</typeparam>
    /// <typeparam name="TSlaveDbContextLocator1">从库</typeparam>
    /// <typeparam name="TSlaveDbContextLocator2">从库</typeparam>
    /// <typeparam name="TSlaveDbContextLocator3">从库</typeparam>
    /// <typeparam name="TSlaveDbContextLocator4">从库</typeparam>
    /// <typeparam name="TSlaveDbContextLocator5">从库</typeparam>
    [SkipScan]
    public class MSRepository<TMasterDbContextLocator, TSlaveDbContextLocator1, TSlaveDbContextLocator2, TSlaveDbContextLocator3, TSlaveDbContextLocator4, TSlaveDbContextLocator5>
        : MSRepository<TMasterDbContextLocator, TSlaveDbContextLocator1, TSlaveDbContextLocator2, TSlaveDbContextLocator3, TSlaveDbContextLocator4>
        , IMSRepository<TMasterDbContextLocator, TSlaveDbContextLocator1, TSlaveDbContextLocator2, TSlaveDbContextLocator3, TSlaveDbContextLocator4, TSlaveDbContextLocator5>
        where TMasterDbContextLocator : class, IDbContextLocator
        where TSlaveDbContextLocator1 : class, IDbContextLocator
        where TSlaveDbContextLocator2 : class, IDbContextLocator
        where TSlaveDbContextLocator3 : class, IDbContextLocator
        where TSlaveDbContextLocator4 : class, IDbContextLocator
        where TSlaveDbContextLocator5 : class, IDbContextLocator
    {
        /// <summary>
        /// 非泛型仓储
        /// </summary>
        private readonly IRepository _repository;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repository">非泛型仓储</param>
        public MSRepository(IRepository repository) : base(repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// 获取从库仓储5
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <returns></returns>
        public virtual IReadableRepository<TEntity, TSlaveDbContextLocator5> Slave5<TEntity>()
            where TEntity : class, IPrivateEntity, new()
        {
            return _repository.Change<TEntity, TSlaveDbContextLocator5>().Constraint<IReadableRepository<TEntity, TSlaveDbContextLocator5>>();
        }
    }

    /// <summary>
    /// 主从库仓储
    /// </summary>
    /// <typeparam name="TMasterDbContextLocator">主库</typeparam>
    /// <typeparam name="TSlaveDbContextLocator1">从库</typeparam>
    /// <typeparam name="TSlaveDbContextLocator2">从库</typeparam>
    /// <typeparam name="TSlaveDbContextLocator3">从库</typeparam>
    /// <typeparam name="TSlaveDbContextLocator4">从库</typeparam>
    /// <typeparam name="TSlaveDbContextLocator5">从库</typeparam>
    /// <typeparam name="TSlaveDbContextLocator6">从库</typeparam>
    [SkipScan]
    public class MSRepository<TMasterDbContextLocator, TSlaveDbContextLocator1, TSlaveDbContextLocator2, TSlaveDbContextLocator3, TSlaveDbContextLocator4, TSlaveDbContextLocator5, TSlaveDbContextLocator6>
        : MSRepository<TMasterDbContextLocator, TSlaveDbContextLocator1, TSlaveDbContextLocator2, TSlaveDbContextLocator3, TSlaveDbContextLocator4, TSlaveDbContextLocator5>
        , IMSRepository<TMasterDbContextLocator, TSlaveDbContextLocator1, TSlaveDbContextLocator2, TSlaveDbContextLocator3, TSlaveDbContextLocator4, TSlaveDbContextLocator5, TSlaveDbContextLocator6>
        where TMasterDbContextLocator : class, IDbContextLocator
        where TSlaveDbContextLocator1 : class, IDbContextLocator
        where TSlaveDbContextLocator2 : class, IDbContextLocator
        where TSlaveDbContextLocator3 : class, IDbContextLocator
        where TSlaveDbContextLocator4 : class, IDbContextLocator
        where TSlaveDbContextLocator5 : class, IDbContextLocator
        where TSlaveDbContextLocator6 : class, IDbContextLocator
    {
        /// <summary>
        /// 非泛型仓储
        /// </summary>
        private readonly IRepository _repository;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repository">非泛型仓储</param>
        public MSRepository(IRepository repository) : base(repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// 获取从库仓储6
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <returns></returns>
        public virtual IReadableRepository<TEntity, TSlaveDbContextLocator6> Slave6<TEntity>()
            where TEntity : class, IPrivateEntity, new()
        {
            return _repository.Change<TEntity, TSlaveDbContextLocator6>().Constraint<IReadableRepository<TEntity, TSlaveDbContextLocator6>>();
        }
    }

    /// <summary>
    /// 主从库仓储
    /// </summary>
    /// <typeparam name="TMasterDbContextLocator">主库</typeparam>
    /// <typeparam name="TSlaveDbContextLocator1">从库</typeparam>
    /// <typeparam name="TSlaveDbContextLocator2">从库</typeparam>
    /// <typeparam name="TSlaveDbContextLocator3">从库</typeparam>
    /// <typeparam name="TSlaveDbContextLocator4">从库</typeparam>
    /// <typeparam name="TSlaveDbContextLocator5">从库</typeparam>
    /// <typeparam name="TSlaveDbContextLocator6">从库</typeparam>
    /// <typeparam name="TSlaveDbContextLocator7">从库</typeparam>
    [SkipScan]
    public class MSRepository<TMasterDbContextLocator, TSlaveDbContextLocator1, TSlaveDbContextLocator2, TSlaveDbContextLocator3, TSlaveDbContextLocator4, TSlaveDbContextLocator5, TSlaveDbContextLocator6, TSlaveDbContextLocator7>
        : MSRepository<TMasterDbContextLocator, TSlaveDbContextLocator1, TSlaveDbContextLocator2, TSlaveDbContextLocator3, TSlaveDbContextLocator4, TSlaveDbContextLocator5, TSlaveDbContextLocator6>
        , IMSRepository<TMasterDbContextLocator, TSlaveDbContextLocator1, TSlaveDbContextLocator2, TSlaveDbContextLocator3, TSlaveDbContextLocator4, TSlaveDbContextLocator5, TSlaveDbContextLocator6, TSlaveDbContextLocator7>
        where TMasterDbContextLocator : class, IDbContextLocator
        where TSlaveDbContextLocator1 : class, IDbContextLocator
        where TSlaveDbContextLocator2 : class, IDbContextLocator
        where TSlaveDbContextLocator3 : class, IDbContextLocator
        where TSlaveDbContextLocator4 : class, IDbContextLocator
        where TSlaveDbContextLocator5 : class, IDbContextLocator
        where TSlaveDbContextLocator6 : class, IDbContextLocator
        where TSlaveDbContextLocator7 : class, IDbContextLocator
    {
        /// <summary>
        /// 非泛型仓储
        /// </summary>
        private readonly IRepository _repository;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repository">非泛型仓储</param>
        public MSRepository(IRepository repository) : base(repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// 获取从库仓储7
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <returns></returns>
        public virtual IReadableRepository<TEntity, TSlaveDbContextLocator7> Slave7<TEntity>()
            where TEntity : class, IPrivateEntity, new()
        {
            return _repository.Change<TEntity, TSlaveDbContextLocator7>().Constraint<IReadableRepository<TEntity, TSlaveDbContextLocator7>>();
        }
    }
}