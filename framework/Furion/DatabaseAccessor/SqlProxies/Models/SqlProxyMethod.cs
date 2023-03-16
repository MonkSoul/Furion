// MIT 许可证
//
// 版权 © 2020-present 百小僧, 百签科技（广东）有限公司 和所有贡献者
//
// 特此免费授予任何获得本软件副本和相关文档文件（下称“软件”）的人不受限制地处置该软件的权利，
// 包括不受限制地使用、复制、修改、合并、发布、分发、转授许可和/或出售该软件副本，
// 以及再授权被配发了本软件的人如上的权利，须在下列条件下：
//
// 上述版权声明和本许可声明应包含在该软件的所有副本或实质成分中。
//
// 本软件是“如此”提供的，没有任何形式的明示或暗示的保证，包括但不限于对适销性、特定用途的适用性和不侵权的保证。
// 在任何情况下，作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

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