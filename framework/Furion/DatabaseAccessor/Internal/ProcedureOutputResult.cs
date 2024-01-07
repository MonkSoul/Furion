// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using System.Data;

namespace Furion.DatabaseAccessor;

/// <summary>
/// 存储过程输出返回值
/// </summary>
[SuppressSniffer]
public sealed class ProcedureOutputResult : ProcedureOutputResult<DataSet>
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public ProcedureOutputResult() : base()
    {
    }
}

/// <summary>
/// 存储过程输出返回值
/// </summary>
/// <typeparam name="TResult">泛型版本</typeparam>
[SuppressSniffer]
public class ProcedureOutputResult<TResult>
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public ProcedureOutputResult()
    {
        OutputValues = new List<ProcedureOutputValue>();
    }

    /// <summary>
    /// 输出值
    /// </summary>
    public IEnumerable<ProcedureOutputValue> OutputValues { get; set; }

    /// <summary>
    /// 返回值
    /// </summary>
    public object ReturnValue { get; set; }

    /// <summary>
    /// 结果集
    /// </summary>
    public TResult Result { get; set; }
}