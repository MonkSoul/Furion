// MIT 许可证
//
// 版权 © 2020-present 百小僧, 百签科技（广东）有限公司 和所有贡献者
//
// 特此免费授予任何获得本软件副本和相关文档文件（下称“软件”）的人不受限制地处置该软件的权利，
// 包括不受限制地使用、复制、修改、合并、发布、分发、转授许可和/或出售该软件副本，
// 以及再授权被配发了本软件的人如上的权利，须在下列条件下：
//
// 上述版权声明和本许可声明应包含在该软件的所有副本或实质成分中。
//
// 本软件是“如此”提供的，没有任何形式的明示或暗示的保证，包括但不限于对适销性、特定用途的适用性和不侵权的保证。
// 在任何情况下，作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

using Furion.Extensions;
using Furion.FriendlyException;
using Furion.Localization;
using Furion.Templates.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Collections.Concurrent;
using System.Reflection;

namespace Furion.UnifyResult;

/// <summary>
/// 规范化结果上下文
/// </summary>
[SuppressSniffer]
public static class UnifyContext
{
    /// <summary>
    /// 是否启用规范化结果
    /// </summary>
    internal static bool EnabledUnifyHandler = false;

    /// <summary>
    /// 规范化结果额外数据键
    /// </summary>
    internal static string UnifyResultExtrasKey = "UNIFY_RESULT_EXTRAS";

    /// <summary>
    /// 规范化结果提供器
    /// </summary>
    internal static ConcurrentDictionary<string, UnifyMetadata> UnifyProviders = new();

    /// <summary>
    /// 规范化序列化配置
    /// </summary>
    internal static ConcurrentDictionary<string, object> UnifySerializerSettings = new();

    /// <summary>
    /// 获取异常元数据
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public static ExceptionMetadata GetExceptionMetadata(ActionContext context)
    {
        object errorCode = default;
        object originErrorCode = default;
        object errors = default;
        object data = default;
        var statusCode = StatusCodes.Status500InternalServerError;
        var isValidationException = false; // 判断是否是验证异常
        var isFriendlyException = false;

        // 判断是否是 ExceptionContext 或者 ActionExecutedContext
        var exception = context is ExceptionContext exContext
            ? exContext.Exception
            : (
                context is ActionExecutedContext edContext
                ? edContext.Exception
                : default
            );

        // 判断是否是友好异常
        if (exception is AppFriendlyException friendlyException)
        {
            isFriendlyException = true;
            errorCode = friendlyException.ErrorCode;
            originErrorCode = friendlyException.OriginErrorCode;
            statusCode = friendlyException.StatusCode;
            isValidationException = friendlyException.ValidationException;
            errors = friendlyException.ErrorMessage;
            data = friendlyException.Data;
        }

        // 处理验证失败异常
        if (!isValidationException)
        {
            // 查找所有全局定义异常
            var ifExceptionAttributes = context.ActionDescriptor is not ControllerActionDescriptor actionDescriptor
                        ? Array.Empty<IfExceptionAttribute>()
                        : actionDescriptor.MethodInfo
                            .GetCustomAttributes<IfExceptionAttribute>(true)
                            .Where(u => u.ErrorCode == null);

            // 处理全局异常
            if (ifExceptionAttributes.Any())
            {
                // 首先判断是否有相同类型的异常
                var actionIfExceptionAttribute = ifExceptionAttributes.FirstOrDefault(u => u.ExceptionType ==
                                                (isFriendlyException && exception?.InnerException != null
                                                                    ? exception?.InnerException.GetType()
                                                                    : exception?.GetType()))
                        ?? ifExceptionAttributes.FirstOrDefault(u => u.ExceptionType == typeof(Exception))
                        ?? ifExceptionAttributes.FirstOrDefault(u => u.ExceptionType == null);

                // 支持渲染配置文件
                if (actionIfExceptionAttribute is { ErrorMessage: not null } && !isFriendlyException)
                {
                    var realErrorMessage = actionIfExceptionAttribute.ErrorMessage.Render();

                    // 多语言处理
                    errors = L.Text == null ? realErrorMessage : L.Text[realErrorMessage];
                }
            }
            else
            {
                var friendlyExceptionSettings = App.GetOptions<FriendlyExceptionSettingsOptions>();
                errors = exception?.InnerException?.Message ?? exception?.Message ?? friendlyExceptionSettings.DefaultErrorMessage;
            }
        }

        return new ExceptionMetadata
        {
            StatusCode = statusCode,
            ErrorCode = errorCode,
            OriginErrorCode = originErrorCode,
            Errors = errors,
            Data = data
        };
    }

    /// <summary>
    /// 填充附加信息
    /// </summary>
    /// <param name="extras"></param>
    public static void Fill(object extras)
    {
        var items = App.HttpContext?.Items;
        if (items.ContainsKey(UnifyResultExtrasKey)) items.Remove(UnifyResultExtrasKey);
        items.Add(UnifyResultExtrasKey, extras);
    }

    /// <summary>
    /// 读取附加信息
    /// </summary>
    public static object Take()
    {
        object extras = null;
        App.HttpContext?.Items?.TryGetValue(UnifyResultExtrasKey, out extras);
        return extras;
    }

    /// <summary>
    /// 设置响应状态码
    /// </summary>
    /// <param name="context"></param>
    /// <param name="statusCode"></param>
    /// <param name="unifyResultSettings"></param>
    public static void SetResponseStatusCodes(HttpContext context, int statusCode, UnifyResultSettingsOptions unifyResultSettings)
    {
        if (unifyResultSettings == null) return;

        // 篡改响应状态码
        if (unifyResultSettings.AdaptStatusCodes != null && unifyResultSettings.AdaptStatusCodes.Length > 0)
        {
            var adaptStatusCode = unifyResultSettings.AdaptStatusCodes.FirstOrDefault(u => u[0] == statusCode);
            if (adaptStatusCode != null && adaptStatusCode.Length > 0 && adaptStatusCode[0] > 0)
            {
                context.Response.StatusCode = adaptStatusCode[1];
                return;
            }
        }

        // 如果为 null，则所有请求错误的状态码设置为 200
        if (unifyResultSettings.Return200StatusCodes == null) context.Response.StatusCode = 200;
        // 否则只有里面的才设置为 200
        else if (unifyResultSettings.Return200StatusCodes.Contains(statusCode)) context.Response.StatusCode = 200;
        else { }
    }

    /// <summary>
    /// 获取序列化配置
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public static object GetSerializerSettings(FilterContext context)
    {
        // 获取控制器信息
        if (context.ActionDescriptor is not ControllerActionDescriptor actionDescriptor) return null;

        // 获取序列化配置
        var unifySerializerSettingAttribute = actionDescriptor.MethodInfo.GetFoundAttribute<UnifySerializerSettingAttribute>(true);
        if (unifySerializerSettingAttribute == null || string.IsNullOrWhiteSpace(unifySerializerSettingAttribute.Name)) return null;

        // 解析全局配置
        var succeed = UnifySerializerSettings.TryGetValue(unifySerializerSettingAttribute.Name, out var serializerSettings);
        return succeed ? serializerSettings : null;
    }

    /// <summary>
    /// 获取序列化配置
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static object GetSerializerSettings(string name)
    {
        // 解析全局配置
        var succeed = UnifySerializerSettings.TryGetValue(name, out var serializerSettings);
        return succeed ? serializerSettings : null;
    }

    /// <summary>
    /// 检查请求成功是否进行规范化处理
    /// </summary>
    /// <param name="method"></param>
    /// <param name="unifyResult"></param>
    /// <param name="isWebRequest"></param>
    /// <returns>返回 true 跳过处理，否则进行规范化处理</returns>
    internal static bool CheckSucceededNonUnify(MethodInfo method, out IUnifyResultProvider unifyResult, bool isWebRequest = true)
    {
        // 解析规范化元数据
        var unityMetadata = GetMethodUnityMetadata(method);

        // 判断是否跳过规范化处理
        var isSkip = !EnabledUnifyHandler
              || method.GetRealReturnType().HasImplementedRawGeneric(unityMetadata.ResultType)
              || method.CustomAttributes.Any(x => typeof(NonUnifyAttribute).IsAssignableFrom(x.AttributeType) || typeof(ProducesResponseTypeAttribute).IsAssignableFrom(x.AttributeType) || typeof(IApiResponseMetadataProvider).IsAssignableFrom(x.AttributeType))
              || method.ReflectedType.IsDefined(typeof(NonUnifyAttribute), true)
              || method.DeclaringType.Assembly.GetName().Name.StartsWith("Microsoft.AspNetCore.OData");

        if (!isWebRequest)
        {
            unifyResult = null;
            return isSkip;
        }

        unifyResult = isSkip ? null : App.RootServices.GetService(unityMetadata.ProviderType) as IUnifyResultProvider;
        return unifyResult == null || isSkip;
    }

    /// <summary>
    /// 检查请求失败（验证失败、抛异常）是否进行规范化处理
    /// </summary>
    /// <param name="method"></param>
    /// <param name="unifyResult"></param>
    /// <returns>返回 true 跳过处理，否则进行规范化处理</returns>
    internal static bool CheckFailedNonUnify(MethodInfo method, out IUnifyResultProvider unifyResult)
    {
        // 解析规范化元数据
        var unityMetadata = GetMethodUnityMetadata(method);

        // 判断是否跳过规范化处理
        var isSkip = !EnabledUnifyHandler
                || method.CustomAttributes.Any(x => typeof(NonUnifyAttribute).IsAssignableFrom(x.AttributeType))
                || (
                        !method.CustomAttributes.Any(x => typeof(ProducesResponseTypeAttribute).IsAssignableFrom(x.AttributeType) || typeof(IApiResponseMetadataProvider).IsAssignableFrom(x.AttributeType))
                        && method.ReflectedType.IsDefined(typeof(NonUnifyAttribute), true)
                    )
                || method.DeclaringType.Assembly.GetName().Name.StartsWith("Microsoft.AspNetCore.OData");

        unifyResult = isSkip ? null : App.RootServices.GetService(unityMetadata.ProviderType) as IUnifyResultProvider;
        return unifyResult == null || isSkip;
    }

    /// <summary>
    /// 检查短路状态码（>=400）是否进行规范化处理
    /// </summary>
    /// <param name="context"></param>
    /// <param name="unifyResult"></param>
    /// <returns>返回 true 跳过处理，否则进行规范化处理</returns>
    internal static bool CheckStatusCodeNonUnify(HttpContext context, out IUnifyResultProvider unifyResult)
    {
        // 获取终点路由特性
        var endpointFeature = context.Features.Get<IEndpointFeature>();
        if (endpointFeature == null) return (unifyResult = null) == null;

        // 判断是否跳过规范化处理
        var isSkip = !EnabledUnifyHandler
                || context.GetMetadata<NonUnifyAttribute>() != null
                || endpointFeature?.Endpoint?.Metadata?.GetMetadata<NonUnifyAttribute>() != null
                || context.Request.Headers["accept"].ToString().Contains("odata.metadata=", StringComparison.OrdinalIgnoreCase)
                || context.Request.Headers["accept"].ToString().Contains("odata.streaming=", StringComparison.OrdinalIgnoreCase);

        if (isSkip == true) unifyResult = null;
        else
        {
            // 解析规范化元数据
            var unifyProviderAttribute = endpointFeature?.Endpoint?.Metadata?.GetMetadata<UnifyProviderAttribute>();
            UnifyProviders.TryGetValue(unifyProviderAttribute?.Name ?? string.Empty, out var unityMetadata);

            unifyResult = context.RequestServices.GetService(unityMetadata.ProviderType) as IUnifyResultProvider;
        }

        return unifyResult == null || isSkip;
    }

    /// <summary>
    /// 判断是否支持 Mvc 控制器规范化处理
    /// </summary>
    /// <param name="httpContext"></param>
    /// <param name="actionDescriptor"></param>
    /// <param name="unifyResultSettings"></param>
    /// <returns></returns>
    internal static bool CheckSupportMvcController(HttpContext httpContext, ControllerActionDescriptor actionDescriptor, out UnifyResultSettingsOptions unifyResultSettings)
    {
        // 获取规范化配置选项
        unifyResultSettings = httpContext.RequestServices.GetService<IOptions<UnifyResultSettingsOptions>>()?.Value;

        // 如果未启用 MVC 规范化处理，则跳过
        if (unifyResultSettings?.SupportMvcController == false && typeof(Controller).IsAssignableFrom(actionDescriptor.ControllerTypeInfo)) return false;

        return true;
    }

    /// <summary>
    /// 检查是否是有效的结果（可进行规范化的结果）
    /// </summary>
    /// <param name="result"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    internal static bool CheckVaildResult(IActionResult result, out object data)
    {
        data = default;

        // 排除以下结果，跳过规范化处理
        var isDataResult = result switch
        {
            ViewResult => false,
            PartialViewResult => false,
            FileResult => false,
            ChallengeResult => false,
            SignInResult => false,
            SignOutResult => false,
            RedirectToPageResult => false,
            RedirectToRouteResult => false,
            RedirectResult => false,
            RedirectToActionResult => false,
            LocalRedirectResult => false,
            ForbidResult => false,
            ViewComponentResult => false,
            PageResult => false,
            NotFoundResult => false,
            NotFoundObjectResult => false,
            _ => true,
        };

        // 目前支持返回值 ActionResult
        if (isDataResult) data = result switch
        {
            // 处理内容结果
            ContentResult content => content.Content,
            // 处理对象结果
            ObjectResult obj => obj.Value,
            // 处理 JSON 对象
            JsonResult json => json.Value,
            _ => null,
        };

        return isDataResult;
    }

    /// <summary>
    /// 获取方法规范化元数据
    /// </summary>
    /// <remarks>如果追求性能，这里理应缓存起来，避免每次请求去检测</remarks>
    /// <param name="method"></param>
    /// <returns></returns>
    internal static UnifyMetadata GetMethodUnityMetadata(MethodInfo method)
    {
        if (method == default) return default;

        var unityProviderAttribute = method.GetFoundAttribute<UnifyProviderAttribute>(true);

        // 获取元数据
        var isExists = UnifyProviders.TryGetValue(unityProviderAttribute?.Name ?? string.Empty, out var metadata);
        if (!isExists)
        {
            // 不存在则将默认的返回
            UnifyProviders.TryGetValue(string.Empty, out metadata);
        }

        return metadata;
    }
}