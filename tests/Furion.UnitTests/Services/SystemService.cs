using Furion.DependencyInjection;

namespace Furion.UnitTests;

public class SystemService : ISystemService, ITransient
{
    public string GetName() => "Furion";
}