using Fur.FriendlyException;
using System;

namespace Fur.Application
{
    public static class ErrorCodes
    {
        [ExceptionMetadata("没找到 {0} 数据")]
        public const int x1000 = 1000;

        [ExceptionMetadata("该 {0} 用户没找到", "百小僧")]
        public const string x1001 = "<user>.<not found>";
    }

    public static class ErrorCodes2
    {
        [ExceptionMetadata("没找到数据")]
        public const int z2000 = 2000;

        [ExceptionMetadata("该 {0} 用户没找到", "百小僧")]
        public const string z2001 = "<employee>.<not found>";
    }

    public class ErrorCodeProvider : IExceptionErrorCodeProvider
    {
        public Type[] Definitions => new[]
        {
            typeof(ErrorCodes),
            typeof(ErrorCodes2)
        };
    }
}