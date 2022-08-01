// MIT License
//
// Copyright (c) 2020-2022 百小僧, Baiqian Co.,Ltd.
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using Furion.DataValidation;
using Furion.Localization;

namespace System.ComponentModel.DataAnnotations;

/// <summary>
/// 数据类型验证特性
/// </summary>
[SuppressSniffer]
public class DataValidationAttribute : ValidationAttribute
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationPattern">验证逻辑</param>
    /// <param name="validationTypes"></param>
    public DataValidationAttribute(ValidationPattern validationPattern, params object[] validationTypes)
    {
        ValidationPattern = validationPattern;
        ValidationTypes = validationTypes;
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationTypes"></param>
    public DataValidationAttribute(params object[] validationTypes)
    {
        ValidationPattern = ValidationPattern.AllOfThem;
        ValidationTypes = validationTypes;
    }

    /// <summary>
    /// 验证逻辑
    /// </summary>
    /// <param name="value"></param>
    /// <param name="validationContext"></param>
    /// <returns></returns>
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        // 判断是否允许 空值
        if (AllowNullValue && value == null) return ValidationResult.Success;

        // 是否忽略空字符串
        if (AllowEmptyStrings && value is string && string.IsNullOrEmpty(value?.ToString())) return ValidationResult.Success;

        // 执行值验证
        var dataValidationResult = value.TryValidate(ValidationPattern, ValidationTypes);
        dataValidationResult.MemberOrValue = validationContext.MemberName;

        // 验证失败
        if (!dataValidationResult.IsValid)
        {
            var resultMessage = dataValidationResult.ValidationResults.FirstOrDefault().ErrorMessage;

            // 进行多语言处理
            var errorMessage = !string.IsNullOrWhiteSpace(ErrorMessage) ? ErrorMessage : resultMessage;

            return new ValidationResult(string.Format(L.Text == null ? errorMessage : L.Text[errorMessage], validationContext.MemberName));
        }

        // 验证成功
        return ValidationResult.Success;
    }

    /// <summary>
    /// 验证类型
    /// </summary>
    public object[] ValidationTypes { get; set; }

    /// <summary>
    /// 验证逻辑
    /// </summary>
    public ValidationPattern ValidationPattern { get; set; }

    /// <summary>
    ///是否允许空字符串
    /// </summary>
    public bool AllowEmptyStrings { get; set; } = false;

    /// <summary>
    /// 允许空值，有值才验证，默认 false
    /// </summary>
    public bool AllowNullValue { get; set; } = false;
}