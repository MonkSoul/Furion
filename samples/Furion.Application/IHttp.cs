using Furion.RemoteRequest;

namespace Furion.Application;

public interface IHttp : IHttpDispatchProxy
{
    [Post("https://localhost:44316/api/test-module/upload-file", ContentType = "multipart/form-data")]
    Task<HttpResponseMessage> TestSingleFileProxyAsync([BodyBytes("file", "image.png")] Byte[] bytes);

    [Post("https://localhost:44316/api/test-module/upload-muliti-file", ContentType = "multipart/form-data")]
    Task<HttpResponseMessage> TestMultiFileProxyAsync([BodyBytes("files", "image.png")] Byte[] bytes, [BodyBytes("files", "image2.png")] Byte[] bytes2);
}