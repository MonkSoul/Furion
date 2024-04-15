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

namespace Furion.ViewEngine;

/// <summary>
/// 字符串模板执行部件
/// </summary>
public sealed partial class ViewEnginePart
{
    /// <summary>
    /// 设置模板
    /// </summary>
    /// <param name="template"></param>
    /// <returns></returns>
    public ViewEnginePart SetTemplate(string template)
    {
        if (!string.IsNullOrWhiteSpace(template)) Template = template;
        return this;
    }

    /// <summary>
    /// 设置模板数据
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="model"></param>
    /// <returns></returns>
    public ViewEnginePart SetTemplateModel<T>(T model)
        where T : class, new()
    {
        TemplateModel = (typeof(T), model);
        return this;
    }

    /// <summary>
    /// 设置模板数据
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public ViewEnginePart SetTemplateModel(object model)
    {
        TemplateModel = (model == null ? typeof(object) : model.GetType(), model);
        return this;
    }

    /// <summary>
    /// 设置模板构建选项
    /// </summary>
    /// <param name="optionsBuilder"></param>
    /// <returns></returns>
    public ViewEnginePart SetTemplateOptionsBuilder(Action<IViewEngineOptionsBuilder> optionsBuilder = default)
    {
        if (optionsBuilder != null) TemplateOptionsBuilder = optionsBuilder;
        return this;
    }

    /// <summary>
    /// 设置模板缓存文件名（不含拓展名）
    /// </summary>
    /// <param name="cachedFileName"></param>
    /// <returns></returns>
    public ViewEnginePart SetTemplateCachedFileName(string cachedFileName)
    {
        if (!string.IsNullOrWhiteSpace(cachedFileName)) TemplateCachedFileName = cachedFileName;
        return this;
    }

    /// <summary>
    /// 视图模板服务作用域
    /// </summary>
    /// <param name="serviceProvider"></param>
    /// <returns></returns>
    public ViewEnginePart SetViewEngineScoped(IServiceProvider serviceProvider)
    {
        if (serviceProvider != null) ViewEngineScoped = serviceProvider;
        return this;
    }
}