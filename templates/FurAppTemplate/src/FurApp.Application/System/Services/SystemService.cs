using Fur.DependencyInjection;

namespace FurApp.Application
{
    public class SystemService : ISystemService, ITransient
    {
        public string GetDescription()
        {
            return "Fur 是 .NET 5 平台下企业应用开发最佳实践框架。";
        }
    }
}