// -----------------------------------------------------------------------------
// 让 .NET 开发更简单，更通用，更流行。
// Copyright © 2020-2021 Furion, 百小僧, Baiqian Co.,Ltd.
//
// 框架名称：Furion
// 框架作者：百小僧
// 框架版本：2.11.1
// 源码地址：Gitee： https://gitee.com/dotnetchina/Furion
//          Github：https://github.com/monksoul/Furion
// 开源协议：Apache-2.0（https://gitee.com/dotnetchina/Furion/blob/master/LICENSE）
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor.Models;

namespace Furion.DatabaseAccessor
{
    /// <summary>
    /// Sql 模板
    /// </summary>
    internal sealed class SqlTemplate
    {
        /// <summary>
        /// Sql 语句
        /// </summary>
        public string Sql { get; set; }

        /// <summary>
        /// Sql 参数
        /// </summary>
        public SqlTemplateParameter[] Params { get; set; }
    }
}