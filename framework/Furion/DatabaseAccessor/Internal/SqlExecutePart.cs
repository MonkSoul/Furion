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

namespace Furion.DatabaseAccessor;

/// <summary>
/// 构建 Sql 字符串执行部件
/// </summary>
[SuppressSniffer]
public sealed partial class SqlExecutePart
{
    /// <summary>
    /// 静态缺省 Sql 部件
    /// </summary>
    public static SqlExecutePart Default => new();

    /// <summary>
    /// Sql 字符串
    /// </summary>
    public string SqlString { get; private set; }

    /// <summary>
    /// 设置超时时间
    /// </summary>
    public int Timeout { get; private set; }

    /// <summary>
    /// 数据库上下文定位器
    /// </summary>
    public Type DbContextLocator { get; private set; } = typeof(MasterDbContextLocator);

    /// <summary>
    /// 设置服务提供器
    /// </summary>
    public IServiceProvider ContextScoped { get; private set; } = App.HttpContext?.RequestServices;
}