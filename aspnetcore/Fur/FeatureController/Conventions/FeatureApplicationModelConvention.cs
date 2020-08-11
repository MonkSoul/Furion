using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Fur.FeatureController
{
    /// <summary>
    /// 自定义应用模型转换器
    /// </summary>
    public sealed class FeatureApplicationModelConvention : IApplicationModelConvention
    {
        /// <summary>
        /// 特性配置选项
        /// </summary>
        private readonly FeatureSettingsOptions _featureSettingsOptions;

        public FeatureApplicationModelConvention()
        {
            _featureSettingsOptions = App.GetOptions<FeatureSettingsOptions>();
        }

        public void Apply(ApplicationModel application)
        {
        }
    }
}
