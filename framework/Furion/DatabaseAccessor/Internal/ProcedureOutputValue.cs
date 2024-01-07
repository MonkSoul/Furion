// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.DatabaseAccessor;

/// <summary>
/// 存储过程输出值模型
/// </summary>
[SuppressSniffer]
public sealed class ProcedureOutputValue
{
    /// <summary>
    /// 输出参数名
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 输出参数值
    /// </summary>
    public object Value { get; set; }
}