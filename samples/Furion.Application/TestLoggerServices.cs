using Furion.Application.Persons;
using Furion.Logging;
using Furion.Logging.Extensions;
using Microsoft.Extensions.Logging;
using System.ComponentModel;

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
        using var scope = _logger.ScopeContext(ctx => ctx.Set("Name", "Furion"));
        _logger.LogInformation("我是一个日志 {id}", 20);
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

    [LoggingMonitor]
    public IEnumerable<List<PersonDto>> GetPersons()
    {
        return Array.Empty<List<PersonDto>>();
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
        using var scope = _logger.ScopeContext(new Dictionary<object, object>
        {
           {"name","Furion" }
        });

        _logger.LogInformation("测试啊");
    }

    public void 测试日志上下文()
    {
        "设置日志上下文".ScopeContext(ctx => ctx.Set("name", "Furion")).LogWarning();
        var (logger, scoped) = Log.ScopeContext(ctx => ctx.Set("name", "Furion"));
        logger.LogInformation("dddd");
        scoped?.Dispose();
    }

    public void 测试批量日志插入()
    {
        for (int i = 0; i < 10000; i++)
        {
            using var scope = _logger.ScopeContext(ctx => ctx.Set("LoggingConst.Color", ConsoleColor.Green));
            _logger.LogInformation($"这是绿色 {i}", i);
        }
    }

    [LoggingMonitor]
    public IActionResult 测试附件类型监听()
    {
        var bytes = File.ReadAllBytes("image.png");
        return new FileContentResult(bytes, "image/png")
        {
            FileDownloadName = "image.png"
        };
    }

    [LoggingMonitor(JsonBehavior = JsonBehavior.OnlyJson)]
    public object 测试指定忽略指定序列化类型(int id)
    {
        return new
        {
            Id = 10,
            Bytes = File.ReadAllBytes("image.png")
        };
    }

    [LoggingMonitor(JsonBehavior = JsonBehavior.OnlyJson
        //, IgnorePropertyNames = new[] { "Bytes" }
        , IgnorePropertyTypes = new[] { typeof(byte[]) })]
    public object 测试指定忽略指定序列化类型2(int id)
    {
        return new
        {
            Id = 10,
            Bytes = File.ReadAllBytes("image.png")
        };
    }

    [LoggingMonitor(Title = "这是一段标题", JsonBehavior = JsonBehavior.OnlyJson, JsonIndented = true)]
    [DisplayName("这是名称")]
    public void 测试显示名称()
    {

    }
}