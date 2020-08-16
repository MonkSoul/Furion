using Fur.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Fur.LazyController
{
    public sealed class LazyControllerApplicationModelConvention : IApplicationModelConvention
    {
        /// <summary>
        /// 接口描述配置选项
        /// </summary>
        private readonly LazyControllerSettingsOptions _lazyControllerSettings;

        /// <summary>
        /// 构造函数
        /// </summary>
        public LazyControllerApplicationModelConvention()
        {
            _lazyControllerSettings = App.GetOptions<LazyControllerSettingsOptions>();
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
                //ConfigureController(controllerModel);
            }
        }

        /// <summary>
        /// 配置控制器信息
        /// </summary>
        /// <param name="controllerModel"></param>
        private void ConfigureController(ControllerModel controllerModel)
        {
            var apiDescriptionSettings = controllerModel.Attributes.FirstOrDefault(u => u is ApiDescriptionSettingsAttribute) as ApiDescriptionSettingsAttribute;
            // 配置控制器可见性
            ConfigureControllerApiExplorer(controllerModel);
            // 配置控制器名称
            ConfigureControllerName(controllerModel, apiDescriptionSettings);
            // 配置控制器区域
            ConfigureControllerArea(controllerModel, apiDescriptionSettings);
            // 配置行为信息
            foreach (var actionModel in controllerModel.Actions)
            {
                ConfigureAction(controllerModel, actionModel, apiDescriptionSettings);

                if (_lazyControllerSettings.LowerCaseRoute) actionModel.ActionName = actionModel.ActionName.ToLower();
            }

            if (_lazyControllerSettings.LowerCaseRoute) controllerModel.ControllerName = controllerModel.ControllerName.ToLower();
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
        /// <param name="apiDescriptionSettings">接口描述配置</param>
        private void ConfigureControllerName(ControllerModel controllerModel, ApiDescriptionSettingsAttribute apiDescriptionSettings)
        {
            // 读取版本号
            var apiVersion = apiDescriptionSettings?.Version;

            // 解析控制器名称
            string tempName = apiDescriptionSettings?.Name;
            if (string.IsNullOrEmpty(tempName))
            {
                tempName = apiDescriptionSettings?.KeepName != true
                      // 移除前后缀
                      ? Penetrates.ClearStringAffixes(controllerModel.ControllerName, affixes: _lazyControllerSettings.AbandonControllerAffixes)
                      : controllerModel.ControllerName;
            }

            controllerModel.ControllerName = tempName + (string.IsNullOrEmpty(apiVersion) ? null : "@" + apiVersion);
        }

        /// <summary>
        /// 配置控制器区域
        /// </summary>
        /// <param name="controllerModel">控制器模型</param>
        /// <param name="apiDescriptionSettings">接口描述配置</param>
        private void ConfigureControllerArea(ControllerModel controllerModel, ApiDescriptionSettingsAttribute apiDescriptionSettings)
        {
            var areaName = "area";
            if (controllerModel.RouteValues.ContainsKey(areaName)) return;

            string area = apiDescriptionSettings?.Module ?? _lazyControllerSettings.DefaultAreaName;
            if (!string.IsNullOrEmpty(area))
            {
                controllerModel.RouteValues[areaName] = area;
            }
            if (apiDescriptionSettings != null)
            {
                apiDescriptionSettings.Module = area;
            }
        }

        /// <summary>
        /// 配置行为信息
        /// </summary>
        /// <param name="controllerModel">控制器模型</param>
        /// <param name="actionModel">行为模型</param>
        /// <param name="controllerApiDescriptionSettings">控制器接口描述配置</param>
        private void ConfigureAction(ControllerModel controllerModel, ActionModel actionModel, ApiDescriptionSettingsAttribute controllerApiDescriptionSettings)
        {
            var apiDescriptionSettings = actionModel.Attributes.FirstOrDefault(u => u is ApiDescriptionSettingsAttribute) as ApiDescriptionSettingsAttribute;
            var actionNameWords = Penetrates.SplitToWords(actionModel.ActionName);
            var selectorModels = actionModel.Selectors;

            // 配置行为可见性
            ConfigureActionApiExplorer(actionModel);
            // 配置行为名称
            ConfigureActionName(actionModel, apiDescriptionSettings, controllerApiDescriptionSettings, actionNameWords);
            // 绑定参数
            foreach (var selectorModel in selectorModels)
            {
                ConfigureActionParameter(actionModel, selectorModel);
            }

            // 如果行为贴了[Route] 特性和 控制器贴了 [Route] 特性，以及 [HttpXXX("")]
            if (controllerModel.Selectors.Any(u => u.AttributeRouteModel != null) /*&& actionModel.Selectors[0].ActionConstraints.Count == 0 && actionModel.Selectors[0].AttributeRouteModel == null*/)
            {
                //CombineControllerRouteAttribute(controllerModel, actionModel, controllerApiDescriptionSettings, apiDescriptionSettings, actionNameWords);
                return;
            }

            if (selectorModels.Count == 0 || selectorModels.Any(a => a.ActionConstraints.Count == 0))
            {
                foreach (var selectorModel in actionModel.Selectors)
                {
                    if (selectorModel.AttributeRouteModel == null)
                    {
                        // 绑定路由
                        selectorModel.AttributeRouteModel = ConfigureCompleteRoute(controllerModel, actionModel, controllerApiDescriptionSettings, apiDescriptionSettings, actionNameWords);
                    }
                }
            }
            else
            {
                foreach (var selectorModel in selectorModels)
                {
                    //selectorModel.AttributeRouteModel ??= ConfigureCompleteRoute(controllerModel, actionModel, controllerApiDescriptionSettings, apiDescriptionSettings, actionNameWords);
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
        /// <param name="apiDescriptionSettings">接口描述配置</param>
        /// <param name="controllerApiDescriptionSettings">控制器接口描述配置</param>
        /// <param name="actionNameWords">行为名称中每一个单词</param>
        private void ConfigureActionName(ActionModel actionModel, ApiDescriptionSettingsAttribute apiDescriptionSettings, ApiDescriptionSettingsAttribute controllerApiDescriptionSettings, string[] actionNameWords)
        {
            // 读取版本号
            var apiVersion = apiDescriptionSettings?.Version;

            // 解析行为名称
            string tempName = apiDescriptionSettings?.Name;
            if (string.IsNullOrEmpty(tempName))
            {
                if (apiDescriptionSettings?.KeepName != true)
                {
                    // 移除前后缀
                    tempName = Penetrates.ClearStringAffixes(actionModel.ActionName, affixes: _lazyControllerSettings.AbandonActionAffixes);

                    // 移除请求谓词
                    if (apiDescriptionSettings?.KeepVerb != true || (apiDescriptionSettings?.KeepVerb == null && controllerApiDescriptionSettings?.KeepVerb != true) || (apiDescriptionSettings?.KeepVerb == null && controllerApiDescriptionSettings?.KeepVerb == null && !_lazyControllerSettings.KeepVerb))
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
                : _lazyControllerSettings.DefaultHttpMethod.ToUpper();

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
        /// <param name="controllerApiDescriptionSettings">控制器接口描述配置</param>
        /// <param name="apiDescriptionSettings">接口描述配置</param>
        /// <param name="actionNameWords">行为名称中每一个单词</param>
        private void CombineControllerRouteAttribute(ControllerModel controllerModel, ActionModel actionModel, ApiDescriptionSettingsAttribute controllerApiDescriptionSettings, ApiDescriptionSettingsAttribute apiDescriptionSettings, string[] actionNameWords)
        {
            var (controllerStartParameters, controllerEndParameters, actionTemplate) = GenerateActoinRouteTemplate(actionModel, apiDescriptionSettings, actionNameWords);

            foreach (var controllerSelectorModel in controllerModel.Selectors)
            {
                var controllerAttributeRouteModel = controllerSelectorModel.AttributeRouteModel;
                //// 处理控制器模板占位符
                //if (controllerAttributeRouteModel.Template.Contains("[controller]"))
                //{
                //    var controllerName = controllerApiDescriptionSettings?.SplitName == true
                //        ? string.Join('/', Penetrates.SplitToWords(controllerModel.ControllerName))
                //        : controllerModel.ControllerName;

                //    // 追加模块，控制器参数
                //    var controllerTemplate = $"{controllerApiDescriptionSettings?.Module}/{string.Join("/", controllerStartParameters)}/{controllerName}/{string.Join("/", controllerEndParameters)}";
                //    controllerTemplate = Regex.Replace(controllerTemplate, @"\/{2,}", "/");
                //    controllerTemplate = Penetrates.ClearStringAffixes(controllerTemplate, 1, "/");

                //    var template = controllerAttributeRouteModel.Template.Replace("[controller]", controllerTemplate);
                //    template = Regex.Replace(template, @"\/{2,}", "/");

                //    controllerAttributeRouteModel.Template = _lazyControllerSettings.LowerCaseRoute ? template.ToLower() : template;
                //}

                foreach (var actionSelectorModel in actionModel.Selectors)
                {
                    //actionTemplate = Regex.Replace(actionTemplate, @"\/{2,}", "/");
                    //actionTemplate = Penetrates.ClearStringAffixes(actionTemplate, 0, "/");
                    //var completeRouteTemplate = $"{controllerAttributeRouteModel.Template}/{actionTemplate}";
                    //completeRouteTemplate = Regex.Replace(completeRouteTemplate, @"\/{2,}", "/");
                    //completeRouteTemplate = _lazyControllerSettings.LowerCaseRoute ? completeRouteTemplate.ToLower() : completeRouteTemplate;
                    //actionSelectorModel.AttributeRouteModel = new AttributeRouteModel(new RouteAttribute(completeRouteTemplate));

                    ConfigureActionHttpMethod(actionSelectorModel, actionNameWords);
                }
            }
        }

        /// <summary>
        /// 生成完整路由模板
        /// </summary>
        /// <param name="controllerModel">控制器模型</param>
        /// <param name="actionModel">行为模型</param>
        /// <param name="controllerApiDescriptionSettings">控制器接口描述配置</param>
        /// <param name="apiDescriptionSettings">接口描述配置</param>
        /// <param name="actionNameWords">行为名称中每一个单词</param>
        /// <returns></returns>
        private AttributeRouteModel ConfigureCompleteRoute(ControllerModel controllerModel, ActionModel actionModel, ApiDescriptionSettingsAttribute controllerApiDescriptionSettings, ApiDescriptionSettingsAttribute apiDescriptionSettings, string[] actionNameWords)
        {
            // 路由前缀
            var routePrefix = _lazyControllerSettings.DefaultRoutePrefix;

            // 控制器名称
            var controllerName = controllerApiDescriptionSettings?.SplitCamelCase == true
                ? string.Join('/', Penetrates.SplitToWords(controllerModel.ControllerName))
                : controllerModel.ControllerName;

            // 行为完整路由
            var (controllerStartParameters, controllerEndParameters, actionRoute) = GenerateActoinRouteTemplate(actionModel, apiDescriptionSettings, actionNameWords);
            var route = $"/{routePrefix}/{controllerApiDescriptionSettings?.Module}/{string.Join("/", controllerStartParameters)}/{controllerName}/{string.Join("/", controllerEndParameters)}/{apiDescriptionSettings?.Module}/{actionRoute}";
            route = _lazyControllerSettings.LowerCaseRoute ? route.ToLower() : route;

            return new AttributeRouteModel(new RouteAttribute(Regex.Replace(route, @"\/{2,}", "/"))); ;
        }

        /// <summary>
        /// 转换路由占位符
        /// </summary>
        /// <param name="controllerModel">控制器模型</param>
        /// <param name="actionModel">行为模型</param>
        /// <param name="controllerApiDescriptionSettings">控制器接口描述配置</param>
        /// <param name="apiDescriptionSettings">接口描述配置</param>
        /// <param name="actionNameWords">行为名称中每一个单词</param>
        /// <returns></returns>
        private (string, string) ConvertPlaceholder(ControllerModel controllerModel, ActionModel actionModel, ApiDescriptionSettingsAttribute controllerApiDescriptionSettings, ApiDescriptionSettingsAttribute apiDescriptionSettings, string[] actionNameWords)
        {
            var (controllerStartParameters, controllerEndParameters, actionTemplate) = GenerateActoinRouteTemplate(actionModel, apiDescriptionSettings, actionNameWords);

            var controllerName = controllerApiDescriptionSettings?.SplitCamelCase == true
                    ? string.Join('/', Penetrates.SplitToWords(controllerModel.ControllerName))
                    : controllerModel.ControllerName;

            // 追加模块，控制器参数
            var controllerTemplate = $"{controllerApiDescriptionSettings?.Module}/{string.Join("/", controllerStartParameters)}/{controllerName}/{string.Join("/", controllerEndParameters)}";
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
        /// <param name="apiDescriptionSettings">接口描述配置</param>
        /// <param name="actionNameWords">行为名称中每一个单词</param>
        /// <returns></returns>
        private (string[], string[], string) GenerateActoinRouteTemplate(ActionModel actionModel, ApiDescriptionSettingsAttribute apiDescriptionSettings, string[] actionNameWords)
        {
            // 解析行为名称
            var actionName = apiDescriptionSettings?.SplitCamelCase == true
                ? string.Join('/', Penetrates.SplitToWords(actionModel.ActionName))
                : actionModel.ActionName;

            // 解析参数路由
            var (controllerStartParameters, controllerEndParameters, actionStartParameters, actionEndParameters) = ResolveParameterRoute(actionModel);
            var template = $"{apiDescriptionSettings?.Module}/{string.Join("/", actionStartParameters)}/{actionName}/{string.Join("/", actionEndParameters)}";
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
                if (_lazyControllerSettings.LowerCaseRoute) parameterModel.ParameterName = parameterModel.ParameterName.ToLower();

                // 只有贴了 [FromRoute] 特性，或者没有贴任何 [FromXXXX] 特性的基元类型
                var parameterAttributes = parameterModel.Attributes;
                if (parameterAttributes.Any(u => u is FromRouteAttribute) || (parameterType.IsRichPrimitive() && !parameterAttributes.Any(u => typeof(IBindingSourceMetadata).IsAssignableFrom(u.GetType()))))
                {
                    var parameterName = $"{{{parameterModel.ParameterName}}}";

                    // 解析参数位置
                    if (!(parameterAttributes.FirstOrDefault(u => u is ApiSeatAttribute) is ApiSeatAttribute seat)) actionEndParameters.Add(parameterName);
                    else
                    {
                        switch (seat.Seat)
                        {
                            case ApiSeats.ControllerStart:
                                controllerStartParameters.Add(parameterName);
                                break;

                            case ApiSeats.ControllerEnd:
                                controllerEndParameters.Add(parameterName);
                                break;

                            case ApiSeats.ActionStart:
                                actionStartParameters.Add(parameterName);
                                break;

                            case ApiSeats.ActionEnd:
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