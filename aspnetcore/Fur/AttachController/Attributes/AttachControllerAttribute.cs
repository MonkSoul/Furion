using Microsoft.AspNetCore.Mvc;
using System;

namespace Fur.AttachController.Attributes
{
    /// <summary>
    /// 附加控制器特性类
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class)]
    public class AttachControllerAttribute : ApiExplorerSettingsAttribute
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public AttachControllerAttribute()
        {
            base.IgnoreApi = this.IgnoreApi = false;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="attach">是否附加</param>
        public AttachControllerAttribute(bool attach)
        {
            Attach = attach;
            base.IgnoreApi = this.IgnoreApi = !attach;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="swaggerGroups">swagger分组名称列表</param>
        public AttachControllerAttribute(params string[] swaggerGroups)
        {
            SwaggerGroups = swaggerGroups;
            base.GroupName = this.GroupName = string.Join(Consts.GroupNameSeparator, swaggerGroups);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="attach">是否附加</param>
        /// <param name="groups">swagger分组名称列表</param>
        public AttachControllerAttribute(bool attach, params string[] swaggerGroups)
        {
            Attach = attach;
            SwaggerGroups = swaggerGroups;
            base.IgnoreApi = this.IgnoreApi = !attach;
            base.GroupName = this.GroupName = string.Join(Consts.GroupNameSeparator, swaggerGroups);
        }

        /// <summary>
        /// 接口版本
        /// </summary>
        public string ApiVersion { get; set; }

        /// <summary>
        /// 是否附加到控制器，默认true（附加）
        /// </summary>
        public bool Attach { get; set; } = true;

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