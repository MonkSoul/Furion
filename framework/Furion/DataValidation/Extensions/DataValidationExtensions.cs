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

using Furion.DependencyInjection;
using Furion.FriendlyException;
using Furion.JsonSerialization;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;

namespace Furion.DataValidation
{
    /// <summary>
    /// 数据验证拓展类
    /// </summary>
    [SuppressSniffer]
    public static class DataValidationExtensions
    {
        /// <summary>
        /// 拓展方法，验证类类型对象
        /// </summary>
        /// <param name="obj">对象实例</param>
        /// <param name="validateAllProperties">是否验证所有属性</param>
        /// <returns>验证结果</returns>
        public static DataValidationResult TryValidate(this object obj, bool validateAllProperties = true)
        {
            return DataValidator.TryValidateObject(obj, validateAllProperties);
        }

        /// <summary>
        /// 拓展方法，验证单个值
        /// </summary>
        /// <param name="value">单个值</param>
        /// <param name="validationAttributes">验证特性</param>
        /// <returns></returns>
        public static DataValidationResult TryValidate(this object value, params ValidationAttribute[] validationAttributes)
        {
            return DataValidator.TryValidateValue(value, validationAttributes);
        }

        /// <summary>
        /// 拓展方法，验证单个值
        /// </summary>
        /// <param name="value">单个值</param>
        /// <param name="validationTypes">验证类型</param>
        /// <returns></returns>
        public static DataValidationResult TryValidate(this object value, params object[] validationTypes)
        {
            return DataValidator.TryValidateValue(value, validationTypes);
        }

        /// <summary>
        /// 拓展方法，验证单个值
        /// </summary>
        /// <param name="value">单个值</param>
        /// <param name="validationOptionss">验证逻辑</param>
        /// <param name="validationTypes">验证类型</param>
        /// <returns></returns>
        public static DataValidationResult TryValidate(this object value, ValidationPattern validationOptionss, params object[] validationTypes)
        {
            return DataValidator.TryValidateValue(value, validationOptionss, validationTypes);
        }

        /// <summary>
        /// 拓展方法，验证类类型对象
        /// </summary>
        /// <param name="obj">对象实例</param>
        /// <param name="validateAllProperties">是否验证所有属性</param>
        public static void Validate(this object obj, bool validateAllProperties = true)
        {
            DataValidator.TryValidateObject(obj, validateAllProperties).ThrowValidateFailedModel();
        }

        /// <summary>
        /// 拓展方法，验证单个值
        /// </summary>
        /// <param name="value">单个值</param>
        /// <param name="validationAttributes">验证特性</param>
        public static void Validate(this object value, params ValidationAttribute[] validationAttributes)
        {
            DataValidator.TryValidateValue(value, validationAttributes).ThrowValidateFailedModel();
        }

        /// <summary>
        /// 拓展方法，验证单个值
        /// </summary>
        /// <param name="value">单个值</param>
        /// <param name="validationTypes">验证类型</param>
        public static void Validate(this object value, params object[] validationTypes)
        {
            DataValidator.TryValidateValue(value, validationTypes).ThrowValidateFailedModel();
        }

        /// <summary>
        /// 拓展方法，验证单个值
        /// </summary>
        /// <param name="value">单个值</param>
        /// <param name="validationOptionss">验证逻辑</param>
        /// <param name="validationTypes">验证类型</param>
        public static void Validate(this object value, ValidationPattern validationOptionss, params object[] validationTypes)
        {
            DataValidator.TryValidateValue(value, validationOptionss, validationTypes).ThrowValidateFailedModel();
        }

        /// <summary>
        /// 拓展方法，验证单个值
        /// </summary>
        /// <param name="value">单个值</param>
        /// <param name="regexPattern">正则表达式</param>
        /// <param name="regexOptions">正则表达式选项</param>
        /// <returns></returns>
        public static bool TryValidate(this object value, string regexPattern, RegexOptions regexOptions = RegexOptions.None)
        {
            return DataValidator.TryValidateValue(value, regexPattern, regexOptions);
        }

        /// <summary>
        /// 直接抛出异常信息
        /// </summary>
        /// <param name="dataValidationResult"></param>
        public static void ThrowValidateFailedModel(this DataValidationResult dataValidationResult)
        {
            if (!dataValidationResult.IsValid)
                throw Oops.Oh("[Validation]" + JSON.Serialize(
                    dataValidationResult.ValidationResults
                    .Select(u => new {
                        MemberNames = u.MemberNames.Any() ? u.MemberNames : new[] { $"{dataValidationResult.MemberOrValue}" },
                        u.ErrorMessage
                    })
                    .OrderBy(u => u.MemberNames.First())
                    .GroupBy(u => u.MemberNames.First())
                    .Select(u =>
                        new ValidateFailedModel(u.Key,
                            u.Select(c => c.ErrorMessage).ToArray()))));
        }
    }
}