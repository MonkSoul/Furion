using System;

namespace Fur.FriendlyException
{
    /// <summary>
    /// 异常代码特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class ExceptionErrorCodesAttribute : Attribute
    {
    }
}