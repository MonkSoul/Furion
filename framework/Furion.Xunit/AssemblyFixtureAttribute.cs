// Copyright (c) 2020-2022 百小僧, Baiqian Co.,Ltd.
// Furion is licensed under Mulan PSL v2.
// You can use this software according to the terms and conditions of the Mulan PSL v2.
// You may obtain a copy of Mulan PSL v2 at:
//             https://gitee.com/dotnetchina/Furion/blob/master/LICENSE
// THIS SOFTWARE IS PROVIDED ON AN "AS IS" BASIS, WITHOUT WARRANTIES OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO NON-INFRINGEMENT, MERCHANTABILITY OR FIT FOR A PARTICULAR PURPOSE.
// See the Mulan PSL v2 for more details.

namespace Furion.Xunit;

/// <summary>
/// 通过特性方式任何类型
/// </summary>
[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
public sealed class AssemblyFixtureAttribute : Attribute
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="fixtureType"></param>
    public AssemblyFixtureAttribute(Type fixtureType)
    {
        FixtureType = fixtureType;
    }

    /// <summary>
    /// 类型
    /// </summary>
    public Type FixtureType { get; private set; }
}