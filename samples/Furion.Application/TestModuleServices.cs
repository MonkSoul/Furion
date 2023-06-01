using Furion.Application.Persons;
using Furion.DatabaseAccessor.Extensions;
using Furion.Logging;
using Furion.RemoteRequest;
using Furion.RemoteRequest.Extensions;
using Furion.UnifyResult;
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

        var res = await "http://furion.baiqian.ltd".SetQueries(obj).GetAsync();
        var res2 = await "http://furion.baiqian.ltd".SetQueries(obj, true).GetAsync();
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
        await "http://furion.baiqian.ltd/img/rm1.png".GetToSaveAsync("D:/rm3.png");
    }
}