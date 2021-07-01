// -----------------------------------------------------------------------------
// 让 .NET 开发更简单，更通用，更流行。
// Copyright © 2020-2021 Furion, 百小僧, Baiqian Co.,Ltd.
//
// 框架名称：Furion
// 框架作者：百小僧
// 框架版本：2.11.1
// 源码地址：Gitee： https://gitee.com/dotnetchina/Furion
//          Github：https://github.com/monksoul/Furion
// 开源协议：Apache-2.0（https://gitee.com/dotnetchina/Furion/blob/master/LICENSE）
// -----------------------------------------------------------------------------

using Furion.DataEncryption;
using Furion.DependencyInjection;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Furion.ViewEngine
{
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
        private static MemoryStream CreateAndCompileToStream(string templateSource, ViewEngineOptions options)
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
                options.ReferencedAssemblies
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
        private static string WriteDirectives(string content, ViewEngineOptions options)
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
}