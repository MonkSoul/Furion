// -----------------------------------------------------------------------------
// Fur 是 .NET 5 平台下极易入门、极速开发的 Web 应用框架。
// Copyright © 2020 Fur, Baiqian Co.,Ltd.
//
// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0
// 源码地址：https://gitee.com/monksoul/Fur
// 开源协议：Apache-2.0（http://www.apache.org/licenses/LICENSE-2.0）
// -----------------------------------------------------------------------------

using Fur.DependencyInjection;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace Fur.ViewEngine
{
    /// <summary>
    /// 视图编译模板实现类
    /// </summary>
    [NonBeScan]
    public class ViewEngineCompiledTemplate : IViewEngineCompiledTemplate
    {
        private readonly MemoryStream assemblyByteCode;
        private readonly Type templateType;

        internal ViewEngineCompiledTemplate(MemoryStream assemblyByteCode)
        {
            this.assemblyByteCode = assemblyByteCode;

            var assembly = Assembly.Load(assemblyByteCode.ToArray());
            templateType = assembly.GetType("TemplateNamespace.Template");
        }

        public static IViewEngineCompiledTemplate LoadFromFile(string fileName)
        {
            return LoadFromFileAsync(fileName: fileName).GetAwaiter().GetResult();
        }

        public static async Task<IViewEngineCompiledTemplate> LoadFromFileAsync(string fileName)
        {
            var memoryStream = new MemoryStream();

            using (var fileStream = new FileStream(
                path: fileName,
                mode: FileMode.Open,
                access: FileAccess.Read,
                share: FileShare.None,
                bufferSize: 4096,
                useAsync: true))
            {
                await fileStream.CopyToAsync(memoryStream);
            }

            return new ViewEngineCompiledTemplate(memoryStream);
        }

        public static IViewEngineCompiledTemplate LoadFromStream(Stream stream)
        {
            return LoadFromStreamAsync(stream).GetAwaiter().GetResult();
        }

        public static async Task<IViewEngineCompiledTemplate> LoadFromStreamAsync(Stream stream)
        {
            var memoryStream = new MemoryStream();
            await stream.CopyToAsync(memoryStream);
            memoryStream.Position = 0;

            return new ViewEngineCompiledTemplate(memoryStream);
        }

        public void SaveToStream(Stream stream)
        {
            SaveToStreamAsync(stream).GetAwaiter().GetResult();
        }

        public Task SaveToStreamAsync(Stream stream)
        {
            return assemblyByteCode.CopyToAsync(stream);
        }

        public void SaveToFile(string fileName)
        {
            SaveToFileAsync(fileName).GetAwaiter().GetResult();
        }

        public Task SaveToFileAsync(string fileName)
        {
            using var fileStream = new FileStream(
                path: fileName,
                mode: FileMode.OpenOrCreate,
                access: FileAccess.Write,
                share: FileShare.None,
                bufferSize: 4096,
                useAsync: true);

            return assemblyByteCode.CopyToAsync(fileStream);
        }

        public string Run(object model = null)
        {
            return RunAsync(model).GetAwaiter().GetResult();
        }

        public async Task<string> RunAsync(object model = null)
        {
            if (model != null && model.IsAnonymous())
            {
                model = new AnonymousTypeWrapper(model);
            }

            var instance = (IViewEngineTemplate)Activator.CreateInstance(templateType);
            instance.Model = model;

            await instance.ExecuteAsync();

            return instance.Result();
        }
    }

    /// <summary>
    /// 泛型视图模板实现类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [NonBeScan]
    public class ViewEngineCompiledTemplate<T> : IViewEngineCompiledTemplate<T> where T : IViewEngineTemplate
    {
        private readonly MemoryStream assemblyByteCode;
        private readonly Type templateType;

        internal ViewEngineCompiledTemplate(MemoryStream assemblyByteCode)
        {
            this.assemblyByteCode = assemblyByteCode;

            var assembly = Assembly.Load(assemblyByteCode.ToArray());
            templateType = assembly.GetType("TemplateNamespace.Template");
        }

        public static IViewEngineCompiledTemplate<T> LoadFromFile(string fileName)
        {
            return LoadFromFileAsync(fileName: fileName).GetAwaiter().GetResult();
        }

        public static async Task<IViewEngineCompiledTemplate<T>> LoadFromFileAsync(string fileName)
        {
            var memoryStream = new MemoryStream();

            using (var fileStream = new FileStream(
                path: fileName,
                mode: FileMode.Open,
                access: FileAccess.Read,
                share: FileShare.None,
                bufferSize: 4096,
                useAsync: true))
            {
                await fileStream.CopyToAsync(memoryStream);
            }

            return new ViewEngineCompiledTemplate<T>(memoryStream);
        }

        public static IViewEngineCompiledTemplate<T> LoadFromStream(Stream stream)
        {
            return LoadFromStreamAsync(stream).GetAwaiter().GetResult();
        }

        public static async Task<IViewEngineCompiledTemplate<T>> LoadFromStreamAsync(Stream stream)
        {
            var memoryStream = new MemoryStream();
            await stream.CopyToAsync(memoryStream);
            memoryStream.Position = 0;

            return new ViewEngineCompiledTemplate<T>(memoryStream);
        }

        public void SaveToStream(Stream stream)
        {
            SaveToStreamAsync(stream).GetAwaiter().GetResult();
        }

        public Task SaveToStreamAsync(Stream stream)
        {
            return assemblyByteCode.CopyToAsync(stream);
        }

        public void SaveToFile(string fileName)
        {
            SaveToFileAsync(fileName).GetAwaiter().GetResult();
        }

        public Task SaveToFileAsync(string fileName)
        {
            using var fileStream = new FileStream(
                path: fileName,
                mode: FileMode.CreateNew,
                access: FileAccess.Write,
                share: FileShare.None,
                bufferSize: 4096,
                useAsync: true);
            return assemblyByteCode.CopyToAsync(fileStream);
        }

        public string Run(Action<T> initializer)
        {
            return RunAsync(initializer).GetAwaiter().GetResult();
        }

        public async Task<string> RunAsync(Action<T> initializer)
        {
            var instance = (T)Activator.CreateInstance(templateType);
            initializer(instance);

            await instance.ExecuteAsync();

            return instance.Result();
        }
    }
}