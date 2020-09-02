// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0
// 开源协议：MIT
// 项目地址：https://gitee.com/monksoul/Fur

namespace Fur.DynamicApiController
{
    /// <summary>
    /// 接口参数位置
    /// </summary>
    public enum ApiSeats
    {
        /// <summary>
        /// 控制器之前
        /// </summary>
        ControllerStart,

        /// <summary>
        /// 控制器之后
        /// </summary>
        ControllerEnd,

        /// <summary>
        /// 行为之前
        /// </summary>
        ActionStart,

        /// <summary>
        /// 行为之后
        /// </summary>
        ActionEnd
    }
}