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
            share: FileShare.None,
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
}