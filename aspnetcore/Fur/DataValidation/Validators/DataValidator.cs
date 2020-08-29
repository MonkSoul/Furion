using Fur.Extensions;
using Fur.FriendlyException;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Fur.DataValidation
{
    /// <summary>
    /// 数据验证器
    /// </summary>
    public static class DataValidator
    {
        /// <summary>
        /// 所有验证类型
        /// </summary>
        private static readonly IEnumerable<Type> ValidationTypes;

        /// <summary>
        /// 验证类型正则表达式
        /// </summary>
        private static readonly Dictionary<string, ValidationRegularExpressionAttribute> ValidationRegularExpressions;

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static DataValidator()
        {
            // 获取所有验证类型
            ValidationTypes = GetValidationTypes();

            // 获取所有验证类型正则表达式
            ValidationRegularExpressions = GetValidationRegularExpressions();

            // 缓存所有正则表达式
            GetValidationTypeRegularExpressionCached = new ConcurrentDictionary<object, (string, ValidationRegularExpressionAttribute)>();
        }

        /// <summary>
        /// 验证类类型对象
        /// </summary>
        /// <param name="obj">对象实例</param>
        /// <param name="validateAllProperties">是否验证所有属性</param>
        /// <returns>验证结果</returns>
        public static DataValidationResult TryValidateObject(object obj, bool validateAllProperties = true)
        {
            // 如果该类型贴有 [NonValidate] 特性，则跳过验证
            if (obj.GetType().IsDefined(typeof(NonValidationAttribute), true))
                return new DataValidationResult
                {
                    IsValid = true
                };

            // 存储验证结果
            ICollection<ValidationResult> results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(obj, new ValidationContext(obj), results, validateAllProperties);

            // 返回验证结果
            return new DataValidationResult
            {
                IsValid = isValid,
                ValidationResults = results
            };
        }

        /// <summary>
        /// 验证单个值
        /// </summary>
        /// <param name="value">单个值</param>
        /// <param name="validationAttributes">验证特性</param>
        /// <returns></returns>
        public static DataValidationResult TryValidateValue(object value, params ValidationAttribute[] validationAttributes)
        {
            // 存储验证结果
            ICollection<ValidationResult> results = new List<ValidationResult>();
            var isValid = Validator.TryValidateValue(value, new ValidationContext(value), results, validationAttributes);

            // 返回验证结果
            return new DataValidationResult
            {
                IsValid = isValid,
                ValidationResults = results
            };
        }

        /// <summary>
        /// 正则表达式验证
        /// </summary>
        /// <param name="value"></param>
        /// <param name="regexPattern"></param>
        /// <param name="regexOptions">正则表达式选项</param>
        /// <returns></returns>
        public static bool TryValidateValue(object value, string regexPattern, RegexOptions regexOptions = RegexOptions.None)
        {
            return Regex.IsMatch(value.ToString(), regexPattern, regexOptions);
        }

        /// <summary>
        /// 验证类型验证
        /// </summary>
        /// <param name="value"></param>
        /// <param name="validationTypes"></param>
        /// <returns></returns>
        public static DataValidationResult TryValidateValue(object value, params object[] validationTypes)
        {
            return TryValidateValue(value, ValidationLogicOptions.And, validationTypes);
        }

        /// <summary>
        /// 验证类型验证
        /// </summary>
        /// <param name="value"></param>
        /// <param name="validationLogics">验证方式</param>
        /// <param name="validationTypes"></param>
        /// <returns></returns>
        public static DataValidationResult TryValidateValue(object value, ValidationLogicOptions validationLogics, params object[] validationTypes)
        {
            // 存储验证结果
            ICollection<ValidationResult> results = new List<ValidationResult>();

            // 如果值未null，验证失败
            if (value == null)
            {
                results.Add(new ValidationResult("The Value is required."));

                // 返回验证结果
                return new DataValidationResult
                {
                    IsValid = false,
                    ValidationResults = results
                };
            }

            // 验证标识
            bool? isValid = null;
            foreach (var validationType in validationTypes)
            {
                // 解析名称和正则表达式
                var (validationName, validationRegularExpression) = GetValidationTypeRegularExpression(validationType);

                // 验证结果
                var validResult = TryValidateValue(value, validationRegularExpression.RegularExpression, validationRegularExpression.RegexOptions);

                // 判断是否需要同时验证通过才通过
                if (validationLogics == ValidationLogicOptions.Or)
                {
                    // 只要有一个验证通过，则跳出
                    if (validResult)
                    {
                        isValid = true;
                        break;
                    }
                }

                if (!validResult)
                {
                    if (isValid != false) isValid = false;
                    // 添加错误消息
                    results.Add(new ValidationResult(
                        string.Format(validationRegularExpression.DefaultErrorMessage, value, validationName)));
                }
            }

            // 返回验证结果
            return new DataValidationResult
            {
                IsValid = isValid ?? true,
                ValidationResults = results
            };
        }

        /// <summary>
        /// <see cref="GetValidationRegularExpression"/> 缓存集合
        /// </summary>
        private static readonly ConcurrentDictionary<object, (string, ValidationRegularExpressionAttribute)> GetValidationTypeRegularExpressionCached;

        /// <summary>
        /// 获取验证类型正则表达式（需要缓存）
        /// </summary>
        /// <param name="validationType"></param>
        /// <returns></returns>
        private static (string ValidationName, ValidationRegularExpressionAttribute RegularExpression) GetValidationTypeRegularExpression(object validationType)
        {
            var isCached = GetValidationTypeRegularExpressionCached.TryGetValue(validationType, out (string, ValidationRegularExpressionAttribute) validation);
            if (isCached) return validation;

            // 本地函数
            static (string, ValidationRegularExpressionAttribute) Function(object validationType)
            {
                // 获取验证类型
                var type = validationType.GetType();

                // 判断是否是有效的验证类型
                if (!ValidationTypes.Any(u => u == type))
                    Oops.Oh($"{type.Name} is not a valid validation type.", typeof(InvalidOperationException));

                // 获取对应的枚举名称
                var validationName = Enum.GetName(type, validationType);

                // 判断是否配置验证正则表达式
                if (!ValidationRegularExpressions.ContainsKey(validationName))
                    Oops.Oh($"No ${validationName} validation type metadata exists.", typeof(InvalidOperationException));

                // 获取对应的正则表达式
                var regularExpression = ValidationRegularExpressions[validationName];

                return (validationName, regularExpression);
            }

            // 调用本地函数
            validation = Function(validationType);
            GetValidationTypeRegularExpressionCached.TryAdd(validationType, validation);
            return validation;
        }

        /// <summary>
        /// 获取所有验证类型
        /// </summary>
        /// <returns></returns>
        private static IEnumerable<Type> GetValidationTypes()
        {
            // 扫描所有公开的的枚举且贴有 [ValidationType] 特性
            var validationTypes = App.Assemblies.SelectMany(a => a.GetTypes()
                .Where(u => u.IsPublic && u.IsEnum && u.IsDefined(typeof(ValidationTypeAttribute), true)));
            return validationTypes;
        }

        /// <summary>
        /// 获取验证类型所有有效的正则表达式
        /// </summary>
        /// <returns></returns>
        private static Dictionary<string, ValidationRegularExpressionAttribute> GetValidationRegularExpressions()
        {
            // 自定义错误异常
            var customErrorMessages = new Dictionary<string, string>();

            // 加载自定义提供器异常消息
            var validationErrorMessageProvider = App.ServiceProvider.GetService<IValidationTypeErrorMessageProvider>();
            if (validationErrorMessageProvider != null)
            {
                // 合并自定义验证消息
                customErrorMessages = customErrorMessages.AddOrUpdate(validationErrorMessageProvider.Definitions ?? new Dictionary<string, string>());
            }

            // 加载配置文件配置
            var validationErrorMessageSettings = App.GetOptions<ValidationTypeErrorMessageSettingsOptions>();
            if (validationErrorMessageSettings is { Definitions: not null })
            {
                // 获取所有参数大于1的配置
                var settingsErrorMessages = validationErrorMessageSettings.Definitions
                    .Where(u => u.Length > 1)
                    .ToDictionary(u => u[0].ToString(), u => u[1].ToString());

                // 合并自定义验证消息
                customErrorMessages = customErrorMessages.AddOrUpdate(settingsErrorMessages);
            }

            // 获取所有验证属性
            var validationFields = ValidationTypes.SelectMany(u => u.GetFields()
                .Where(u => u.IsDefined(typeof(ValidationRegularExpressionAttribute))))
                .ToDictionary(u => u.Name, u => ReplaceValidateErrorMessage(u.Name, u, customErrorMessages));

            return validationFields;
        }

        /// <summary>
        /// 替换默认验证失败消息
        /// </summary>
        /// <param name="name">验证唯一名称</param>
        /// <param name="type"></param>
        /// <param name="customErrorMessages"></param>
        private static ValidationRegularExpressionAttribute ReplaceValidateErrorMessage(string name, FieldInfo field, Dictionary<string, string> customErrorMessages)
        {
            var validationRegularExpression = field.GetCustomAttribute<ValidationRegularExpressionAttribute>();
            if (customErrorMessages.ContainsKey(name))
            {
                validationRegularExpression.DefaultErrorMessage = customErrorMessages[name];
            }

            return validationRegularExpression;
        }
    }
}