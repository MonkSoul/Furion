using System.ComponentModel.DataAnnotations;

namespace Fur.AttachController.Options
{
    /// <summary>
    /// 附加控制器配置选项
    /// </summary>
    public class AttactControllerOptions
    {
        /// <summary>
        /// 默认Api路由统一前缀
        /// </summary>
        public string DefaultStartRoutePrefix { get; set; }

        /// <summary>
        /// 生成路由时，自动去除控制器名称前后缀
        /// </summary>
        [Required]
        public string[] ClearControllerRouteAffix { get; set; }

        /// <summary>
        /// 生成路由时，自动去除Action名称前后缀
        /// </summary>
        [Required]
        public string[] ClearActionRouteAffix { get; set; }

        /// <summary>
        /// 默认接口版本
        /// </summary>
        public string DefaultApiVersion { get; set; }

        /// <summary>
        /// 默认请求方式
        /// </summary>
        [Required]
        public string DefaultHttpMethod { get; set; }

        /// <summary>
        /// 小写Api地址
        /// </summary>
        [Required]
        public bool LowerCaseUri { get; set; }

        /// <summary>
        /// 移除Actoin路由名称请求动词
        /// </summary>
        [Required]
        public bool RemoveActionRouteVerb { get; set; }
    }
}