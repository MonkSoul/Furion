using Microsoft.AspNetCore.Mvc;
using System;

namespace Fur.SimulateController
{
    /// <summary>
    /// 模刻配置
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class | AttributeTargets.Method)]
    public sealed class SimulateSettingsAttribute : ApiExplorerSettingsAttribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="enabled">是否启用</param>
        public SimulateSettingsAttribute(bool enabled)
        {
            base.IgnoreApi = !enabled;
            Enabled = enabled;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="groups">分组列表</param>
        public SimulateSettingsAttribute(params string[] groups)
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
        /// 切割名称
        /// </summary>
        /// <remarks>
        /// <para>如：FurAppUser -> fur/app/user</para>
        /// </remarks>
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