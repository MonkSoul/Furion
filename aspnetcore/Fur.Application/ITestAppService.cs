using Fur.Record.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fur.Application
{
    public interface ITestAppService
    {
        Task<IEnumerable<Test>> GetAsync();

        Task<Test> GetAsync(int id);
    }
}
