using Furion;
using System.Reflection;

namespace FurionRazor.Web.Entry;

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
            "FurionRazor.Application",
            "FurionRazor.Core",
            "FurionRazor.EntityFramework.Core",
            "FurionRazor.Web.Core"
        };
    }
}