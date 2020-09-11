using Fur.DependencyInjection;

namespace Fur.Application
{
    public class TestService : ITestService, ITransientDependency
    {
        public string Name()
        {
            return "Hh";
        }
    }

    public interface ITestService
    {
        string Name();
    }
}