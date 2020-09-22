using Fur.DatabaseAccessor;
using System.Data;

namespace Fur.Application
{
    public class ProcOutputModel
    {
        public int Id { get; set; } // 输入参数

        [DbParameter(ParameterDirection.Output, Size = 32)]
        public string Name { get; set; }    // 输出参数

        [DbParameter(ParameterDirection.Output)]
        public int Age { get; set; }    // 输出参数

        [DbParameter(ParameterDirection.ReturnValue)]
        public int ReturnValue { get; set; }    // 返回值
    }
}