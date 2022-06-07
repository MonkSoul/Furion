using Furion.ConfigurableOptions;
using Microsoft.Extensions.Options;

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
}

public class AppInfoOptions : IConfigurableOptions
{
    public string Name { get; set; }
    public string Version { get; set; }

    [MapSettings("Company_Name")]
    public string Company { get; set; }
}