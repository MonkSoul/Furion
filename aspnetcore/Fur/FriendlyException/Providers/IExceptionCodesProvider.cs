using System;

namespace Fur.FriendlyException
{
    /// <summary>
    /// 异常信息提供器
    /// </summary>
    public interface IExceptionCodesProvider
    {
        /// <summary>
        /// 设置异常状态码
        /// </summary>
        /// <returns></returns>
        Type ExceptionCodesType();
    }
}