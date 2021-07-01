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

using System.IO;

namespace Furion.ViewEngine
{
    /// <summary>
    /// 常量、公共方法配置类
    /// </summary>
    internal static class Penetrates
    {
        /// <summary>
        /// 模板保存目录
        /// </summary>
        internal static string TemplateSaveDir = "templates";

        /// <summary>
        /// 获取模板文件名
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        internal static string GetTemplateFileName(string fileName)
        {
            if (!Directory.Exists(TemplateSaveDir)) Directory.CreateDirectory(TemplateSaveDir);

            if (!fileName.EndsWith(".dll", System.StringComparison.OrdinalIgnoreCase)) fileName += ".dll";
            var templatePath = Path.Combine(TemplateSaveDir, "~" + fileName);

            return templatePath;
        }
    }
}