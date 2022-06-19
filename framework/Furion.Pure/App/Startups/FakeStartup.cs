// Copyright (c) 2020-2022 百小僧, Baiqian Co.,Ltd.
// Furion is licensed under Mulan PSL v2.
// You can use this software according to the terms and conditions of the Mulan PSL v2.
// You may obtain a copy of Mulan PSL v2 at:
//             https://gitee.com/dotnetchina/Furion/blob/master/LICENSE
// THIS SOFTWARE IS PROVIDED ON AN "AS IS" BASIS, WITHOUT WARRANTIES OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO NON-INFRINGEMENT, MERCHANTABILITY OR FIT FOR A PARTICULAR PURPOSE.
// See the Mulan PSL v2 for more details.

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Furion;

/// <summary>
/// 模拟 Starup，解决不设置 UseStartup 时错误问题
/// </summary>
[SuppressSniffer]
public sealed class FakeStartup
{
    /// <summary>
    /// 配置服务
    /// </summary>
    public void ConfigureServices(IServiceCollection _)
    {
    }

    /// <summary>
    /// 配置请求
    /// </summary>
    public void Configure(IApplicationBuilder _)
    {
    }
}