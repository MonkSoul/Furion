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

using Furion.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Furion.ViewEngine.Extensions
{
    /// <summary>
    /// 字符串视图引擎拓展
    /// </summary>
    [SuppressSniffer]
    public static class ViewEngineStringExtensions
    {
        /// <summary>
        /// 设置模板数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="template"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public static StringTemplateExecutePart SetTemplateModel<T>(this string template, T model)
            where T : class, new()
        {
            return new StringTemplateExecutePart().SetTemplate(template).SetTemplateModel<T>(model);
        }

        /// <summary>
        /// 设置模板数据
        /// </summary>
        /// <param name="template"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public static StringTemplateExecutePart SetTemplateModel(this string template, object model)
        {
            return new StringTemplateExecutePart().SetTemplate(template).SetTemplateModel(model);
        }

        /// <summary>
        /// 设置模板构建选项
        /// </summary>
        /// <param name="template"></param>
        /// <param name="optionsBuilder"></param>
        /// <returns></returns>
        public static StringTemplateExecutePart SetTemplateOptionsBuilder(this string template, Action<IViewEngineOptionsBuilder> optionsBuilder = default)
        {
            return new StringTemplateExecutePart().SetTemplate(template).SetTemplateOptionsBuilder(optionsBuilder);
        }

        /// <summary>
        /// 设置模板缓存文件名（不含拓展名）
        /// </summary>
        /// <param name="template"></param>
        /// <param name="cachedFileName"></param>
        /// <returns></returns>
        public static StringTemplateExecutePart SetTemplateCachedFileName(this string template, string cachedFileName)
        {
            return new StringTemplateExecutePart().SetTemplate(template).SetTemplateCachedFileName(cachedFileName);
        }

        /// <summary>
        /// 视图模板服务作用域
        /// </summary>
        /// <param name="template"></param>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public static StringTemplateExecutePart SetViewEngineScoped(this string template, IServiceProvider serviceProvider)
        {
            return new StringTemplateExecutePart().SetTemplate(template).SetViewEngineScoped(serviceProvider);
        }

        /// <summary>
        /// 编译并运行
        /// </summary>
        /// <param name="template"></param>
        /// <param name="model"></param>
        /// <param name="builderAction"></param>
        /// <returns></returns>
        public static string RunCompile(this string template, object model = null, Action<IViewEngineOptionsBuilder> builderAction = null)
        {
            return new StringTemplateExecutePart().SetTemplate(template).SetTemplateModel(model).SetTemplateOptionsBuilder(builderAction).RunCompile();
        }

        /// <summary>
        /// 编译并运行
        /// </summary>
        /// <param name="template"></param>
        /// <param name="model"></param>
        /// <param name="builderAction"></param>
        /// <returns></returns>
        public static Task<string> RunCompileAsync(this string template, object model = null, Action<IViewEngineOptionsBuilder> builderAction = null)
        {
            return new StringTemplateExecutePart().SetTemplate(template).SetTemplateModel(model).SetTemplateOptionsBuilder(builderAction).RunCompileAsync();
        }

        /// <summary>
        /// 编译并运行
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="template"></param>
        /// <param name="model"></param>
        /// <param name="builderAction"></param>
        /// <returns></returns>
        public static string RunCompile<T>(this string template, T model, Action<IViewEngineOptionsBuilder> builderAction = null)
            where T : class, new()
        {
            return new StringTemplateExecutePart().SetTemplate(template).SetTemplateModel(model).SetTemplateOptionsBuilder(builderAction).RunCompile();
        }

        /// <summary>
        /// 编译并运行
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="template"></param>
        /// <param name="model"></param>
        /// <param name="builderAction"></param>
        /// <returns></returns>
        public static Task<string> RunCompileAsync<T>(this string template, T model, Action<IViewEngineOptionsBuilder> builderAction = null)
            where T : class, new()
        {
            return new StringTemplateExecutePart().SetTemplate(template).SetTemplateModel(model).SetTemplateOptionsBuilder(builderAction).RunCompileAsync();
        }

        /// <summary>
        /// 通过缓存解析模板
        /// </summary>
        /// <param name="template"></param>
        /// <param name="model"></param>
        /// <param name="cachedFileName"></param>
        /// <param name="builderAction"></param>
        /// <returns></returns>
        public static string RunCompileFromCached(this string template, object model = null, string cachedFileName = default, Action<IViewEngineOptionsBuilder> builderAction = null)
        {
            return new StringTemplateExecutePart().SetTemplate(template).SetTemplateModel(model).SetTemplateCachedFileName(cachedFileName).SetTemplateOptionsBuilder(builderAction).RunCompileFromCached();
        }

        /// <summary>
        /// 通过缓存解析模板
        /// </summary>
        /// <param name="template"></param>
        /// <param name="model"></param>
        /// <param name="cachedFileName"></param>
        /// <param name="builderAction"></param>
        /// <returns></returns>
        public static Task<string> RunCompileFromCachedAsync(this string template, object model = null, string cachedFileName = default, Action<IViewEngineOptionsBuilder> builderAction = null)
        {
            return new StringTemplateExecutePart().SetTemplate(template).SetTemplateModel(model).SetTemplateCachedFileName(cachedFileName).SetTemplateOptionsBuilder(builderAction).RunCompileFromCachedAsync();
        }

        /// <summary>
        /// 通过缓存解析模板
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="template"></param>
        /// <param name="model"></param>
        /// <param name="cachedFileName"></param>
        /// <param name="builderAction"></param>
        /// <returns></returns>
        public static string RunCompileFromCached<T>(this string template, T model, string cachedFileName = default, Action<IViewEngineOptionsBuilder> builderAction = null)
            where T : class, new()
        {
            return new StringTemplateExecutePart().SetTemplate(template).SetTemplateModel(model).SetTemplateCachedFileName(cachedFileName).SetTemplateOptionsBuilder(builderAction).RunCompileFromCached();
        }

        /// <summary>
        /// 通过缓存解析模板
        /// </summary>
        /// <param name="template"></param>
        /// <param name="model"></param>
        /// <param name="cachedFileName"></param>
        /// <param name="builderAction"></param>
        /// <returns></returns>
        public static Task<string> RunCompileFromCachedAsync<T>(this string template, T model, string cachedFileName = default, Action<IViewEngineOptionsBuilder> builderAction = null)
            where T : class, new()
        {
            return new StringTemplateExecutePart().SetTemplate(template).SetTemplateModel(model).SetTemplateCachedFileName(cachedFileName).SetTemplateOptionsBuilder(builderAction).RunCompileFromCachedAsync();
        }
    }
}