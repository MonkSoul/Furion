// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0
// 开源协议：MIT
// 项目地址：https://gitee.com/monksoul/Fur

using Fur.FriendlyException;

namespace Fur.DatabaseAccessor
{
    /// <summary>
    /// EF Core 内置异常
    /// </summary>
    [ErrorCodeType]
    public enum EFCoreErrorCodes
    {
        /// <summary>
        /// 未找到数据
        /// </summary>
        [ErrorCodeItemMetadata("Sequence contains no elements", ErrorCode = "EFCore.DataNotFound")]
        DataNotFound,

        /// <summary>
        /// 键没有设置
        /// </summary>
        [ErrorCodeItemMetadata("The primary key value is not set", ErrorCode = "EFCore.KeyNotSet")]
        KeyNotSet
    }
}