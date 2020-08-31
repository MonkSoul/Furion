using System;

namespace Fur.FriendlyException
{
    /// <summary>
    /// 异常错误代码提供器
    /// </summary>
    public interface IErrorCodeTypeProvider
    {
        /// <summary>
        /// 错误代码定义类型
        /// </summary>
        Type[] Definitions { get; }
    }
}