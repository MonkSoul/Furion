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

namespace Furion.ViewEngine
{
    /// <summary>
    /// 字符串模板执行部件
    /// </summary>
    public sealed partial class StringTemplateExecutePart
    {
        /// <summary>
        /// 设置模板
        /// </summary>
        /// <param name="template"></param>
        /// <returns></returns>
        public StringTemplateExecutePart SetTemplate(string template)
        {
            Template = template;
            return this;
        }

        /// <summary>
        /// 设置模板数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public StringTemplateExecutePart SetTemplateModel<T>(T model)
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
        public StringTemplateExecutePart SetTemplateModel(object model)
        {
            TemplateModel = (typeof(object), model);
            return this;
        }

        /// <summary>
        /// 设置模板构建选项
        /// </summary>
        /// <param name="optionsBuilder"></param>
        /// <returns></returns>
        public StringTemplateExecutePart SetTemplateOptionsBuilder(Action<IViewEngineOptionsBuilder> optionsBuilder = default)
        {
            TemplateOptionsBuilder = optionsBuilder;
            return this;
        }

        /// <summary>
        /// 设置模板缓存文件名（不含拓展名）
        /// </summary>
        /// <param name="cachedFileName"></param>
        /// <returns></returns>
        public StringTemplateExecutePart SetTemplateCachedFileName(string cachedFileName)
        {
            TemplateCachedFileName = cachedFileName;
            return this;
        }

        /// <summary>
        /// 视图模板服务作用域
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public StringTemplateExecutePart SetViewEngineScoped(IServiceProvider serviceProvider)
        {
            ViewEngineScoped = serviceProvider;
            return this;
        }
    }
}