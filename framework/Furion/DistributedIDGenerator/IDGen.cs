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
            return App.GetService<IDistributedIDGenerator>(serviceProvider).Create(idGeneratorOptions);
        }

        /// <summary>
        /// 生成连续 GUID
        /// </summary>
        /// <param name="guidType"></param>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public static Guid NextID(SequentialGuidType guidType = SequentialGuidType.SequentialAsString, IServiceProvider serviceProvider = default)
        {
            var sequentialGuid = (App.GetService(typeof(SequentialGuidIDGenerator), serviceProvider) as IDistributedIDGenerator);
            return (Guid)sequentialGuid.Create(new SequentialGuidSettings { GuidType = guidType });
        }
    }
}