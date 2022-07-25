// Copyright (c) 2020-2022 百小僧, Baiqian Co.,Ltd.
// Furion is licensed under Mulan PSL v2.
// You can use this software according to the terms and conditions of the Mulan PSL v2.
// You may obtain a copy of Mulan PSL v2 at:
//             https://gitee.com/dotnetchina/Furion/blob/master/LICENSE
// THIS SOFTWARE IS PROVIDED ON AN "AS IS" BASIS, WITHOUT WARRANTIES OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO NON-INFRINGEMENT, MERCHANTABILITY OR FIT FOR A PARTICULAR PURPOSE.
// See the Mulan PSL v2 for more details.

using Furion.Templates;
using Furion.UnifyResult;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using System.Diagnostics;
using System.Reflection;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace Furion.Logging;

/// <summary>
/// 强大的日志监听器
/// </summary>
/// <remarks>主要用于将请求的信息打印出来</remarks>
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
public sealed class LoggingMonitorAttribute : Attribute, IAsyncActionFilter
{
    /// <summary>
    /// 固定日志分类名
    /// </summary>
    /// <remarks>方便对日志进行过滤写入不同的存储介质中</remarks>
    private const string LOG_CATEGORY_NAME = "System.Logging.LoggingMonitor";

    /// <summary>
    /// 构造函数
    /// </summary>
    public LoggingMonitorAttribute()
    {
    }

    /// <summary>
    /// 日志标题
    /// </summary>
    public string Title { get; set; } = "Logging Monitor";

    /// <summary>
    /// 监视 Action 执行
    /// </summary>
    /// <param name="context"></param>
    /// <param name="next"></param>
    /// <returns></returns>
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        // 获取控制器/操作描述器
        var controllerActionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;

        // 获取请求方法
        var actionMethod = controllerActionDescriptor.MethodInfo;

        // 如果贴了 [SuppressMonitor] 特性则跳过
        if (actionMethod.IsDefined(typeof(SuppressMonitorAttribute), true))
        {
            _ = await next();
            return;
        }

        // 获取路由表信息
        var routeData = context.RouteData;
        var controllerName = routeData.Values["controller"];
        var actionName = routeData.Values["action"];
        var areaName = routeData.DataTokens["area"];

        // 调用呈现链名称
        var displayName = controllerActionDescriptor.DisplayName;

        // 获取 HttpContext 和 HttpRequest 对象
        var httpContext = context.HttpContext;
        var httpRequest = httpContext.Request;

        // 获取服务端 IPv4 地址
        var localIPv4 = httpContext.GetLocalIpAddressToIPv4();

        // 获取客户端 IPv4 地址
        var remoteIPv4 = httpContext.GetRemoteIpAddressToIPv4();

        // 获取请求的 Url 地址
        var requestUrl = Uri.UnescapeDataString(httpRequest.GetRequestUrlAddress());

        // 获取来源 Url 地址
        var refererUrl = Uri.UnescapeDataString(httpRequest.GetRefererUrlAddress());

        // 服务器环境
        var environmentName = httpContext.RequestServices.GetRequiredService<IWebHostEnvironment>().EnvironmentName;

        // 获取方法参数
        var parameterValues = context.ActionArguments;

        // 获取授权用户
        var user = httpContext.User;

        // token 信息
        var authorization = httpRequest.Headers["Authorization"].ToString();

        // 解析 token 内容

        // 客户端浏览器信息
        var userAgent = httpRequest.Headers["User-Agent"];

        // 计算接口执行时间
        var timeOperation = Stopwatch.StartNew();
        var resultContext = await next();
        timeOperation.Stop();

        // 获取异常对象情况
        var exception = resultContext.Exception;

        var monitorItems = new List<string>()
        {
            $"##控制器名称## {controllerActionDescriptor.ControllerTypeInfo.Name}"
            , $"##操作名称## {actionMethod.Name}"
            , $"##路由信息## [area]: {areaName}; [controller]: {controllerName}; [action]: {actionName}"
            , $"##请求地址## {requestUrl}"
            , $"##来源地址## {refererUrl}"
            , $"##浏览器标识## {userAgent}"
            , $"##客户端 IP 地址## {remoteIPv4}"
            , $"##服务端 IP 地址## {localIPv4}"
            , $"##服务端运行环境## {environmentName}"
            , $"##执行耗时## {timeOperation.ElapsedMilliseconds}ms"
        };

        // 添加 JWT 授权信息日志模板
        monitorItems.AddRange(GenerateAuthorizationTemplate(user, authorization));

        // 添加请求参数信息日志模板
        monitorItems.AddRange(GenerateParameterTemplate(parameterValues, actionMethod, httpRequest.Headers["Content-Type"]));

        // 添加返回值信息日志模板
        monitorItems.AddRange(GenerateReturnInfomationTemplate(resultContext, actionMethod));

        // 添加异常信息日志模板
        monitorItems.AddRange(GenerateExcetpionInfomationTemplate(exception));

        // 生成最终模板
        var monitor = TP.Wrapper(Title, displayName, monitorItems.ToArray());

        // 创建日志记录器
        var logger = httpContext.RequestServices.GetRequiredService<ILoggerFactory>()
            .CreateLogger(LOG_CATEGORY_NAME);

        // 写入日志
        logger.LogInformation(monitor);
    }

    /// <summary>
    /// 生成 JWT 授权信息日志模板
    /// </summary>
    /// <param name="claimsPrincipal"></param>
    /// <param name="authorization"></param>
    /// <returns></returns>
    private static List<string> GenerateAuthorizationTemplate(ClaimsPrincipal claimsPrincipal, StringValues authorization)
    {
        var templates = new List<string>();

        if (!claimsPrincipal.Claims.Any()) return templates;

        templates.AddRange(new[]
        {
            $"━━━━━━━━━━━━━━━  授权信息 ━━━━━━━━━━━━━━━"
            , $"##JWT Token## {authorization}"
            , $""
        });

        // 遍历身份信息
        foreach (var claim in claimsPrincipal.Claims)
        {
            templates.Add($"##{claim.Type} ({(claim.ValueType.Replace("http://www.w3.org/2001/XMLSchema#", ""))})## {claim.Value}");
        }

        return templates;
    }

    /// <summary>
    /// 生成请求参数信息日志模板
    /// </summary>
    /// <param name="parameterValues"></param>
    /// <param name="method"></param>
    /// <param name="contentType"></param>
    /// <returns></returns>
    private static List<string> GenerateParameterTemplate(IDictionary<string, object> parameterValues, MethodInfo method, StringValues contentType)
    {
        var templates = new List<string>();

        if (parameterValues.Count == 0) return templates;

        templates.AddRange(new[]
        {
            $"━━━━━━━━━━━━━━━  参数列表 ━━━━━━━━━━━━━━━"
            , $"##Content-Type## {contentType}"
            , $""
        });

        var parameters = method.GetParameters();
        foreach (var parameter in parameters)
        {
            var name = parameter.Name;
            var value = parameterValues[name];
            var type = parameter.ParameterType;

            object rawValue = default;

            // 文件类型参数
            if (value is IFormFile || value is List<IFormFile>)
            {
                // 单文件
                if (value is IFormFile formFile)
                {
                    rawValue = $"[name]: {formFile.FileName}; [size]: {(Math.Round(formFile.Length / 1024D))}KB; [content-type]: {formFile.ContentType}";
                }
                // 多文件
                else if (value is List<IFormFile> formFiles)
                {
                    for (var i = 0; i < formFiles.Count; i++)
                    {
                        var file = formFiles[i];
                        templates.Add($"##{name}{i} ({nameof(IFormFile)})## [name]: {file.FileName}; [size]: {(Math.Round(file.Length / 1024D))}KB; [content-type]: {file.ContentType}");
                    }

                    continue;
                }
            }
            // 处理基元类型，字符串类型和空值
            else if (type.IsPrimitive || value is string || value == null) rawValue = value;
            // 其他类型统一进行序列化
            else
            {
                rawValue = JsonSerializer.Serialize(value, new JsonSerializerOptions
                {
                    // 处理中文乱码问题
                    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                });
            }

            templates.Add($"##{name} ({type.Name})## {rawValue}");
        }

        return templates;
    }

    /// <summary>
    /// 生成返回值信息日志模板
    /// </summary>
    /// <param name="resultContext"></param>
    /// <param name="method"></param>
    /// <returns></returns>
    private static List<string> GenerateReturnInfomationTemplate(ActionExecutedContext resultContext, MethodInfo method)
    {
        var templates = new List<string>();

        object returnValue = null;

        // 解析返回值
        if (UnifyContext.CheckVaildResult(resultContext.Result, out var data)) returnValue = data;

        // 获取最终呈现值（字符串类型）
        var displayValue = method.ReturnType == typeof(void)
            ? string.Empty
            : JsonSerializer.Serialize(returnValue, new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            });

        templates.AddRange(new[]
        {
            $"━━━━━━━━━━━━━━━  返回信息 ━━━━━━━━━━━━━━━"
            , $"##类型## {method.ReturnType.FullName}"
            , $"##返回值## {displayValue}"
        });

        return templates;
    }

    /// <summary>
    /// 生成异常信息日志模板
    /// </summary>
    /// <param name="exception"></param>
    /// <returns></returns>
    private static List<string> GenerateExcetpionInfomationTemplate(Exception exception)
    {
        var templates = new List<string>();

        if (exception == null) return templates;

        templates.AddRange(new[]
        {
            $"━━━━━━━━━━━━━━━  异常信息 ━━━━━━━━━━━━━━━"
            , $"##类型## {exception.GetType()}"
            , $"##消息## {exception.Message}"
            , $"##错误堆栈## {exception.StackTrace}"
        });

        return templates;
    }
}