// Copyright (c) 2020-2022 百小僧, Baiqian Co.,Ltd.
// Furion is licensed under Mulan PSL v2.
// You can use this software according to the terms and conditions of the Mulan PSL v2.
// You may obtain a copy of Mulan PSL v2 at:
//             https://gitee.com/dotnetchina/Furion/blob/master/LICENSE
// THIS SOFTWARE IS PROVIDED ON AN "AS IS" BASIS, WITHOUT WARRANTIES OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO NON-INFRINGEMENT, MERCHANTABILITY OR FIT FOR A PARTICULAR PURPOSE.
// See the Mulan PSL v2 for more details.

using System.Reflection;

namespace Furion.Extensitions.EventBus;

/// <summary>
/// 事件总线拓展类
/// </summary>
[SuppressSniffer]
public static class EventBusExtensitions
{
    /// <summary>
    /// 将事件枚举 Id 转换成字符串对象
    /// </summary>
    /// <param name="em"></param>
    /// <returns></returns>
    public static string ParseToString(this Enum em)
    {
        var enumType = em.GetType();
        return $"{enumType.Assembly.GetName().Name};{enumType.FullName}.{em}";
    }

    /// <summary>
    /// 将事件枚举字符串转换成枚举对象
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static Enum ParseToEnum(this string str)
    {
        var assemblyName = str[..str.IndexOf(';')];
        var fullName = str[(str.IndexOf(';') + 1)..str.LastIndexOf('.')];
        var name = str[(str.LastIndexOf('.') + 1)..];

        return Enum.Parse(Assembly.Load(assemblyName).GetType(fullName), name) as Enum;
    }
}