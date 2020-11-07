using Fur.DependencyInjection;
using Fur.DynamicApiController;
using System;

namespace Microsoft.AspNetCore.Mvc
{
    /// <summary>
    /// 接口描述设置
    /// </summary>
    [SkipScan, AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class | AttributeTargets.Method)]
    public sealed class ApiDescriptionSettingsAttribute : ApiExplorerSettingsAttribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ApiDescriptionSettingsAttribute() : base()
        {
            SplitCamelCase = true;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="enabled">是否启用</param>
        public ApiDescriptionSettingsAttribute(bool enabled) : base()
        {
            base.IgnoreApi = !enabled;
            Enabled = enabled;
            SplitCamelCase = true;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="groups">分组列表</param>
        public ApiDescriptionSettingsAttribute(params string[] groups) : base()
        {
            base.GroupName = string.Join(Penetrates.GroupSeparator, groups);
            Groups = groups;
            SplitCamelCase = true;
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
        /// 切割骆驼命名
        /// </summary>
        public bool SplitCamelCase { get; set; }

        /// <summary>
        /// 保留路由谓词
        /// </summary>
        public bool KeepVerb { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// 模块名
        /// </summary>
        public string Module { get; set; }

        /// <summary>
        /// 版本号
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// 分组
        /// </summary>
        public string[] Groups { get; set; }

        /// <summary>
        /// 标签
        /// </summary>
        public string Tag { get; set; }
    }
}