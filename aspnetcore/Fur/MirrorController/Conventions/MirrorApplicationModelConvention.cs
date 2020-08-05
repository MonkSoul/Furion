using Fur.Extensions;
using Fur.Linq.Extensions;
using Fur.MirrorController.Attributes;
using Fur.MirrorController.Extensions;
using Fur.MirrorController.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Fur.MirrorController.Conventions
{
    internal sealed class MirrorApplicationModelConvention : IApplicationModelConvention
    {
        private readonly FurMirrorControllerOptions _mirrorControllerOptions;

        public MirrorApplicationModelConvention()
        {
            _mirrorControllerOptions = App.Settings.MirrorControllerOptions;
        }

        public void Apply(ApplicationModel application)
        {
            foreach (var controllerModel in application.Controllers)
            {
                // 无需配置 ControllerBase 衍生类
                if (typeof(ControllerBase).IsAssignableFrom(controllerModel.ControllerType)) continue;
                var mirrorControllerAttribute = controllerModel.ControllerType.GetCustomAttribute<MirrorControllerAttribute>();

                ConfigureController(controllerModel, mirrorControllerAttribute);
                ConfigureActions(controllerModel, mirrorControllerAttribute);
            }
        }

        /// <summary>
        /// 配置控制器
        /// </summary>
        /// <param name="controllerModel">控制器模型</param>
        private void ConfigureController(ControllerModel controllerModel, MirrorControllerAttribute mirrorControllerAttribute)
        {
            // 配置控制器可见性
            if (controllerModel.ApiExplorer.IsVisible == null) controllerModel.ApiExplorer.IsVisible = true;

            // 配置控制器名称
            // 控制器版本号
            var controllerApiVersion = mirrorControllerAttribute?.ApiVersion ?? _mirrorControllerOptions.DefaultApiVersion;
            // 控制器名称
            var controllerName = mirrorControllerAttribute?.Name ?? (controllerModel.ControllerName.ClearStringAffix(_mirrorControllerOptions.ClearControllerRouteAffix));
            var controllerFullName = $"{controllerName}" + (!string.IsNullOrEmpty(controllerApiVersion) ? $"@{controllerApiVersion}" : string.Empty);
            controllerModel.ControllerName = _mirrorControllerOptions.LowerCasePath ? controllerFullName.ToLower() : controllerFullName;
        }

        /// <summary>
        /// 配置控制器Action
        /// </summary>
        /// <param name="controllerModel"></param>
        private void ConfigureActions(ControllerModel controllerModel, MirrorControllerAttribute mirrorControllerAttribute)
        {
            if (!controllerModel.ApiExplorer.IsVisible.Value) return;

            foreach (var actionModel in controllerModel.Actions)
            {
                // 配置Action可见性
                if (actionModel.ApiExplorer.IsVisible == null) actionModel.ApiExplorer.IsVisible = true;

                // 配置Action参数
                ConfigureActionParameters(actionModel);

                // 配置Action路由
                if (controllerModel.Selectors.Any(s => s.AttributeRouteModel != null)) continue;

                var mirrorActionAttribute = actionModel.ActionMethod.GetCustomAttribute<MirrorActionAttribute>();
                var attributeRouteModel = ConfigureRoute(controllerModel, mirrorControllerAttribute, actionModel, mirrorActionAttribute);
                if (actionModel.Selectors.IsNullOrEmpty() || actionModel.Selectors.Any(a => a.ActionConstraints.IsNullOrEmpty()))
                {
                    var selectorModel = actionModel.Selectors[0];
                    if (selectorModel.AttributeRouteModel == null)
                    {
                        selectorModel.AttributeRouteModel = attributeRouteModel;
                    }
                    ConfigureActionHttpMethod(actionModel, selectorModel);
                }
                else
                {
                    // 合并路由
                    foreach (var selector in actionModel.Selectors)
                    {
                        selector.AttributeRouteModel = selector.AttributeRouteModel == null
                            ? attributeRouteModel
                            : AttributeRouteModel.CombineAttributeRouteModel(attributeRouteModel, selector.AttributeRouteModel);
                    }
                }
            }
        }

        /// <summary>
        /// 配置控制器Action参数
        /// </summary>
        /// <param name="actionModel"></param>
        private void ConfigureActionParameters(ActionModel actionModel)
        {
            foreach (var parameterModel in actionModel.Parameters)
            {
                if (parameterModel.BindingInfo != null) continue;

                if (!parameterModel.ParameterType.IsPrimitivePlusIncludeNullable() && CanBindingFromBody(actionModel, parameterModel))
                {
                    parameterModel.BindingInfo = BindingInfo.GetBindingInfo(new[] { new FromBodyAttribute() });
                }
            }
        }

        /// <summary>
        /// 配置Action请求方式
        /// </summary>
        /// <param name="actionModel">Action模型</param>
        /// <param name="selectorModel">选择器模型</param>
        private void ConfigureActionHttpMethod(ActionModel actionModel, SelectorModel selectorModel)
        {
            var verbKey = actionModel.ActionMethod.Name.GetCamelCaseFirstWord().ToLower();
            var verb = Consts.HttpVerbSetter.ContainsKey(verbKey) ? Consts.HttpVerbSetter[verbKey] : _mirrorControllerOptions.DefaultHttpMethod.ToUpper();

            if (!selectorModel.ActionConstraints.Any())
            {
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
        }

        /// <summary>
        /// 配置路由
        /// </summary>
        /// <param name="controllerModel">控制器模型</param>
        /// <param name="mirrorControllerAttribute">控制器特性</param>
        /// <param name="actionModel">Action模型</param>
        /// <param name="mirrorActionAttribute">Action特性</param>
        /// <returns></returns>
        private AttributeRouteModel ConfigureRoute(ControllerModel controllerModel, MirrorControllerAttribute mirrorControllerAttribute, ActionModel actionModel, MirrorActionAttribute mirrorActionAttribute)
        {
            var stringBuilder = new StringBuilder();

            // 默认路由前置
            var apiPrefix = _mirrorControllerOptions.DefaultRoutePrefix;
            // Action名称
            var actionName = actionModel.ActionName;
            if (mirrorActionAttribute != null)
            {
                if (mirrorActionAttribute.Name.HasValue()) actionName = mirrorActionAttribute.Name;
                else if (!mirrorActionAttribute.KeepOriginalName)
                {
                    // 移除前后缀
                    actionName = actionName.ClearStringAffix(_mirrorControllerOptions.ClearActionRouteAffix);

                    // 判断是否保留路由动词
                    if (!mirrorActionAttribute.KeepRouteVerb)
                    {
                        var verbKey = actionName.GetCamelCaseFirstWord();
                        if (Consts.HttpVerbSetter.ContainsKey(verbKey.ToLower()))
                        {
                            actionName = actionName.Substring(verbKey.Length);
                        }
                    }

                    if (mirrorActionAttribute.SplitWordToRoutePath) actionName = string.Join('/', actionName.CamelCaseSplitString());
                }
            }
            else
            {
                if (_mirrorControllerOptions.RemoveActionRouteVerb)
                {
                    var verbKey = actionName.GetCamelCaseFirstWord();
                    if (Consts.HttpVerbSetter.ContainsKey(verbKey.ToLower()))
                    {
                        actionName = actionName.Substring(verbKey.Length);
                    }
                }
            }
            stringBuilder.Append($"{apiPrefix}/{mirrorControllerAttribute?.Module}/{controllerModel.ControllerName}");

            // Action 版本号
            string actionApiVersion = null;
            var parameterIndex = 0;
            string actionStartParameters = string.Empty;
            string actionEndParameters = string.Empty;
            var versionIndex = -1;
            foreach (var parameterModel in actionModel.Parameters)
            {
                parameterIndex++;

                var parameterType = parameterModel.ParameterType;

                // 是否包含下划线参数，用作版本控制
                var isContainUnderline = parameterModel.Name.Equals("_");
                if (isContainUnderline)
                {
                    // 生成版本号并移除路由参数
                    actionApiVersion = parameterModel.ParameterInfo.DefaultValue?.ToString();
                    versionIndex = parameterIndex - 1;
                    continue;
                }

                if (parameterType.IsPrimitivePlusIncludeNullable() && !parameterType.IsNullable() && !isContainUnderline)
                {
                    var parameterAttributes = parameterModel.Attributes;
                    var IsFromRouteParameter = !parameterAttributes.Any() ||
                                                                 !parameterAttributes.Any(u => typeof(IBindingSourceMetadata).IsAssignableFrom(u.GetType())) ||
                                                                 parameterAttributes.Any(u => u is FromRouteAttribute);

                    if (!IsFromRouteParameter) continue;

                    // 是否需要配置参数位置
                    if (parameterAttributes.FirstOrDefault(a => a is RouteSeatAttribute) is RouteSeatAttribute routeSeatAttribute)
                    {
                        if (routeSeatAttribute.RouteSeat == RouteSeatOptions.ActionStart)
                        {
                            actionStartParameters += $"/{{{parameterModel.Name}}}";
                        }
                        else actionEndParameters += $"/{{{parameterModel.Name}}}";
                    }
                    else actionEndParameters += $"/{{{parameterModel.Name}}}";
                }
            }
            if (versionIndex > -1)
            {
                actionModel.Parameters.RemoveAt(versionIndex);
            }

            if (!string.IsNullOrEmpty(actionStartParameters)) stringBuilder.Append($"{actionStartParameters}");
            stringBuilder.Append($"/{actionName}");
            if (!string.IsNullOrEmpty(actionApiVersion)) stringBuilder.Append($"@{actionApiVersion}");
            if (!string.IsNullOrEmpty(actionEndParameters)) stringBuilder.Append($"{actionEndParameters}");

            var route = stringBuilder.ToString().Replace("//", "/");

            return new AttributeRouteModel(new RouteAttribute(_mirrorControllerOptions.LowerCasePath ? route.ToLower() : route));
        }

        /// <summary>
        /// 检查是否能够通过Body绑定参数值
        /// </summary>
        /// <param name="actionModel">Action模型</param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns>是否能够绑定</returns>
        private bool CanBindingFromBody(ActionModel actionModel, ParameterModel parameterModel)
        {
            if (Consts.BindFromBodyIgnoreTypes.Any(u => u.IsAssignableFrom(parameterModel.ParameterType))) return false;

            foreach (var actionModelSelector in actionModel.Selectors)
            {
                if (actionModelSelector.ActionConstraints == null) continue;

                foreach (var actionConstraint in actionModelSelector.ActionConstraints)
                {
                    if (!(actionConstraint is HttpMethodActionConstraint httpMethodActionConstraint)) continue;

                    var httpMethods = new string[] { "GET", "DELETE", "TRACE", "HEAD" };
                    if (httpMethodActionConstraint.HttpMethods.All(u => httpMethods.Contains(u))) return false;
                }
            }

            return true;
        }
    }
}