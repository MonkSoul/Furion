using Furion.RemoteRequest;

namespace Furion.Application;

[BaseAddress("https://localhost:44316")]
public interface IHttp : IBase
{
    [Post("/api/test-module/upload-file", ContentType = "multipart/form-data")]
    Task<HttpResponseMessage> TestSingleFileProxyAsync(HttpFile file);

    [Post("/api/test-module/upload-muliti-file", ContentType = "multipart/form-data")]
    Task<HttpResponseMessage> TestMultiFileProxyAsync(HttpFile[] files);

}

public interface IBase : IHttpDispatchProxy
{
    [Furion.RemoteRequest.Interceptor(InterceptorTypes.Request)]
    static void OnRequest(HttpClient client, HttpRequestMessage req)
    {
    }
}