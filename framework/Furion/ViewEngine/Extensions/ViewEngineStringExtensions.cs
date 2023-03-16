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

namespace Furion.ViewEngine.Extensions;

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
    public static ViewEnginePart SetTemplateModel<T>(this string template, T model)
        where T : class, new()
    {
        return ViewEnginePart.Default().SetTemplate(template).SetTemplateModel<T>(model);
    }

    /// <summary>
    /// 设置模板数据
    /// </summary>
    /// <param name="template"></param>
    /// <param name="model"></param>
    /// <returns></returns>
    public static ViewEnginePart SetTemplateModel(this string template, object model)
    {
        return ViewEnginePart.Default().SetTemplate(template).SetTemplateModel(model);
    }

    /// <summary>
    /// 设置模板构建选项
    /// </summary>
    /// <param name="template"></param>
    /// <param name="optionsBuilder"></param>
    /// <returns></returns>
    public static ViewEnginePart SetTemplateOptionsBuilder(this string template, Action<IViewEngineOptionsBuilder> optionsBuilder = default)
    {
        return ViewEnginePart.Default().SetTemplate(template).SetTemplateOptionsBuilder(optionsBuilder);
    }

    /// <summary>
    /// 设置模板缓存文件名（不含拓展名）
    /// </summary>
    /// <param name="template"></param>
    /// <param name="cachedFileName"></param>
    /// <returns></returns>
    public static ViewEnginePart SetTemplateCachedFileName(this string template, string cachedFileName)
    {
        return ViewEnginePart.Default().SetTemplate(template).SetTemplateCachedFileName(cachedFileName);
    }

    /// <summary>
    /// 视图模板服务作用域
    /// </summary>
    /// <param name="template"></param>
    /// <param name="serviceProvider"></param>
    /// <returns></returns>
    public static ViewEnginePart SetViewEngineScoped(this string template, IServiceProvider serviceProvider)
    {
        return ViewEnginePart.Default().SetTemplate(template).SetViewEngineScoped(serviceProvider);
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
        return ViewEnginePart.Default().SetTemplate(template).SetTemplateModel(model).SetTemplateOptionsBuilder(builderAction).RunCompile();
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
        return ViewEnginePart.Default().SetTemplate(template).SetTemplateModel(model).SetTemplateOptionsBuilder(builderAction).RunCompileAsync();
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
        return ViewEnginePart.Default().SetTemplate(template).SetTemplateModel(model).SetTemplateOptionsBuilder(builderAction).RunCompile();
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
        return ViewEnginePart.Default().SetTemplate(template).SetTemplateModel(model).SetTemplateOptionsBuilder(builderAction).RunCompileAsync();
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
        return ViewEnginePart.Default().SetTemplate(template).SetTemplateModel(model).SetTemplateCachedFileName(cachedFileName).SetTemplateOptionsBuilder(builderAction).RunCompileFromCached();
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
        return ViewEnginePart.Default().SetTemplate(template).SetTemplateModel(model).SetTemplateCachedFileName(cachedFileName).SetTemplateOptionsBuilder(builderAction).RunCompileFromCachedAsync();
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
        return ViewEnginePart.Default().SetTemplate(template).SetTemplateModel(model).SetTemplateCachedFileName(cachedFileName).SetTemplateOptionsBuilder(builderAction).RunCompileFromCached();
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
        return ViewEnginePart.Default().SetTemplate(template).SetTemplateModel(model).SetTemplateCachedFileName(cachedFileName).SetTemplateOptionsBuilder(builderAction).RunCompileFromCachedAsync();
    }
}