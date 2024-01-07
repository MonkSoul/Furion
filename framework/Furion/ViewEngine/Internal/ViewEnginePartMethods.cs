// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using Furion.Extensions;
using System.Reflection;

namespace Furion.ViewEngine;

/// <summary>
/// 字符串模板执行部件
/// </summary>
public sealed partial class ViewEnginePart
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
        var runCompileMethod = TemplateModel.Type.IsAnonymous() || TemplateModel.Type == typeof(object)
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

    /// <summary>
    /// 获取视图引擎对象
    /// </summary>
    /// <returns></returns>
    private IViewEngine GetViewEngine()
    {
        return App.GetService<IViewEngine>(ViewEngineScoped ?? App.RootServices)
            ?? throw new InvalidOperationException("Please confirm whether the view engine is registered successfully.");
    }
}