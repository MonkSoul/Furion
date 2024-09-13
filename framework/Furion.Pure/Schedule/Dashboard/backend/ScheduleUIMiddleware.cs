// ------------------------------------------------------------------------
// 版权信息
// 版权归百小僧及百签科技（广东）有限公司所有。
// 所有权利保留。
// 官方网站：https://baiqian.com
//
// 许可证信息
// Furion 项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。
// 许可证的完整文本可以在源代码树根目录中的 LICENSE-APACHE 和 LICENSE-MIT 文件中找到。
// 官方网站：https://furion.net
//
// 使用条款
// 使用本代码应遵守相关法律法规和许可证的要求。
//
// 免责声明
// 对于因使用本代码而产生的任何直接、间接、偶然、特殊或后果性损害，我们不承担任何责任。
//
// 其他重要信息
// Furion 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。
// 有关 Furion 项目的其他详细信息，请参阅位于源代码树根目录中的 COPYRIGHT 和 DISCLAIMER 文件。
//
// 更多信息
// 请访问 https://gitee.com/dotnetchina/Furion 获取更多关于 Furion 项目的许可证和版权信息。
// ------------------------------------------------------------------------

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Collections.Concurrent;
using System.Text;
using System.Text.Json;

namespace Furion.Schedule;

/// <summary>
/// Schedule 模块 UI 中间件
/// </summary>
[SuppressSniffer]
public sealed class ScheduleUIMiddleware
{
    private const string STATIC_FILES_PATH = "/__schedule__";

    /// <summary>
    /// 请求委托
    /// </summary>
    private readonly RequestDelegate _next;

    /// <summary>
    /// 作业计划工厂
    /// </summary>
    private readonly ISchedulerFactory _schedulerFactory;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="next">请求委托</param>
    /// <param name="schedulerFactory">作业计划工厂</param>
    /// <param name="options">UI 配置选项</param>
    public ScheduleUIMiddleware(RequestDelegate next
        , ISchedulerFactory schedulerFactory
        , ScheduleUIOptions options)
    {
        _next = next;
        _schedulerFactory = schedulerFactory;
        Options = options;
        ApiRequestPath = $"{options.RequestPath}/api";
    }

    /// <summary>
    /// UI 配置选项
    /// </summary>
    public ScheduleUIOptions Options { get; }

    /// <summary>
    /// API 入口地址
    /// </summary>
    public string ApiRequestPath { get; }

    /// <summary>
    /// 中间件执行方法
    /// </summary>
    /// <param name="context"><see cref="HttpContext"/></param>
    /// <returns><see cref="Task"/></returns>
    public async Task InvokeAsync(HttpContext context)
    {
        // 非看板请求跳过
        if (!context.Request.Path.StartsWithSegments(Options.RequestPath, StringComparison.OrdinalIgnoreCase))
        {
            await _next(context);
            return;
        }

        // ================================ 处理静态文件请求 ================================
        var staticFilePath = Options.RequestPath + "/";
        if (context.Request.Path.Equals(staticFilePath, StringComparison.OrdinalIgnoreCase) || context.Request.Path.Equals(staticFilePath + "apiconfig.js", StringComparison.OrdinalIgnoreCase))
        {
            var targetPath = context.Request.Path.Value?[staticFilePath.Length..];
            var isIndex = string.IsNullOrEmpty(targetPath);

            // 获取当前类型所在程序集和对应嵌入式文件路径
            var currentAssembly = typeof(ScheduleUIExtensions).Assembly;

            // 读取配置文件内容
            byte[] buffer;
            using (var readStream = currentAssembly.GetManifestResourceStream($"{currentAssembly.GetName().Name}.Schedule.Dashboard.frontend.{(isIndex ? "index.html" : targetPath)}"))
            {
                buffer = new byte[readStream.Length];
                _ = await readStream.ReadAsync(buffer);
            }

            // 替换配置占位符
            string content;
            using (var stream = new MemoryStream(buffer))
            {
                using var streamReader = new StreamReader(stream, new UTF8Encoding(false));
                content = await streamReader.ReadToEndAsync();
                content = isIndex
                    ? content.Replace(STATIC_FILES_PATH, $"{Options.VirtualPath}{Options.RequestPath}")
                    : content.Replace("%(RequestPath)", $"{Options.VirtualPath}{Options.RequestPath}")
                             .Replace("%(DisplayEmptyTriggerJobs)", Options.DisplayEmptyTriggerJobs ? "true" : "false")
                             .Replace("%(DisplayHead)", Options.DisplayHead ? "true" : "false")
                             .Replace("%(DefaultExpandAllJobs)", Options.DefaultExpandAllJobs ? "true" : "false")
                             .Replace("%(UseUtcTimestamp)", ScheduleOptionsBuilder.UseUtcTimestampProperty ? "true" : "false");
            }

            // 输出到客户端
            context.Response.ContentType = $"text/{(isIndex ? "html" : "javascript")}; charset=utf-8";
            await context.Response.WriteAsync(content);
            return;
        }

        // ================================ 处理 API 请求 ================================

        // 如果不是以 API_REQUEST_PATH 开头，则跳过
        if (!context.Request.Path.StartsWithSegments(ApiRequestPath, StringComparison.OrdinalIgnoreCase))
        {
            await _next(context);
            return;
        }

        // 只处理 GET/POST 请求
        if (context.Request.Method.ToUpper() != "GET" && context.Request.Method.ToUpper() != "POST")
        {
            await _next(context);
            return;
        }

        // 获取匹配的路由标识
        var action = context.Request.Path.Value?[ApiRequestPath.Length..]?.ToLower();

        // 允许跨域，设置返回 json
        context.Response.ContentType = "application/json; charset=utf-8";
        context.Response.Headers["Access-Control-Allow-Origin"] = "*";
        context.Response.Headers["Access-Control-Allow-Headers"] = "*";

        // 路由匹配
        switch (action)
        {
            // 获取所有作业
            case "/get-jobs":
                var jobs = _schedulerFactory.GetJobsOfModels().OrderBy(u => u.JobDetail.GroupName);

                // 输出 JSON
                await context.Response.WriteAsync(SerializeToJson(jobs));
                break;
            // 操作作业
            case "/operate-job":
                // 获取作业 Id
                var jobId = context.Request.Query["jobid"];
                // 获取操作方法
                var operate = context.Request.Query["action"];

                // 获取作业计划
                var scheduleResult = _schedulerFactory.TryGetJob(jobId, out var scheduler);

                // 处理找不到作业情况
                if (scheduleResult != ScheduleResult.Succeed)
                {
                    // 标识状态码为 500
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                    // 输出 JSON
                    await context.Response.WriteAsync(SerializeToJson(new {
                        msg = scheduleResult.ToString(),
                        ok = false
                    }));

                    return;
                }

                switch (operate)
                {
                    // 启动作业
                    case "start":
                        scheduler?.Start();
                        break;
                    // 暂停作业
                    case "pause":
                        scheduler?.Pause();
                        break;
                    // 移除作业
                    case "remove":
                        _schedulerFactory.RemoveJob(jobId);
                        break;
                    // 立即执行
                    case "run":
                        _schedulerFactory.RunJob(jobId);
                        break;
                }

                // 输出 JSON
                await context.Response.WriteAsync(SerializeToJson(new {
                    msg = ScheduleResult.Succeed.ToString(),
                    ok = true
                }));

                break;
            // 操作触发器
            case "/operate-trigger":
                // 获取作业 Id
                var jobId1 = context.Request.Query["jobid"];
                var triggerId = context.Request.Query["triggerid"];
                // 获取操作方法
                var operate1 = context.Request.Query["action"];

                // 获取作业计划
                var scheduleResult1 = _schedulerFactory.TryGetJob(jobId1, out var scheduler1);

                // 处理找不到作业情况
                if (scheduleResult1 != ScheduleResult.Succeed)
                {
                    // 标识状态码为 500
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                    // 输出 JSON
                    await context.Response.WriteAsync(SerializeToJson(new {
                        msg = scheduleResult1.ToString(),
                        ok = false
                    }));

                    return;
                }

                switch (operate1)
                {
                    // 启动作业触发器
                    case "start":
                        scheduler1?.StartTrigger(triggerId);
                        break;
                    // 暂停作业触发器
                    case "pause":
                        scheduler1?.PauseTrigger(triggerId);
                        break;
                    // 移除作业触发器
                    case "remove":
                        scheduler1?.RemoveTrigger(triggerId);
                        break;
                    // 立即执行
                    case "run":
                        scheduler1?.Run(triggerId);
                        break;
                    // 获取作业触发器最近运行时间
                    case "timelines":
                        var trigger = scheduler1?.GetTrigger(triggerId);
                        var timelines = trigger?.GetTimelines() ?? Array.Empty<TriggerTimeline>();

                        // 输出 JSON
                        await context.Response.WriteAsync(SerializeToJson(timelines));
                        return;
                }

                // 输出 JSON
                await context.Response.WriteAsync(SerializeToJson(new {
                    msg = ScheduleResult.Succeed.ToString(),
                    ok = true
                }));

                break;

            // 推送更新
            case "/check-change":
                // 检查请求类型，是否为 text/event-stream 格式
                if (!context.WebSockets.IsWebSocketRequest && context.Request.Headers["Accept"].ToString().Contains("text/event-stream"))
                {
                    // 设置响应头的 content-type 为 text/event-stream
                    context.Response.ContentType = "text/event-stream";

                    // 设置响应头，启用响应发送保持活动性
                    context.Response.Headers.CacheControl = "no-cache";
                    context.Response.Headers.Connection = "keep-alive";

                    // 防止 Nginx 缓存 Server-Sent Events
                    context.Response.Headers["X-Accel-Buffering"] = "no";

                    var queue = new BlockingCollection<JobDetail>();

                    // 监听作业计划变化
                    void Subscribe(object sender, SchedulerEventArgs args)
                    {
                        if (!queue.IsAddingCompleted)
                        {
                            queue.Add(args.JobDetail);
                        }
                    }
                    _schedulerFactory.OnChanged += Subscribe;

                    // 持续发送 SSE 协议数据
                    foreach (var jobDetail in queue.GetConsumingEnumerable())
                    {
                        // 如果请求已终止则停止推送
                        if (!context.RequestAborted.IsCancellationRequested)
                        {
                            var message = "data: " + SerializeToJson(jobDetail) + "\n\n";
                            await context.Response.WriteAsync(message, context.RequestAborted);
                            //await context.Response.Body.FlushAsync();
                        }
                        else break;
                    }

                    queue.CompleteAdding();
                    _schedulerFactory.OnChanged -= Subscribe;
                }
                break;
        }
    }

    /// <summary>
    /// 将对象输出为 JSON 字符串
    /// </summary>
    /// <param name="obj">对象</param>
    /// <returns><see cref="string"/></returns>
    private static string SerializeToJson(object obj)
    {
        // 初始化默认序列化选项
        var jsonSerializerOptions = Penetrates.GetDefaultJsonSerializerOptions();
        jsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;

        return JsonSerializer.Serialize(obj, jsonSerializerOptions);
    }
}