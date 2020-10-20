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

namespace Fur.DatabaseAccessor
{
    /// <summary>
    /// Sql 对象类型执行代理
    /// </summary>
    [SkipScan]
    public class SqlObjectProxyAttribute : SqlProxyAttribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">对象名</param>
        public SqlObjectProxyAttribute(string name)
        {
            Name = name;
        }

        /// <summary>
        /// 对象名
        /// </summary>
        public string Name { get; set; }
    }
}