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

namespace Furion.DistributedIDGenerator
{
    /// <summary>
    /// 随机数帮助类
    /// </summary>
    internal static class RandomHelpers
    {
        /// <summary>
        /// 随机数对象
        /// </summary>
        private static readonly Random Random = new();

        /// <summary>
        /// 线程锁
        /// </summary>
        private static readonly object ThreadLock = new();

        /// <summary>
        /// 生成线程安全的范围内随机数
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static int GenerateNumberInRange(int min, int max)
        {
            lock (ThreadLock)
            {
                return Random.Next(min, max);
            }
        }
    }
}