using Furion.DependencyInjection;

namespace Furion.DistributedIDGenerator
{
    /// <summary>
    /// 连续 GUID 配置
    /// </summary>
    [SkipScan]
    public sealed class SequentialGuidSettings
    {
        /// <summary>
        /// 连续 GUID 类型
        /// </summary>
        public SequentialGuidType GuidType { get; set; }
    }
}