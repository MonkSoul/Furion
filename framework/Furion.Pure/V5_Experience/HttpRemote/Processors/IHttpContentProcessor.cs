// 版权归百小僧及百签科技（广东）有限公司所有。
// 
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using System.Text;

namespace Furion.HttpRemote;

/// <summary>
///     <see cref="HttpContent" /> 处理器
/// </summary>
/// <remarks>用于将原始请求内容转换成 <see cref="HttpContent" /> 实例</remarks>
public interface IHttpContentProcessor
{
    /// <summary>
    ///     判断当前处理器是否可以处理指定的内容类型
    /// </summary>
    /// <param name="rawContent">原始请求内容</param>
    /// <param name="contentType">内容类型</param>
    /// <returns>
    ///     <see cref="bool" />
    /// </returns>
    bool CanProcess(object? rawContent, string contentType);

    /// <summary>
    ///     将原始内容转换为 <see cref="HttpContent" /> 实例
    /// </summary>
    /// <param name="rawContent">原始请求内容</param>
    /// <param name="contentType">内容类型</param>
    /// <param name="encoding">内容编码</param>
    /// <returns>
    ///     <see cref="HttpContent" />
    /// </returns>
    HttpContent? Process(object? rawContent, string contentType, Encoding? encoding);
}