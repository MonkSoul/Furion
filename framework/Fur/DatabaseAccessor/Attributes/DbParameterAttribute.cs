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
using System.Data;

namespace Fur.DatabaseAccessor
{
    /// <summary>
    /// DbParameter 配置特性
    /// </summary>
    [SkipScan, AttributeUsage(AttributeTargets.Property)]
    public sealed class DbParameterAttribute : Attribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public DbParameterAttribute()
        {
            Direction = ParameterDirection.Input;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="direction">参数方向</param>
        public DbParameterAttribute(ParameterDirection direction)
        {
            Direction = direction;
        }

        /// <summary>
        /// 参数输出方向
        /// </summary>
        public ParameterDirection Direction { get; set; }

        /// <summary>
        /// 数据库对应类型
        /// </summary>
        public object DbType { get; set; }

        /// <summary>
        /// 大小
        /// </summary>
        /// <remarks>Nvarchar/varchar类型需指定</remarks>
        public int Size { get; set; }
    }
}