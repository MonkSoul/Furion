using Fur.AppCore.Attributes;
using System;

namespace Fur.MirrorController.Attributes
{
    /// <summary>
    /// 镜面控制器特性类
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class), NonInflated]
    public sealed class MirrorControllerAttribute : MirrorSettingsAttribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public MirrorControllerAttribute() { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="enabled">是否启用镜像控制器</param>
        public MirrorControllerAttribute(bool enabled) : base(enabled) { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="swaggerGroups">swagger分组名称列表</param>
        public MirrorControllerAttribute(params string[] swaggerGroups) : base(swaggerGroups) { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="enabled">是否启用镜像控制器</param>
        /// <param name="groups">swagger分组名称列表</param>
        public MirrorControllerAttribute(bool enabled, params string[] swaggerGroups) : base(enabled, swaggerGroups) { }

        /// <summary>
        /// 接口版本
        /// </summary>
        public string ApiVersion { get; set; }
    }
}