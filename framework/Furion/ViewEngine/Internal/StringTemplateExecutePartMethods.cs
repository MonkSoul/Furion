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
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Furion.ViewEngine
{
    /// <summary>
    /// 字符串模板执行部件
    /// </summary>
    public sealed partial class StringTemplateExecutePart
    {
        /// <summary>
        /// 编译并运行
        /// </summary>
        public string RunCompile()
        {
            return InvokeRunCompileMethod(nameof(IViewEngine.RunCompile)) as string;
        }

        /// <summary>
        /// 编译并运行
        /// </summary>
        public Task<string> RunCompileAsync()
        {
            return InvokeRunCompileMethod(nameof(IViewEngine.RunCompileAsync)) as Task<string>;
        }

        /// <summary>
        /// 通过缓存解析模板
        /// </summary>
        public string RunCompileFromCached()
        {
            return InvokeRunCompileMethod(nameof(IViewEngine.RunCompileFromCached), true) as string;
        }

        /// <summary>
        /// 通过缓存解析模板
        /// </summary>
        public Task<string> RunCompileFromCachedAsync()
        {
            return InvokeRunCompileMethod(nameof(IViewEngine.RunCompileFromCachedAsync), true) as Task<string>;
        }

        /// <summary>
        /// 获取视图引擎对象
        /// </summary>
        /// <returns></returns>
        private IViewEngine GetViewEngine()
        {
            return App.GetService<IViewEngine>(ViewEngineScoped ?? App.RootServices)
                ?? throw new InvalidOperationException("Please confirm whether the view engine is registered successfully.");
        }

        /// <summary>
        /// 执行模板方法
        /// </summary>
        /// <param name="methodName"></param>
        /// <param name="isCached"></param>
        /// <returns></returns>
        private object InvokeRunCompileMethod(string methodName, bool isCached = false)
        {
            var viewEngine = GetViewEngine();
            var viewEngineType = viewEngine.GetType();

            // 反射获取视图引擎方法
            var runCompileMethod = TemplateModel.Type == typeof(object)
                ? viewEngineType.GetMethods(BindingFlags.Public | BindingFlags.Instance)
                           .First(m => m.Name == methodName && !m.IsGenericMethod)
                : viewEngineType.GetMethods(BindingFlags.Public | BindingFlags.Instance)
                           .First(m => m.Name == methodName && m.IsGenericMethod)
                           .MakeGenericMethod(TemplateModel.Type);

            return !isCached
                ? runCompileMethod.Invoke(viewEngine, new object[] {
                    Template,TemplateModel.Model,TemplateOptionsBuilder
                })
                : runCompileMethod.Invoke(viewEngine, new object[] {
                    Template,TemplateModel.Model,TemplateCachedFileName, TemplateOptionsBuilder
                });
        }
    }
}