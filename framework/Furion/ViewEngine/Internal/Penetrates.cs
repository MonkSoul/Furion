// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.ViewEngine;

/// <summary>
/// 常量、公共方法配置类
/// </summary>
internal static class Penetrates
{
    /// <summary>
    /// 获取模板文件名
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    internal static string GetTemplateFileName(string fileName)
    {
        var templateSaveDir = Path.Combine(AppContext.BaseDirectory, "templates");
        if (!Directory.Exists(templateSaveDir)) Directory.CreateDirectory(templateSaveDir);

        if (!fileName.EndsWith(".dll", System.StringComparison.OrdinalIgnoreCase)) fileName += ".dll";
        var templatePath = Path.Combine(templateSaveDir, "~" + fileName);

        return templatePath;
    }
}