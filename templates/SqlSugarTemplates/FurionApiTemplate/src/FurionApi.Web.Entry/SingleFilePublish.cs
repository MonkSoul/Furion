using Furion;
using System.Reflection;

namespace FurionApi.Web.Entry;

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
            "FurionApi.Application",
            "FurionApi.Core",
            "FurionApi.Web.Core"
        };
    }
}