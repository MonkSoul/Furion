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

using System.Text;

namespace Microsoft.AspNetCore.Mvc.Formatters;

/// <summary>
/// text/plain 请求参数类型支持
/// </summary>
/// <remarks>让 WebAPI 支持该格式参数</remarks>
public sealed class TextPlainMediaTypeFormatter : TextInputFormatter
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TextPlainMediaTypeFormatter()
    {
        SupportedMediaTypes.Add("text/plain");
        SupportedEncodings.Add(Encoding.UTF8);
    }

    /// <summary>
    /// 重写 <see cref="ReadRequestBodyAsync(InputFormatterContext, Encoding)"/> 方法
    /// </summary>
    /// <param name="context"></param>
    /// <param name="encoding"></param>
    /// <returns></returns>
    public override async Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context, Encoding encoding)
    {
        string content;
        using (var reader = context.ReaderFactory(context.HttpContext.Request.Body, encoding))
        {
            content = await reader.ReadToEndAsync();
        }

        return await InputFormatterResult.SuccessAsync(content);
    }
}