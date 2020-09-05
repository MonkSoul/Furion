// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0
// 开源协议：Apache-2.0
// 项目地址：https://gitee.com/monksoul/Fur

namespace Fur.SpecificationDocument
{
    /// <summary>
    /// 分组排序
    /// </summary>
    internal sealed class GroupOrder
    {
        /// <summary>
        /// 分组名
        /// </summary>
        public string Group { get; set; }

        /// <summary>
        /// 分组排序
        /// </summary>
        public int Order { get; set; }
    }
}