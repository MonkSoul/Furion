// MIT 许可证
//
// 版权 © 2020-present 百小僧, 百签科技（广东）有限公司 和所有贡献者
//
// 特此免费授予任何获得本软件副本和相关文档文件（下称“软件”）的人不受限制地处置该软件的权利，
// 包括不受限制地使用、复制、修改、合并、发布、分发、转授许可和/或出售该软件副本，
// 以及再授权被配发了本软件的人如上的权利，须在下列条件下：
//
// 上述版权声明和本许可声明应包含在该软件的所有副本或实质成分中。
//
// 本软件是“如此”提供的，没有任何形式的明示或暗示的保证，包括但不限于对适销性、特定用途的适用性和不侵权的保证。
// 在任何情况下，作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

using Furion.Extensions;
using Furion.Templates.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Furion.DataValidation;

/// <summary>
/// 数据验证器
/// </summary>
[SuppressSniffer]
public static class DataValidator
{
    /// <summary>
    /// 所有验证类型
    /// </summary>
    private static readonly IEnumerable<Type> ValidationTypes;

    /// <summary>
    /// 所有验证类型
    /// </summary>
    private static readonly IEnumerable<Type> ValidationMessageTypes;

    /// <summary>
    /// 验证类型正则表达式
    /// </summary>
    private static readonly ConcurrentDictionary<string, ValidationItemMetadataAttribute> ValidationItemMetadatas;

    /// <summary>
    /// 构造函数
    /// </summary>
    static DataValidator()
    {
        // 获取所有验证类型
        ValidationTypes = GetValidationTypes();

        // 获取所有验证消息类型
        ValidationMessageTypes = GetValidationMessageTypes();

        // 获取所有验证类型正则表达式
        ValidationItemMetadatas = GetValidationValidationItemMetadatas();

        // 缓存所有正则表达式
        GetValidationTypeValidationItemMetadataCached = new ConcurrentDictionary<object, (string, ValidationItemMetadataAttribute)>();
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
                IsValid = true,
                MemberOrValue = obj
            };

        // 存储验证结果
        ICollection<ValidationResult> results = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(obj, new ValidationContext(obj), results, validateAllProperties);

        // 返回验证结果
        return new DataValidationResult
        {
            IsValid = isValid,
            ValidationResults = results,
            MemberOrValue = obj
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
            ValidationResults = results,
            MemberOrValue = value
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
        return value == null
            ? throw new ArgumentNullException(nameof(value))
            : Regex.IsMatch(value.ToString(), regexPattern, regexOptions);
    }

    /// <summary>
    /// 验证类型验证
    /// </summary>
    /// <param name="value"></param>
    /// <param name="validationTypes"></param>
    /// <returns></returns>
    public static DataValidationResult TryValidateValue(object value, params object[] validationTypes)
    {
        return TryValidateValue(value, ValidationPattern.AllOfThem, validationTypes);
    }

    /// <summary>
    /// 验证类型验证
    /// </summary>
    /// <param name="value"></param>
    /// <param name="validationOptionss">验证方式</param>
    /// <param name="validationTypes"></param>
    /// <returns></returns>
    public static DataValidationResult TryValidateValue(object value, ValidationPattern validationOptionss, params object[] validationTypes)
    {
        // 存储验证结果
        ICollection<ValidationResult> results = new List<ValidationResult>();

        // 如果值未null，验证失败
        if (value == null)
        {
            results.Add(new ValidationResult("The value is required"));

            // 返回验证结果
            return new DataValidationResult
            {
                IsValid = false,
                ValidationResults = results,
                MemberOrValue = value
            };
        }

        // 验证标识
        bool? isValid = null;
        foreach (var validationType in validationTypes)
        {
            // 解析名称和正则表达式
            var (validationName, validationItemMetadata) = GetValidationTypeValidationItemMetadata(validationType);

            // 验证结果
            var validResult = TryValidateValue(value, validationItemMetadata.RegularExpression, validationItemMetadata.RegexOptions);

            // 判断是否需要同时验证通过才通过
            if (validationOptionss == ValidationPattern.AtLeastOne)
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
                    string.Format(validationItemMetadata.DefaultErrorMessage.Render(), value, validationName)));
            }
        }

        // 返回验证结果
        return new DataValidationResult
        {
            IsValid = isValid ?? true,
            ValidationResults = results,
            MemberOrValue = value
        };
    }

    /// <summary>
    /// 获取验证类型验证Item集合
    /// </summary>
    private static readonly ConcurrentDictionary<object, (string, ValidationItemMetadataAttribute)> GetValidationTypeValidationItemMetadataCached;

    /// <summary>
    /// 获取验证类型正则表达式（需要缓存）
    /// </summary>
    /// <param name="validationType"></param>
    /// <returns></returns>
    private static (string ValidationName, ValidationItemMetadataAttribute ValidationItemMetadata) GetValidationTypeValidationItemMetadata(object validationType)
    {
        return GetValidationTypeValidationItemMetadataCached.GetOrAdd(validationType, Function);

        // 本地函数
        static (string, ValidationItemMetadataAttribute) Function(object validationType)
        {
            // 获取验证类型
            var type = validationType.GetType();

            // 判断是否是有效的验证类型
            if (!ValidationTypes.Any(u => u == type))
                throw new InvalidOperationException($"{type.Name} is not a valid validation type.");

            // 获取对应的枚举名称
            var validationName = Enum.GetName(type, validationType);

            // 判断是否配置验证正则表达式
            if (!ValidationItemMetadatas.ContainsKey(validationName))
                throw new InvalidOperationException($"No ${validationName} validation type metadata exists.");

            // 获取对应的验证选项
            var validationItemMetadataAttribute = ValidationItemMetadatas[validationName];

            return (validationName, validationItemMetadataAttribute);
        }
    }

    /// <summary>
    /// 获取所有验证类型
    /// </summary>
    /// <returns></returns>
    private static IEnumerable<Type> GetValidationTypes()
    {
        // 扫描所有公开的枚举且贴有 [ValidationType] 特性
        var validationTypes = App.EffectiveTypes.Where(u => u.IsDefined(typeof(ValidationTypeAttribute), true) && u.IsEnum);
        return validationTypes;
    }

    /// <summary>
    /// 获取所有验证消息类型
    /// </summary>
    /// <returns></returns>
    private static IEnumerable<Type> GetValidationMessageTypes()
    {
        // 扫描所有公开的的枚举且贴有 [ValidationMessageType] 特性
        var validationMessageTypes = App.EffectiveTypes
            .Where(u => u.IsDefined(typeof(ValidationMessageTypeAttribute), true) && u.IsEnum);

        // 加载自定义验证消息类型提供器
        var validationMessageTypeProvider = App.GetService<IValidationMessageTypeProvider>(App.RootServices);
        if (validationMessageTypeProvider is { Definitions: not null }) validationMessageTypes = validationMessageTypes.Concat(validationMessageTypeProvider.Definitions);

        return validationMessageTypes.Distinct();
    }

    /// <summary>
    /// 获取验证类型所有有效的正则表达式
    /// </summary>
    /// <returns></returns>
    private static ConcurrentDictionary<string, ValidationItemMetadataAttribute> GetValidationValidationItemMetadatas()
    {
        var vaidationItems = new ConcurrentDictionary<string, ValidationItemMetadataAttribute>();

        // 查找所有 [ValidationMessageType] 类型中的 [ValidationMessage] 消息定义
        var customErrorMessages = ValidationMessageTypes.SelectMany(u => u.GetFields()
                .Where(u => u.IsDefined(typeof(ValidationMessageAttribute))))
            .ToDictionary(u => u.Name, u => u.GetCustomAttribute<ValidationMessageAttribute>().ErrorMessage.Render());

        // 加载配置文件配置
        var validationTypeMessageSettings = App.GetConfig<ValidationTypeMessageSettingsOptions>("ValidationTypeMessageSettings", true);
        if (validationTypeMessageSettings is { Definitions: not null })
        {
            // 获取所有参数大于1的配置
            var settingsErrorMessages = validationTypeMessageSettings.Definitions
                .Where(u => u.Length > 1)
                .ToDictionary(u => u[0].ToString(), u => u[1].ToString());

            customErrorMessages = customErrorMessages.AddOrUpdate(settingsErrorMessages);
        }

        // 获取所有验证属性
        var validationFields = ValidationTypes.SelectMany(u => u.GetFields()
            .Where(u => u.IsDefined(typeof(ValidationItemMetadataAttribute))))
            .ToDictionary(u => u.Name, u => ReplaceValidateErrorMessage(u.Name, u, customErrorMessages));

        vaidationItems.AddOrUpdate(validationFields);

        return vaidationItems;
    }

    /// <summary>
    /// 替换默认验证失败消息
    /// </summary>
    /// <param name="name">验证唯一名称</param>
    /// <param name="field"></param>
    /// <param name="customErrorMessages"></param>
    private static ValidationItemMetadataAttribute ReplaceValidateErrorMessage(string name, FieldInfo field, Dictionary<string, string> customErrorMessages)
    {
        var validationValidationItemMetadata = field.GetCustomAttribute<ValidationItemMetadataAttribute>();
        if (customErrorMessages.ContainsKey(name))
        {
            validationValidationItemMetadata.DefaultErrorMessage = customErrorMessages[name];
        }

        return validationValidationItemMetadata;
    }
}