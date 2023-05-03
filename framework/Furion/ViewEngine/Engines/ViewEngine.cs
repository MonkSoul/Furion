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

using Furion.DataEncryption;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Reflection.Metadata;
using System.Text;

namespace Furion.ViewEngine;

/// <summary>
/// 视图引擎实现类
/// </summary>
[SuppressSniffer]
public class ViewEngine : IViewEngine
{
    /// <summary>
    /// 编译并运行
    /// </summary>
    /// <param name="content"></param>
    /// <param name="model"></param>
    /// <param name="builderAction"></param>
    /// <returns></returns>
    public string RunCompile(string content, object model = null, Action<IViewEngineOptionsBuilder> builderAction = null)
    {
        var template = Compile(content, builderAction);
        var result = template.Run(model);
        return result;
    }

    /// <summary>
    /// 编译并运行
    /// </summary>
    /// <param name="content"></param>
    /// <param name="model"></param>
    /// <param name="builderAction"></param>
    /// <returns></returns>
    public async Task<string> RunCompileAsync(string content, object model = null, Action<IViewEngineOptionsBuilder> builderAction = null)
    {
        var template = await CompileAsync(content, builderAction);
        var result = await template.RunAsync(model);
        return result;
    }

    /// <summary>
    /// 编译并运行
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="content"></param>
    /// <param name="model"></param>
    /// <param name="builderAction"></param>
    /// <returns></returns>
    public string RunCompile<T>(string content, T model, Action<IViewEngineOptionsBuilder> builderAction = null)
        where T : class, new()
    {
        var template = Compile<ViewEngineModel<T>>(content, builderAction);
        var result = template.Run(u =>
        {
            u.Model = model;
        });
        return result;
    }

    /// <summary>
    /// 编译并运行
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="content"></param>
    /// <param name="model"></param>
    /// <param name="builderAction"></param>
    /// <returns></returns>
    public async Task<string> RunCompileAsync<T>(string content, T model, Action<IViewEngineOptionsBuilder> builderAction = null)
        where T : class, new()
    {
        var template = await CompileAsync<ViewEngineModel<T>>(content, builderAction);
        var result = await template.RunAsync(u =>
        {
            u.Model = model;
        });
        return result;
    }

    /// <summary>
    /// 通过缓存解析模板
    /// </summary>
    /// <param name="content"></param>
    /// <param name="model"></param>
    /// <param name="cacheFileName"></param>
    /// <param name="builderAction"></param>
    /// <returns></returns>
    public string RunCompileFromCached(string content, object model = null, string cacheFileName = default, Action<IViewEngineOptionsBuilder> builderAction = null)
    {
        var fileName = cacheFileName ?? MD5Encryption.Encrypt(content);

        IViewEngineTemplate template;

        if (File.Exists(Penetrates.GetTemplateFileName(fileName)))
            template = ViewEngineTemplate.LoadFromFile(fileName);
        else
        {
            template = Compile(content, builderAction);
            template.SaveToFile(fileName);
        }

        var result = template.Run(model);
        return result;
    }

    /// <summary>
    /// 通过缓存解析模板
    /// </summary>
    /// <param name="content"></param>
    /// <param name="model"></param>
    /// <param name="cacheFileName"></param>
    /// <param name="builderAction"></param>
    /// <returns></returns>
    public async Task<string> RunCompileFromCachedAsync(string content, object model = null, string cacheFileName = default, Action<IViewEngineOptionsBuilder> builderAction = null)
    {
        var fileName = cacheFileName ?? MD5Encryption.Encrypt(content);

        IViewEngineTemplate template;

        if (File.Exists(Penetrates.GetTemplateFileName(fileName)))
            template = await ViewEngineTemplate.LoadFromFileAsync(fileName);
        else
        {
            template = await CompileAsync(content, builderAction);
            await template.SaveToFileAsync(fileName);
        }

        var result = await template.RunAsync(model);
        return result;
    }

    /// <summary>
    /// 通过缓存解析模板
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="content"></param>
    /// <param name="model"></param>
    /// <param name="cacheFileName"></param>
    /// <param name="builderAction"></param>
    /// <returns></returns>
    public string RunCompileFromCached<T>(string content, T model, string cacheFileName = default, Action<IViewEngineOptionsBuilder> builderAction = null)
        where T : class, new()
    {
        var fileName = cacheFileName ?? MD5Encryption.Encrypt(content);

        IViewEngineTemplate<ViewEngineModel<T>> template;

        if (File.Exists(Penetrates.GetTemplateFileName(fileName)))
            template = ViewEngineTemplate<ViewEngineModel<T>>.LoadFromFile(fileName);
        else
        {
            template = Compile<ViewEngineModel<T>>(content, builderAction);
            template.SaveToFile(fileName);
        }

        var result = template.Run(u =>
        {
            u.Model = model;
        });
        return result;
    }

    /// <summary>
    /// 通过缓存解析模板
    /// </summary>
    /// <param name="content"></param>
    /// <param name="model"></param>
    /// <param name="cacheFileName"></param>
    /// <param name="builderAction"></param>
    /// <returns></returns>
    public async Task<string> RunCompileFromCachedAsync<T>(string content, T model, string cacheFileName = default, Action<IViewEngineOptionsBuilder> builderAction = null)
        where T : class, new()
    {
        var fileName = cacheFileName ?? MD5Encryption.Encrypt(content);

        IViewEngineTemplate<ViewEngineModel<T>> template;

        if (File.Exists(Penetrates.GetTemplateFileName(fileName)))
            template = await ViewEngineTemplate<ViewEngineModel<T>>.LoadFromFileAsync(fileName);
        else
        {
            template = await CompileAsync<ViewEngineModel<T>>(content, builderAction);
            await template.SaveToFileAsync(fileName);
        }

        var result = await template.RunAsync(u =>
        {
            u.Model = model;
        });
        return result;
    }

    /// <summary>
    /// 编译模板
    /// </summary>
    /// <param name="content"></param>
    /// <param name="builderAction"></param>
    /// <returns></returns>
    public IViewEngineTemplate Compile(string content, Action<IViewEngineOptionsBuilder> builderAction = null)
    {
        IViewEngineOptionsBuilder compilationOptionsBuilder = new ViewEngineOptionsBuilder();
        compilationOptionsBuilder.Inherits(typeof(ViewEngineModel));

        builderAction?.Invoke(compilationOptionsBuilder);

        var memoryStream = CreateAndCompileToStream(content, compilationOptionsBuilder.Options);

        return new ViewEngineTemplate(memoryStream);
    }

    /// <summary>
    /// 编译模板
    /// </summary>
    /// <param name="content"></param>
    /// <param name="builderAction"></param>
    /// <returns></returns>
    public Task<IViewEngineTemplate> CompileAsync(string content, Action<IViewEngineOptionsBuilder> builderAction = null)
    {
        return Task.Factory.StartNew(() => Compile(content: content, builderAction: builderAction));
    }

    /// <summary>
    /// 编译模板
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="content"></param>
    /// <param name="builderAction"></param>
    /// <returns></returns>
    public IViewEngineTemplate<T> Compile<T>(string content, Action<IViewEngineOptionsBuilder> builderAction = null)
        where T : IViewEngineModel
    {
        IViewEngineOptionsBuilder compilationOptionsBuilder = new ViewEngineOptionsBuilder();

        compilationOptionsBuilder.AddAssemblyReference(typeof(T).Assembly);
        compilationOptionsBuilder.Inherits(typeof(T));

        builderAction?.Invoke(compilationOptionsBuilder);

        var memoryStream = CreateAndCompileToStream(content, compilationOptionsBuilder.Options);

        return new ViewEngineTemplate<T>(memoryStream);
    }

    /// <summary>
    /// 编译模板
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="content"></param>
    /// <param name="builderAction"></param>
    /// <returns></returns>
    public Task<IViewEngineTemplate<T>> CompileAsync<T>(string content, Action<IViewEngineOptionsBuilder> builderAction = null)
        where T : IViewEngineModel
    {
        return Task.Factory.StartNew(() => Compile<T>(content: content, builderAction: builderAction));
    }

    /// <summary>
    /// 将模板内容编译并输出内存流
    /// </summary>
    /// <param name="templateSource"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    protected virtual MemoryStream CreateAndCompileToStream(string templateSource, ViewEngineOptions options)
    {
        templateSource = WriteDirectives(templateSource, options);

        var engine = RazorProjectEngine.Create(
            RazorConfiguration.Default,
            RazorProjectFileSystem.Create(@"."),
            (builder) =>
            {
                builder.SetNamespace(options.TemplateNamespace);
            });

        var fileName = Path.GetRandomFileName();

        var document = RazorSourceDocument.Create(templateSource, fileName);

        var codeDocument = engine.Process(
            document,
            null,
            new List<RazorSourceDocument>(),
            new List<TagHelperDescriptor>());

        var razorCSharpDocument = codeDocument.GetCSharpDocument();

        var syntaxTree = CSharpSyntaxTree.ParseText(razorCSharpDocument.GeneratedCode);

        var compilation = CSharpCompilation.Create(
            fileName,
            new[]
            {
                    syntaxTree
            },
            options.ReferencedAssemblies.Where(ass =>
            {
                unsafe
                {
                    return ass.TryGetRawMetadata(out var blob, out var length);
                }
            })
            .Select(ass =>
            {
                // MetadataReference.CreateFromFile(ass.Location)

                unsafe
                {
                    ass.TryGetRawMetadata(out var blob, out var length);
                    var moduleMetadata = ModuleMetadata.CreateFromMetadata((IntPtr)blob, length);
                    var assemblyMetadata = AssemblyMetadata.Create(moduleMetadata);
                    var metadataReference = assemblyMetadata.GetReference();
                    return metadataReference;
                }
            })
            .Concat(options.MetadataReferences)
            .ToList(),
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

        var memoryStream = new MemoryStream();

        var emitResult = compilation.Emit(memoryStream);

        if (!emitResult.Success)
        {
            var exception = new ViewEngineTemplateException()
            {
                Errors = emitResult.Diagnostics.ToList(),
                GeneratedCode = razorCSharpDocument.GeneratedCode
            };

            throw exception;
        }

        memoryStream.Position = 0;

        return memoryStream;
    }

    /// <summary>
    /// 写入Razor 命令
    /// </summary>
    /// <param name="content"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    protected virtual string WriteDirectives(string content, ViewEngineOptions options)
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.AppendLine($"@inherits {options.Inherits}");

        foreach (var entry in options.DefaultUsings)
        {
            stringBuilder.AppendLine($"@using {entry}");
        }

        stringBuilder.Append(content);

        return stringBuilder.ToString();
    }
}