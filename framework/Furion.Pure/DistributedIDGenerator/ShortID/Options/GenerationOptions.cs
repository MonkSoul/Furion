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

namespace Furion.DistributedIDGenerator;

/// <summary>
/// 短 ID 生成配置选项
/// </summary>
[SuppressSniffer]
public class GenerationOptions
{
    /// <summary>
    /// 是否使用数字
    /// <para>默认 false</para>
    /// </summary>
    public bool UseNumbers { get; set; }

    /// <summary>
    /// 是否使用特殊字符
    /// <para>默认 true</para>
    /// </summary>
    public bool UseSpecialCharacters { get; set; } = true;

    /// <summary>
    /// 设置短 ID 长度
    /// </summary>
    public int Length { get; set; } = RandomHelpers.GenerateNumberInRange(Constants.MinimumAutoLength, Constants.MaximumAutoLength);
}