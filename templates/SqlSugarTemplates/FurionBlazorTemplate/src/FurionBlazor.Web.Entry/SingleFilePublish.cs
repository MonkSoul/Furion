using Furion;
using System.Reflection;

namespace FurionBlazor.Web.Entry;

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
            "FurionBlazor.Application",
            "FurionBlazor.Core",
            "FurionBlazor.Web.Core"
        };
    }
}