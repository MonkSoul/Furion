using Furion.Application.Persons;
using Furion.Logging.Extensions;
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

    public void 测试日志异常()
    {
        _logger.LogError(new Exception("错误啦"), "测试日志异常", 20);
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

    public int 测试直接抛出异常拦截(int id)
    {
        var c = id / 0;
        return c;
    }

    [LoggingMonitor(WithReturnValue = false)]
    public string 不输出返回值()
    {
        return "让 .NET 开发更简单，更通用，更流行。";
    }

    [LoggingMonitor(ReturnValueThreshold = 30)]
    public string 只输出返回值30个长度()
    {
        return "让 .NET 开发更简单，更通用，更流行。";
    }

    public void 测试日志多线程ID打印()
    {
        _logger.LogInformation("我是 Web 主线程");

        new Thread(() =>
        {
            _logger.LogInformation("我是其他线程");
        }).Start();
    }

    public void 测试字符串拓展日志()
    {
        "This is log".LogInformation<TestLoggerServices>();
    }

    public void 测试作用域()
    {
        _logger.ScopeContext(new Dictionary<object, object>
       {
           {"name","Furion" }
       }).LogInformation("测试啊");
    }
}