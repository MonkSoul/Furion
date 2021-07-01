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

using System;
using System.Threading.Tasks;

namespace Furion.ViewEngine
{
    /// <summary>
    /// 视图引擎接口
    /// </summary>
    public interface IViewEngine
    {
        /// <summary>
        /// 编译并运行
        /// </summary>
        /// <param name="content"></param>
        /// <param name="model"></param>
        /// <param name="builderAction"></param>
        /// <returns></returns>
        string RunCompile(string content, object model = null, Action<IViewEngineOptionsBuilder> builderAction = null);

        /// <summary>
        /// 编译并运行
        /// </summary>
        /// <param name="content"></param>
        /// <param name="model"></param>
        /// <param name="builderAction"></param>
        /// <returns></returns>
        Task<string> RunCompileAsync(string content, object model = null, Action<IViewEngineOptionsBuilder> builderAction = null);

        /// <summary>
        /// 编译并运行
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="content"></param>
        /// <param name="model"></param>
        /// <param name="builderAction"></param>
        /// <returns></returns>
        string RunCompile<T>(string content, T model, Action<IViewEngineOptionsBuilder> builderAction = null)
            where T : class, new();

        /// <summary>
        /// 编译并运行
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="content"></param>
        /// <param name="model"></param>
        /// <param name="builderAction"></param>
        /// <returns></returns>
        Task<string> RunCompileAsync<T>(string content, T model, Action<IViewEngineOptionsBuilder> builderAction = null)
            where T : class, new();

        /// <summary>
        /// 通过缓存解析模板
        /// </summary>
        /// <param name="content"></param>
        /// <param name="model"></param>
        /// <param name="cacheFileName"></param>
        /// <param name="builderAction"></param>
        /// <returns></returns>
        string RunCompileFromCached(string content, object model = null, string cacheFileName = default, Action<IViewEngineOptionsBuilder> builderAction = null);

        /// <summary>
        /// 通过缓存解析模板
        /// </summary>
        /// <param name="content"></param>
        /// <param name="model"></param>
        /// <param name="cacheFileName"></param>
        /// <param name="builderAction"></param>
        /// <returns></returns>
        Task<string> RunCompileFromCachedAsync(string content, object model = null, string cacheFileName = default, Action<IViewEngineOptionsBuilder> builderAction = null);

        /// <summary>
        /// 通过缓存解析模板
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="content"></param>
        /// <param name="model"></param>
        /// <param name="cacheFileName"></param>
        /// <param name="builderAction"></param>
        /// <returns></returns>
        string RunCompileFromCached<T>(string content, T model, string cacheFileName = default, Action<IViewEngineOptionsBuilder> builderAction = null)
            where T : class, new();

        /// <summary>
        /// 通过缓存解析模板
        /// </summary>
        /// <param name="content"></param>
        /// <param name="model"></param>
        /// <param name="cacheFileName"></param>
        /// <param name="builderAction"></param>
        /// <returns></returns>
        Task<string> RunCompileFromCachedAsync<T>(string content, T model, string cacheFileName = default, Action<IViewEngineOptionsBuilder> builderAction = null)
            where T : class, new();

        /// <summary>
        /// 编译模板
        /// </summary>
        /// <param name="content"></param>
        /// <param name="builderAction"></param>
        /// <returns></returns>
        IViewEngineTemplate Compile(string content, Action<IViewEngineOptionsBuilder> builderAction = null);

        /// <summary>
        /// 编译模板
        /// </summary>
        /// <param name="content"></param>
        /// <param name="builderAction"></param>
        /// <returns></returns>
        Task<IViewEngineTemplate> CompileAsync(string content, Action<IViewEngineOptionsBuilder> builderAction = null);

        /// <summary>
        /// 编译模板
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="content"></param>
        /// <param name="builderAction"></param>
        /// <returns></returns>
        IViewEngineTemplate<T> Compile<T>(string content, Action<IViewEngineOptionsBuilder> builderAction = null)
            where T : IViewEngineModel;

        /// <summary>
        /// 编译模板
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="content"></param>
        /// <param name="builderAction"></param>
        /// <returns></returns>
        Task<IViewEngineTemplate<T>> CompileAsync<T>(string content, Action<IViewEngineOptionsBuilder> builderAction = null)
            where T : IViewEngineModel;
    }
}