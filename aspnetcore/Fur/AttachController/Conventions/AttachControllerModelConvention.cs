using Fur.ApplicationSystem;
using Fur.AttachController.Attributes;
using Fur.AttachController.Helpers;
using Fur.AttachController.Options;
using Fur.Extensions;
using Fur.Linq.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Fur.AttachController.Conventions
{
    internal sealed class AttachControllerModelConvention : IApplicationModelConvention
    {
        private readonly AttactControllerOptions _attactControllerOptions;
        public AttachControllerModelConvention(AttactControllerOptions attactControllerOptions)
        {
            _attactControllerOptions = attactControllerOptions;
        }

        #region 解析附加控制器 + public void Apply(ApplicationModel application)
        /// <summary>
        /// 解析附加控制器
        /// </summary>
        /// <param name="application">应用模型</param>
        public void Apply(ApplicationModel application)
        {
            foreach (var controllerModel in application.Controllers)
            {
                var controllerTypeInfo = controllerModel.ControllerType;
                if (typeof(ControllerBase).IsAssignableFrom(controllerTypeInfo)) continue;

                // 配置控制器
                ConfigureController(controllerModel, controllerTypeInfo);

                // 配置Action
                if (controllerModel.ApiExplorer.IsVisible.Value)
                {
                    var hasRouteAttribute = controllerModel.Selectors.Any(s => s.AttributeRouteModel != null);
                    if (hasRouteAttribute)
                    {
                        foreach (var actionModel in controllerModel.Actions)
                        {
                            ConfigureActionApiExplorerAndParameters(actionModel);
                        }
                    }
                    else ConfigureAction(controllerModel);
                }
            }
        }
        #endregion

        #region 配置控制器模型信息 - private void ConfigureController(ControllerModel controllerModel, TypeInfo controllerTypeInfo)
        /// <summary>
        /// 配置控制器模型信息
        /// </summary>
        /// <param name="controllerModel">控制器模型</param>
        /// <param name="controllerTypeInfo">控制器类型</param>
        private void ConfigureController(ControllerModel controllerModel, TypeInfo controllerTypeInfo)
        {
            var attactControllerAttribute = ApplicationGlobal.GetTypeAttribute<AttachControllerAttribute>(controllerTypeInfo.AsType());

            ConfigureAreaName(controllerModel, attactControllerAttribute);
            ConfigureControllerName(controllerModel);
            ConfigureControllerApiExplorer(controllerModel);
        }
        #endregion

        #region 配置区域/ApiVersion名称 - private void ConfigureAreaName(ControllerModel controllerModel, AttachControllerAttribute attactControllerAttribute)
        /// <summary>
        /// 配置区域/ApiVersion名称
        /// </summary>
        /// <param name="controllerModel">控制器模型</param>
        /// <param name="attactControllerAttribute">附加控制器特性</param>
        private void ConfigureAreaName(ControllerModel controllerModel, AttachControllerAttribute attactControllerAttribute)
        {
            if (!controllerModel.RouteValues.ContainsKey("area"))
            {
                if (!string.IsNullOrEmpty(attactControllerAttribute.ApiVersion))
                {
                    controllerModel.RouteValues["area"] = attactControllerAttribute.ApiVersion;
                }
                else if (!string.IsNullOrEmpty(_attactControllerOptions.DefaultApiVersion))
                {
                    controllerModel.RouteValues["area"] = _attactControllerOptions.DefaultApiVersion;
                }
            }
        }
        #endregion

        #region 配置控制器名称 - private void ConfigureControllerName(ControllerModel controllerModel)
        /// <summary>
        /// 配置控制器名称
        /// </summary>
        /// <param name="controllerModel">控制器模型</param>
        private void ConfigureControllerName(ControllerModel controllerModel)
        {
            controllerModel.ControllerName = Helper.ClearStringAffix(controllerModel.ControllerName, _attactControllerOptions.ClearControllerRouteAffix);
        }
        #endregion

        #region 配置控制器导出可见情况 - private void ConfigureControllerApiExplorer(ControllerModel controllerModel)
        /// <summary>
        /// 配置控制器导出可见情况
        /// </summary>
        /// <param name="controllerModel">控制器模型</param>
        private void ConfigureControllerApiExplorer(ControllerModel controllerModel)
        {
            if (controllerModel.ApiExplorer.IsVisible == null)
            {
                controllerModel.ApiExplorer.IsVisible = true;
            }
        }
        #endregion

        #region 配置Action模型信息 - private void ConfigureAction(ControllerModel controllerModel)
        /// <summary>
        /// 配置Action模型信息
        /// </summary>
        /// <param name="controllerModel">控制器模型</param>
        private void ConfigureAction(ControllerModel controllerModel)
        {
            foreach (var actionModel in controllerModel.Actions)
            {
                ConfigureActionApiExplorerAndParameters(actionModel);
                ConfigureActionName(actionModel);

                var attachActionAttribute = ApplicationGlobal.GetMethodAttribute<AttachActionAttribute>(actionModel.ActionMethod);
                if (actionModel.Selectors.IsNullOrEmpty() || actionModel.Selectors.Any(a => a.ActionConstraints.IsNullOrEmpty()))
                {
                    ConfigureActionRouteAndHttpMethod(controllerModel, actionModel, attachActionAttribute);
                }
                else
                {
                    // 合并路由
                    foreach (var selector in actionModel.Selectors)
                    {
                        var attributeRouteModel = ConfigureActionRoute(controllerModel, actionModel, attachActionAttribute);
                        selector.AttributeRouteModel = selector.AttributeRouteModel == null
                            ? attributeRouteModel
                            : AttributeRouteModel.CombineAttributeRouteModel(attributeRouteModel, selector.AttributeRouteModel);
                    }
                }
            }
        }
        #endregion

        #region 配置Action导出可见情况 - private void ConfigureActionApiExplorerAndParameters(ActionModel actionModel)
        /// <summary>
        /// 配置Action导出可见情况及参数绑定
        /// </summary>
        /// <param name="actionModel">Action模型</param>
        private void ConfigureActionApiExplorerAndParameters(ActionModel actionModel)
        {
            if (actionModel.ApiExplorer.IsVisible == null)
            {
                actionModel.ApiExplorer.IsVisible = true;
            }

            // 参数值绑定
            ConfigureActionParameters(actionModel);
        }
        #endregion

        #region 配置Action名称 - private void ConfigureActionName(ActionModel actionModel)
        /// <summary>
        /// 配置Action名称
        /// </summary>
        /// <param name="actionModel"></param>
        private void ConfigureActionName(ActionModel actionModel)
        {
            actionModel.ActionName = Helper.ClearStringAffix(actionModel.ActionName, _attactControllerOptions.ClearActionRouteAffix);
        }
        #endregion

        #region 配置Action路由和请求方式 - private void ConfigureActionRouteAndHttpMethod(ControllerModel controllerModel, ActionModel actionModel, AttachActionAttribute attachActionAttribute)
        /// <summary>
        /// 配置Action路由和请求方式
        /// </summary>
        /// <param name="controllerModel">控制器模型</param>
        /// <param name="actionModel">Action模型</param>
        /// <param name="attachActionAttribute">附加控制器模型</param>
        private void ConfigureActionRouteAndHttpMethod(ControllerModel controllerModel, ActionModel actionModel, AttachActionAttribute attachActionAttribute)
        {
            var verbKey = Helper.GetCamelCaseFirstWord(actionModel.ActionName).ToLower();
            var verb = Consts.HttpVerbSetter.ContainsKey(verbKey) ? Consts.HttpVerbSetter[verbKey] : _attactControllerOptions.DefaultHttpMethod.ToLower();

            var actionModelSelector = actionModel.Selectors[0];

            if (actionModelSelector.AttributeRouteModel == null)
            {
                actionModelSelector.AttributeRouteModel = ConfigureActionRoute(controllerModel, actionModel, attachActionAttribute);
            }

            if (!actionModelSelector.ActionConstraints.Any())
            {
                actionModelSelector.ActionConstraints.Add(new HttpMethodActionConstraint(new[] { verb }));
                switch (verb)
                {
                    case "GET":
                        actionModelSelector.EndpointMetadata.Add(new HttpGetAttribute());
                        break;
                    case "POST":
                        actionModelSelector.EndpointMetadata.Add(new HttpPostAttribute());
                        break;
                    case "PUT":
                        actionModelSelector.EndpointMetadata.Add(new HttpPutAttribute());
                        break;
                    case "DELETE":
                        actionModelSelector.EndpointMetadata.Add(new HttpDeleteAttribute());
                        break;
                    default: throw new System.Exception($"Unsupported HttpVerb: {verb}");
                }
            }
        }
        #endregion

        #region 配置Action路由信息 - private AttributeRouteModel ConfigureActionRoute(ControllerModel controllerModel, ActionModel actionModel, AttachActionAttribute attachActionAttribute)
        /// <summary>
        /// 配置Action路由信息
        /// </summary>
        /// <param name="controllerModel">控制器模型</param>
        /// <param name="actionModel">Acton模型</param>
        /// <param name="attachActionAttribute">附加Action特性</param>
        /// <returns>路由模型</returns>
        private AttributeRouteModel ConfigureActionRoute(ControllerModel controllerModel, ActionModel actionModel, AttachActionAttribute attachActionAttribute)
        {
            var stringBuilder = new StringBuilder();
            var areaName = controllerModel.RouteValues.ContainsKey("area") ? controllerModel.RouteValues["area"] : null;

            stringBuilder.Append($"{_attactControllerOptions.DefaultStartRoutePrefix}/{areaName}/{controllerModel.ControllerName}/{attachActionAttribute?.ApiVersion}/{actionModel.ActionName}");

            // 读取参数信息
            var parameters = ApplicationGlobal.ApplicationInfo.PublicInstanceMethods.FirstOrDefault(u => u.Method == actionModel.ActionMethod).Parameters;
            foreach (var parameterInfo in parameters)
            {
                var parameterType = parameterInfo.Type;
                if (Helper.IsPrimitiveIncludeNullable(parameterType))
                {
                    var parameterAttributes = parameterInfo.CustomAttributes;
                    var hasFromAttribute = parameterAttributes.Count() == 0 ||
                                                           parameterAttributes.Any(u => u.GetType() == typeof(FromRouteAttribute)) ||
                                                           parameterAttributes.Count(u => typeof(IBindingSourceMetadata).IsAssignableFrom(u.GetType())) == 0;

                    if (!hasFromAttribute) continue;

                    // 设置路由约束
                    var routeConstraintAttribute = parameterAttributes.FirstOrDefault(u => u.GetType() == typeof(RouteConstraintAttribute)) as RouteConstraintAttribute;
                    if (routeConstraintAttribute != null && !string.IsNullOrEmpty(routeConstraintAttribute.Constraint))
                    {
                        stringBuilder.Append($"/{{{parameterInfo.Name + routeConstraintAttribute.Constraint + (parameterType.IsNullable() ? "?" : "")}}}");
                    }
                    else
                    {
                        stringBuilder.Append($"/{{{parameterInfo.Name + (parameterType.IsNullable() ? "?" : "")}}}");
                    }
                }
            }

            return new AttributeRouteModel(new RouteAttribute(stringBuilder.ToString().Replace("//", "/")));
        }
        #endregion

        #region Action模型参数值绑定 - private void ConfigureActionParameters(ActionModel actionModel)
        /// <summary>
        /// Action模型参数值绑定
        /// </summary>
        /// <param name="actionModel">Action模型</param>
        private void ConfigureActionParameters(ActionModel actionModel)
        {
            foreach (var parameterModel in actionModel.Parameters)
            {
                if (parameterModel.BindingInfo != null) continue;

                if (!Helper.IsPrimitiveIncludeNullable(parameterModel.ParameterType) && CanBindingFromBody(actionModel, parameterModel))
                {
                    parameterModel.BindingInfo = BindingInfo.GetBindingInfo(new[] { new FromBodyAttribute() });
                }
            }
        }
        #endregion

        #region 检查是否能够通过Body绑定参数值 - private bool CanBindingFromBody(ActionModel actionModel, ParameterModel parameterModel)
        /// <summary>
        /// 检查是否能够通过Body绑定参数值
        /// </summary>
        /// <param name="actionModel">Action模型</param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns>是否能够绑定</returns>
        private bool CanBindingFromBody(ActionModel actionModel, ParameterModel parameterModel)
        {
            var parameterType = parameterModel.ParameterType;
            if (Consts.BindFromBodyIgnoreTypes.Any(u => u.IsAssignableFrom(parameterType))) return false;

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
        #endregion
    }
}
