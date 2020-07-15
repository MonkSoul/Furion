using Microsoft.AspNetCore.Mvc;
using System;

namespace Fur.MirrorController.Attributes
{
    /// <summary>
    /// 镜面控制器特性类
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class)]
    public class MirrorControllerAttribute : ApiExplorerSettingsAttribute
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public MirrorControllerAttribute()
        {
            base.IgnoreApi = this.IgnoreApi = false;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="enabled">是否启用镜像控制器</param>
        public MirrorControllerAttribute(bool enabled)
        {
            Enabled = enabled;
            base.IgnoreApi = this.IgnoreApi = !enabled;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="swaggerGroups">swagger分组名称列表</param>
        public MirrorControllerAttribute(params string[] swaggerGroups)
        {
            SwaggerGroups = swaggerGroups;
            base.GroupName = this.GroupName = string.Join(Consts.GroupNameSeparator, swaggerGroups);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="enabled">是否启用镜像控制器</param>
        /// <param name="groups">swagger分组名称列表</param>
        public MirrorControllerAttribute(bool enabled, params string[] swaggerGroups)
        {
            Enabled = enabled;
            SwaggerGroups = swaggerGroups;
            base.IgnoreApi = this.IgnoreApi = !enabled;
            base.GroupName = this.GroupName = string.Join(Consts.GroupNameSeparator, swaggerGroups);
        }

        /// <summary>
        /// 接口版本
        /// </summary>
        public string ApiVersion { get; set; }

        /// <summary>
        /// 是否启用镜像控制器，默认true
        /// </summary>
        public bool Enabled { get; set; } = true;

        /// <summary>
        /// 附加到swagger分组名称列表
        /// </summary>
        public string[] SwaggerGroups { get; set; }

        /// <summary>
        /// 接口授权标识名称列表
        /// </summary>
        public string[] AuthorizeTo { get; set; }


        /// <summary>
        /// 分组名
        /// </summary>
        private new string GroupName { get; set; }

        /// <summary>
        /// 是否忽略Api
        /// </summary>
        private new bool IgnoreApi { get; set; }
    }
}