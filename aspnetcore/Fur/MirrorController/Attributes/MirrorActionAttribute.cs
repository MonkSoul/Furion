using System;

namespace Fur.MirrorController.Attributes
{
    /// <summary>
    /// 镜面控制器Action特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class MirrorActionAttribute : MirrorBaseAttribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public MirrorActionAttribute() { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="enabled">是否附加</param>
        public MirrorActionAttribute(bool enabled) : base(enabled) { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="swaggerGroups">swagger分组名称列表</param>
        public MirrorActionAttribute(params string[] swaggerGroups) : base(swaggerGroups) { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="enabled">是否启用镜像Action</param>
        /// <param name="swaggerGroups">swagger分组名称列表</param>
        public MirrorActionAttribute(bool enabled, params string[] swaggerGroups) : base(enabled, swaggerGroups) { }

        /// <summary>
        /// 保持原始名称
        /// </summary>
        public bool KeepOriginalName { get; set; } = false;

        /// <summary>
        /// 保留路由谓词
        /// </summary>
        public bool KeepRouteVerb { get; set; } = false;

        /// <summary>
        /// 每个单词都生成路由路径
        /// </summary>
        public bool SplitWordToRoutePath { get; set; } = false;
    }
}