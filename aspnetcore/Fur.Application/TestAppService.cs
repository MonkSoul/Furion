using Fur.AttachController.Attributes;
using Fur.AttachController.Dependencies;
using System.Threading.Tasks;

namespace Fur.Application
{
    [AttachController]
    public class TestAppService : ITestAppService, IAttachControllerDependency
    {
        public Task<string> GetByName(string name)
        {
            return Task.FromResult(name);
        }

        public Task<string> GetName()
        {
            return Task.FromResult(nameof(Fur));
        }
    }
}
