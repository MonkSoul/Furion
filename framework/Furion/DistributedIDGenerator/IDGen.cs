// -----------------------------------------------------------------------------
// 让 .NET 开发更简单，更通用，更流行。
// Copyright © 2020-2021 Furion, 百小僧, Baiqian Co.,Ltd.
//
// 框架名称：Furion
// 框架作者：百小僧
// 框架版本：2.7.9
// 源码地址：Gitee： https://gitee.com/dotnetchina/Furion
//          Github：https://github.com/monksoul/Furion
// 开源协议：Apache-2.0（https://gitee.com/dotnetchina/Furion/blob/master/LICENSE）
// -----------------------------------------------------------------------------

using Furion.DependencyInjection;
using System;

namespace Furion.DistributedIDGenerator
{
    /// <summary>
    /// ID 生成器
    /// </summary>
    [SkipScan]
    public static class IDGen
    {
        /// <summary>
        /// 生成唯一 ID
        /// </summary>
        /// <param name="idGeneratorOptions"></param>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public static object NextID(object idGeneratorOptions, IServiceProvider serviceProvider = default)
        {
            return App.GetService<IDistributedIDGenerator>(serviceProvider ?? App.RootServices).Create(idGeneratorOptions);
        }

        /// <summary>
        /// 生成连续 GUID
        /// </summary>
        /// <param name="guidType"></param>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public static Guid NextID(SequentialGuidType guidType = SequentialGuidType.SequentialAsString, IServiceProvider serviceProvider = default)
        {
            var sequentialGuid = (App.GetService(typeof(SequentialGuidIDGenerator), serviceProvider ?? App.RootServices) as IDistributedIDGenerator);
            return (Guid)sequentialGuid.Create(new SequentialGuidSettings { GuidType = guidType });
        }
    }
}