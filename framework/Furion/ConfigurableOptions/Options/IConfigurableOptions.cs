// MIT License
//
// Copyright (c) 2020-2022 百小僧, Baiqian Co.,Ltd.
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Furion.ConfigurableOptions;

/// <summary>
/// 应用选项依赖接口
/// </summary>
public partial interface IConfigurableOptions
{ }

/// <summary>
/// 选项后期配置
/// </summary>
/// <typeparam name="TOptions"></typeparam>
public partial interface IConfigurableOptions<TOptions> : IConfigurableOptions
    where TOptions : class, IConfigurableOptions
{
    /// <summary>
    /// 选项后期配置
    /// </summary>
    /// <param name="options"></param>
    /// <param name="configuration"></param>
    void PostConfigure(TOptions options, IConfiguration configuration);
}

/// <summary>
/// 带验证的应用选项依赖接口
/// </summary>
/// <typeparam name="TOptions"></typeparam>
/// <typeparam name="TOptionsValidation"></typeparam>
public partial interface IConfigurableOptions<TOptions, TOptionsValidation> : IConfigurableOptions<TOptions>
    where TOptions : class, IConfigurableOptions
    where TOptionsValidation : class, IValidateOptions<TOptions>
{
}

/// <summary>
/// 带监听的应用选项依赖接口
/// </summary>
/// <typeparam name="TOptions"></typeparam>
public partial interface IConfigurableOptionsListener<TOptions> : IConfigurableOptions<TOptions>
    where TOptions : class, IConfigurableOptions
{
    /// <summary>
    /// 监听
    /// </summary>
    /// <param name="options"></param>
    /// <param name="configuration"></param>
    void OnListener(TOptions options, IConfiguration configuration);
}