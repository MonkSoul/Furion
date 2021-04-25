using Furion.DatabaseAccessor;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Furion.DependencyInjection
{
    /// <summary>
    /// 创建作用域静态类
    /// </summary>
    [SkipScan]
    public static class Scoped
    {
        /// <summary>
        /// 创建一个作用域范围
        /// </summary>
        /// <param name="handle"></param>
        public static void Create(Action<IServiceScopeFactory, IServiceScope> handle)
        {
            if (handle == null) throw new ArgumentNullException(nameof(handle));

            // 解析服务作用域工厂
            var scopeFactory = InternalApp.InternalServices.BuildServiceProvider().GetService<IServiceScopeFactory>();
            using var scoped = scopeFactory.CreateScope();

            // 执行方法
            handle.Invoke(scopeFactory, scoped);
        }

        /// <summary>
        /// 创建一个工作单元作用域
        /// </summary>
        /// <param name="handle"></param>
        public static void CreateUnitOfWork(Action<IServiceScopeFactory, IServiceScope> handle)
        {
            if (handle == null) throw new ArgumentNullException(nameof(handle));

            // 解析服务作用域工厂
            var scopeFactory = InternalApp.InternalServices.BuildServiceProvider().GetService<IServiceScopeFactory>();
            using var scoped = scopeFactory.CreateScope();

            // 创建一个数据库上下文池
            var dbContextPool = scoped.ServiceProvider.GetService<IDbContextPool>();
            handle.Invoke(scopeFactory, scoped);
            dbContextPool.SavePoolNow();
        }

        /// <summary>
        /// 创建一个作用域范围
        /// </summary>
        /// <param name="handle"></param>
        /// <returns></returns>
        public static object Create(Func<IServiceScopeFactory, IServiceScope, object> handle)
        {
            if (handle == null) throw new ArgumentNullException(nameof(handle));

            // 解析服务作用域工厂
            var scopeFactory = InternalApp.InternalServices.BuildServiceProvider().GetService<IServiceScopeFactory>();
            using var scoped = scopeFactory.CreateScope();

            // 执行方法
            return handle.Invoke(scopeFactory, scoped);
        }

        /// <summary>
        /// 创建一个工作单元作用域
        /// </summary>
        /// <param name="handle"></param>
        /// <returns></returns>
        public static object CreateUnitOfWork(Func<IServiceScopeFactory, IServiceScope, object> handle)
        {
            if (handle == null) throw new ArgumentNullException(nameof(handle));

            // 解析服务作用域工厂
            var scopeFactory = InternalApp.InternalServices.BuildServiceProvider().GetService<IServiceScopeFactory>();
            using var scoped = scopeFactory.CreateScope();

            // 创建一个数据库上下文池
            var dbContextPool = scoped.ServiceProvider.GetService<IDbContextPool>();
            var result = handle.Invoke(scopeFactory, scoped);
            dbContextPool.SavePoolNow();

            return result;
        }
    }
}