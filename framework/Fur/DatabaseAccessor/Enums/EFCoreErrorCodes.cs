// -----------------------------------------------------------------------------
// Fur 是 .NET 5 平台下极易入门、极速开发的 Web 应用框架。
// Copyright © 2020 Fur, Baiqian Co.,Ltd.
//
// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0
// 源码地址：Gitee：https://gitee.com/monksoul/Fur
// 				   Github：https://github.com/monksoul/Fur
// 开源协议：Apache-2.0（http://www.apache.org/licenses/LICENSE-2.0）
// -----------------------------------------------------------------------------

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
        KeyNotSet,

        /// <summary>
        /// 未找到假删除的属性
        /// </summary>
        [ErrorCodeItemMetadata("No attributes marked as fake deleted were found", ErrorCode = "EFCore.FakeDeleteNotFound")]
        FakeDeleteNotFound
    }
}