using Microsoft.AspNetCore.Mvc;
using System;

namespace Fur.AttachController.Attributes
{
    /// <summary>
    /// 附加控制器Action特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class AttachActionAttribute : ApiExplorerSettingsAttribute
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public AttachActionAttribute()
        {
            base.IgnoreApi = this.IgnoreApi = false;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="attach">是否附加</param>
        public AttachActionAttribute(bool attach)
        {
            Attach = attach;
            base.IgnoreApi = this.IgnoreApi = !attach;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="swaggerGroups">swagger分组名称列表</param>
        public AttachActionAttribute(params string[] swaggerGroups)
        {
            SwaggerGroups = swaggerGroups;
            base.GroupName = this.GroupName = string.Join(Consts.GroupNameSeparator, swaggerGroups);
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="attach">是否附加</param>
        /// <param name="swaggerGroups">swagger分组名称列表</param>
        public AttachActionAttribute(bool attach, params string[] swaggerGroups)
        {
            Attach = attach;
            SwaggerGroups = swaggerGroups;
            base.IgnoreApi = this.IgnoreApi = !attach;
            base.GroupName = this.GroupName = string.Join(Consts.GroupNameSeparator, swaggerGroups);
        }
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
        /// 保持原始名称（如果全局启用名词前后缀处理，还是会去掉前后占位符）
        /// </summary>
        public bool KeepOriginalName { get; set; }
        /// <summary>
        /// 每个单词都生成路由路径（如果全局启用名词前后缀处理，还是会去掉前后占位符）
        /// </summary>
        public bool EveryWordToRoutePath { get; set; }
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
