using Furion;
using System.Reflection;

namespace FurionBlazorApi.Web.Entry;

public class SingleFilePublish : ISingleFilePublish
{
    public Assembly[] IncludeAssemblies()
    {
        return Array.Empty<Assembly>();
    }

    public string[] IncludeAssemblyNames()
    {
        return new[]
        {
            "FurionBlazorApi.Application",
            "FurionBlazorApi.Core",
            "FurionBlazorApi.EntityFramework.Core",
            "FurionBlazorApi.Web.Core"
        };
    }
}