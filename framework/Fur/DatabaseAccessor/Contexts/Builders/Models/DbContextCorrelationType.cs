// -----------------------------------------------------------------------------
// Fur 是 .NET 5 平台下企业应用开发最佳实践框架。
// Copyright © 2020 Fur, Baiqian Co.,Ltd.
//
// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0-rc.final.17
// 官方网站：https://chinadot.net
// 源码地址：Gitee：https://gitee.com/monksoul/Fur
// 				    Github：https://github.com/monksoul/Fur
// 开源协议：Apache-2.0（http://www.apache.org/licenses/LICENSE-2.0）
// -----------------------------------------------------------------------------

using Fur.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Fur.DatabaseAccessor
{
    /// <summary>
    /// 数据库上下文关联类型
    /// </summary>
    [SkipScan]
    internal sealed class DbContextCorrelationType
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        internal DbContextCorrelationType()
        {
            EntityTypes = new List<Type>();
            EntityNoKeyTypes = new List<Type>();
            EntityTypeBuilderTypes = new List<Type>();
            EntitySeedDataTypes = new List<Type>();
            ModelBuilderFilterTypes = new List<Type>();
            ModelBuilderFilterInstances = new List<IPrivateModelBuilderFilter>();
            DbFunctionMethods = new List<MethodInfo>();
        }

        /// <summary>
        /// 关联的数据库上下文
        /// </summary>
        internal Type DbContextLocator { get; set; }

        /// <summary>
        /// 所有关联类型
        /// </summary>
        internal List<Type> Types { get; set; }

        /// <summary>
        /// 实体类型集合
        /// </summary>
        internal List<Type> EntityTypes { get; set; }

        /// <summary>
        /// 无键实体类型集合
        /// </summary>
        internal List<Type> EntityNoKeyTypes { get; set; }

        /// <summary>
        /// 实体构建器类型集合
        /// </summary>
        internal List<Type> EntityTypeBuilderTypes { get; set; }

        /// <summary>
        /// 种子数据类型集合
        /// </summary>
        internal List<Type> EntitySeedDataTypes { get; set; }

        /// <summary>
        /// 模型构建筛选器类型集合
        /// </summary>
        internal List<Type> ModelBuilderFilterTypes { get; set; }

        /// <summary>
        /// 数据库函数方法集合
        /// </summary>
        internal List<MethodInfo> DbFunctionMethods { get; set; }

        /// <summary>
        /// 模型构建器筛选器实例
        /// </summary>
        internal List<IPrivateModelBuilderFilter> ModelBuilderFilterInstances { get; set; }
    }
}