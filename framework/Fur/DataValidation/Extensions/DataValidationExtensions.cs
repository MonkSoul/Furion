// -----------------------------------------------------------------------------
// Fur 是 .NET 5 平台下企业应用开发最佳实践框架。
// Copyright © 2020 Fur, Baiqian Co.,Ltd.
//
// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0-rc.final.17
// 官方网站：https://chinadot.net
// 源码地址：Gitee：https://gitee.com/monksoul/Fur
// 				    Github：https://github.com/monksoul/Fur
// 开源协议：Apache-2.0（http://www.apache.org/licenses/LICENSE-2.0）
// -----------------------------------------------------------------------------

using Fur.DependencyInjection;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Fur.DataValidation
{
    /// <summary>
    /// 数据验证拓展类
    /// </summary>
    [SkipScan]
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
        /// <param name="regexPattern">正则表达式</param>
        /// <param name="regexOptions">正则表达式选项</param>
        /// <returns></returns>
        public static bool TryValidate(this object value, string regexPattern, RegexOptions regexOptions = RegexOptions.None)
        {
            return DataValidator.TryValidateValue(value, regexPattern, regexOptions);
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
    }
}