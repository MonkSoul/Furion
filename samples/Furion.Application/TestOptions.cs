using Furion.ConfigurableOptions;
using Furion.RemoteRequest.Extensions;
using Microsoft.Extensions.Options;
using System.Text;

namespace Furion.Application;

public class TestOptions : IDynamicApiController
{
    private readonly AppInfoOptions options1;

    public TestOptions(IOptions<AppInfoOptions> options1)
    {
        this.options1 = options1.Value;
    }

    public AppInfoOptions Getxxx()
    {
        return options1;
    }

    public AppInfoOptions GetXXX2()
    {
        return App.GetOptions<AppInfoOptions>();
    }
    public async Task 测试远程请求()
    {
        var token = "fS2mNpKwo7gpTl72WquamyBG8AmSCMOeLzRy+vnX2fdMpzNTi+72hQwdfmIHV+gCSMiEmOil6Mau3q36D43o8WZAwSZJnOOD8rp9+ISpeFY=";
        HttpResponseMessage resp = await "http://www.baidu.com"
            .SetHttpMethod(HttpMethod.Post)
            .SetHeaders(new Dictionary<string, object> {
                        { "authorization", token}
            })
            .SetBody("", "application/json", Encoding.UTF8)
            .SendAsync();
        var jjj = await resp.Content.ReadAsStringAsync();
    }

    public void 测试异常添加额外数据()
    {
        throw Oops.Bah("我是业务异常").WithData(new
        {
            Name = "Furion"
        });
    }
}

public class AppInfoOptions : IConfigurableOptions
{
    public string Name { get; set; }
    public string Version { get; set; }

    [MapSettings("Company_Name")]
    public string Company { get; set; }
}