using Fur.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Fur.FeatureApiController
{
    /// <summary>
    /// 自定义应用模型转换器
    /// </summary>
    public sealed class FeatureApiApplicationModelConvention2 : IApplicationModelConvention
    {
        /// <summary>
        /// 特性配置选项
        /// </summary>
        private readonly FeatureApiSettingsOptions _featureApiSettings;

        /// <summary>
        /// 构造函数
        /// </summary>
        public FeatureApiApplicationModelConvention2()
        {
            _featureApiSettings = App.GetOptions<FeatureApiSettingsOptions>();
        }

        public void Apply(ApplicationModel application)
        {
            foreach (var controllerModel in application.Controllers)
            {
                // 跳过 ControllerBase 子类配置
                if (typeof(ControllerBase).IsAssignableFrom(controllerModel.ControllerType)) continue;

                // 配置控制器
                var featureApiSettingsAttribute = controllerModel.Attributes.FirstOrDefault(u => u is FeatureApiSettingsAttribute) as FeatureApiSettingsAttribute;
                ConfigureControllerModel(controllerModel, featureApiSettingsAttribute);
            }
        }

        /// <summary>
        /// 配置控制器模型
        /// </summary>
        /// <param name="controllerModel"></param>
        private void ConfigureControllerModel(ControllerModel controllerModel, FeatureApiSettingsAttribute featureApiSettingsAttribute)
        {
            // 配置控制器可见性
            if (!controllerModel.ApiExplorer.IsVisible.HasValue) controllerModel.ApiExplorer.IsVisible = true;

            // 配置控制器名称
            ConfigureControllerName(controllerModel, featureApiSettingsAttribute);

            // 配置行为
            ConfigureActionModels(controllerModel, featureApiSettingsAttribute);
        }

        /// <summary>
        /// 配置控制器名称
        /// </summary>
        /// <param name="controllerModel"></param>
        /// <param name="featureApiSettingsAttribute"></param>
        private void ConfigureControllerName(ControllerModel controllerModel, FeatureApiSettingsAttribute featureApiSettingsAttribute)
        {
            // 读取版本号
            var apiVersion = featureApiSettingsAttribute?.ApiVersion;

            // 解析控制器名称
            string tempName;
            if (!string.IsNullOrEmpty(featureApiSettingsAttribute?.Name)) tempName = featureApiSettingsAttribute.Name;
            else
            {
                tempName = featureApiSettingsAttribute?.KeepName != true
                       ? Penetrates.ClearStringAffixes(controllerModel.ControllerName, affixes: _featureApiSettings.ControllerNameAffixes)
                       : controllerModel.ControllerName;
            }

            // 大小写
            tempName = _featureApiSettings.LowerCaseRoute ? tempName.ToLower() : tempName;

            // 切割名称

            // 设置控制器名称
            controllerModel.ControllerName = tempName + (string.IsNullOrEmpty(apiVersion) ? null : "@" + apiVersion);
        }

        /// <summary>
        /// 配置行为模型
        /// </summary>
        /// <param name="controllerModel"></param>
        private void ConfigureActionModels(ControllerModel controllerModel, FeatureApiSettingsAttribute controllerFeatureApiSettingsAttribute)
        {
            var actionModels = controllerModel.Actions;

            foreach (var actionModel in actionModels)
            {
                // 配置行为可见性
                if (!actionModel.ApiExplorer.IsVisible.HasValue) actionModel.ApiExplorer.IsVisible = true;

                var selectorModels = actionModel.Selectors;
                foreach (var selectorModel in selectorModels)
                {
                    // 绑定引用类型参数
                    ConfigureParameterModels(actionModel, selectorModel);
                }

                // 这里需要验证，如果行为贴了[Route] 特性和 控制器贴了 [Route] 特性，以及 [HttpXXX("")]
                if (controllerModel.Selectors.Any(u => u.AttributeRouteModel != null)) continue;

                // 配置路由和请求谓词
                ConfigureRouteAndVerb(controllerModel, actionModel, controllerFeatureApiSettingsAttribute, selectorModels);
            }
        }

        /// <summary>
        /// 配置路由和请求谓词
        /// </summary>
        /// <param name="controllerModel"></param>
        /// <param name="actionModel"></param>
        /// <param name="controllerFeatureApiSettingsAttribute"></param>
        /// <param name="selectorModels"></param>
        private void ConfigureRouteAndVerb(ControllerModel controllerModel, ActionModel actionModel, FeatureApiSettingsAttribute controllerFeatureApiSettingsAttribute, IList<SelectorModel> selectorModels)
        {
            var featureApiSettingsAttribute = actionModel.Attributes.FirstOrDefault(u => u is FeatureApiSettingsAttribute) as FeatureApiSettingsAttribute;

            var actionNames = Penetrates.SplitToWords(actionModel.ActionName);
            if (selectorModels.Count == 0 || selectorModels.Any(a => a.ActionConstraints.Count == 0))
            {
                var selectorModel = actionModel.Selectors[0];
                if (selectorModel.AttributeRouteModel == null)
                {
                    selectorModel.AttributeRouteModel = ConfigureRouteModel(controllerModel, actionModel, controllerFeatureApiSettingsAttribute, featureApiSettingsAttribute, actionNames);
                }

                ConfigureHttpMethod(selectorModel, actionNames);
            }
            else
            {
                foreach (var selectorModel in selectorModels)
                {
                    var attributeRouteModel = ConfigureRouteModel(controllerModel, actionModel, controllerFeatureApiSettingsAttribute, featureApiSettingsAttribute, actionNames);
                    selectorModel.AttributeRouteModel = selectorModel.AttributeRouteModel == null
                        ? attributeRouteModel
                        : AttributeRouteModel.CombineAttributeRouteModel(attributeRouteModel, selectorModel.AttributeRouteModel);
                }
            }
        }

        /// <summary>
        /// 绑定引用类型参数
        /// </summary>
        /// <param name="actionModel"></param>
        private void ConfigureParameterModels(ActionModel actionModel, SelectorModel selectorModel)
        {
            var parameterModels = actionModel.Parameters;
            foreach (var parameterModel in parameterModels)
            {
                // 如果参数已提供绑定方式，则跳过
                if (parameterModel.BindingInfo != null) continue;

                var parameterType = parameterModel.ParameterType;

                // 如果是基元类型，则跳过
                if (parameterType.IsRichPrimitive()) continue;

                // 如果是文件类型，则跳过
                if (typeof(IFormFile).IsAssignableFrom(parameterType) || typeof(IFormFileCollection).IsAssignableFrom(parameterType)) continue;

                // 如果不能被绑定，则跳过
                if (!CanBindingFromBody(selectorModel)) continue;

                parameterModel.BindingInfo = BindingInfo.GetBindingInfo(new[] { new FromBodyAttribute() });
            }
        }

        /// <summary>
        /// 检查参数是否能够通过请求报文体绑定
        /// </summary>
        /// <param name="selectorModel"></param>
        /// <returns></returns>
        private bool CanBindingFromBody(SelectorModel selectorModel)
        {
            var actionConstraints = selectorModel.ActionConstraints;
            if (actionConstraints == null || actionConstraints.Count == 0) return false;

            foreach (var actionConstraint in actionConstraints)
            {
                // 如果不是 HttpMethod约束，则跳过
                if (actionConstraint is not HttpMethodActionConstraint httpMethodActionConstraint) continue;

                if (httpMethodActionConstraint.HttpMethods.All(u => Penetrates.HttpMethodOfCanNotBindFromBody.Contains(u))) return false;
            }

            return true;
        }

        /// <summary>
        /// 配置Http请求
        /// </summary>
        /// <param name="actionModel"></param>
        /// <param name="selectorModel"></param>
        private void ConfigureHttpMethod(SelectorModel selectorModel, string[] actionNames)
        {
            if (selectorModel.ActionConstraints.Any()) return;

            var verbKey = actionNames.First().ToLower();
            var verb = Penetrates.HttpVerbSetters.ContainsKey(verbKey) ? Penetrates.HttpVerbSetters[verbKey] : _featureApiSettings.DefaultHttpMethod.ToUpper();
            selectorModel.ActionConstraints.Add(new HttpMethodActionConstraint(new[] { verb }));

            switch (verb)
            {
                case "GET":
                    selectorModel.EndpointMetadata.Add(new HttpGetAttribute());
                    break;

                case "POST":
                    selectorModel.EndpointMetadata.Add(new HttpPostAttribute());
                    break;

                case "PUT":
                    selectorModel.EndpointMetadata.Add(new HttpPutAttribute());
                    break;

                case "DELETE":
                    selectorModel.EndpointMetadata.Add(new HttpDeleteAttribute());
                    break;

                default: throw new System.Exception($"Unsupported HttpVerb: {verb}");
            }
        }

        /// <summary>
        /// 配置路由模型
        /// </summary>
        /// <param name="controllerModel"></param>
        /// <param name="actionModel"></param>
        /// <returns></returns>
        private AttributeRouteModel ConfigureRouteModel(ControllerModel controllerModel, ActionModel actionModel, FeatureApiSettingsAttribute controllerFeatureApiSettingsAttribute, FeatureApiSettingsAttribute featureApiSettingsAttribute, string[] actionNames)
        {
            // 路由前缀
            var routePrefix = _featureApiSettings.DefaultRoutePrefix;

            // 控制器名称
            var controllerName = controllerModel.ControllerName;

            // 行为名称
            var actionName = ResolveActionName(actionModel, controllerFeatureApiSettingsAttribute, featureApiSettingsAttribute, actionNames);

            // 控制器前后参数
            var controllerStartParameters = new List<string>();
            var controllerEndParameters = new List<string>();

            // 行为前后参数
            var actionStartParameters = new List<string>();
            var actionEndParameters = new List<string>();

            var parameterModels = actionModel.Parameters;

            // 生成路由参数
            foreach (var parameterModel in parameterModels)
            {
                var parameterType = parameterModel.ParameterType;

                // 处理小写路由参数问题
                if (_featureApiSettings.LowerCaseRoute) parameterModel.ParameterName = parameterModel.ParameterName.ToLower();

                // 只有贴了 [FromRoute] 特性，或者没有贴任何 [FromXXXX] 特性的基元类型
                var parameterAttributes = parameterModel.Attributes;
                if (parameterAttributes.Any(u => u is FromRouteAttribute) || (parameterType.IsRichPrimitive() && !parameterAttributes.Any(u => typeof(IBindingSourceMetadata).IsAssignableFrom(u.GetType()))))
                {
                    var parameterName = $"{{{parameterModel.ParameterName}}}";

                    // 解析参数位置
                    if (!(parameterAttributes.FirstOrDefault(u => u is SeatAttribute) is SeatAttribute seat))
                    {
                        actionEndParameters.Add(parameterName);
                    }
                    else
                    {
                        switch (seat.Seat)
                        {
                            case Seat.ControllerStart:
                                controllerStartParameters.Add(parameterName);
                                break;

                            case Seat.ControllerEnd:
                                controllerEndParameters.Add(parameterName);
                                break;

                            case Seat.ActionStart:
                                actionStartParameters.Add(parameterName);
                                break;

                            case Seat.ActionEnd:
                                actionEndParameters.Add(parameterName);
                                break;
                        }
                    }
                }
            }

            var route = $"/{routePrefix}/{controllerFeatureApiSettingsAttribute?.Module}/{string.Join("/", controllerStartParameters)}/{controllerName}/{string.Join("/", controllerEndParameters)}/{featureApiSettingsAttribute?.Module}/{string.Join("/", actionStartParameters)}/{actionName}/{string.Join("/", actionEndParameters)}";

            return new AttributeRouteModel(new RouteAttribute(Regex.Replace(route, @"\/{2,}", "/")));
        }

        /// <summary>
        /// 解析行为名称
        /// </summary>
        /// <param name="actionModel"></param>
        /// <param name="featureApiSettingsAttribute"></param>
        /// <returns></returns>
        private string ResolveActionName(ActionModel actionModel, FeatureApiSettingsAttribute controllerFeatureApiSettingsAttribute, FeatureApiSettingsAttribute featureApiSettingsAttribute, string[] actionNames)
        {
            // 读取版本号
            var apiVersion = featureApiSettingsAttribute?.ApiVersion;

            // 解析行为名称
            string tempName;
            if (!string.IsNullOrEmpty(featureApiSettingsAttribute?.Name)) tempName = featureApiSettingsAttribute.Name;
            else
            {
                if (!(featureApiSettingsAttribute?.KeepName ?? false))
                {
                    // 移除前后缀
                    tempName = Penetrates.ClearStringAffixes(actionModel.ActionName, affixes: _featureApiSettings.ActionNameAffixes);

                    // 移除行为动词
                    if (featureApiSettingsAttribute?.KeepVerb != true || (featureApiSettingsAttribute?.KeepVerb == null && controllerFeatureApiSettingsAttribute?.KeepVerb != true) || (featureApiSettingsAttribute?.KeepVerb == null && controllerFeatureApiSettingsAttribute?.KeepVerb == null && !_featureApiSettings.KeepVerb))
                    {
                        var verbKey = actionNames.First().ToLower();
                        if (Penetrates.HttpVerbSetters.ContainsKey(verbKey.ToLower())) tempName = tempName[verbKey.Length..];
                    }
                }
                else tempName = actionModel.ActionName;
            }

            //// 切割名称
            //if (featureApiSettingsAttribute?.SplitName == true)
            //{
            //    tempName = string.Join('/', Penetrates.SplitToWords(tempName));
            //}

            // 大小写
            tempName = _featureApiSettings.LowerCaseRoute ? tempName.ToLower() : tempName;

            return tempName + (string.IsNullOrEmpty(apiVersion) ? null : "@" + apiVersion);
        }
    }
}