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

namespace Furion.DynamicApiController;

/// <summary>
/// 参数路由模板
/// </summary>
internal class ParameterRouteTemplate
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public ParameterRouteTemplate()
    {
        ControllerStartTemplates = new List<string>();
        ControllerEndTemplates = new List<string>();
        ActionStartTemplates = new List<string>();
        ActionEndTemplates = new List<string>();
    }

    /// <summary>
    /// 控制器之前的参数
    /// </summary>
    public IList<string> ControllerStartTemplates { get; set; }

    /// <summary>
    /// 控制器之后的参数
    /// </summary>
    public IList<string> ControllerEndTemplates { get; set; }

    /// <summary>
    /// 行为之前的参数
    /// </summary>
    public IList<string> ActionStartTemplates { get; set; }

    /// <summary>
    /// 行为之后的参数
    /// </summary>
    public IList<string> ActionEndTemplates { get; set; }
}