// MIT License
//
// Copyright (c) 2020-2022 百小僧, Baiqian Co.,Ltd.
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

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
    /// 日志上下文统一 Key
    /// </summary>
    private const string LOG_CONTEXT_NAME = "LoggingMonitor";

    /// <summary>
    /// 构造函数
    /// </summary>
    public LoggingMonitorAttribute()
        : this(new LoggingMonitorSettings())
    {
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="settings"></param>
    internal LoggingMonitorAttribute(LoggingMonitorSettings settings)
    {
        Settings = settings;
    }

    /// <summary>
    /// 日志标题
    /// </summary>
    public string Title { get; set; } = "Logging Monitor";

    /// <summary>
    /// 配置信息
    /// </summary>
    private LoggingMonitorSettings Settings { get; set; }

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

        // 获取方法完整名称
        var methodFullName = controllerActionDescriptor.ControllerTypeInfo.FullName + "." + actionMethod.Name;

        // 只有方法没有贴有 [LoggingMonitor] 特性才判断全局，贴了特性优先级最大
        if (!actionMethod.IsDefined(typeof(LoggingMonitorAttribute), true))
        {
            // 处理不启用但排除的情况
            if (!Settings.GlobalEnabled
                && !Settings.IncludeOfMethods.Contains(methodFullName, StringComparer.OrdinalIgnoreCase))
            {
                // 查找是否包含匹配，忽略大小写
                _ = await next();
                return;
            }

            // 处理启用但排除的情况
            if (Settings.GlobalEnabled
                && Settings.ExcludeOfMethods.Contains(methodFullName, StringComparer.OrdinalIgnoreCase))
            {
                _ = await next();
                return;
            }
        }

        // 创建日志上下文
        var logContext = new LogContext();

        // 获取路由表信息
        var routeData = context.RouteData;
        var controllerName = routeData.Values["controller"];
        var actionName = routeData.Values["action"];
        var areaName = routeData.DataTokens["area"];
        logContext.Set($"{LOG_CONTEXT_NAME}.ControllerName", controllerName)
                  .Set($"{LOG_CONTEXT_NAME}.ControllerTypeName", controllerActionDescriptor.ControllerTypeInfo.Name)
                  .Set($"{LOG_CONTEXT_NAME}.ActionName", actionName)
                  .Set($"{LOG_CONTEXT_NAME}.ActionTypeName", actionMethod.Name)
                  .Set($"{LOG_CONTEXT_NAME}.AreaName", areaName);

        // 调用呈现链名称
        var displayName = controllerActionDescriptor.DisplayName;
        logContext.Set($"{LOG_CONTEXT_NAME}.DisplayName", displayName);

        // 获取 HttpContext 和 HttpRequest 对象
        var httpContext = context.HttpContext;
        var httpRequest = httpContext.Request;

        // 获取服务端 IPv4 地址
        var localIPv4 = httpContext.GetLocalIpAddressToIPv4();
        logContext.Set($"{LOG_CONTEXT_NAME}.LocalIPv4", localIPv4);

        // 获取客户端 IPv4 地址
        var remoteIPv4 = httpContext.GetRemoteIpAddressToIPv4();
        logContext.Set($"{LOG_CONTEXT_NAME}.RemoteIPv4", localIPv4);

        // 获取请求的 Url 地址
        var requestUrl = Uri.UnescapeDataString(httpRequest.GetRequestUrlAddress());
        logContext.Set($"{LOG_CONTEXT_NAME}.RequestUrl", requestUrl);

        // 获取来源 Url 地址
        var refererUrl = Uri.UnescapeDataString(httpRequest.GetRefererUrlAddress());
        logContext.Set($"{LOG_CONTEXT_NAME}.RefererUrl", refererUrl);

        // 服务器环境
        var environmentName = httpContext.RequestServices.GetRequiredService<IWebHostEnvironment>().EnvironmentName;
        logContext.Set($"{LOG_CONTEXT_NAME}.EnvironmentName", environmentName);

        // 客户端浏览器信息
        var userAgent = httpRequest.Headers["User-Agent"];
        logContext.Set($"{LOG_CONTEXT_NAME}.UserAgent", userAgent);

        // 获取方法参数
        var parameterValues = context.ActionArguments;

        // 获取授权用户
        var user = httpContext.User;

        // token 信息
        var authorization = httpRequest.Headers["Authorization"].ToString();
        logContext.Set($"{LOG_CONTEXT_NAME}.Authorization", authorization);

        // 计算接口执行时间
        var timeOperation = Stopwatch.StartNew();
        var resultContext = await next();
        timeOperation.Stop();
        logContext.Set($"{LOG_CONTEXT_NAME}.ElapsedMilliseconds", timeOperation.ElapsedMilliseconds);

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
        monitorItems.AddRange(GenerateAuthorizationTemplate(logContext, user, authorization));

        // 添加请求参数信息日志模板
        monitorItems.AddRange(GenerateParameterTemplate(logContext, parameterValues, actionMethod, httpRequest.Headers["Content-Type"]));

        // 添加返回值信息日志模板
        monitorItems.AddRange(GenerateReturnInfomationTemplate(logContext, resultContext, actionMethod));

        // 添加异常信息日志模板
        monitorItems.AddRange(GenerateExcetpionInfomationTemplate(logContext, exception));

        // 生成最终模板
        var monitor = TP.Wrapper(Title, displayName, monitorItems.ToArray());

        // 创建日志记录器
        var logger = httpContext.RequestServices.GetRequiredService<ILoggerFactory>()
            .CreateLogger(LOG_CATEGORY_NAME)
            .ScopeContext(logContext);

        // 写入日志，如果没有异常使用 LogInformation，否则使用 LogError
        if (exception == null) logger.LogInformation(monitor);
        else logger.LogError(exception, monitor);
    }

    /// <summary>
    /// 生成 JWT 授权信息日志模板
    /// </summary>
    /// <param name="logContext"></param>
    /// <param name="claimsPrincipal"></param>
    /// <param name="authorization"></param>
    /// <returns></returns>
    private static List<string> GenerateAuthorizationTemplate(LogContext logContext, ClaimsPrincipal claimsPrincipal, StringValues authorization)
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
            logContext.Set($"{LOG_CONTEXT_NAME}.Authorization.{claim.Type}", claim.Value);
        }

        return templates;
    }

    /// <summary>
    /// 生成请求参数信息日志模板
    /// </summary>
    /// <param name="logContext"></param>
    /// <param name="parameterValues"></param>
    /// <param name="method"></param>
    /// <param name="contentType"></param>
    /// <returns></returns>
    private static List<string> GenerateParameterTemplate(LogContext logContext, IDictionary<string, object> parameterValues, MethodInfo method, StringValues contentType)
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
                    var fileSize = Math.Round(formFile.Length / 1024D);
                    templates.Add($"##{name} ({type.Name})## [name]: {formFile.FileName}; [size]: {fileSize}KB; [content-type]: {formFile.ContentType}");
                    logContext.Set($"{LOG_CONTEXT_NAME}.Parameter.{name}", $"File {{ name: {formFile.FileName}, size: {fileSize}, contentType: {formFile.ContentType} }}");
                    continue;
                }
                // 多文件
                else if (value is List<IFormFile> formFiles)
                {
                    for (var i = 0; i < formFiles.Count; i++)
                    {
                        var file = formFiles[i];
                        var size = Math.Round(file.Length / 1024D);
                        templates.Add($"##{name}[{i}] ({nameof(IFormFile)})## [name]: {file.FileName}; [size]: {size}KB; [content-type]: {file.ContentType}");
                        logContext.Set($"{LOG_CONTEXT_NAME}.Parameter.{name}[{i}]", $"File {{ name: {file.FileName}, size: {size}, contentType: {file.ContentType} }}");
                    }

                    continue;
                }
            }
            // 处理 byte[] 参数类型
            else if (value is byte[] byteArray)
            {
                templates.Add($"##{name} ({type.Name})## [Length]: {byteArray.Length}");
                logContext.Set($"{LOG_CONTEXT_NAME}.Parameter.{name}", $"Byte[{byteArray.Length}]");
                continue;
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
            logContext.Set($"{LOG_CONTEXT_NAME}.Parameter.{name}", rawValue);
        }

        return templates;
    }

    /// <summary>
    /// 生成返回值信息日志模板
    /// </summary>
    /// <param name="logContext"></param>
    /// <param name="resultContext"></param>
    /// <param name="method"></param>
    /// <returns></returns>
    private static List<string> GenerateReturnInfomationTemplate(LogContext logContext, ActionExecutedContext resultContext, MethodInfo method)
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
        logContext.Set($"{LOG_CONTEXT_NAME}.Return.Type", method.ReturnType.FullName)
                  .Set($"{LOG_CONTEXT_NAME}.Return.Value", displayValue);

        return templates;
    }

    /// <summary>
    /// 生成异常信息日志模板
    /// </summary>
    /// <param name="logContext"></param>
    /// <param name="exception"></param>
    /// <returns></returns>
    private static List<string> GenerateExcetpionInfomationTemplate(LogContext logContext, Exception exception)
    {
        var templates = new List<string>();

        if (exception == null) return templates;

        templates.AddRange(new[]
        {
            $"━━━━━━━━━━━━━━━  异常信息 ━━━━━━━━━━━━━━━"
            , $"##类型## {exception.GetType().FullName}"
            , $"##消息## {exception.Message}"
            , $"##错误堆栈## {exception.StackTrace}"
        });
        logContext.Set($"{LOG_CONTEXT_NAME}.Exception.Type", exception.GetType().FullName)
                  .Set($"{LOG_CONTEXT_NAME}.Exception.Message", exception.Message)
                  .Set($"{LOG_CONTEXT_NAME}.Exception.StackTrace", exception.StackTrace);

        return templates;
    }
}