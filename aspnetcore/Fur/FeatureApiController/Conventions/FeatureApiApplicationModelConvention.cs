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
    public sealed class FeatureApiApplicationModelConvention : IApplicationModelConvention
    {
        /// <summary>
        /// 特性配置选项
        /// </summary>
        private readonly FeatureApiSettingsOptions _featureApiSettings;

        /// <summary>
        /// 构造函数
        /// </summary>
        public FeatureApiApplicationModelConvention()
        {
            _featureApiSettings = App.GetOptions<FeatureApiSettingsOptions>();
        }

        /// <summary>
        /// 配置应用信息
        /// </summary>
        /// <param name="application">应用模型</param>
        public void Apply(ApplicationModel application)
        {
            foreach (var controllerModel in application.Controllers)
            {
                // 跳过 ControllerBase 子类配置
                if (typeof(ControllerBase).IsAssignableFrom(controllerModel.ControllerType)) continue;

                // 配置控制器
                ConfigureController(controllerModel);
            }
        }

        /// <summary>
        /// 配置控制器信息
        /// </summary>
        /// <param name="controllerModel"></param>
        private void ConfigureController(ControllerModel controllerModel)
        {
            var featureApiSettingsAttribute = controllerModel.Attributes.FirstOrDefault(u => u is FeatureApiSettingsAttribute) as FeatureApiSettingsAttribute;
            // 配置控制器可见性
            ConfigureControllerApiExplorer(controllerModel);
            // 配置控制器名称
            ConfigureControllerName(controllerModel, featureApiSettingsAttribute);
            // 配置控制器区域
            ConfigureControllerArea(controllerModel, featureApiSettingsAttribute);
            // 配置行为信息
            foreach (var actionModel in controllerModel.Actions)
            {
                ConfigureAction(controllerModel, actionModel, featureApiSettingsAttribute);

                if (_featureApiSettings.LowerCaseRoute) actionModel.ActionName = actionModel.ActionName.ToLower();
            }

            if (_featureApiSettings.LowerCaseRoute) controllerModel.ControllerName = controllerModel.ControllerName.ToLower();
        }

        /// <summary>
        /// 配置控制器可见性
        /// </summary>
        /// <param name="controllerModel">控制器模型</param>
        private void ConfigureControllerApiExplorer(ControllerModel controllerModel)
        {
            if (!controllerModel.ApiExplorer.IsVisible.HasValue) controllerModel.ApiExplorer.IsVisible = true;
        }

        /// <summary>
        /// 配置控制器名称
        /// </summary>
        /// <param name="controllerModel">控制器模型</param>
        /// <param name="featureApiSettings">特性接口配置</param>
        private void ConfigureControllerName(ControllerModel controllerModel, FeatureApiSettingsAttribute featureApiSettingsAttribute)
        {
            // 读取版本号
            var apiVersion = featureApiSettingsAttribute?.ApiVersion;

            // 解析控制器名称
            string tempName = featureApiSettingsAttribute?.Name;
            if (string.IsNullOrEmpty(tempName))
            {
                tempName = featureApiSettingsAttribute?.KeepName != true
                      // 移除前后缀
                      ? Penetrates.ClearStringAffixes(controllerModel.ControllerName, affixes: _featureApiSettings.ControllerNameAffixes)
                      : controllerModel.ControllerName;
            }

            controllerModel.ControllerName = tempName + (string.IsNullOrEmpty(apiVersion) ? null : "@" + apiVersion);
        }

        /// <summary>
        /// 配置控制器区域
        /// </summary>
        /// <param name="controllerModel">控制器模型</param>
        /// <param name="featureApiSettingsAttribute">特性接口配置</param>
        private void ConfigureControllerArea(ControllerModel controllerModel, FeatureApiSettingsAttribute featureApiSettingsAttribute)
        {
            var areaName = "area";
            if (controllerModel.RouteValues.ContainsKey(areaName)) return;

            string area = featureApiSettingsAttribute?.Module ?? _featureApiSettings.DefaultAreaName;
            if (!string.IsNullOrEmpty(area))
            {
                controllerModel.RouteValues[areaName] = area;
            }
            if (featureApiSettingsAttribute != null)
            {
                featureApiSettingsAttribute.Module = area;
            }
        }

        /// <summary>
        /// 配置行为信息
        /// </summary>
        /// <param name="controllerModel">控制器模型</param>
        /// <param name="actionModel">行为模型</param>
        /// <param name="controllerFeatureApiSettingsAttribute">控制器特性接口配置</param>
        private void ConfigureAction(ControllerModel controllerModel, ActionModel actionModel, FeatureApiSettingsAttribute controllerFeatureApiSettingsAttribute)
        {
            var featureApiSettingsAttribute = actionModel.Attributes.FirstOrDefault(u => u is FeatureApiSettingsAttribute) as FeatureApiSettingsAttribute;
            var actionNameWords = Penetrates.SplitToWords(actionModel.ActionName);
            var selectorModels = actionModel.Selectors;

            // 配置行为可见性
            ConfigureActionApiExplorer(actionModel);
            // 配置行为名称
            ConfigureActionName(actionModel, featureApiSettingsAttribute, controllerFeatureApiSettingsAttribute, actionNameWords);
            // 绑定参数
            foreach (var selectorModel in selectorModels)
            {
                ConfigureActionParameter(actionModel, selectorModel);
            }

            // 如果行为贴了[Route] 特性和 控制器贴了 [Route] 特性，以及 [HttpXXX("")]
            if (controllerModel.Selectors.Any(u => u.AttributeRouteModel != null) && actionModel.Selectors[0].ActionConstraints.Count == 0 && actionModel.Selectors[0].AttributeRouteModel == null)
            {
                CombineControllerRouteAttribute(controllerModel, actionModel, controllerFeatureApiSettingsAttribute, featureApiSettingsAttribute, actionNameWords);
                return;
            }

            if (selectorModels.Count == 0 || selectorModels.Any(a => a.ActionConstraints.Count == 0))
            {
                var selectorModel = actionModel.Selectors[0];
                if (selectorModel.AttributeRouteModel == null)
                {
                    // 绑定路由
                    selectorModel.AttributeRouteModel = ConfigureCompleteRoute(controllerModel, actionModel, controllerFeatureApiSettingsAttribute, featureApiSettingsAttribute, actionNameWords);
                }

                // 绑定请求谓词
                ConfigureActionHttpMethod(selectorModel, actionNameWords);
            }
            else
            {
                foreach (var selectorModel in selectorModels)
                {
                    selectorModel.AttributeRouteModel ??= ConfigureCompleteRoute(controllerModel, actionModel, controllerFeatureApiSettingsAttribute, featureApiSettingsAttribute, actionNameWords);
                }
            }
        }

        /// <summary>
        /// 配置行为可见性
        /// </summary>
        /// <param name="actionModel">行为模型</param>
        private void ConfigureActionApiExplorer(ActionModel actionModel)
        {
            if (!actionModel.ApiExplorer.IsVisible.HasValue) actionModel.ApiExplorer.IsVisible = true;
        }

        /// <summary>
        /// 配置行为名称
        /// </summary>
        /// <param name="actionModel">行为模型</param>
        /// <param name="featureApiSettingsAttribute">特性接口配置</param>
        /// <param name="controllerFeatureApiSettingsAttribute">控制器特性接口配置</param>
        /// <param name="actionNameWords">行为名称中每一个单词</param>
        private void ConfigureActionName(ActionModel actionModel, FeatureApiSettingsAttribute featureApiSettingsAttribute, FeatureApiSettingsAttribute controllerFeatureApiSettingsAttribute, string[] actionNameWords)
        {
            // 读取版本号
            var apiVersion = featureApiSettingsAttribute?.ApiVersion;

            // 解析行为名称
            string tempName = featureApiSettingsAttribute?.Name;
            if (string.IsNullOrEmpty(tempName))
            {
                if (featureApiSettingsAttribute?.KeepName != true)
                {
                    // 移除前后缀
                    tempName = Penetrates.ClearStringAffixes(actionModel.ActionName, affixes: _featureApiSettings.ActionNameAffixes);

                    // 移除请求谓词
                    if (featureApiSettingsAttribute?.KeepVerb != true || (featureApiSettingsAttribute?.KeepVerb == null && controllerFeatureApiSettingsAttribute?.KeepVerb != true) || (featureApiSettingsAttribute?.KeepVerb == null && controllerFeatureApiSettingsAttribute?.KeepVerb == null && !_featureApiSettings.KeepVerb))
                    {
                        var verbKey = actionNameWords[0].ToLower();
                        if (Penetrates.HttpVerbSetters.ContainsKey(verbKey.ToLower())) tempName = tempName[verbKey.Length..];
                    }
                }
                else tempName = actionModel.ActionName;
            }

            actionModel.ActionName = tempName + (string.IsNullOrEmpty(apiVersion) ? null : "@" + apiVersion);
        }

        /// <summary>
        /// 配置行为请求谓词
        /// </summary>
        /// <param name="selectorModel">选择器模型</param>
        /// <param name="actionNameWords">行为名称中每一个单词</param>
        private void ConfigureActionHttpMethod(SelectorModel selectorModel, string[] actionNameWords)
        {
            if (selectorModel.ActionConstraints.Any()) return;

            var verbKey = actionNameWords[0].ToLower();
            var verb = Penetrates.HttpVerbSetters.ContainsKey(verbKey)
                ? Penetrates.HttpVerbSetters[verbKey]
                : _featureApiSettings.DefaultHttpMethod.ToUpper();

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
        /// 合并控制器路由
        /// </summary>
        /// <param name="controllerModel">控制器模型</param>
        /// <param name="actionModel">行为模型</param>
        /// <param name="controllerFeatureApiSettingsAttribute">控制器特性接口配置</param>
        /// <param name="featureApiSettingsAttribute">特性接口配置</param>
        /// <param name="actionNameWords">行为名称中每一个单词</param>
        private void CombineControllerRouteAttribute(ControllerModel controllerModel, ActionModel actionModel, FeatureApiSettingsAttribute controllerFeatureApiSettingsAttribute, FeatureApiSettingsAttribute featureApiSettingsAttribute, string[] actionNameWords)
        {
            var (controllerStartParameters, controllerEndParameters, actionTemplate) = GenerateActoinRouteTemplate(actionModel, featureApiSettingsAttribute, actionNameWords);

            foreach (var controllerSelectorModel in controllerModel.Selectors)
            {
                var controllerAttributeRouteModel = controllerSelectorModel.AttributeRouteModel;
                // 处理控制器模板占位符
                if (controllerAttributeRouteModel.Template.Contains("[controller]"))
                {
                    var controllerName = controllerFeatureApiSettingsAttribute?.SplitName == true
                        ? string.Join('/', Penetrates.SplitToWords(controllerModel.ControllerName))
                        : controllerModel.ControllerName;

                    // 追加模块，控制器参数
                    var controllerTemplate = $"{controllerFeatureApiSettingsAttribute?.Module}/{string.Join("/", controllerStartParameters)}/{controllerName}/{string.Join("/", controllerEndParameters)}";
                    controllerTemplate = Regex.Replace(controllerTemplate, @"\/{2,}", "/");
                    controllerTemplate = Penetrates.ClearStringAffixes(controllerTemplate, 1, "/");

                    var template = controllerAttributeRouteModel.Template.Replace("[controller]", controllerTemplate);
                    template = Regex.Replace(template, @"\/{2,}", "/");

                    controllerAttributeRouteModel.Template = _featureApiSettings.LowerCaseRoute ? template.ToLower() : template;
                }

                foreach (var actionSelectorModel in actionModel.Selectors)
                {
                    actionTemplate = Regex.Replace(actionTemplate, @"\/{2,}", "/");
                    actionTemplate = Penetrates.ClearStringAffixes(actionTemplate, 0, "/");
                    var completeRouteTemplate = $"{controllerAttributeRouteModel.Template}/{actionTemplate}";
                    completeRouteTemplate = Regex.Replace(completeRouteTemplate, @"\/{2,}", "/");
                    completeRouteTemplate = _featureApiSettings.LowerCaseRoute ? completeRouteTemplate.ToLower() : completeRouteTemplate;
                    actionSelectorModel.AttributeRouteModel = new AttributeRouteModel(new RouteAttribute(completeRouteTemplate));

                    ConfigureActionHttpMethod(actionSelectorModel, actionNameWords);
                }
            }
        }

        /// <summary>
        /// 生成完整路由模板
        /// </summary>
        /// <param name="controllerModel">控制器模型</param>
        /// <param name="actionModel">行为模型</param>
        /// <param name="controllerFeatureApiSettingsAttribute">控制器特性接口配置</param>
        /// <param name="featureApiSettingsAttribute">特性接口配置</param>
        /// <param name="actionNameWords">行为名称中每一个单词</param>
        /// <returns></returns>
        private AttributeRouteModel ConfigureCompleteRoute(ControllerModel controllerModel, ActionModel actionModel, FeatureApiSettingsAttribute controllerFeatureApiSettingsAttribute, FeatureApiSettingsAttribute featureApiSettingsAttribute, string[] actionNameWords)
        {
            // 路由前缀
            var routePrefix = _featureApiSettings.DefaultRoutePrefix;

            // 控制器名称
            var controllerName = controllerFeatureApiSettingsAttribute?.SplitName == true
                ? string.Join('/', Penetrates.SplitToWords(controllerModel.ControllerName))
                : controllerModel.ControllerName;

            // 行为完整路由
            var (controllerStartParameters, controllerEndParameters, actionRoute) = GenerateActoinRouteTemplate(actionModel, featureApiSettingsAttribute, actionNameWords);
            var route = $"/{routePrefix}/{controllerFeatureApiSettingsAttribute?.Module}/{string.Join("/", controllerStartParameters)}/{controllerName}/{string.Join("/", controllerEndParameters)}/{featureApiSettingsAttribute?.Module}/{actionRoute}";
            route = _featureApiSettings.LowerCaseRoute ? route.ToLower() : route;

            return new AttributeRouteModel(new RouteAttribute(Regex.Replace(route, @"\/{2,}", "/"))); ;
        }

        /// <summary>
        /// 转换路由占位符
        /// </summary>
        /// <param name="controllerModel">控制器模型</param>
        /// <param name="actionModel">行为模型</param>
        /// <param name="controllerFeatureApiSettingsAttribute">控制器特性接口配置</param>
        /// <param name="featureApiSettingsAttribute">特性接口配置</param>
        /// <param name="actionNameWords">行为名称中每一个单词</param>
        /// <returns></returns>
        private (string, string) ConvertPlaceholder(ControllerModel controllerModel, ActionModel actionModel, FeatureApiSettingsAttribute controllerFeatureApiSettingsAttribute, FeatureApiSettingsAttribute featureApiSettingsAttribute, string[] actionNameWords)
        {
            var (controllerStartParameters, controllerEndParameters, actionTemplate) = GenerateActoinRouteTemplate(actionModel, featureApiSettingsAttribute, actionNameWords);

            var controllerName = controllerFeatureApiSettingsAttribute?.SplitName == true
                    ? string.Join('/', Penetrates.SplitToWords(controllerModel.ControllerName))
                    : controllerModel.ControllerName;

            // 追加模块，控制器参数
            var controllerTemplate = $"{controllerFeatureApiSettingsAttribute?.Module}/{string.Join("/", controllerStartParameters)}/{controllerName}/{string.Join("/", controllerEndParameters)}";
            controllerTemplate = Regex.Replace(controllerTemplate, @"\/{2,}", "/");
            controllerTemplate = Penetrates.ClearStringAffixes(controllerTemplate, 1, "/");

            actionTemplate = Regex.Replace(actionTemplate, @"\/{2,}", "/");
            actionTemplate = Penetrates.ClearStringAffixes(actionTemplate, 0, "/");

            return (controllerTemplate, actionTemplate);
        }

        /// <summary>
        /// 生成行为路由模板
        /// </summary>
        /// <param name="actionModel">行为模型</param>
        /// <param name="featureApiSettingsAttribute">特性接口配置</param>
        /// <param name="actionNameWords">行为名称中每一个单词</param>
        /// <returns></returns>
        private (string[], string[], string) GenerateActoinRouteTemplate(ActionModel actionModel, FeatureApiSettingsAttribute featureApiSettingsAttribute, string[] actionNameWords)
        {
            // 解析行为名称
            var actionName = featureApiSettingsAttribute?.SplitName == true
                ? string.Join('/', Penetrates.SplitToWords(actionModel.ActionName))
                : actionModel.ActionName;

            // 解析参数路由
            var (controllerStartParameters, controllerEndParameters, actionStartParameters, actionEndParameters) = ResolveParameterRoute(actionModel);
            var template = $"{featureApiSettingsAttribute?.Module}/{string.Join("/", actionStartParameters)}/{actionName}/{string.Join("/", actionEndParameters)}";
            return (controllerStartParameters, controllerEndParameters, template);
        }

        /// <summary>
        /// 解析参数路由信息
        /// </summary>
        /// <param name="actionModel">行为模型</param>
        /// <returns></returns>
        private (string[], string[], string[], string[]) ResolveParameterRoute(ActionModel actionModel)
        {
            // 控制器前后参数
            var controllerStartParameters = new List<string>();
            var controllerEndParameters = new List<string>();

            // 行为前后参数
            var actionStartParameters = new List<string>();
            var actionEndParameters = new List<string>();

            foreach (var parameterModel in actionModel.Parameters)
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
                    if (!(parameterAttributes.FirstOrDefault(u => u is SeatAttribute) is SeatAttribute seat)) actionEndParameters.Add(parameterName);
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

            return (controllerStartParameters.ToArray(), controllerEndParameters.ToArray(), actionStartParameters.ToArray(), actionEndParameters.ToArray());
        }

        /// <summary>
        /// 配置行为参数
        /// </summary>
        /// <param name="actionModel"></param>
        /// <param name="selectorModel"></param>
        private void ConfigureActionParameter(ActionModel actionModel, SelectorModel selectorModel)
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
    }
}