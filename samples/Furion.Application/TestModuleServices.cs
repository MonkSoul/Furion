using Furion.RemoteRequest.Extensions;

namespace Furion.Application;

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
        var result = await _http.TestSingleFileProxyAsync(bytes);
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
        var result = await _http.TestMultiFileProxyAsync(bytes, bytes);
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
                            .SetBodyBytes(("file", bytes, "image.png")).PostAsync();

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
                            .SetBodyBytes(("files", bytes, "image.png"), ("files", bytes, "image2.png")).PostAsync();
        var fileName = await result.Content.ReadAsStringAsync();

        return fileName;
    }

    public async Task<string> TestRequestEncode([FromQuery] string ip)
    {
        var url = $"http://whois.pconline.com.cn/ipJson.jsp?ip={ip}";
        var resultStr = await url.GetAsStringAsync();
        return resultStr;
    }
}