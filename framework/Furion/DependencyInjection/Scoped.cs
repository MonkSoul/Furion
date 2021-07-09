// -----------------------------------------------------------------------------
// 让 .NET 开发更简单，更通用，更流行。
// Copyright © 2020-2021 Furion, 百小僧, Baiqian Co.,Ltd.
//
// 框架名称：Furion
// 框架作者：百小僧
// 框架版本：2.12.4
// 源码地址：Gitee： https://gitee.com/dotnetchina/Furion
//          Github：https://github.com/monksoul/Furion
// 开源协议：Apache-2.0（https://gitee.com/dotnetchina/Furion/blob/master/LICENSE）
// -----------------------------------------------------------------------------

using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Furion.DependencyInjection
{
    /// <summary>
    /// 创建作用域静态类
    /// </summary>
    [SuppressSniffer]
    public static partial class Scoped
    {
        /// <summary>
        /// 创建一个作用域范围
        /// </summary>
        /// <param name="handler"></param>
        /// <param name="scopeFactory"></param>
        public static void Create(Action<IServiceScopeFactory, IServiceScope> handler, IServiceScopeFactory scopeFactory = default)
        {
            Create(async (fac, scope) =>
            {
                handler(fac, scope);
                await Task.CompletedTask;
            }, scopeFactory).GetAwaiter().GetResult();
        }

        /// <summary>
        /// 创建一个作用域范围
        /// </summary>
        /// <param name="handler"></param>
        /// <param name="scopeFactory"></param>
        public static async Task Create(Func<IServiceScopeFactory, IServiceScope, Task> handler, IServiceScopeFactory scopeFactory = default)
        {
            // 禁止空调用
            if (handler == null) throw new ArgumentNullException(nameof(handler));

            // 创建作用域
            var (scoped, serviceProvider) = CreateScope(ref scopeFactory);

            try
            {
                // 执行方法
                await handler(scopeFactory, scoped);
            }
            catch
            {
                throw;
            }
            finally
            {
                // 释放
                scoped.Dispose();
                if (serviceProvider != null) await serviceProvider.DisposeAsync();
            }
        }

        /// <summary>
        /// 创建一个作用域范围
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="handler"></param>
        /// <param name="scopeFactory"></param>
        /// <returns></returns>
        public static T CreateRef<T>(Func<IServiceScopeFactory, IServiceScope, T> handler, IServiceScopeFactory scopeFactory = default)
        {
            return CreateRef(async (fac, scope) =>
             {
                 var result = handler(fac, scope);
                 await Task.CompletedTask;
                 return result;
             }, scopeFactory).GetAwaiter().GetResult();
        }

        /// <summary>
        /// 创建一个作用域范围
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="handler"></param>
        /// <param name="scopeFactory"></param>
        /// <returns></returns>
        public static async Task<T> CreateRef<T>(Func<IServiceScopeFactory, IServiceScope, Task<T>> handler, IServiceScopeFactory scopeFactory = default)
        {
            // 禁止空调用
            if (handler == null) throw new ArgumentNullException(nameof(handler));

            // 创建作用域
            var (scoped, serviceProvider) = CreateScope(ref scopeFactory);

            T result = default;

            try
            {
                // 执行方法
                result = await handler(scopeFactory, scoped);
            }
            catch
            {
                throw;
            }
            finally
            {
                // 释放
                scoped.Dispose();
                if (serviceProvider != null) await serviceProvider.DisposeAsync();
            }

            return result;
        }

        /// <summary>
        /// 创建一个作用域
        /// </summary>
        /// <param name="scopeFactory"></param>
        /// <returns></returns>
        private static (IServiceScope Scoped, ServiceProvider ServiceProvider) CreateScope(ref IServiceScopeFactory scopeFactory)
        {
            ServiceProvider undisposeServiceProvider = default;

            if (scopeFactory == null)
            {
                if (App.RootServices != null) scopeFactory = App.RootServices.GetService<IServiceScopeFactory>();
                else
                {
                    // 这里创建了一个待释放服务提供器
                    undisposeServiceProvider = InternalApp.InternalServices.BuildServiceProvider();
                    scopeFactory = undisposeServiceProvider.GetService<IServiceScopeFactory>();
                }
            }

            // 解析服务作用域工厂
            var scoped = scopeFactory.CreateScope();
            return (scoped, undisposeServiceProvider);
        }
    }
}