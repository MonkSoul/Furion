using Fur.AppCore.Attributes;
using System;

namespace Fur.FriendlyException.Attributes
{
    [AttributeUsage(AttributeTargets.Field), NonInflated]
    public sealed class ExceptionMetaAttribute : Attribute
    {
        public ExceptionMetaAttribute(string message) => Message = message;

        public string Message { get; set; }
    }
}