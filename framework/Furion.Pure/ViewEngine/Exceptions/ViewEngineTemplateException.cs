// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using Microsoft.CodeAnalysis;
using System.Runtime.Serialization;

namespace Furion.ViewEngine;

/// <summary>
/// 视图引擎模板编译异常类
/// </summary>
[SuppressSniffer]
public class ViewEngineTemplateException : ViewEngineException
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public ViewEngineTemplateException()
    {
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected ViewEngineTemplateException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="innerException"></param>
    public ViewEngineTemplateException(Exception innerException) : base(null, innerException)
    {
    }

    /// <summary>
    /// 错误信息
    /// </summary>
    public List<Diagnostic> Errors { get; set; }

    /// <summary>
    /// 生成的代码
    /// </summary>
    public string GeneratedCode { get; set; }

    /// <summary>
    /// 重写异常消息
    /// </summary>
    public override string Message => $"Unable to compile template: {string.Join("\n", Errors.Where(w => w.IsWarningAsError || w.Severity == DiagnosticSeverity.Error))}";
}