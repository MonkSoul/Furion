using Fur.Core;
using Fur.DatabaseAccessor;
using System.Collections.Generic;

namespace Fur.Application
{
    public interface ISqlExecuteProxy : ISqlDispatchProxy
    {
        [SqlProduce("proc_GetPersons")]
        List<Person> GetPersons(string keyword);
    }
}