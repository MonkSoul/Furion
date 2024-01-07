// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Reflection;

namespace Furion.DatabaseAccessor;

/// <summary>
/// Sql 代理方法元数据
/// </summary>
[SuppressSniffer]
public sealed class SqlProxyMethod
{
    /// <summary>
    /// 参数模型
    /// </summary>
    public object ParameterModel { get; set; }

    /// <summary>
    /// 方法返回值
    /// </summary>
    public Type ReturnType { get; internal set; }

    /// <summary>
    /// 数据库操作上下文
    /// </summary>
    public DbContext Context { get; set; }

    /// <summary>
    /// 是否是异步方法
    /// </summary>
    public bool IsAsync { get; internal set; }

    /// <summary>
    /// 命令类型
    /// </summary>
    public CommandType CommandType { get; set; }

    /// <summary>
    /// 最终 Sql 语句
    /// </summary>
    public string FinalSql { get; set; }

    /// <summary>
    /// 当前执行的方法
    /// </summary>
    public MethodInfo Method { get; internal set; }

    /// <summary>
    /// 传递参数
    /// </summary>
    public object[] Arguments { get; internal set; }

    /// <summary>
    /// 拦截Id
    /// </summary>
    public string InterceptorId { get; internal set; }

    /// <summary>
    /// 返回受影响行数
    /// </summary>
    /// <remarks>只有非查询类操作有效</remarks>
    public bool RowEffects { get; internal set; }
}