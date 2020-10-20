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
using System.Collections.Generic;

namespace Fur.DatabaseAccessor
{
    /// <summary>
    /// 分页泛型集合
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    [SkipScan]
    public class PagedList<TEntity>
        where TEntity : class, new()
    {
        /// <summary>
        /// 页码
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 页容量
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 总条数
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPages { get; set; }

        /// <summary>
        /// 当前页集合
        /// </summary>
        public IEnumerable<TEntity> Items { get; set; }

        /// <summary>
        /// 是否有上一页
        /// </summary>
        public bool HasPrevPages { get; set; }

        /// <summary>
        /// 是否有下一页
        /// </summary>
        public bool HasNextPages { get; set; }
    }

    /// <summary>
    /// 分页集合
    /// </summary>
    public class PagedList : PagedList<object>
    {
    }
}