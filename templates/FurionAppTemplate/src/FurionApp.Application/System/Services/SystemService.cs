using Furion.DependencyInjection;

namespace FurionApp.Application
{
    public class SystemService : ISystemService, ITransient
    {
        public string GetDescription()
        {
            return "Furion 让 .NET 开发变得更简单，更通用，更流行。";
        }
    }
}