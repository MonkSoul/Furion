﻿// -----------------------------------------------------------------------------
// 让 .NET 开发更简单，更通用，更流行。
// Copyright © 2020-2021 Furion, 百小僧, Baiqian Co.,Ltd.
//
// 框架名称：Furion
// 框架作者：百小僧
// 框架版本：2.14.1
// 源码地址：Gitee： https://gitee.com/dotnetchina/Furion
//          Github：https://github.com/monksoul/Furion
// 开源协议：Apache-2.0（https://gitee.com/dotnetchina/Furion/blob/master/LICENSE）
// -----------------------------------------------------------------------------

namespace Furion.DataValidation
{
    /// <summary>
    /// 数据验证服务配置选项
    /// </summary>
    public sealed class DataValidationServiceOptions
    {
        /// <summary>
        /// 启用全局数据验证
        /// </summary>
        public bool EnableGlobalDataValidation { get; set; } = true;

        /// <summary>
        /// 禁止C# 8.0 验证非可空引用类型
        /// </summary>
        public bool SuppressImplicitRequiredAttributeForNonNullableReferenceTypes { get; set; } = true;
    }
}