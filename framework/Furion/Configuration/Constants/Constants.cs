// Copyright (c) 2020-2022 百小僧, Baiqian Co.,Ltd.
// Furion is licensed under Mulan PSL v2.
// You can use this software according to the terms and conditions of the Mulan PSL v2.
// You may obtain a copy of Mulan PSL v2 at:
//             https://gitee.com/dotnetchina/Furion/blob/master/LICENSE
// THIS SOFTWARE IS PROVIDED ON AN "AS IS" BASIS, WITHOUT WARRANTIES OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO NON-INFRINGEMENT, MERCHANTABILITY OR FIT FOR A PARTICULAR PURPOSE.
// See the Mulan PSL v2 for more details.

namespace Furion.Configuration;

/// <summary>
/// Configuration 模块常量
/// </summary>
internal static class Constants
{
    /// <summary>
    /// 正则表达式常量
    /// </summary>
    internal static class Patterns
    {
        /// <summary>
        /// 配置文件名
        /// </summary>
        internal const string ConfigurationFileName = @"(?<fileName>(?<realName>.+?)(\.(?<environmentName>\w+))?(?<extension>\.(json|xml|ini)))";

        /// <summary>
        /// 配置文件参数
        /// </summary>
        internal const string ConfigurationFileParameter = @"\s+(?<parameter>\b\w+\b)\s*=\s*(?<value>\btrue\b|\bfalse\b)";
    }
}