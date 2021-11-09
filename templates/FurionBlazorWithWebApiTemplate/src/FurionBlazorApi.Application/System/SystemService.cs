using Furion.DependencyInjection;
using Furion.DynamicApiController;

namespace FurionBlazorApi.Application;

public class SystemService : ISystemService, IDynamicApiController, ITransient
{
    public string GetDescription()
    {
        return "让 .NET 开发更简单，更通用，更流行。";
    }
}
