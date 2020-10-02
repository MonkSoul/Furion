// -----------------------------------------------------------------------------
// Fur 是 .NET 5 平台下极易入门、极速开发的 Web 应用框架。
// Copyright © 2020 Fur, Baiqian Co.,Ltd.
//
// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0
// 官方网站：https://chinadot.net
// 源码地址：Gitee：https://gitee.com/monksoul/Fur
// 				    Github：https://github.com/monksoul/Fur
// 开源协议：Apache-2.0（http://www.apache.org/licenses/LICENSE-2.0）
// -----------------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fur.DatabaseAccessor
{
    /// <summary>
    /// 租户实体
    /// </summary>
    public class Tenant : IEntityDependency
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public Tenant()
        {
            CreatedTime = DateTime.Now;
        }

        /// <summary>
        /// 租户Id
        /// </summary>
        [Key]
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid TenantId { get; set; }

        /// <summary>
        /// 租户名
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// 来源主机地址
        /// </summary>
        [Required]
        public string Host { get; set; }

        /// <summary>
        /// 架构名
        /// </summary>
        public string Schema { get; set; }

        /// <summary>
        /// 数据库链接字符串
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }
    }
}