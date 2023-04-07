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