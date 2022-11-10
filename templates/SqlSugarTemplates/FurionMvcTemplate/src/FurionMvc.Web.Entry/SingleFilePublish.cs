using Furion;
using System.Reflection;

namespace FurionMvc.Web.Entry;

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
            "FurionMvc.Application",
            "FurionMvc.Core",
            "FurionMvc.Web.Core"
        };
    }
}