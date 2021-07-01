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

using Furion.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Threading;

namespace Furion.DatabaseAccessor
{
    /// <summary>
    /// 动态模型缓存工厂
    /// </summary>
    /// <remarks>主要用来实现数据库分表分库</remarks>
    [SuppressSniffer]
    public class DynamicModelCacheKeyFactory : IModelCacheKeyFactory
    {
        /// <summary>
        /// 动态模型缓存Key
        /// </summary>
        private static int cacheKey;

        /// <summary>
        /// 重写构建模型
        /// </summary>
        /// <remarks>动态切换表之后需要调用该方法</remarks>
        public static void RebuildModels()
        {
            Interlocked.Increment(ref cacheKey);
        }

        /// <summary>
        /// 更新模型缓存
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public object Create(DbContext context)
        {
            return (context.GetType(), cacheKey);
        }
    }
}