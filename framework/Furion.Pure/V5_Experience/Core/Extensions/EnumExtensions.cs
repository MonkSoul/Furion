// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using System.ComponentModel;
using System.Reflection;

namespace Furion.Extensions;

/// <summary>
///     枚举拓展类
/// </summary>
internal static class EnumExtensions
{
    /// <summary>
    ///     获取枚举值描述
    /// </summary>
    /// <param name="enumValue">枚举值</param>
    /// <returns>
    ///     <see cref="string" />
    /// </returns>
    /// <exception cref="ArgumentException"></exception>
    internal static string GetEnumDescription(this object enumValue)
    {
        // 空检查
        ArgumentNullException.ThrowIfNull(enumValue);

        // 获取枚举类型
        var enumType = enumValue.GetType();

        // 检查是否是枚举类型
        if (!enumType.IsEnum)
        {
            throw new ArgumentException("The parameter is not an enumeration type.", nameof(enumValue));
        }

        // 获取枚举名称
        var enumName = Enum.GetName(enumType, enumValue);

        // 空检查
        ArgumentNullException.ThrowIfNull(enumName);

        // 获取枚举字段
        var enumField = enumType.GetField(enumName);

        // 空检查
        ArgumentNullException.ThrowIfNull(enumField);

        // 获取 [Description] 特性描述
        return enumField.GetCustomAttribute<DescriptionAttribute>(false)
            ?.Description ?? enumName;
    }
}