using Furion.DependencyInjection;
using System;

namespace Furion.ViewEngine
{
    /// <summary>
    /// 字符串模板执行部件
    /// </summary>
    [SkipScan]
    public sealed partial class StringTemplateExecutePart
    {
        /// <summary>
        /// 字符串模板
        /// </summary>
        public string Template { get; private set; }

        /// <summary>
        /// 视图配置选项
        /// </summary>
        public Action<IViewEngineOptionsBuilder> TemplateOptionsBuilder { get; private set; }

        /// <summary>
        /// 模型数据
        /// </summary>
        public (Type Type, object Model) TemplateModel { get; private set; } = (typeof(object), default);

        /// <summary>
        /// 模板缓存名称（不含拓展名）
        /// </summary>
        public string TemplateCachedFileName { get; private set; }

        /// <summary>
        /// 视图模板服务作用域
        /// </summary>
        public IServiceProvider ViewEngineScoped { get; private set; }
    }
}