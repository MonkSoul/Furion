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

using Furion.ClayObject.Extensions;
using Furion.Extensions;
using System.Text.RegularExpressions;

namespace Furion.Templates.Extensions;

/// <summary>
/// 字符串渲染模板拓展类
/// </summary>
[SuppressSniffer]
public static class StringRenderExtensions
{
    /// <summary>
    /// 模板正则表达式
    /// </summary>
    private const string commonTemplatePattern = @"\{(?<p>.+?)\}";

    /// <summary>
    /// 读取配置模板正则表达式
    /// </summary>
    private const string configTemplatePattern = @"\#\((?<p>.*?)\)";

    /// <summary>
    /// 渲染模板
    /// </summary>
    /// <param name="template"></param>
    /// <param name="templateData"></param>
    /// <param name="encode"></param>
    /// <returns></returns>
    public static string Render(this string template, object templateData, bool encode = false)
    {
        if (template == null) return default;

        return template.Render(templateData == null ? default : templateData.ToDictionary(), encode);
    }

    /// <summary>
    /// 渲染模板
    /// </summary>
    /// <param name="template"></param>
    /// <param name="templateData"></param>
    /// <param name="encode"></param>
    /// <returns></returns>
    public static string Render(this string template, IDictionary<string, object> templateData, bool encode = false)
    {
        if (template == null) return default;

        // 如果模板为空，则跳过
        if (templateData == null || templateData.Count == 0) return template;

        // 判断字符串是否包含模板
        if (!Regex.IsMatch(template, commonTemplatePattern)) return template;

        // 获取所有匹配的模板
        var templateValues = Regex.Matches(template, commonTemplatePattern)
                                                   .Select(u => new {
                                                       Template = u.Groups["p"].Value,
                                                       Value = MatchTemplateValue(u.Groups["p"].Value, templateData)
                                                   });

        // 循环替换模板
        foreach (var item in templateValues)
        {
            template = template.Replace($"{{{item.Template}}}", encode ? Uri.EscapeDataString(item.Value?.ToString() ?? string.Empty) : item.Value?.ToString());
        }

        return template;
    }

    /// <summary>
    /// 从配置中渲染字符串模板
    /// </summary>
    /// <param name="template"></param>
    /// <param name="encode"></param>
    /// <returns></returns>
    public static string Render(this string template, bool encode = false)
    {
        if (template == null) return default;

        // 判断字符串是否包含模板
        if (!Regex.IsMatch(template, configTemplatePattern)) return template;

        // 获取所有匹配的模板
        var templateValues = Regex.Matches(template, configTemplatePattern)
                                                   .Select(u => new {
                                                       Template = u.Groups["p"].Value,
                                                       Value = App.Configuration[u.Groups["p"].Value]
                                                   });

        // 循环替换模板
        foreach (var item in templateValues)
        {
            template = template.Replace($"#({item.Template})", encode ? Uri.EscapeDataString(item.Value?.ToString() ?? string.Empty) : item.Value?.ToString());
        }

        return template;
    }

    /// <summary>
    /// 匹配模板值
    /// </summary>
    /// <param name="template"></param>
    /// <param name="templateData"></param>
    /// <returns></returns>
    private static object MatchTemplateValue(string template, IDictionary<string, object> templateData)
    {
        string tmpl;
        if (!template.Contains('.', StringComparison.CurrentCulture)) tmpl = template;
        else tmpl = template.Split('.', StringSplitOptions.RemoveEmptyEntries).First();

        var succeed = templateData.TryGetValue(tmpl, out var templateValue);
        return ResolveTemplateValue(template, succeed ? templateValue : default);
    }

    /// <summary>
    /// 解析模板的值
    /// </summary>
    /// <param name="template"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    private static object ResolveTemplateValue(string template, object data)
    {
        // 根据 . 分割模板
        var propertyCrumbs = template.Split('.', StringSplitOptions.RemoveEmptyEntries);
        return GetValue(propertyCrumbs, data);

        // 静态本地函数
        static object GetValue(string[] propertyCrumbs, object data)
        {
            if (data == null || propertyCrumbs == null || propertyCrumbs.Length <= 1) return data;
            var dataType = data.GetType();

            // 如果是基元类型则直接返回
            if (dataType.IsRichPrimitive()) return data;
            object value = null;

            // 递归获取下一级模板值
            for (var i = 1; i < propertyCrumbs.Length; i++)
            {
                var propery = dataType.GetProperty(propertyCrumbs[i]);
                if (propery == null) break;

                value = propery.GetValue(data);
                if (i + 1 < propertyCrumbs.Length)
                {
                    value = GetValue(propertyCrumbs.Skip(i).ToArray(), value);
                }
                else break;
            }

            return value;
        }
    }
}