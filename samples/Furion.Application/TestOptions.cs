using Furion.ConfigurableOptions;
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