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
using System.Diagnostics;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace Furion.Logging;

/// <summary>
/// 日志监视器
/// </summary>
/// <remarks>主要用于将请求的信息打印出来</remarks>
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
public sealed class LoggingMonitorAttribute : Attribute, IAsyncActionFilter
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public LoggingMonitorAttribute()
    {
    }

    /// <summary>
    /// 显示调用堆栈
    /// </summary>
    public bool StackTrace { get; set; } = false;

    /// <summary>
    /// 监视 Action 执行
    /// </summary>
    /// <param name="context"></param>
    /// <param name="next"></param>
    /// <returns></returns>
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        // 获取控制器/操作描述器材
        var controllerActionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;

        // 获取路由表信息
        var routeData = context.RouteData;
        var controllerName = routeData.Values["controller"];
        var actionName = routeData.Values["action"];
        var areaName = routeData.DataTokens["area"];

        // 调用呈现链
        var displayName = controllerActionDescriptor.DisplayName;

        // 获取请求方法
        var actionMethod = controllerActionDescriptor.MethodInfo;

        // 获取 HttpContext 和 HttpRequest 对象
        var httpContext = context.HttpContext;
        var httpRequest = httpContext.Request;

        // 获取客户端 IPv4 地址
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
        var parameters = context.ActionArguments;

        // 获取授权用户
        var user = httpContext.User;

        // token 信息
        var token = httpRequest.Headers["Authorization"].ToString();

        // 解析 token 内容

        // 客户端浏览器信息
        var userAgent = httpRequest.Headers["User-Agent"];

        var resultContext = await next();

        // 获取异常对象情况
        var exception = resultContext.Exception;

        // 获取调用堆栈
        var stackTrace = EnhancedStackTrace.Current();

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
        };

        // 授权信息
        if (user.Claims.Any())
        {
            monitorItems.AddRange(new[]
            {
                ""
                , $"━━━━━━━━━━━━━━  授权信息 ━━━━━━━━━━━━━━"
                , $"##JWT Token## {token}"
                , $""
            });

            // 遍历身份信息
            foreach (var claim in user.Claims)
            {
                monitorItems.Add($"##{claim.Type} ({(claim.ValueType.Replace("http://www.w3.org/2001/XMLSchema#", ""))})## {claim.Value}");
            }
        }

        // 处理参数
        if (parameters.Count > 0)
        {
            monitorItems.AddRange(new[]
            {
                ""
                , $"━━━━━━━━━━━━━━  参数列表 ━━━━━━━━━━━━━━"
                , $"##Content-Type## {httpRequest.Headers.ContentType}"
                , $""
            });

            // 遍历参数
            foreach (var parameter in actionMethod.GetParameters())
            {
                var name = parameter.Name;
                var value = parameters[name];
                var type = parameter.ParameterType;

                object rawValue = default;

                if (value is IFormFile || value is List<IFormFile>)
                {
                    if (value is IFormFile formFile)
                    {
                        rawValue = $"[name]: {formFile.FileName}; [size]: {(Math.Round(formFile.Length / 1024D))}kb; [content-type]: {formFile.ContentType}";
                    }
                    else if (value is List<IFormFile> formFiles)
                    {
                        for (var i = 0; i < formFiles.Count; i++)
                        {
                            var file = formFiles[i];
                            monitorItems.Add($"##{name}{i} ({nameof(IFormFile)})## [name]: {file.FileName}; [size]: {(Math.Round(file.Length / 1024D))}kb; [content-type]: {file.ContentType}");
                        }

                        continue;
                    }
                }
                else if (type.IsPrimitive || value is string || value == null) rawValue = value;
                else
                {
                    rawValue = JsonSerializer.Serialize(value, new JsonSerializerOptions
                    {
                        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                    });
                }

                monitorItems.Add($"##{name} ({type.Name})## {rawValue}");
            }
        }

        // 返回值
        object returnValue = null;
        if (UnifyContext.CheckVaildResult(resultContext.Result, out var data)) returnValue = data;
        var jsonReturnValue = actionMethod.ReturnType == typeof(void)
            ? string.Empty
            : JsonSerializer.Serialize(returnValue, new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            });

        monitorItems.AddRange(new[]
        {
            ""
            , $"━━━━━━━━━━━━━━  返回信息 ━━━━━━━━━━━━━━"
            , $"##类型## {actionMethod.ReturnType.FullName}"
            , $"##返回值## {jsonReturnValue}"
        });

        // 处理异常
        if (exception != null)
        {
            monitorItems.AddRange(new[]
            {
                ""
                , $"━━━━━━━━━━━━━━  异常信息 ━━━━━━━━━━━━━━"
                , $"##类型## {exception.GetType()}"
                , $"##消息## {exception.Message}"
                , $"##错误堆栈## {exception.StackTrace}"
            });
        }

        var monitor = TP.Wrapper("Logging Monitor", displayName, monitorItems.ToArray());

        // 创建日志工厂并写入
        var loggerFactory = httpContext.RequestServices.GetRequiredService<ILoggerFactory>();
        var logger = loggerFactory.CreateLogger(displayName);

        logger.LogInformation(monitor);
    }
}