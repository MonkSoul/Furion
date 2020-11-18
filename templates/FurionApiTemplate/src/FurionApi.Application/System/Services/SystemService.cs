using Furion.DependencyInjection;

namespace FurionApi.Application
{
    public class SystemService : ISystemService, ITransient
    {
        public string GetDescription()
        {
            return "Furion 立志使 .NET 变得更简单，更通用，更流行。";
        }
    }
}