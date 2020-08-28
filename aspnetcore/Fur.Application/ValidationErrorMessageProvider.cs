using Fur.DataValidation;
using System.Collections.Generic;

namespace Fur.Application
{
    public class ValidationErrorMessageProvider : IValidationErrorMessageProvider
    {
        public Dictionary<string, string> ErrorMessageDefinitions => new Dictionary<string, string>
        {
            [nameof(ValidationTypes.Integer)] = "我是自定义异常消息"
        };
    }
}