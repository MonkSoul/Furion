using Furion.DependencyInjection;
using Furion.Extensions;
using Furion.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;

namespace Furion.DataValidation
{
    /// <summary>
    /// 验证上下文
    /// </summary>
    [SkipScan]
    internal static class ValidatorContext
    {
        /// <summary>
        /// 输出验证信息
        /// </summary>
        /// <param name="errors"></param>
        /// <returns></returns>
        internal static (Dictionary<string, IEnumerable<string>> validationResults, string validateFaildMessage, ModelStateDictionary modelState) OutputValidationInfo(object errors)
        {
            ModelStateDictionary _modelState = null;

            Dictionary<string, IEnumerable<string>> validationResults = null;
            if (errors is ModelStateDictionary modelState)
            {
                _modelState = modelState;
                // 将验证错误信息转换成字典并序列化成 Json
                validationResults = modelState.ToDictionary(u => !JsonSerializerUtility.EnabledPascalPropertyNaming ? u.Key.ToTitlePascal() : u.Key
                                                                 , u => modelState[u.Key].Errors.Select(c => c.ErrorMessage));
            }
            else if (errors is ValidationProblemDetails validation)
            {
                validationResults = validation.Errors.ToDictionary(u => !JsonSerializerUtility.EnabledPascalPropertyNaming ? u.Key.ToTitlePascal() : u.Key
                                                                 , u => u.Value.AsEnumerable());
            }
            else validationResults = new Dictionary<string, IEnumerable<string>>
            {
                { "", new[] { errors.ToString() } }
            };

            var validateFaildMessage = JsonSerializerUtility.Serialize(validationResults);

            return (validationResults, validateFaildMessage, _modelState);
        }
    }
}