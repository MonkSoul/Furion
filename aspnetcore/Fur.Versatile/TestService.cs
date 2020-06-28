using Fur.DependencyInjection.Lifetimes;

namespace Fur.Versatile
{
    public class TestService : ITestService, ITransientLifetime
    {
        public string GetName()
        {
            return "Fur";
        }
    }
}
