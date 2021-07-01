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

using Furion.DependencyInjection;

namespace Furion.DatabaseAccessor
{
    /// <summary>
    /// Sql 对象类型执行代理
    /// </summary>
    [SuppressSniffer]
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