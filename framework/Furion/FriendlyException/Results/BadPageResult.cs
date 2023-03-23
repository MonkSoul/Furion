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

using Furion.Reflection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace Furion.FriendlyException;

/// <summary>
/// 错误页面
/// </summary>
public class BadPageResult : StatusCodeResult
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public BadPageResult()
        : base(400)
    {
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="statusCode">状态码</param>
    public BadPageResult(int statusCode)
        : base(statusCode)
    {
    }

    /// <summary>
    /// 标题
    /// </summary>
    public string Title { get; set; } = "ModelState Invalid";

    /// <summary>
    /// 描述
    /// </summary>
    public string Description { get; set; } = "User data verification failed. Please input it correctly.";

    /// <summary>
    /// 图标
    /// </summary>
    /// <remarks>必须是 base64 类型</remarks>
    public string Base64Icon { get; set; } = "data:image/svg+xml;base64,PHN2ZyB3aWR0aD0iMTYiIGhlaWdodD0iMTYiIGZpbGw9Im5vbmUiIHhtbG5zPSJodHRwOi8vd3d3LnczLm9yZy8yMDAwL3N2ZyI+PHBhdGggZD0iTTE0LjIxIDEzLjVsMS43NjcgMS43NzMtLjcwNC43MDRMMTMuNSAxNC4yMWwtMS43NzMgMS43NzMtLjcwNC0uNzEgMS43NzQtMS43NzQtMS43NzQtMS43NzMuNzA0LS43MDQgMS43NzMgMS43NzQgMS43NzMtMS43NzQuNzA0LjcxMUwxNC4yMSAxMy41ek0yIDE1aDh2MUgxVjBoOC43MUwxNCA0LjI5VjEwaC0xVjVIOVYxSDJ2MTR6bTgtMTFoMi4yOUwxMCAxLjcxVjR6IiBmaWxsPSIjMTAxMDEwIi8+PC9zdmc+";

    /// <summary>
    /// 错误代码
    /// </summary>
    public string Code { get; set; } = "";

    /// <summary>
    /// 错误代码语言
    /// </summary>
    public string CodeLang { get; set; } = "json";

    /// <summary>
    /// 返回通用 401 错误页
    /// </summary>
    public static BadPageResult Status401Unauthorized => new(StatusCodes.Status401Unauthorized)
    {
        Title = "401 Unauthorized",
        Code = "401 Unauthorized",
        Description = "",
        CodeLang = "txt"
    };

    /// <summary>
    /// 返回通用 403 错误页
    /// </summary>
    public static BadPageResult Status403Forbidden => new(StatusCodes.Status403Forbidden)
    {
        Title = "403 Forbidden",
        Code = "403 Forbidden",
        Description = "",
        CodeLang = "txt"
    };

    /// <summary>
    /// 返回通用 404 错误页
    /// </summary>
    public static BadPageResult Status404NotFound => new(StatusCodes.Status404NotFound)
    {
        Title = "404 Not Found",
        Code = "404 Not Found",
        Description = "",
        CodeLang = "txt"
    };

    /// <summary>
    /// 返回通用 500 错误页
    /// </summary>
    public static BadPageResult Status500InternalServerError => new(StatusCodes.Status500InternalServerError)
    {
        Title = "500 Internal Server Error",
        Code = "500 Internal Server Error",
        Description = "",
        CodeLang = "txt"
    };

    /// <summary>
    /// 重写返回结果
    /// </summary>
    /// <param name="context"></param>
    public override void ExecuteResult(ActionContext context)
    {
        base.ExecuteResult(context);

        var httpContext = context.HttpContext;

        // 如果 Response 已经完成输出或 WebSocket 请求，则禁止写入
        if (httpContext.IsWebSocketRequest() || httpContext.Response.HasStarted) return;
        httpContext.Response.Body.WriteAsync(ToByteArray());
    }

    /// <summary>
    /// 将 <see cref="BadPageResult"/> 转换成字符串
    /// </summary>
    /// <returns><see cref="string"/></returns>
    public override string ToString()
    {
        // 获取当前类型信息
        var thisType = typeof(BadPageResult);
        var thisAssembly = thisType.Assembly;

        // 读取嵌入式页面路径
        var errorhtml = $"{Reflect.GetAssemblyName(thisAssembly)}{thisType.Namespace.Replace(nameof(Furion), string.Empty)}.Assets.error.html";

        // 解析嵌入式文件流
        byte[] buffer;
        using (var readStream = thisAssembly.GetManifestResourceStream(errorhtml))
        {
            buffer = new byte[readStream.Length];
            readStream.Read(buffer, 0, buffer.Length);
        }

        // 读取内容并替换
        var content = Encoding.UTF8.GetString(buffer);
        content = content.Replace($"@{{{nameof(Title)}}}", Title)
                         .Replace($"@{{{nameof(Description)}}}", Description)
                         .Replace($"@{{{nameof(StatusCode)}}}", StatusCode.ToString())
                         .Replace($"@{{{nameof(Code)}}}", Code)
                         .Replace($"@{{{nameof(CodeLang)}}}", CodeLang)
                         .Replace($"@{{{nameof(Base64Icon)}}}", Base64Icon);

        return content;
    }

    /// <summary>
    /// 将 <see cref="BadPageResult"/> 转换成字节数组
    /// </summary>
    /// <returns><see cref="byte"/></returns>
    public byte[] ToByteArray()
    {
        return Encoding.UTF8.GetBytes(ToString());
    }
}