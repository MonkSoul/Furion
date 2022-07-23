using Microsoft.Extensions.Logging;

namespace Furion.Application;

/// <summary>
/// 测试文件日志写入
/// </summary>
public class TestFileLoggerServices : IDynamicApiController
{
    private readonly ILogger<TestFileLoggerServices> _logger;
    public TestFileLoggerServices(ILogger<TestFileLoggerServices> logger)
    {
        _logger = logger;
    }

    public void 测试日志()
    {
        _logger.LogInformation("我是一个日志 {id}", 20);
    }

    public void 测试配置日志()
    {
        _logger.LogWarning("我是一个日志 {id}", 20);
    }
}