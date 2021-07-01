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

using System.Data;

namespace Furion.DatabaseAccessor.Models
{
    /// <summary>
    /// Sql 模板参数
    /// </summary>
    internal class SqlTemplateParameter
    {
        /// <summary>
        /// 参数名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 参数值
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// 参数输出方向
        /// </summary>
        public ParameterDirection? Direction { get; set; } = ParameterDirection.Input;

        /// <summary>
        /// 数据库对应类型
        /// </summary>
        public DbType? DbType { get; set; }

        /// <summary>
        /// 大小
        /// </summary>
        /// <remarks>Nvarchar/varchar类型需指定</remarks>
        public int? Size { get; set; }
    }
}