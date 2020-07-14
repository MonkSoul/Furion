using Fur.DependencyInjection.Lifetimes;
using Fur.FriendlyException;
using System.Collections.Generic;

namespace Fur.Application
{
    /// <summary>
    /// 异常状态码
    /// </summary>
    public static class ExceptionCodes
    {
        public const int DataNotFound1000 = 1000;
        public const int InvalidData1001 = 1000;
    }

    /// <summary>
    /// 异常提供器
    /// </summary>
    public class ExceptionCodesProvider : IExceptionCodesProvider, ISingletonLifetime
    {
        public Dictionary<int, string> GetExceptionCodes()
        {
            return new Dictionary<int, string>
            {
                [ExceptionCodes.DataNotFound1000] = "Data not found.",
                [ExceptionCodes.InvalidData1001] = "Invalid data."
            };
        }
    }
}
