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