using Fur.MirrorController.Options;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Fur.MirrorController.Attributes
{
    /// <summary>
    /// 镜面控制器Action特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class MirrorActionAttribute : ApiExplorerSettingsAttribute
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public MirrorActionAttribute()
        {
            base.IgnoreApi = this.IgnoreApi = false;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="enabled">是否附加</param>
        public MirrorActionAttribute(bool enabled)
        {
            Enabled = enabled;
            base.IgnoreApi = this.IgnoreApi = !enabled;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="swaggerGroups">swagger分组名称列表</param>
        public MirrorActionAttribute(params string[] swaggerGroups)
        {
            SwaggerGroups = swaggerGroups;
            base.GroupName = this.GroupName = string.Join(Consts.GroupNameSeparator, swaggerGroups);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="enabled">是否启用镜像Action</param>
        /// <param name="swaggerGroups">swagger分组名称列表</param>
        public MirrorActionAttribute(bool enabled, params string[] swaggerGroups)
        {
            Enabled = enabled;
            SwaggerGroups = swaggerGroups;
            base.IgnoreApi = this.IgnoreApi = !enabled;
            base.GroupName = this.GroupName = string.Join(Consts.GroupNameSeparator, swaggerGroups);
        }

        /// <summary>
        /// 接口版本号
        /// </summary>
        public string ApiVersion { get; set; }

        /// <summary>
        /// 是否启用镜像Action，默认true
        /// </summary>
        public bool Enabled { get; set; } = true;

        /// <summary>
        /// 附加到swagger分组名称列表
        /// </summary>
        public string[] SwaggerGroups { get; set; }

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

        /// <summary>
        /// 参数路由位置
        /// </summary>
        public ParameterRoutePosition[] ParameterRoutePositions { get; set; }

        /// <summary>
        /// 接口授权标识名称列表
        /// </summary>
        public string[] AuthorizeTo { get; set; }

        /// <summary>
        /// 分组名
        /// </summary>
        internal new string GroupName { get; set; }

        /// <summary>
        /// 是否忽略Api
        /// </summary>
        internal new bool IgnoreApi { get; set; }
    }
}