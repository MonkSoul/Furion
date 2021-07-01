// -----------------------------------------------------------------------------
// 让 .NET 开发更简单，更通用，更流行。
// Copyright © 2020-2021 Furion, 百小僧, Baiqian Co.,Ltd.
//
// 框架名称：Furion
// 框架作者：百小僧
// 框架版本：2.11.1
// 源码地址：Gitee： https://gitee.com/dotnetchina/Furion
//          Github：https://github.com/monksoul/Furion
// 开源协议：Apache-2.0（https://gitee.com/dotnetchina/Furion/blob/master/LICENSE）
// -----------------------------------------------------------------------------

using Furion.JsonSerialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;

namespace Furion.DataValidation
{
    /// <summary>
    /// 验证上下文
    /// </summary>
    internal static class ValidatorContext
    {
        /// <summary>
        /// 输出验证信息
        /// </summary>
        /// <param name="errors"></param>
        /// <returns></returns>
        internal static (IEnumerable<ValidateFailedModel> validationResults, string validateFaildMessage, ModelStateDictionary modelState) OutputValidationInfo(object errors)
        {
            ModelStateDictionary _modelState = null;
            IEnumerable<ValidateFailedModel> validationResults = null;

            // 如果是模型验证字典类型
            if (errors is ModelStateDictionary modelState)
            {
                _modelState = modelState;
                // 将验证错误信息转换成字典并序列化成 Json
                validationResults = modelState.Where(u => modelState[u.Key].ValidationState == ModelValidationState.Invalid)
                        .Select(u =>
                           new ValidateFailedModel(u.Key,
                               modelState[u.Key].Errors.Select(c => c.ErrorMessage).ToArray()));
            }
            // 如果是 ValidationProblemDetails 特殊类型
            else if (errors is ValidationProblemDetails validation)
            {
                validationResults = validation.Errors
                    .Select(u =>
                        new ValidateFailedModel(u.Key,
                            u.Value.ToArray()));
            }
            // 其他类型
            else validationResults = new List<ValidateFailedModel>
            {
                new ValidateFailedModel(string.Empty,new[]{errors?.ToString()})
            };

            // 序列化
            var validateFaildMessage = JSON.Serialize(validationResults);

            return (validationResults, validateFaildMessage, _modelState);
        }
    }
}