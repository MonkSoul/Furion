using Furion.Application.Persons;
using Furion.ClayObject;
using Furion.DatabaseAccessor.Extensions;
using Furion.Logging;
using Furion.RemoteRequest;
using Furion.RemoteRequest.Extensions;
using Furion.UnifyResult;
using Furion.ViewEngine;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using System.Text.Json;

namespace Furion.Application;

/// <summary>
/// 测试模块
/// </summary>
public class TestModuleServices : IDynamicApiController
{
    private readonly IHttp _http;

    public TestModuleServices(IHttp http)
    {
        _http = http;
    }

    [HttpPost]
    public IActionResult UploadFileAsync(IFormFile file)
    {
        return new ContentResult() { Content = file.FileName };
    }

    [HttpPost]
    public IActionResult UploadMulitiFileAsync(List<IFormFile> files)
    {
        return new ContentResult() { Content = string.Join(',', files.Select(u => u.FileName)) };
    }

    /// <summary>
    /// 测试单文件上传
    /// </summary>
    /// <returns></returns>
    public async Task<string> TestSingleFileProxy()
    {
        var bytes = File.ReadAllBytes("image.png");
        var result = await _http.TestSingleFileProxyAsync(HttpFile.Create("file", bytes, "image.png"));
        var fileName = await result.Content.ReadAsStringAsync();

        return fileName;
    }

    /// <summary>
    /// 测试多文件上传
    /// </summary>
    /// <returns></returns>
    public async Task<string> TestMultiFileProxy()
    {
        var bytes = File.ReadAllBytes("image.png");
        var result = await _http.TestMultiFileProxyAsync(HttpFile.CreateMultiple("files", (bytes, "image1.png"), (bytes, "image2.png")));
        var fileName = await result.Content.ReadAsStringAsync();

        return fileName;
    }

    /// <summary>
    /// 测试单文件上传（字符串方式）
    /// </summary>
    /// <returns></returns>
    public async Task<string> TestSingleFileProxyString()
    {
        var bytes = File.ReadAllBytes("image.png");

        var result = await "https://localhost:44316/api/test-module/upload-file".SetContentType("multipart/form-data")
                            .SetFiles(HttpFile.Create("file", bytes, "image.png")).PostAsync();

        var fileName = await result.Content.ReadAsStringAsync();

        return fileName;
    }

    /// <summary>
    /// 测试多文件上传（字符串方式）
    /// </summary>
    /// <returns></returns>
    public async Task<string> TestMultiFileProxyString()
    {
        var bytes = File.ReadAllBytes("image.png");
        var result = await "https://localhost:44316/api/test-module/upload-muliti-file".SetContentType("multipart/form-data")
                            .SetFiles(HttpFile.CreateMultiple("files", (bytes, "image1.png"), (bytes, "image2.png"))).PostAsync();
        var fileName = await result.Content.ReadAsStringAsync();

        return fileName;
    }

    public async Task<string> TestRequestEncode([FromQuery] string ip)
    {
        var url = $"http://whois.pconline.com.cn/ipJson.jsp?ip={ip}";
        var resultStr = await url.GetAsStringAsync();
        return resultStr;
    }

    public async Task<PersonDto> TestSerial()
    {
        var result = await "https://localhost:44316/api/person/1".GetAsAsync<RESTfulResult<PersonDto>>();

        return result.Data;
    }

    public async Task<string> TestBaidu()
    {
        var url = $"https://www.baidu.com";
        var resultStr = await url.GetAsStringAsync();
        return resultStr;
    }


    public void 测试高频远程请求()
    {
        Parallel.For(0, 5000, (i) =>
        {
            "https://www.baidu.com".GetAsStringAsync();
        });
    }

    /// <summary>
    /// 测试文件流上传
    /// </summary>
    /// <returns></returns>
    public async Task<string> TestSingleFileSteamProxyString()
    {
        var fileStream = new FileStream("image.png", FileMode.Open);

        var result = await "https://localhost:44316/api/test-module/upload-file".SetContentType("multipart/form-data")
                            .SetFiles(HttpFile.Create("file", fileStream, "image.png")).PostAsync();

        var fileName = await result.Content.ReadAsStringAsync();

        await fileStream.DisposeAsync();

        return fileName;
    }

    /// <summary>
    /// 测试单文件流上传
    /// </summary>
    /// <returns></returns>
    public async Task<string> TestSingleFileStreamProxy()
    {
        var fileStream = new FileStream("image.png", FileMode.Open);
        var result = await _http.TestSingleFileProxyAsync(HttpFile.Create("file", fileStream, "image.png"));
        var fileName = await result.Content.ReadAsStringAsync();

        await fileStream.DisposeAsync();
        return fileName;
    }

    [NonUnify]
    public IActionResult SpecialApi()
    {
        return new JsonResult(new RESTfulResult<object>
        {
            StatusCode = 200,
            Succeeded = true,
            Data = new
            {
                Name = "Furion"
            },
            Timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
        }, new JsonSerializerOptions
        {
            PropertyNamingPolicy = null
        });
    }

    [UnifySerializerSetting("special")]
    public object SpecialApi2()
    {
        return new
        {
            Name = "Furion"
        };
    }

    public async Task 测试Url参数空值情况()
    {
        var obj = new
        {
            id = 1,
            name = default(string),
            age = 30
        };

        var res = await "https://furion.net".SetQueries(obj).GetAsync();
        var res2 = await "https://furion.net".SetQueries(obj, true).GetAsync();
    }

    [HttpGet, LoggingMonitor]
    public string WithCookies([FromServices] IHttpContextAccessor contextAccessor)
    {
        contextAccessor.HttpContext.Response.Cookies.Append("name", "百小僧");
        contextAccessor.HttpContext.Response.Cookies.Append("age", "30");

        return "Furion";
    }

    public async Task<Dictionary<string, string>> 测试远程请求Cookies()
    {
        var response = await "https://localhost:5001/api/test-module/with-cookies".GetAsync();
        var cookies = response.GetCookies();

        return cookies;
    }

    public async Task 测试中文编码问题()
    {
        var obj = new
        {
            id = 1,
            name = "百小僧",
            age = 30
        };

        var res = await "https://localhost:5001/test"
            .SetBody(obj, "application/x-www-form-urlencoded")
            .WithEncodeUrl(false)
            .PostAsync();
    }

    public ServiceLifetime? 测试服务生命周期()
    {
        var d = App.GetServiceLifetime(typeof(IConfiguration));
        var c = App.GetServiceLifetime(typeof(IRepository<Person>));

        return d | c;
    }

    [LoggingMonitor(ContractResolver = ContractResolverTypes.CamelCase)]
    public DataTable 测试监听日志属性序列化规则()
    {
        var d = "select * from person".SqlQuery();
        return d;
    }

    public void 测试GUID正则表达式()
    {
        var a = "41E3DAF5-6E37-4BCC-9F8E-0D9521E2AA8D".TryValidate(ValidationTypes.GUID_OR_UUID).IsValid;
        var b = "e155518c-ca1b-443c-9be9-fe90fdab7345".TryValidate(ValidationTypes.GUID_OR_UUID).IsValid;
        var c = "00000000-0000-0000-0000-000000000000".TryValidate(ValidationTypes.GUID_OR_UUID).IsValid;

        var d = true == a && a == b && a == c;
    }

    public async Task 测试远程请求下载文件()
    {
        await "https://furion.net/img/rm1.png".GetToSaveAsync("D:/rm3.png");
    }

    public void 测试创建新的数据库上下文()
    {
        var dbcontext1 = Db.GetDbContext(typeof(MasterDbContextLocator));
        var dbcontext2 = Db.GetDbContext(typeof(MasterDbContextLocator));

        var c = dbcontext1 == dbcontext2;

        var dbcontext3 = Db.GetNewDbContext(typeof(MasterDbContextLocator));
        var dbcontext4 = Db.GetNewDbContext(typeof(MasterDbContextLocator));

        var d = dbcontext1 != dbcontext3;
        var e = dbcontext2 != dbcontext3;
        var f = dbcontext3 != dbcontext4;

        Task.Run(() =>
        {
            var dbcontext5 = Db.GetNewDbContext(typeof(MasterDbContextLocator));
            var dbcontext6 = Db.GetNewDbContext(typeof(MasterDbContextLocator));

            var f = dbcontext5 != dbcontext6;
        });
    }

    public void 测试JWTSettings()
    {
        var settings = JWTEncryption.GetJWTSettings();
    }

    [HttpPost("api/sse"), AllowAnonymous]
    public async Task CreateSseDemo([FromServices] IHttpContextAccessor accessor)
    {
        // 设置响应头，指定 SSE 的内容类型
        accessor.HttpContext.Response.Headers.Append("Content-Type", "text/event-stream");

        // 写入 SSE 消息到响应流
        for (int i = 0; i < 10; i++)
        {
            var message = $"消息{i}";
            await accessor.HttpContext.Response.WriteAsync(message);
            await accessor.HttpContext.Response.Body.FlushAsync();
            await Console.Out.WriteLineAsync(message);
            Task.Delay(1000).Wait();
        }
        await accessor.HttpContext.Response.CompleteAsync();
    }

    public async Task 测试事务是否回滚和捕获异常()
    {
        try
        {
            await Scoped.CreateUowAsync(async (_, f) =>
            {
                throw Oops.Oh("抛出异常");

                await Task.CompletedTask;
            });
        }
        catch (Exception)
        {

        }
    }

    public async Task<string> 测试模板引擎模式匹配([FromServices] IViewEngine viewEngine)
    {
        var template = """
             @{
                 var ss = new { QueryType = "like"};
                 string result = "";
                 switch (ss)
                 {
                     case {QueryType: "like"}:
                         result = "显示此处内容";
                         break;
                 }
                 @:@result
             }
             """;

        var str = await viewEngine.RunCompileAsync(template);

        return str;
    }


    public async Task 测试下载互联网图片()
    {
        await "https://img-s-msn-com.akamaized.net/tenant/amp/entityid/BB1jrXtM.img?w=760&h=559&m=6&x=328&y=121&s=109&d=109".GetToSaveAsync(Path.Combine(Directory.GetCurrentDirectory(), "xxx.png"));
    }

    public TestLong TestLong2(TestLong test)
    {
        return test;
    }

    public dynamic 测试嵌套Clay和序列化()
    {
        dynamic a1 = Clay.Object(new
        {
            Name = "我是第一层"
        });

        dynamic a2 = Clay.Object(new
        {
            Name = "我是第二层"
        });

        dynamic a3 = Clay.Object(new object[] { });

        a3[0] = new
        {
            Name = "明细1"
        };

        a3[1] = new
        {
            Name = "明细2"
        };

        a1.Child = a2;
        a1.Entry = a3;

        var str = a1.ToString();

        return a1;
    }

    public dynamic 测试嵌套Clay和序列化2()
    {
        // 这里是另外一个Arr 子集
        dynamic a3 = Clay.Object(new object[] { });

        // 创建粘土
        dynamic a1 = Clay.Object(new
        {
            Name = "我是第一层粘土"
        });

        // 把值些进
        a3[0] = a1;

        var str = a3.ToString();

        return a3;
    }

    public class TestLong
    {
        public long? Property { get; set; } = 10;
    }
}