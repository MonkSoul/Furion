using Furion;
using System.Reflection;

namespace FurionApp.Web.Entry;

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
            "FurionApp.Application",
            "FurionApp.Core",
            "FurionApp.EntityFramework.Core",
            "FurionApp.Web.Core"
        };
    }
}