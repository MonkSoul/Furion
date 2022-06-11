// Copyright (c) 2020-2022 百小僧, Baiqian Co.,Ltd.
// Furion is licensed under Mulan PSL v2.
// You can use this software according to the terms and conditions of the Mulan PSL v2.
// You may obtain a copy of Mulan PSL v2 at:
//             https://gitee.com/dotnetchina/Furion/blob/master/LICENSE
// THIS SOFTWARE IS PROVIDED ON AN "AS IS" BASIS, WITHOUT WARRANTIES OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO NON-INFRINGEMENT, MERCHANTABILITY OR FIT FOR A PARTICULAR PURPOSE.
// See the Mulan PSL v2 for more details.

using System.Reflection;

namespace Furion;

/// <summary>
/// 解决单文件发布程序集扫描问题
/// </summary>
public interface ISingleFilePublish
{
    /// <summary>
    /// 包含程序集数组
    /// </summary>
    /// <remarks>配置单文件发布扫描程序集</remarks>
    /// <returns></returns>
    Assembly[] IncludeAssemblies();

    /// <summary>
    /// 包含程序集名称数组
    /// </summary>
    /// <remarks>配置单文件发布扫描程序集名称</remarks>
    /// <returns></returns>
    string[] IncludeAssemblyNames();
}