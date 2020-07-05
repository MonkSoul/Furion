using System;

namespace Fur.FriendlyException.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class IfExceptionAttribute : Attribute
    {
        public IfExceptionAttribute(string exceptionCode, string exceptionMessage)
        {
            this.ExceptionCode = exceptionCode;
            this.ExceptionMessage = exceptionMessage;
        }

        public IfExceptionAttribute(int exceptionCode, string exceptionMessage)
        {
            this.ExceptionCode = exceptionCode;
            this.ExceptionMessage = exceptionMessage;
        }

        public object ExceptionCode { get; set; }
        public string ExceptionMessage { get; set; }
    }
}
