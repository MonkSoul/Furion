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
            return;
        }

        // 创建请求客户端
        using var httpClient = _httpClientFactory.CreateClient(httpJobMessage.ClientName);

        // 添加请求报文头 User-Agent
        httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/104.0.5112.81 Safari/537.36 Edg/104.0.1293.47");

        // 创建请求对象
        var httpRequestMessage = new HttpRequestMessage(httpJobMessage.HttpMethod, httpJobMessage.RequestUri);

        // 添加请求报文体，默认只支持发送 application/json 类型
        if (httpJobMessage.HttpMethod != HttpMethod.Get
            && httpJobMessage.HttpMethod != HttpMethod.Head
            && !string.IsNullOrWhiteSpace(httpJobMessage.Body))
        {
            var stringContent = new StringContent(httpJobMessage.Body, Encoding.UTF8);
            stringContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            httpRequestMessage.Content = stringContent;
        }

        // 发送请求
        var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage, stoppingToken);

        // 确保请求成功
        if (httpJobMessage.EnsureSuccessStatusCode)
        {
            httpResponseMessage = httpResponseMessage.EnsureSuccessStatusCode();
        }

        // 解析返回值
        var bodyString = await httpResponseMessage.Content.ReadAsStringAsync(stoppingToken);

        // 输出日志
        _logger.LogInformation($"Received HTTP response body with a length of <{bodyString.Length}> output as follows - {(int)httpResponseMessage.StatusCode}{Environment.NewLine}{bodyString}");

        // 设置本次执行结果
        context.Result = Penetrates.Serialize(new {
            httpResponseMessage.StatusCode,
            Body = bodyString
        });
    }
}