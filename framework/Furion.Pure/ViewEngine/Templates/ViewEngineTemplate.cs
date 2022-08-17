// MIT License
//
// Copyright (c) 2020-2022 百小僧, Baiqian Co.,Ltd and Contributors
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

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