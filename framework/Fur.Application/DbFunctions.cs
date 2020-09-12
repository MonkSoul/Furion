using Fur.DatabaseAccessor;
using System;

namespace Fur.Application
{
    public static class DbFunctions
    {
        [QueryableFunction("FN_GetName", "dbo")]
        public static string GetName(string name) => throw new NotImplementedException();
    }
}
