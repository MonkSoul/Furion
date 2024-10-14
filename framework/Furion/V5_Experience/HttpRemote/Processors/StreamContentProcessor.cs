// 版权归百小僧及百签科技（广东）有限公司所有。
// 
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using System.Net.Http.Headers;
using System.Text;

namespace Furion.HttpRemote;

/// <summary>
///     流内容处理器
/// </summary>
public class StreamContentProcessor : IHttpContentProcessor
{
    /// <inheritdoc />
    public virtual bool CanProcess(object? rawContent, string contentType) =>
        rawContent is StreamContent or Stream;

    /// <inheritdoc />
    public virtual HttpContent? Process(object? rawContent, string contentType, Encoding? encoding)
    {
        // 跳过空值和 HttpContent 类型
        switch (rawContent)
        {
            case null:
                return null;
            case HttpContent httpContent:
                // 设置 Content-Type
                httpContent.Headers.ContentType ??=
                    new MediaTypeHeaderValue(contentType) { CharSet = encoding?.BodyName ?? Constants.UTF8_ENCODING };

                return httpContent;
        }

        // 检查是否是流类型
        if (rawContent is Stream stream)
        {
            // 初始化 StreamContent 实例
            var streamContent = new StreamContent(stream);
            streamContent.Headers.ContentType =
                new MediaTypeHeaderValue(contentType) { CharSet = encoding?.BodyName ?? Constants.UTF8_ENCODING };

            return streamContent;
        }

        throw new InvalidOperationException(
            $"Expected a stream, but received an object of type `{rawContent.GetType()}`.");
    }
}