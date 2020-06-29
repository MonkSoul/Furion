using System.Threading.Tasks;

namespace Fur.Application
{
    public interface ITestAppService
    {
        Task<string> GetName();

        Task<string> GetByName(string name);
    }
}
