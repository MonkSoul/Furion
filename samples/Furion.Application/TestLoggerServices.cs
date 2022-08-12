using Furion.Application.Persons;
using Microsoft.Extensions.Logging;

namespace Furion.Application;

/// <summary>
/// 测试文件/数据库日志写入
/// </summary>
public class TestLoggerServices : IDynamicApiController
{
    private readonly ILogger<TestLoggerServices> _logger;
    private readonly ILoggerFactory _loggerFactory;
    public TestLoggerServices(ILogger<TestLoggerServices> logger, ILoggerFactory loggerFactory)
    {
        _logger = logger;
        _loggerFactory = loggerFactory;
    }

    public void 测试日志()
    {
        _logger.ScopeContext(ctx => ctx.Set("Name", "Furion")).LogInformation("我是一个日志 {id}", 20);
    }

    public void 测试配置日志()
    {
        _logger.LogWarning("我是一个二配置日志 {id}", 20);
    }

    [LoggingMonitor]
    public void 测试日志监听1()
    {
    }

    [LoggingMonitor]
    public void 测试日志监听2(string name)
    {
    }

    [LoggingMonitor]
    public void 测试日志监听3(PersonDto dto)
    {
    }

    [LoggingMonitor]
    public void 测试日志监听4(IFormFile file)
    {
    }

    [LoggingMonitor]
    public void 测试日志监听6(List<IFormFile> file)
    {
    }

    [LoggingMonitor]
    public PersonDto 测试日志监听5(int id)
    {
        return new PersonDto
        {
            Id = 10
        };
    }

    [LoggingMonitor]
    public int 测试日志监听7(int id)
    {
        return id;
    }

    [LoggingMonitor]
    public int 测试日志监听8(int id)
    {
        var c = id / 0;
        return c;
    }

    [LoggingMonitor]
    public PersonDto GetPerson(int id)
    {
        return new PersonDto
        {
            Id = id
        };
    }
}