using Furion;
using System.Reflection;

namespace FurionRazorApi.Web.Entry;

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
            "FurionRazorApi.Application",
            "FurionRazorApi.Core",
            "FurionRazorApi.Web.Core"
        };
    }
}