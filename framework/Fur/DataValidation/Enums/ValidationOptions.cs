// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0
// 开源协议：MIT
// 项目地址：https://gitee.com/monksoul/Fur

namespace Fur.DataValidation
{
    /// <summary>
    /// 验证逻辑
    /// </summary>
    public enum ValidationOptions
    {
        /// <summary>
        /// 全部都要验证通过
        /// </summary>
        AllOfThem,

        /// <summary>
        /// 至少一个验证通过
        /// </summary>
        AtLeastOne
    }
}