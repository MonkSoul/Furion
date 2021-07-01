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

using System;

namespace Furion.DependencyInjection.Extensions
{
    /// <summary>
    /// 依赖注入拓展
    /// </summary>
    public static class DependencyInjectionExtensions
    {
        /// <summary>
        /// 解析服务
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <param name="obj"></param>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public static TService GetService<TService>(this object obj, IServiceProvider serviceProvider = default)
            where TService : class
        {
            _ = obj;

            return App.GetService<TService>(serviceProvider);
        }

        /// <summary>
        /// 解析服务
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <param name="obj"></param>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public static TService GetRequiredService<TService>(this object obj, IServiceProvider serviceProvider = default)
            where TService : class
        {
            _ = obj;

            return App.GetRequiredService<TService>(serviceProvider);
        }
    }
}