// MIT License
//
// Copyright (c) 2020-2022 百小僧, Baiqian Co.,Ltd and Contributors
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

using System.Data;

namespace Furion.DatabaseAccessor;

/// <summary>
/// DbParameter 配置特性
/// </summary>
[SuppressSniffer, AttributeUsage(AttributeTargets.Property)]
public sealed class DbParameterAttribute : Attribute
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public DbParameterAttribute()
    {
        Direction = ParameterDirection.Input;
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="direction">参数方向</param>
    public DbParameterAttribute(ParameterDirection direction)
    {
        Direction = direction;
    }

    /// <summary>
    /// 参数输出方向
    /// </summary>
    public ParameterDirection Direction { get; set; }

    /// <summary>
    /// 数据库对应类型
    /// </summary>
    public object DbType { get; set; }

    /// <summary>
    /// 大小
    /// </summary>
    /// <remarks>Nvarchar/varchar类型需指定</remarks>
    public int Size { get; set; }
}