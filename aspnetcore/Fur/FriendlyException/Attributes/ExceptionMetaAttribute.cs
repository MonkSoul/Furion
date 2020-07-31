using Fur.AppBasic.Attributes;
using System;

namespace Fur.FriendlyException.Attributes
{
    [AttributeUsage(AttributeTargets.Field), NonWrapper]
    public sealed class ExceptionMetaAttribute : Attribute
    {
        public ExceptionMetaAttribute(string message) => Message = message;

        public string Message { get; set; }
    }
}