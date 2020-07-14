using System;

namespace Fur.FriendlyException.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public class ExceptionMetaAttribute : Attribute
    {
        public ExceptionMetaAttribute(string message) => Message = message;

        public string Message { get; set; }
    }
}
