// 版权归百小僧及百签科技（广东）有限公司所有。
// 
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.HttpRemote;

/// <summary>
///     压力测试构建器
/// </summary>
/// <remarks>使用 <c>HttpRequestBuilder.StressTestHarness(requestUri)</c> 静态方法创建。</remarks>
public sealed class HttpStressTestHarnessBuilder
{
    /// <summary>
    ///     <inheritdoc cref="HttpStressTestHarnessBuilder" />
    /// </summary>
    /// <param name="httpMethod">请求方式</param>
    /// <param name="requestUri">请求地址</param>
    internal HttpStressTestHarnessBuilder(HttpMethod httpMethod, Uri? requestUri)
    {
        // 空检查
        ArgumentNullException.ThrowIfNull(httpMethod);

        Method = httpMethod;
        RequestUri = requestUri;
    }

    /// <summary>
    ///     请求地址
    /// </summary>
    public Uri? RequestUri { get; }

    /// <summary>
    ///     请求方式
    /// </summary>
    public HttpMethod Method { get; }

    /// <summary>
    ///     并发请求数量
    /// </summary>
    /// <remarks>默认值为：100。</remarks>
    public int NumberOfRequests { get; private set; } = 100;

    /// <summary>
    ///     最大并发度
    /// </summary>
    /// <remarks>用于控制系统在同一时间内处理的请求数量。默认值为：100。</remarks>
    public int MaxDegreeOfParallelism { get; private set; } = 100;

    /// <summary>
    ///     压测轮次
    /// </summary>
    /// <remarks>默认值为：1。</remarks>
    public int NumberOfRounds { get; private set; } = 1;

    /// <summary>
    ///     设置并发请求数量
    /// </summary>
    /// <param name="numberOfRequests">并发请求数量</param>
    /// <returns>
    ///     <see cref="HttpStressTestHarnessBuilder" />
    /// </returns>
    /// <exception cref="ArgumentException"></exception>
    public HttpStressTestHarnessBuilder SetNumberOfRequests(int numberOfRequests)
    {
        // 小于或等于 0 检查
        if (numberOfRequests <= 0)
        {
            throw new ArgumentException("Number of requests must be greater than 0.", nameof(numberOfRequests));
        }

        NumberOfRequests = numberOfRequests;

        return this;
    }

    /// <summary>
    ///     设置最大并发度
    /// </summary>
    /// <param name="maxDegreeOfParallelism">最大并发度</param>
    /// <returns>
    ///     <see cref="HttpStressTestHarnessBuilder" />
    /// </returns>
    /// <exception cref="ArgumentException"></exception>
    public HttpStressTestHarnessBuilder SetMaxDegreeOfParallelism(int maxDegreeOfParallelism)
    {
        // 小于或等于 0 检查
        if (maxDegreeOfParallelism <= 0)
        {
            throw new ArgumentException("Max degree of parallelism must be greater than 0.",
                nameof(maxDegreeOfParallelism));
        }

        MaxDegreeOfParallelism = maxDegreeOfParallelism;

        return this;
    }

    /// <summary>
    ///     设置压测轮次
    /// </summary>
    /// <param name="numberOfRounds">压测轮次</param>
    /// <returns>
    ///     <see cref="HttpStressTestHarnessBuilder" />
    /// </returns>
    /// <exception cref="ArgumentException"></exception>
    public HttpStressTestHarnessBuilder SetNumberOfRounds(int numberOfRounds)
    {
        // 小于或等于 0 检查
        if (numberOfRounds <= 0)
        {
            throw new ArgumentException("Number of rounds must be greater than 0.",
                nameof(numberOfRounds));
        }

        NumberOfRounds = numberOfRounds;

        return this;
    }

    /// <summary>
    ///     构建 <see cref="HttpRequestBuilder" /> 实例
    /// </summary>
    /// <param name="httpRemoteOptions">
    ///     <see cref="HttpRemoteOptions" />
    /// </param>
    /// <param name="configure">自定义配置委托</param>
    /// <returns>
    ///     <see cref="HttpRequestBuilder" />
    /// </returns>
    internal HttpRequestBuilder Build(HttpRemoteOptions httpRemoteOptions, Action<HttpRequestBuilder>? configure = null)
    {
        // 空检查
        ArgumentNullException.ThrowIfNull(httpRemoteOptions);

        // 初始化 HttpRequestBuilder 实例，并确保请求标头中添加了 X-Stress-Test: Harness；
        // 同时启用 HttpClient 池化管理
        var httpRequestBuilder = HttpRequestBuilder.Create(Method, RequestUri, configure)
            .WithHeaders(new Dictionary<string, object?>
            {
                { Constants.X_STRESS_TEST_HEADER, Constants.X_STRESS_TEST_VALUE }
            }).UseHttpClientPool();

        return httpRequestBuilder;
    }
}