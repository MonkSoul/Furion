using Furion.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Furion.ViewEngine.Extensions
{
    /// <summary>
    /// 字符串视图引擎拓展
    /// </summary>
    [SkipScan]
    public static class ViewEngineStringExtensions
    {
        /// <summary>
        /// 编译并运行
        /// </summary>
        /// <param name="content"></param>
        /// <param name="model"></param>
        /// <param name="builderAction"></param>
        /// <param name="scoped"></param>
        /// <returns></returns>
        public static string RunCompile(this string content, object model = null, Action<IViewEngineOptionsBuilder> builderAction = null, IServiceProvider scoped = default)
        {
            return GetViewEngine(scoped).RunCompile(content, model, builderAction);
        }

        /// <summary>
        /// 编译并运行
        /// </summary>
        /// <param name="content"></param>
        /// <param name="model"></param>
        /// <param name="builderAction"></param>
        /// <param name="scoped"></param>
        /// <returns></returns>
        public static Task<string> RunCompileAsync(this string content, object model = null, Action<IViewEngineOptionsBuilder> builderAction = null, IServiceProvider scoped = default)
        {
            return GetViewEngine(scoped).RunCompileAsync(content, model, builderAction);
        }

        /// <summary>
        /// 编译并运行
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="content"></param>
        /// <param name="model"></param>
        /// <param name="builderAction"></param>
        /// <param name="scoped"></param>
        /// <returns></returns>
        public static string RunCompile<T>(this string content, T model, Action<IViewEngineOptionsBuilder> builderAction = null, IServiceProvider scoped = default)
            where T : class, new()
        {
            return GetViewEngine(scoped).RunCompile(content, model, builderAction);
        }

        /// <summary>
        /// 编译并运行
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="content"></param>
        /// <param name="model"></param>
        /// <param name="builderAction"></param>
        /// <param name="scoped"></param>
        /// <returns></returns>
        public static Task<string> RunCompileAsync<T>(this string content, T model, Action<IViewEngineOptionsBuilder> builderAction = null, IServiceProvider scoped = default)
            where T : class, new()
        {
            return GetViewEngine(scoped).RunCompileAsync(content, model, builderAction);
        }

        /// <summary>
        /// 通过缓存解析模板
        /// </summary>
        /// <param name="content"></param>
        /// <param name="model"></param>
        /// <param name="cacheFile"></param>
        /// <param name="builderAction"></param>
        /// <param name="scoped"></param>
        /// <returns></returns>
        public static string RunCompileFromCached(this string content, object model = null, string cacheFile = default, Action<IViewEngineOptionsBuilder> builderAction = null, IServiceProvider scoped = default)
        {
            return GetViewEngine(scoped).RunCompileFromCached(content, model, cacheFile, builderAction);
        }

        /// <summary>
        /// 通过缓存解析模板
        /// </summary>
        /// <param name="content"></param>
        /// <param name="model"></param>
        /// <param name="cacheFile"></param>
        /// <param name="builderAction"></param>
        /// <param name="scoped"></param>
        /// <returns></returns>
        public static Task<string> RunCompileFromCachedAsync(this string content, object model = null, string cacheFile = default, Action<IViewEngineOptionsBuilder> builderAction = null, IServiceProvider scoped = default)
        {
            return GetViewEngine(scoped).RunCompileFromCachedAsync(content, model, cacheFile, builderAction);
        }

        /// <summary>
        /// 通过缓存解析模板
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="content"></param>
        /// <param name="model"></param>
        /// <param name="cacheFile"></param>
        /// <param name="builderAction"></param>
        /// <param name="scoped"></param>
        /// <returns></returns>
        public static string RunCompileFromCached<T>(this string content, T model, string cacheFile = default, Action<IViewEngineOptionsBuilder> builderAction = null, IServiceProvider scoped = default)
            where T : class, new()
        {
            return GetViewEngine(scoped).RunCompileFromCached(content, model, cacheFile, builderAction);
        }

        /// <summary>
        /// 通过缓存解析模板
        /// </summary>
        /// <param name="content"></param>
        /// <param name="model"></param>
        /// <param name="cacheFile"></param>
        /// <param name="builderAction"></param>
        /// <param name="scoped"></param>
        /// <returns></returns>
        public static Task<string> RunCompileFromCachedAsync<T>(this string content, T model, string cacheFile = default, Action<IViewEngineOptionsBuilder> builderAction = null, IServiceProvider scoped = default)
            where T : class, new()
        {
            return GetViewEngine(scoped).RunCompileFromCachedAsync(content, model, cacheFile, builderAction);
        }

        /// <summary>
        /// 获取视图引擎对象
        /// </summary>
        /// <param name="scoped"></param>
        /// <returns></returns>
        private static IViewEngine GetViewEngine(IServiceProvider scoped = default)
        {
            return App.GetService<IViewEngine>(scoped)
                ?? throw new InvalidOperationException("Please confirm whether the view engine is registered successfully.");
        }
    }
}