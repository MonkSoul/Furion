// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0
// 开源协议：MIT
// 项目地址：https://gitee.com/monksoul/Fur

using System.Collections.Generic;

namespace Fur.DynamicApiController
{
    /// <summary>
    /// 参数路由模板
    /// </summary>
    internal class ParameterRouteTemplate
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ParameterRouteTemplate()
        {
            ControllerStartTemplates = new List<string>();
            ControllerEndTemplates = new List<string>();
            ActionStartTemplates = new List<string>();
            ActionEndTemplates = new List<string>();
        }

        /// <summary>
        /// 控制器之前的参数
        /// </summary>
        public IList<string> ControllerStartTemplates { get; set; }

        /// <summary>
        /// 控制器之后的参数
        /// </summary>
        public IList<string> ControllerEndTemplates { get; set; }

        /// <summary>
        /// 行为之前的参数
        /// </summary>
        public IList<string> ActionStartTemplates { get; set; }

        /// <summary>
        /// 行为之后的参数
        /// </summary>
        public IList<string> ActionEndTemplates { get; set; }
    }
}