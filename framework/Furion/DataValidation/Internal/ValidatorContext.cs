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

            // 如果是模型验证字典类型
            if (errors is ModelStateDictionary modelState)
            {
                _modelState = modelState;
                // 将验证错误信息转换成字典并序列化成 Json
                validationResults = modelState.ToDictionary(u => !JsonSerializerUtility.EnabledPascalPropertyNaming ? u.Key.ToTitlePascal() : u.Key
                                                                 , u => modelState[u.Key].Errors.Select(c => c.ErrorMessage));
            }
            // 如果是 ValidationProblemDetails 特殊类型
            else if (errors is ValidationProblemDetails validation)
            {
                validationResults = validation.Errors.ToDictionary(u => !JsonSerializerUtility.EnabledPascalPropertyNaming ? u.Key.ToTitlePascal() : u.Key
                                                                 , u => u.Value.AsEnumerable());
            }
            // 其他类型
            else validationResults = new Dictionary<string, IEnumerable<string>>
            {
                { string.Empty, new[] { errors?.ToString() } }
            };

            // 序列化
            var validateFaildMessage = JsonSerializerUtility.Serialize(validationResults);

            return (validationResults, validateFaildMessage, _modelState);
        }
    }
}