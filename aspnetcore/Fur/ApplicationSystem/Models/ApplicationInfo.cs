using System.Collections.Generic;

namespace Fur.ApplicationSystem.Models
{
    public sealed class ApplicationInfo
    {
        public IEnumerable<ApplicationAssemblyInfo> Assemblies { get; set; }
        public IEnumerable<ApplicationTypeInfo> PublicClassTypes { get; set; }
        public IEnumerable<ApplicationMethodInfo> PublicInstanceMethods { get; set; }
    }
}