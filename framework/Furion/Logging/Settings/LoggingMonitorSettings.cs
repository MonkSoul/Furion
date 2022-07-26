// Copyright (c) 2020-2022 百小僧, Baiqian Co.,Ltd.
// Furion is licensed under Mulan PSL v2.
// You can use this software according to the terms and conditions of the Mulan PSL v2.
// You may obtain a copy of Mulan PSL v2 at:
//             https://gitee.com/dotnetchina/Furion/blob/master/LICENSE
// THIS SOFTWARE IS PROVIDED ON AN "AS IS" BASIS, WITHOUT WARRANTIES OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO NON-INFRINGEMENT, MERCHANTABILITY OR FIT FOR A PARTICULAR PURPOSE.
// See the Mulan PSL v2 for more details.

namespace Furion.Logging;

/// <summary>
/// 日志监视器配置
/// </summary>
/// <remarks>默认配置节点：Logging:Monitor，支持自定义</remarks>
public sealed class LoggingMonitorSettings
{
    /// <summary>
    /// 全局启用
    /// </summary>
    public bool GlobalEnabled { get; set; } = false;

    /// <summary>
    /// 配置包含拦截的方法名列表（完全限定名格式：程序集名称.类名.方法名），注意无需添加参数签名
    /// </summary>
    /// <remarks>结合 <seealso cref="GlobalEnabled"/> 使用，当 <see cref="GlobalEnabled"/> 为 false 时有效，</remarks>
    public string[] IncludeOfMethods { get; set; } = Array.Empty<string>();

    /// <summary>
    /// 配置排除拦截的方法名列表（完全限定名格式：程序集名称.类名.方法名），注意无需添加参数签名
    /// </summary>
    /// <remarks>结合 <seealso cref="GlobalEnabled"/> 使用，当 <see cref="GlobalEnabled"/> 为 true 时有效，</remarks>
    public string[] ExcludeOfMethods { get; set; } = Array.Empty<string>();
}