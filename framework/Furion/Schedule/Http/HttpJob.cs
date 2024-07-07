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

using System.Net.Http.Headers;
using System.Text;

namespace Furion.Schedule;

/// <summary>
/// HTTP 请求作业处理程序
/// </summary>
[SuppressSniffer]
public class HttpJob : IJob
{
    /// <summary>
    /// 无效 HTTP 请求错误消息
    /// </summary>
    private const string INVALID_HTTP_ERROR_MESSAGE = "Invalid HTTP job request. (Parameter 'RequestUri')";

    /// <summary>
    /// 默认请求 User-Agent
    /// </summary>
    private const string DEFAULT_HTTP_USER_AGENT = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/104.0.5112.81 Safari/537.36 Edg/104.0.1293.47";

    /// <summary>
    /// <see cref="HttpClient"/> 创建工厂
    /// </summary>
    private readonly IHttpClientFactory _httpClientFactory;

    /// <summary>
    /// 作业调度器日志服务
    /// </summary>
    private readonly IScheduleLogger _logger;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="httpClientFactory"><see cref="HttpClient"/> 创建工厂</param>
    /// <param name="logger">作业调度器日志服务</param>
    public HttpJob(IHttpClientFactory httpClientFactory
        , IScheduleLogger logger)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }

    /// <summary>
    /// 具体处理逻辑
    /// </summary>
    /// <param name="context">作业执行前上下文</param>
    /// <param name="stoppingToken">取消任务 Token</param>
    /// <returns><see cref="Task"/></returns>
    public async Task ExecuteAsync(JobExecutingContext context, CancellationToken stoppingToken)
    {
        var jobDetail = context.JobDetail;

        // 解析 HTTP 请求参数
        var httpJobMessage = Penetrates.Deserialize<HttpJobMessage>(jobDetail.GetProperty<string>(nameof(HttpJob)));

        // 空检查
        if (httpJobMessage == null || string.IsNullOrWhiteSpace(httpJobMessage.RequestUri))
        {
            _logger.LogWarning(INVALID_HTTP_ERROR_MESSAGE);
            context.Result = INVALID_HTTP_ERROR_MESSAGE;

            return;
        }

        // 创建请求客户端
        using var httpClient = _httpClientFactory.CreateClient(httpJobMessage.ClientName);

        // 添加超时时间
        if (httpJobMessage.Timeout != null)
        {
            httpClient.Timeout = TimeSpan.FromMilliseconds(httpJobMessage.Timeout.Value);
        }

        // 添加请求报文头 User-Agent
        httpClient.DefaultRequestHeaders.Add("User-Agent", DEFAULT_HTTP_USER_AGENT);

        // 创建请求对象
        using var httpRequestMessage = new HttpRequestMessage(httpJobMessage.HttpMethod, httpJobMessage.RequestUri);

        // 添加请求报文体，默认只支持发送 application/json 类型
        if (httpJobMessage.HttpMethod != HttpMethod.Get
            && httpJobMessage.HttpMethod != HttpMethod.Head
            && !string.IsNullOrWhiteSpace(httpJobMessage.Body))
        {
            var stringContent = new StringContent(httpJobMessage.Body, Encoding.UTF8);
            stringContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            httpRequestMessage.Content = stringContent;
        }

        // 添加请求头
        if (httpJobMessage.Headers != null && httpJobMessage.Headers.Count > 0)
        {
            foreach (var (name, value) in httpJobMessage.Headers)
            {
                httpRequestMessage.Headers.TryAddWithoutValidation(name, value);
            }
        }

        // 发送请求
        var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage, stoppingToken);

        // 确保请求成功
        if (httpJobMessage.EnsureSuccessStatusCode)
        {
            httpResponseMessage = httpResponseMessage.EnsureSuccessStatusCode();
        }

        // 是否解析返回值并打印
        string responseContent;
        if (httpJobMessage.PrintResponseContent)
        {
            // 解析返回值
            responseContent = await httpResponseMessage.Content.ReadAsStringAsync(stoppingToken);

            // 输出日志
            _logger.LogInformation($"Received HTTP response body with a length of <{responseContent.Length}> output as follows - {(int)httpResponseMessage.StatusCode}{Environment.NewLine}{responseContent}");
        }
        else responseContent = "COMPLETED";

        // 设置本次执行结果
        context.Result = Penetrates.Serialize(new {
            httpResponseMessage.StatusCode,
            Body = responseContent
        });

        // 释放响应报文对象
        httpResponseMessage?.Dispose();
    }
}