// -----------------------------------------------------------------------------
// 让 .NET 开发更简单，更通用，更流行。
// Copyright © 2020-2021 Furion, 百小僧, Baiqian Co.,Ltd.
//
// 框架名称：Furion
// 框架作者：百小僧
// 框架版本：2.11.1
// 源码地址：Gitee： https://gitee.com/dotnetchina/Furion
//          Github：https://github.com/monksoul/Furion
// 开源协议：Apache-2.0（https://gitee.com/dotnetchina/Furion/blob/master/LICENSE）
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Furion.DependencyInjection
{
    /// <summary>
    /// 创建作用域静态类
    /// </summary>
    public static partial class Scoped
    {
        /// <summary>
        /// 创建一个工作单元作用域
        /// </summary>
        /// <param name="handler"></param>
        /// <param name="scopeFactory"></param>
        public static void CreateUow(Action<IServiceScopeFactory, IServiceScope> handler, IServiceScopeFactory scopeFactory = default)
        {
            // 禁止空调用
            if (handler == null) throw new ArgumentNullException(nameof(handler));

            // 创建作用域
            using var scoped = CreateScope(ref scopeFactory);

            // 创建一个数据库上下文池
            var dbContextPool = scoped.ServiceProvider.GetService<IDbContextPool>();
            handler(scopeFactory, scoped);
            dbContextPool.SavePoolNow();
        }

        /// <summary>
        /// 创建一个工作单元作用域
        /// </summary>
        /// <param name="handler"></param>
        /// <param name="scopeFactory"></param>
        public static async Task CreateUow(Func<IServiceScopeFactory, IServiceScope, Task> handler, IServiceScopeFactory scopeFactory = default)
        {
            // 禁止空调用
            if (handler == null) throw new ArgumentNullException(nameof(handler));

            // 创建作用域
            using var scoped = CreateScope(ref scopeFactory);

            // 创建一个数据库上下文池
            var dbContextPool = scoped.ServiceProvider.GetService<IDbContextPool>();
            await handler(scopeFactory, scoped);
            dbContextPool.SavePoolNow();
        }

        /// <summary>
        /// 创建一个工作单元作用域
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="handler"></param>
        /// <param name="scopeFactory"></param>
        /// <returns></returns>
        public static T CreateUowRef<T>(Func<IServiceScopeFactory, IServiceScope, T> handler, IServiceScopeFactory scopeFactory = default)
        {
            // 禁止空调用
            if (handler == null) throw new ArgumentNullException(nameof(handler));

            // 创建作用域
            using var scoped = CreateScope(ref scopeFactory);

            // 创建一个数据库上下文池
            var dbContextPool = scoped.ServiceProvider.GetService<IDbContextPool>();
            var result = handler(scopeFactory, scoped);
            dbContextPool.SavePoolNow();

            return result;
        }

        /// <summary>
        /// 创建一个工作单元作用域
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="handler"></param>
        /// <param name="scopeFactory"></param>
        /// <returns></returns>
        public static async Task<T> CreateUowRef<T>(Func<IServiceScopeFactory, IServiceScope, Task<T>> handler, IServiceScopeFactory scopeFactory = default)
        {
            // 禁止空调用
            if (handler == null) throw new ArgumentNullException(nameof(handler));

            // 创建作用域
            using var scoped = CreateScope(ref scopeFactory);

            // 创建一个数据库上下文池
            var dbContextPool = scoped.ServiceProvider.GetService<IDbContextPool>();
            var result = await handler(scopeFactory, scoped);
            dbContextPool.SavePoolNow();

            return result;
        }
    }
}