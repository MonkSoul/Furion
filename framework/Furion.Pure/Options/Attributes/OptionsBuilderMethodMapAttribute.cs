// Copyright (c) 2020-2022 百小僧, Baiqian Co.,Ltd.
// Furion is licensed under Mulan PSL v2.
// You can use this software according to the terms and conditions of the Mulan PSL v2.
// You may obtain a copy of Mulan PSL v2 at:
//             https://gitee.com/dotnetchina/Furion/blob/master/LICENSE
// THIS SOFTWARE IS PROVIDED ON AN "AS IS" BASIS, WITHOUT WARRANTIES OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO NON-INFRINGEMENT, MERCHANTABILITY OR FIT FOR A PARTICULAR PURPOSE.
// See the Mulan PSL v2 for more details.

namespace Furion.Options;

/// <summary>
/// 选项构建器方法映射特性
/// </summary>
[AttributeUsage(AttributeTargets.Interface, AllowMultiple = false)]
internal sealed class OptionsBuilderMethodMapAttribute : Attribute
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="methodName">映射方法名</param>
    /// <param name="voidReturn">无返回值</param>
    internal OptionsBuilderMethodMapAttribute(string methodName, bool voidReturn)
    {
        MethodName = methodName;
        VoidReturn = voidReturn;
    }

    /// <summary>
    /// 方法名称
    /// </summary>
    internal string MethodName { get; set; }

    /// <summary>
    /// 有无返回值
    /// </summary>
    internal bool VoidReturn { get; set; }
}