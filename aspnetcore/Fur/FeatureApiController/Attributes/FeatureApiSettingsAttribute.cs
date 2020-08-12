using Microsoft.AspNetCore.Mvc;
using System;

namespace Fur.FeatureApiController
{
    /// <summary>
    /// 特性接口控制器配置
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class | AttributeTargets.Method)]
    public sealed class FeatureApiSettingsAttribute : ApiExplorerSettingsAttribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="enabled">是否启用</param>
        public FeatureApiSettingsAttribute(bool enabled)
        {
            base.IgnoreApi = !enabled;
            Enabled = enabled;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="groups">分组列表</param>
        public FeatureApiSettingsAttribute(params string[] groups)
        {
            base.GroupName = string.Join(Penetrates.GroupSeparator, groups);
            Groups = groups;
        }

        /// <summary>
        /// 自定义名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 保留原有名称
        /// </summary>
        public bool KeepName { get; set; }

        /// <summary>
        /// 切割名称（只对行为有效）
        /// </summary>
        /// <remarks>
        /// <para>切割后将生成路由路径</para>
        /// </remarks>
        /// <example>
        /// <para>FurConfigure -> fur/configure</para>
        /// </example>
        public bool SplitName { get; set; }

        /// <summary>
        /// 保留路由谓词
        /// </summary>
        public bool KeepVerb { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// 分组
        /// </summary>
        public string[] Groups { get; set; }

        /// <summary>
        /// 标签
        /// </summary>
        public string Tags { get; set; }

        /// <summary>
        /// 模块名
        /// </summary>
        public string Module { get; set; }

        /// <summary>
        /// 接口版本号
        /// </summary>
        public string ApiVersion { get; set; }
    }
}