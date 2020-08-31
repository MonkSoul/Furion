using System;

namespace Fur.FriendlyException
{
    /// <summary>
    /// 错误代码类型特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class ErrorCodeTypeAttribute : Attribute
    {
    }
}