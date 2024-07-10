// ------------------------------------------------------------------------
// 版权信息
// 版权归百小僧及百签科技（广东）有限公司所有。
// 所有权利保留。
// 官方网站：https://baiqian.com
//
// 许可证信息
// Furion 项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。
// 许可证的完整文本可以在源代码树根目录中的 LICENSE-APACHE 和 LICENSE-MIT 文件中找到。
// 官方网站：https://furion.net
//
// 使用条款
// 使用本代码应遵守相关法律法规和许可证的要求。
//
// 免责声明
// 对于因使用本代码而产生的任何直接、间接、偶然、特殊或后果性损害，我们不承担任何责任。
//
// 其他重要信息
// Furion 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。
// 有关 Furion 项目的其他详细信息，请参阅位于源代码树根目录中的 COPYRIGHT 和 DISCLAIMER 文件。
//
// 更多信息
// 请访问 https://gitee.com/dotnetchina/Furion 获取更多关于 Furion 项目的许可证和版权信息。
// ------------------------------------------------------------------------

using Furion.Extensions;
using Furion.Reflection;

namespace Furion.ViewEngine;

/// <summary>
/// 视图引擎模板（编译后）
/// </summary>
[SuppressSniffer]
public class ViewEngineTemplate : IViewEngineTemplate
{
    /// <summary>
    /// 内存流
    /// </summary>
    private readonly MemoryStream assemblyByteCode;

    /// <summary>
    /// 模板类型
    /// </summary>
    private readonly Type templateType;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="assemblyByteCode"></param>
    internal ViewEngineTemplate(MemoryStream assemblyByteCode)
    {
        this.assemblyByteCode = assemblyByteCode;
        templateType = Reflect.GetType(assemblyByteCode, "Furion.ViewEngine.Template");
    }

    /// <summary>
    /// 保存到流中
    /// </summary>
    /// <param name="stream"></param>
    public void SaveToStream(Stream stream)
    {
        SaveToStreamAsync(stream).GetAwaiter().GetResult();
    }

    /// <summary>
    /// 保存到流中
    /// </summary>
    /// <param name="stream"></param>
    /// <returns></returns>
    public Task SaveToStreamAsync(Stream stream)
    {
        return assemblyByteCode.CopyToAsync(stream);
    }

    /// <summary>
    /// 保存到文件
    /// </summary>
    /// <param name="fileName"></param>
    public void SaveToFile(string fileName)
    {
        SaveToFileAsync(fileName).GetAwaiter().GetResult();
    }

    /// <summary>
    /// 保存到文件
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public async Task SaveToFileAsync(string fileName)
    {
        using var fileStream = new FileStream(
            path: Penetrates.GetTemplateFileName(fileName),
            mode: FileMode.OpenOrCreate,
            access: FileAccess.Write,
            share: FileShare.None,
            bufferSize: 8192,
            useAsync: true);

        await assemblyByteCode.CopyToAsync(fileStream);
    }

    /// <summary>
    /// 执行编译
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public string Run(object model = null)
    {
        return RunAsync(model).GetAwaiter().GetResult();
    }

    /// <summary>
    /// 执行编译
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<string> RunAsync(object model = null)
    {
        if (model != null && model.IsAnonymous())
        {
            model = new AnonymousTypeWrapper(model);
        }

        var instance = (IViewEngineModel)Activator.CreateInstance(templateType);
        instance.Model = model;

        await instance.ExecuteAsync();

        return await instance.ResultAsync();
    }

    /// <summary>
    /// 从文件中加载模板
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public static IViewEngineTemplate LoadFromFile(string fileName)
    {
        return LoadFromFileAsync(fileName: fileName).GetAwaiter().GetResult();
    }

    /// <summary>
    /// 从文件中加载模板
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public static async Task<IViewEngineTemplate> LoadFromFileAsync(string fileName)
    {
        using var memoryStream = new MemoryStream();

        using (var fileStream = new FileStream(
            path: Penetrates.GetTemplateFileName(fileName),
            mode: FileMode.Open,
            access: FileAccess.Read,
            share: FileShare.None,
            bufferSize: 8192,
            useAsync: true))
        {
            await fileStream.CopyToAsync(memoryStream);
        }

        return new ViewEngineTemplate(memoryStream);
    }

    /// <summary>
    /// 从流中加载模板
    /// </summary>
    /// <param name="stream"></param>
    /// <returns></returns>
    public static IViewEngineTemplate LoadFromStream(Stream stream)
    {
        return LoadFromStreamAsync(stream).GetAwaiter().GetResult();
    }

    /// <summary>
    /// 从流中加载模板
    /// </summary>
    /// <param name="stream"></param>
    /// <returns></returns>
    public static async Task<IViewEngineTemplate> LoadFromStreamAsync(Stream stream)
    {
        var memoryStream = new MemoryStream();
        await stream.CopyToAsync(memoryStream);
        memoryStream.Position = 0;

        return new ViewEngineTemplate(memoryStream);
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        assemblyByteCode?.Dispose();
    }
}

/// <summary>
/// 视图引擎模板（编译后）
/// </summary>
/// <typeparam name="T"></typeparam>
[SuppressSniffer]
public class ViewEngineTemplate<T> : IViewEngineTemplate<T>
    where T : IViewEngineModel
{
    /// <summary>
    /// 内存流
    /// </summary>
    private readonly MemoryStream assemblyByteCode;

    /// <summary>
    /// 内存流
    /// </summary>
    private readonly Type templateType;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="assemblyByteCode"></param>
    internal ViewEngineTemplate(MemoryStream assemblyByteCode)
    {
        this.assemblyByteCode = assemblyByteCode;
        templateType = Reflect.GetType(assemblyByteCode, "Furion.ViewEngine.Template");
    }

    /// <summary>
    /// 保存到流中
    /// </summary>
    /// <param name="stream"></param>
    public void SaveToStream(Stream stream)
    {
        SaveToStreamAsync(stream).GetAwaiter().GetResult();
    }

    /// <summary>
    /// 保存到流中
    /// </summary>
    /// <param name="stream"></param>
    /// <returns></returns>
    public Task SaveToStreamAsync(Stream stream)
    {
        return assemblyByteCode.CopyToAsync(stream);
    }

    /// <summary>
    /// 保存到文件中
    /// </summary>
    /// <param name="fileName"></param>
    public void SaveToFile(string fileName)
    {
        SaveToFileAsync(fileName).GetAwaiter().GetResult();
    }

    /// <summary>
    /// 保存到文件中
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public async Task SaveToFileAsync(string fileName)
    {
        using var fileStream = new FileStream(
            path: Penetrates.GetTemplateFileName(fileName),
            mode: FileMode.OpenOrCreate,
            access: FileAccess.Write,
            share: FileShare.None,
            bufferSize: 8192,
            useAsync: true);

        await assemblyByteCode.CopyToAsync(fileStream);
    }

    /// <summary>
    /// 执行编译
    /// </summary>
    /// <param name="initializer"></param>
    /// <returns></returns>
    public string Run(Action<T> initializer)
    {
        return RunAsync(initializer).GetAwaiter().GetResult();
    }

    /// <summary>
    /// 执行编译
    /// </summary>
    /// <param name="initializer"></param>
    /// <returns></returns>
    public async Task<string> RunAsync(Action<T> initializer)
    {
        var instance = (T)Activator.CreateInstance(templateType);
        initializer(instance);

        await instance.ExecuteAsync();

        return await instance.ResultAsync();
    }

    /// <summary>
    /// 从文件中加载模板
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public static IViewEngineTemplate<T> LoadFromFile(string fileName)
    {
        return LoadFromFileAsync(fileName: fileName).GetAwaiter().GetResult();
    }

    /// <summary>
    /// 从文件中加载模板
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public static async Task<IViewEngineTemplate<T>> LoadFromFileAsync(string fileName)
    {
        using var memoryStream = new MemoryStream();

        using (var fileStream = new FileStream(
            path: Penetrates.GetTemplateFileName(fileName),
            mode: FileMode.Open,
            access: FileAccess.Read,
            share: FileShare.Read,
            bufferSize: 8192,
            useAsync: true))
        {
            await fileStream.CopyToAsync(memoryStream);
        }

        return new ViewEngineTemplate<T>(memoryStream);
    }

    /// <summary>
    /// 从流中加载模板
    /// </summary>
    /// <param name="stream"></param>
    /// <returns></returns>
    public static IViewEngineTemplate<T> LoadFromStream(Stream stream)
    {
        return LoadFromStreamAsync(stream).GetAwaiter().GetResult();
    }

    /// <summary>
    /// 从流中加载模板
    /// </summary>
    /// <param name="stream"></param>
    /// <returns></returns>
    public static async Task<IViewEngineTemplate<T>> LoadFromStreamAsync(Stream stream)
    {
        var memoryStream = new MemoryStream();
        await stream.CopyToAsync(memoryStream);
        memoryStream.Position = 0;

        return new ViewEngineTemplate<T>(memoryStream);
    }

    /// <inheritdoc />
    public void Dispose()
    {
        assemblyByteCode.Dispose();
    }
}