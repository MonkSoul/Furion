// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using System.ComponentModel;

namespace Furion.DatabaseAccessor;

/// <summary>
/// 数据库上下文模式
/// </summary>
[SuppressSniffer]
public enum DbContextMode
{
    /// <summary>
    /// 缓存模型数据库上下文
    /// <para>
    /// OnModelCreating 只会初始化一次
    /// </para>
    /// </summary>
    [Description("缓存模型数据库上下文")]
    Cached,

    /// <summary>
    /// 动态模型数据库上下文
    /// <para>
    /// OnModelCreating 每次都会调用
    /// </para>
    /// </summary>
    [Description("动态模型数据库上下文")]
    Dynamic
}