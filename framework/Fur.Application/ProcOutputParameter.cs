using Fur.DatabaseAccessor;
using System.Data;

namespace Fur.Application
{
    public class ProcOutputParameter
    {
        public int Id { get; set; }

        [DbParameter(ParameterDirection.Output, Size = 32)]
        public string Name { get; set; }

        [DbParameter(ParameterDirection.Output)]
        public int Age { get; set; }

        [DbParameter(ParameterDirection.ReturnValue)]
        public int ReturnValue { get; set; }
    }
}
