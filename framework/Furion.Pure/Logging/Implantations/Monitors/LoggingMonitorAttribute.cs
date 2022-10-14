// MIT License
//
// Copyright (c) 2020-2022 百小僧, Baiqian Co.,Ltd and Contributors
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

using Furion.DataValidation;
using Furion.FriendlyException;
using Furion.Logging;
using Furion.Templates;
using Furion.UnifyResult;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Logging;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace System;

/// <summary>
/// 强大的日志监听器
/// </summary>
/// <remarks>主要用于将请求的信息打印出来</remarks>
[SuppressSniffer, AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
public sealed class LoggingMonitorAttribute : Attribute, IAsyncActionFilter, IOrderedFilter
{
    /// <summary>
    /// 过滤器排序
    /// </summary>
    private const int FilterOrder = -2000;

    /// <summary>
    /// 排序属性
    /// </summary>
    public int Order => FilterOrder;

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
    /// 是否记录返回值
    /// </summary>
    /// <remarks>bool 类型，默认输出</remarks>
    public object WithReturnValue { get; set; } = null;

    /// <summary>
    /// 设置返回值阈值
    /// </summary>
    /// <remarks>配置返回值字符串阈值，超过这个阈值将截断，默认全量输出</remarks>
    public object ReturnValueThreshold { get; set; } = null;

    /// <summary>
    /// 配置 Json 输出行为
    /// </summary>
    public object JsonBehavior { get; set; } = null;

    /// <summary>
    /// 配置序列化忽略的属性名称
    /// </summary>
    public string[] IgnorePropertyNames { get; set; }

    /// <summary>
    /// 配置序列化忽略的属性类型
    /// </summary>
    public Type[] IgnorePropertyTypes { get; set; }

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
        // 判断是否是 Razor Pages
        var isPageDescriptor = context.ActionDescriptor is CompiledPageActionDescriptor;

        // 抛出不支持 Razor Pages 异常
        if (isPageDescriptor) throw new InvalidOperationException("The LoggingMonitorAttribute is not support the Razor Pages application.");

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
        var isDefinedScopedAttribute = actionMethod.IsDefined(typeof(LoggingMonitorAttribute), true);

        // 解决局部和全局触发器同时配置触发两次问题
        if (isDefinedScopedAttribute && Settings.FromGlobalFilter == true)
        {
            _ = await next();
            return;
        }

        if (!isDefinedScopedAttribute)
        {
            // 解决通过 AddMvcFilter 的问题
            if (!Settings.IsMvcFilterRegister)
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
        }

        // 判断是否自定义了日志筛选器，如果是则检查是否符合条件
        if (LoggingMonitorSettings.InternalWriteFilter?.Invoke(context) == false)
        {
            _ = await next();
            return;
        }

        // 创建 json 写入器
        using var stream = new MemoryStream();
        using var writer = new Utf8JsonWriter(stream, Settings.JsonWriterOptions);
        writer.WriteStartObject();

        // 获取全局 LoggingMonitorMethod 配置
        var monitorMethod = Settings.MethodsSettings.FirstOrDefault(m => m.FullName.Equals(methodFullName, StringComparison.OrdinalIgnoreCase));

        // 创建日志上下文
        var logContext = new LogContext();

        // 获取路由表信息
        var routeData = context.RouteData;
        var controllerName = routeData.Values["controller"];
        var actionName = routeData.Values["action"];
        var areaName = routeData.DataTokens["area"];
        writer.WriteString(nameof(controllerName), controllerName?.ToString());
        writer.WriteString("controllerTypeName", controllerActionDescriptor.ControllerTypeInfo.Name);
        writer.WriteString(nameof(actionName), actionName?.ToString());
        writer.WriteString("actionTypeName", actionMethod.Name);
        writer.WriteString("areaName", areaName?.ToString());

        // 调用呈现链名称
        var displayName = controllerActionDescriptor.DisplayName;
        writer.WriteString(nameof(displayName), displayName);

        // 获取 HttpContext 和 HttpRequest 对象
        var httpContext = context.HttpContext;
        var httpRequest = httpContext.Request;

        // 获取服务端 IPv4 地址
        var localIPv4 = httpContext.GetLocalIpAddressToIPv4();
        writer.WriteString(nameof(localIPv4), localIPv4);

        // 获取客户端 IPv4 地址
        var remoteIPv4 = httpContext.GetRemoteIpAddressToIPv4();
        writer.WriteString(nameof(remoteIPv4), remoteIPv4);

        // 获取请求方式
        var httpMethod = httpContext.Request.Method;
        writer.WriteString(nameof(httpMethod), httpMethod);

        // 获取请求的 Url 地址
        var requestUrl = Uri.UnescapeDataString(httpRequest.GetRequestUrlAddress());
        writer.WriteString(nameof(requestUrl), requestUrl);

        // 获取来源 Url 地址
        var refererUrl = Uri.UnescapeDataString(httpRequest.GetRefererUrlAddress());
        writer.WriteString(nameof(refererUrl), refererUrl);

        // 服务器环境
        var environment = httpContext.RequestServices.GetRequiredService<IWebHostEnvironment>().EnvironmentName;
        writer.WriteString(nameof(environment), environment);

        // 客户端浏览器信息
        var userAgent = httpRequest.Headers["User-Agent"];
        writer.WriteString(nameof(userAgent), userAgent);

        // 获取方法参数
        var parameterValues = context.ActionArguments;

        // 获取授权用户
        var user = httpContext.User;

        // token 信息
        var authorization = httpRequest.Headers["Authorization"].ToString();
        writer.WriteString("requestHeaderAuthorization", authorization);

        // 计算接口执行时间
        var timeOperation = Stopwatch.StartNew();
        var resultContext = await next();
        timeOperation.Stop();
        writer.WriteNumber("timeOperationElapsedMilliseconds", timeOperation.ElapsedMilliseconds);

        // 获取异常对象情况
        var exception = resultContext.Exception;
        if (exception == null)
        {
            // 解析存储的验证信息
            var validationFailedKey = nameof(DataValidationFilter) + nameof(ValidationMetadata);
            var validationMetadata = !httpContext.Items.ContainsKey(validationFailedKey)
                ? default
                : httpContext.Items[validationFailedKey] as ValidationMetadata;

            if (validationMetadata != null)
            {
                // 创建全局验证友好异常
                var error = TrySerializeObject(validationMetadata.ValidationResult, monitorMethod, out _);
                exception = new AppFriendlyException(error, validationMetadata.OriginErrorCode)
                {
                    ErrorCode = validationMetadata.ErrorCode,
                    StatusCode = validationMetadata.StatusCode ?? StatusCodes.Status400BadRequest,
                    ValidationException = true
                };
            }
        }

        // 判断是否是验证异常
        var isValidationException = exception is AppFriendlyException friendlyException && friendlyException.ValidationException;

        var monitorItems = new List<string>()
        {
            $"##控制器名称## {controllerActionDescriptor.ControllerTypeInfo.Name}"
            , $"##操作名称## {actionMethod.Name}"
            , $"##路由信息## [area]: {areaName}; [controller]: {controllerName}; [action]: {actionName}"
            , $"##请求方式## {httpMethod}"
            , $"##请求地址## {requestUrl}"
            , $"##来源地址## {refererUrl}"
            , $"##浏览器标识## {userAgent}"
            , $"##客户端 IP 地址## {remoteIPv4}"
            , $"##服务端 IP 地址## {localIPv4}"
            , $"##服务端运行环境## {environment}"
            , $"##执行耗时## {timeOperation.ElapsedMilliseconds}ms"
        };

        // 添加 JWT 授权信息日志模板
        monitorItems.AddRange(GenerateAuthorizationTemplate(writer, user, authorization));

        // 添加请求参数信息日志模板
        monitorItems.AddRange(GenerateParameterTemplate(writer, parameterValues, actionMethod, httpRequest.Headers["Content-Type"], monitorMethod));

        // 判断是否启用返回值打印
        if (CheckIsSetWithReturnValue(monitorMethod))
        {
            // 添加返回值信息日志模板
            monitorItems.AddRange(GenerateReturnInfomationTemplate(writer, resultContext, actionMethod, monitorMethod));
        }

        // 添加异常信息日志模板
        monitorItems.AddRange(GenerateExcetpionInfomationTemplate(writer, exception, isValidationException));

        // 生成最终模板
        var monitorMessage = TP.Wrapper(Title, displayName, monitorItems.ToArray());

        // 创建日志记录器
        var logger = httpContext.RequestServices.GetRequiredService<ILogger<LoggingMonitor>>();

        // 调用外部配置
        LoggingMonitorSettings.Configure?.Invoke(logger, logContext, resultContext);

        writer.WriteEndObject();
        writer.Flush();

        // 获取 json 字符串
        var jsonString = Encoding.UTF8.GetString(stream.ToArray());
        logContext.Set("loggingMonitor", jsonString);

        // 设置日志上下文
        using var scope = logger.ScopeContext(logContext);

        // 获取最终写入日志消息格式
        var finalMessage = GetJsonBehavior(JsonBehavior, monitorMethod) == Furion.Logging.JsonBehavior.OnlyJson ? jsonString : monitorMessage;

        // 写入日志，如果没有异常使用 LogInformation，否则使用 LogError
        if (exception == null) logger.LogInformation(finalMessage);
        else
        {
            // 如果不是验证异常，写入 Error
            if (!isValidationException) logger.LogError(exception, finalMessage);
            else
            {
                // 读取配置的日志级别并写入
                logger.Log(Settings.BahLogLevel, finalMessage);
            }
        }
    }

    /// <summary>
    /// 生成 JWT 授权信息日志模板
    /// </summary>
    /// <param name="writer"></param>
    /// <param name="claimsPrincipal"></param>
    /// <param name="authorization"></param>
    /// <returns></returns>
    private List<string> GenerateAuthorizationTemplate(Utf8JsonWriter writer, ClaimsPrincipal claimsPrincipal, StringValues authorization)
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
        writer.WritePropertyName("authorizationClaims");
        writer.WriteStartArray();
        foreach (var claim in claimsPrincipal.Claims)
        {
            var valueType = claim.ValueType.Replace("http://www.w3.org/2001/XMLSchema#", "");
            writer.WriteStartObject();
            templates.Add($"##{claim.Type} ({valueType})## {claim.Value}");
            writer.WriteString("type", claim.Type);
            writer.WriteString("valueType", valueType);
            writer.WriteString("value", claim.Value);
            writer.WriteEndObject();
        }
        writer.WriteEndArray();

        return templates;
    }

    /// <summary>
    /// 生成请求参数信息日志模板
    /// </summary>
    /// <param name="writer"></param>
    /// <param name="parameterValues"></param>
    /// <param name="method"></param>
    /// <param name="contentType"></param>
    /// <param name="monitorMethod"></param>
    /// <returns></returns>
    private List<string> GenerateParameterTemplate(Utf8JsonWriter writer, IDictionary<string, object> parameterValues, MethodInfo method, StringValues contentType, LoggingMonitorMethod monitorMethod)
    {
        var templates = new List<string>();
        writer.WritePropertyName("parameters");

        if (parameterValues.Count == 0)
        {
            writer.WriteStartArray();
            writer.WriteEndArray();
            return templates;
        }

        templates.AddRange(new[]
        {
            $"━━━━━━━━━━━━━━━  参数列表 ━━━━━━━━━━━━━━━"
            , $"##Content-Type## {contentType}"
            , $""
        });

        var parameters = method.GetParameters();

        writer.WriteStartArray();
        foreach (var parameter in parameters)
        {
            var name = parameter.Name;
            _ = parameterValues.TryGetValue(name, out var value);
            var parameterType = parameter.ParameterType;
            writer.WriteStartObject();
            writer.WriteString("name", name);
            writer.WriteString("type", HandleGenericType(parameterType));

            object rawValue = default;

            // 文件类型参数
            if (value is IFormFile || value is List<IFormFile>)
            {
                writer.WritePropertyName("value");

                // 单文件
                if (value is IFormFile formFile)
                {
                    var fileSize = Math.Round(formFile.Length / 1024D);
                    templates.Add($"##{name} ({parameterType.Name})## [name]: {formFile.FileName}; [size]: {fileSize}KB; [content-type]: {formFile.ContentType}");

                    writer.WriteStartObject();
                    writer.WriteString(name, formFile.Name);
                    writer.WriteString("fileName", formFile.FileName);
                    writer.WriteNumber("length", formFile.Length);
                    writer.WriteString("contentType", formFile.ContentType);
                    writer.WriteEndObject();

                    goto writeEndObject;
                }
                // 多文件
                else if (value is List<IFormFile> formFiles)
                {
                    writer.WriteStartArray();
                    for (var i = 0; i < formFiles.Count; i++)
                    {
                        var file = formFiles[i];
                        var size = Math.Round(file.Length / 1024D);
                        templates.Add($"##{name}[{i}] ({nameof(IFormFile)})## [name]: {file.FileName}; [size]: {size}KB; [content-type]: {file.ContentType}");

                        writer.WriteStartObject();
                        writer.WriteString(name, file.Name);
                        writer.WriteString("fileName", file.FileName);
                        writer.WriteNumber("length", file.Length);
                        writer.WriteString("contentType", file.ContentType);
                        writer.WriteEndObject();
                    }
                    writer.WriteEndArray();

                    goto writeEndObject;
                }
            }
            // 处理 byte[] 参数类型
            else if (value is byte[] byteArray)
            {
                writer.WritePropertyName("value");
                templates.Add($"##{name} ({parameterType.Name})## [Length]: {byteArray.Length}");

                writer.WriteStartObject();
                writer.WriteNumber("length", byteArray.Length);
                writer.WriteEndObject();

                goto writeEndObject;
            }
            // 处理基元类型，字符串类型和空值
            else if (parameterType.IsPrimitive || value is string || value == null)
            {
                writer.WritePropertyName("value");
                rawValue = value;

                if (value == null) writer.WriteNullValue();
                else if (value is string str) writer.WriteStringValue(str);
                else if (double.TryParse(value.ToString(), out var r)) writer.WriteNumberValue(r);
                else writer.WriteStringValue(value.ToString());
            }
            // 其他类型统一进行序列化
            else
            {
                writer.WritePropertyName("value");
                rawValue = TrySerializeObject(value, monitorMethod, out var succeed);

                if (succeed) writer.WriteRawValue(rawValue?.ToString());
                else writer.WriteNullValue();
            }

            templates.Add($"##{name} ({parameterType.Name})## {rawValue}");

        writeEndObject: writer.WriteEndObject();
        }
        writer.WriteEndArray();

        return templates;
    }

    /// <summary>
    /// 生成返回值信息日志模板
    /// </summary>
    /// <param name="writer"></param>
    /// <param name="resultContext"></param>
    /// <param name="method"></param>
    /// <param name="monitorMethod"></param>
    /// <returns></returns>
    private List<string> GenerateReturnInfomationTemplate(Utf8JsonWriter writer, ActionExecutedContext resultContext, MethodInfo method, LoggingMonitorMethod monitorMethod)
    {
        var templates = new List<string>();

        object returnValue = null;
        Type finalReturnType;

        // 解析返回值
        if (UnifyContext.CheckVaildResult(resultContext.Result, out var data))
        {
            returnValue = data;
            finalReturnType = data?.GetType();
        }
        // 处理文件类型
        else if (resultContext.Result is FileResult fresult)
        {
            returnValue = new {
                FileName = fresult.FileDownloadName,
                fresult.ContentType,
                Length = fresult is FileContentResult cresult ? (object)cresult.FileContents.Length : null
            };
            finalReturnType = fresult?.GetType();
        }
        else finalReturnType = resultContext.Result?.GetType();

        var succeed = true;
        // 获取最终呈现值（字符串类型）
        var displayValue = method.ReturnType == typeof(void)
            ? string.Empty
            : TrySerializeObject(returnValue, monitorMethod, out succeed);
        var originValue = displayValue;

        // 获取返回值阈值
        var threshold = GetReturnValueThreshold(monitorMethod);
        if (threshold > 0)
        {
            displayValue = displayValue.Length <= threshold ? displayValue : displayValue[..threshold];
        }

        var returnTypeName = HandleGenericType(method.ReturnType);
        var finalReturnTypeName = HandleGenericType(finalReturnType);

        templates.AddRange(new[]
        {
            $"━━━━━━━━━━━━━━━  返回信息 ━━━━━━━━━━━━━━━"
            , $"##原始类型## {returnTypeName}"
            , $"##最终类型## {finalReturnTypeName}"
            , $"##最终返回值## {displayValue}"
        });

        writer.WritePropertyName("returnInformation");
        writer.WriteStartObject();
        writer.WriteString("type", finalReturnTypeName);
        writer.WriteString("actType", returnTypeName);
        writer.WritePropertyName("value");
        if (succeed && method.ReturnType != typeof(void) && returnValue != null)
        {
            // 解决返回值被截断后 json 验证失败异常问题
            if (threshold > 0 && originValue != displayValue)
            {
                writer.WriteStringValue(displayValue);
            }
            else writer.WriteRawValue(displayValue);
        }
        else writer.WriteNullValue();
        writer.WriteEndObject();

        return templates;
    }

    /// <summary>
    /// 生成异常信息日志模板
    /// </summary>
    /// <param name="writer"></param>
    /// <param name="exception"></param>
    /// <param name="isValidationException">是否是验证异常</param>
    /// <returns></returns>
    private List<string> GenerateExcetpionInfomationTemplate(Utf8JsonWriter writer, Exception exception, bool isValidationException)
    {
        var templates = new List<string>();

        if (exception == null)
        {
            writer.WritePropertyName("exception");
            writer.WriteNullValue();

            writer.WritePropertyName("validation");
            writer.WriteNullValue();
            return templates;
        }

        // 处理不是验证异常情况
        if (!isValidationException)
        {
            var exceptionTypeName = HandleGenericType(exception.GetType());
            templates.AddRange(new[]
            {
                $"━━━━━━━━━━━━━━━  异常信息 ━━━━━━━━━━━━━━━"
                , $"##类型## {exceptionTypeName}"
                , $"##消息## {exception.Message}"
                , $"##错误堆栈## {exception.StackTrace}"
            });

            writer.WritePropertyName("exception");
            writer.WriteStartObject();
            writer.WriteString("type", exceptionTypeName);
            writer.WriteString("message", exception.Message);
            writer.WriteString("stackTrace", exception.StackTrace.ToString());
            writer.WriteEndObject();

            writer.WritePropertyName("validation");
            writer.WriteNullValue();
        }
        else
        {
            var friendlyException = exception as AppFriendlyException;
            templates.AddRange(new[]
            {
                $"━━━━━━━━━━━━━━━  业务异常 ━━━━━━━━━━━━━━━"
                , $"##业务码## {friendlyException.ErrorCode}"
                , $"##业务码（原）## {friendlyException.OriginErrorCode}"
                , $"##业务消息## {friendlyException.ErrorMessage}"
            });

            writer.WritePropertyName("exception");
            writer.WriteNullValue();

            writer.WritePropertyName("validation");
            writer.WriteStartObject();
            writer.WriteString("errorCode", friendlyException.ErrorCode?.ToString());
            writer.WriteString("originErrorCode", friendlyException.OriginErrorCode?.ToString());
            writer.WriteString("message", friendlyException.Message);
            writer.WriteEndObject();
        }

        return templates;
    }

    /// <summary>
    /// 序列化对象
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="monitorMethod"></param>
    /// <param name="succeed"></param>
    /// <returns></returns>
    private string TrySerializeObject(object obj, LoggingMonitorMethod monitorMethod, out bool succeed)
    {
        try
        {
            // 序列化默认配置
            var jsonSerializerSettings = new JsonSerializerSettings()
            {
                ContractResolver = new IgnorePropertiesContractResolver(GetIgnorePropertyNames(monitorMethod), GetIgnorePropertyTypes(monitorMethod)),
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            };
            // 解决 long 精度问题
            jsonSerializerSettings.Converters.AddLongTypeConverters();

            var result = Newtonsoft.Json.JsonConvert.SerializeObject(obj, jsonSerializerSettings);

            succeed = true;
            return result;
        }
        catch
        {
            succeed = true;
            return "<Error Serialize>";
        }
    }

    /// <summary>
    /// 检查是否开启启用返回值
    /// </summary>
    /// <param name="monitorMethod"></param>
    /// <returns></returns>
    private bool CheckIsSetWithReturnValue(LoggingMonitorMethod monitorMethod)
    {
        return WithReturnValue == null
            ? (monitorMethod?.WithReturnValue ?? Settings.WithReturnValue)
            : Convert.ToBoolean(WithReturnValue);
    }

    /// <summary>
    /// 获取返回值阈值
    /// </summary>
    /// <param name="monitorMethod"></param>
    /// <returns></returns>
    private int GetReturnValueThreshold(LoggingMonitorMethod monitorMethod)
    {
        return ReturnValueThreshold == null
            ? (monitorMethod?.ReturnValueThreshold ?? Settings.ReturnValueThreshold)
            : Convert.ToInt32(ReturnValueThreshold);
    }

    /// <summary>
    /// 获取 Json 输出行为
    /// </summary>
    /// <param name="jsonBehavior"></param>
    /// <param name="monitorMethod"></param>
    /// <returns></returns>
    private JsonBehavior GetJsonBehavior(object jsonBehavior, LoggingMonitorMethod monitorMethod)
    {
        return jsonBehavior == null
            ? (monitorMethod?.JsonBehavior ?? Settings.JsonBehavior)
            : (JsonBehavior)jsonBehavior;
    }

    /// <summary>
    /// 获取忽略序列化属性名称集合
    /// </summary>
    /// <param name="monitorMethod"></param>
    /// <returns></returns>
    private string[] GetIgnorePropertyNames(LoggingMonitorMethod monitorMethod)
    {
        IEnumerable<string> ignorePropertyNamesList = IgnorePropertyNames ?? Array.Empty<string>();

        return ignorePropertyNamesList.Concat(monitorMethod?.IgnorePropertyNames ?? Array.Empty<string>())
                                      .Concat(Settings.IgnorePropertyNames ?? Array.Empty<string>())
                                      .ToArray();
    }

    /// <summary>
    /// 获取忽略序列化属性类型集合
    /// </summary>
    /// <param name="monitorMethod"></param>
    /// <returns></returns>
    private Type[] GetIgnorePropertyTypes(LoggingMonitorMethod monitorMethod)
    {
        IEnumerable<Type> ignorePropertyTypesList = IgnorePropertyTypes ?? Array.Empty<Type>();

        return ignorePropertyTypesList.Concat(monitorMethod?.IgnorePropertyTypes ?? Array.Empty<Type>())
                                      .Concat(Settings.IgnorePropertyTypes ?? Array.Empty<Type>())
                                      .ToArray();
    }

    /// <summary>
    /// 处理泛型类型转字符串打印问题
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    private static string HandleGenericType(Type type)
    {
        if (type == null) return string.Empty;

        var typeName = type.FullName;

        // 处理泛型类型问题
        if (type.IsConstructedGenericType)
        {
            var prefix = type.GetGenericArguments()
                .Select(genericArg => HandleGenericType(genericArg))
                .Aggregate((previous, current) => previous + current);

            // 通过 _ 拼接多个泛型
            typeName = typeName.Split('`').First() + "_" + prefix;
        }

        return typeName;
    }
}