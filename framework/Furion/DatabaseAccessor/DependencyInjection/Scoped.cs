// -----------------------------------------------------------------------------
// 让 .NET 开发更简单，更通用，更流行。
// Copyright © 2020-2021 Furion, 百小僧, Baiqian Co.,Ltd.
//
// 框架名称：Furion
// 框架作者：百小僧
// 框架版本：2.13.3
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
            CreateUow(async (fac, scope) =>
            {
                handler(fac, scope);
                await Task.CompletedTask;
            }, scopeFactory).GetAwaiter().GetResult();
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
            var (scoped, serviceProvider) = CreateScope(ref scopeFactory);

            try
            {
                // 创建一个数据库上下文池
                var dbContextPool = scoped.ServiceProvider.GetService<IDbContextPool>();

                // 执行方法
                await handler(scopeFactory, scoped);

                // 提交工作单元
                dbContextPool.SavePoolNow();
            }
            finally
            {
                // 释放
                scoped.Dispose();
                if (serviceProvider != null) await serviceProvider.DisposeAsync();
            }
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
            return CreateUowRef(async (fac, scope) =>
            {
                var result = handler(fac, scope);
                await Task.CompletedTask;
                return result;
            }, scopeFactory).GetAwaiter().GetResult();
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
            var (scoped, serviceProvider) = CreateScope(ref scopeFactory);

            T result = default;

            try
            {
                // 创建一个数据库上下文池
                var dbContextPool = scoped.ServiceProvider.GetService<IDbContextPool>();

                // 执行方法
                result = await handler(scopeFactory, scoped);

                // 提交工作单元
                dbContextPool.SavePoolNow();
            }
            finally
            {
                // 释放
                scoped.Dispose();
                if (serviceProvider != null) await serviceProvider.DisposeAsync();
            }

            return result;
        }
    }
}