using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System.Reflection;

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

        /// <summary>
        /// 构造函数
        /// </summary>
        public FeatureApplicationModelConvention()
        {
            _featureSettingsOptions = App.GetOptions<FeatureSettingsOptions>();
        }

        public void Apply(ApplicationModel application)
        {
            foreach (var controllerModel in application.Controllers)
            {
                var controllerType = controllerModel.ControllerType;

                // 跳过 ControllerBase 子类配置
                if (typeof(ControllerBase).IsAssignableFrom(controllerType)) continue;

                // 配置控制器
                var featureSettings = controllerType.IsDefined(typeof(FeatureSettingsAttribute), true)
                    ? default
                    : controllerType.GetCustomAttribute<FeatureSettingsAttribute>(true);
                ConfigureControllerModel(controllerModel, featureSettings);
            }
        }

        /// <summary>
        /// 配置控制器模型
        /// </summary>
        /// <param name="controllerModel"></param>
        private void ConfigureControllerModel(ControllerModel controllerModel, FeatureSettingsAttribute featureSettings)
        {
            // 配置控制器可见性
            if (!controllerModel.ApiExplorer.IsVisible.HasValue) controllerModel.ApiExplorer.IsVisible = true;

            // 配置控制器名称
            ConfigureControllerName(controllerModel, featureSettings);
        }

        /// <summary>
        /// 配置控制器名称
        /// </summary>
        /// <param name="controllerModel"></param>
        /// <param name="featureSettings"></param>
        private void ConfigureControllerName(ControllerModel controllerModel, FeatureSettingsAttribute featureSettings)
        {
            // 读取版本号
            var apiVersion = featureSettings?.ApiVersion;

            // 解析控制器名称
            string tempName;
            if (!string.IsNullOrEmpty(featureSettings?.Name)) tempName = featureSettings.Name;
            else
            {
                tempName = !(featureSettings?.KeepName.Value ?? false)
                       ? Penetrates.ClearStringAffixes(controllerModel.ControllerName, affixes: _featureSettingsOptions.ControllerNameAffixes)
                       : controllerModel.ControllerName;
            }

            // 大小写
            tempName = _featureSettingsOptions.LowerCaseRoute ? tempName.ToLower() : tempName;

            // 设置控制器名称
            controllerModel.ControllerName = tempName + (string.IsNullOrEmpty(apiVersion) ? null : "@" + apiVersion);
        }
    }
}