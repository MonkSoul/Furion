// ------------------------------------------------------------------------
// 版权信息
// 版权归百小僧及百签科技（广东）有限公司所有。
// 所有权利保留。
// 官方网站：https://baiqian.com
//
// 许可证信息
// Furion 项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。
// 许可证的完整文本可以在源代码树根目录中的 LICENSE-APACHE 和 LICENSE-MIT 文件中找到。
// 官方网站：https://furion.net
//
// 使用条款
// 使用本代码应遵守相关法律法规和许可证的要求。
//
// 免责声明
// 对于因使用本代码而产生的任何直接、间接、偶然、特殊或后果性损害，我们不承担任何责任。
//
// 其他重要信息
// Furion 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。
// 有关 Furion 项目的其他详细信息，请参阅位于源代码树根目录中的 COPYRIGHT 和 DISCLAIMER 文件。
//
// 更多信息
// 请访问 https://gitee.com/dotnetchina/Furion 获取更多关于 Furion 项目的许可证和版权信息。
// ------------------------------------------------------------------------

using Furion.ClayObject;
using Furion.Extensions;
using System.Dynamic;
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
        var runCompileMethod = TemplateModel.Type.IsAnonymous() || TemplateModel.Type == typeof(object) || TemplateModel.Type == typeof(Clay) || typeof(DynamicObject).IsAssignableFrom(TemplateModel.Type)
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