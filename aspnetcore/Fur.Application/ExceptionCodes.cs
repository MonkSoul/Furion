using Fur.DependencyInjection.Lifetimes;
using Fur.FriendlyException;
using Fur.FriendlyException.Attributes;
using System;

namespace Fur.Application
{
    /// <summary>
    /// 异常状态码
    /// </summary>
    public static class ExceptionCodes
    {
        /// <summary>
        /// 数据没找到
        /// </summary>
        [ExceptionMeta("Data not found.")]
        public const int DataNotFound1000 = 1000;

        /// <summary>
        /// 无效数据
        /// </summary>
        [ExceptionMeta("Invalid data.")]
        public const int InvalidData1001 = 1001;
    }

    /// <summary>
    /// 异常提供器
    /// </summary>
    public class ExceptionCodesProvider : IExceptionProvider, ISingletonLifetime
    {
        public Type ExceptionCodesType() => typeof(ExceptionCodes);
    }
}