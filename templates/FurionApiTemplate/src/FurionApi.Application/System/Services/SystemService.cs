using Furion.DependencyInjection;

namespace FurionApi.Application
{
    public class SystemService : ISystemService, ITransient
    {
        public string GetDescription()
        {
            return "让 .NET 开发变得更简单，更通用，更流行。";
        }
    }
}