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
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Threading;

namespace Fur.DatabaseAccessor
{
    /// <summary>
    /// 动态模型缓存工厂
    /// </summary>
    /// <remarks>主要用来实现数据库分表分库</remarks>
    [SkipScan]
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