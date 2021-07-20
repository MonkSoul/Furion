// Copyright (c) 2020-2021 百小僧, Baiqian Co.,Ltd.
// Furion is licensed under Mulan PSL v2.
// You can use this software according to the terms and conditions of the Mulan PSL v2.
// You may obtain a copy of Mulan PSL v2 at:
//             http://license.coscl.org.cn/MulanPSL2
// THIS SOFTWARE IS PROVIDED ON AN "AS IS" BASIS, WITHOUT WARRANTIES OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO NON-INFRINGEMENT, MERCHANTABILITY OR FIT FOR A PARTICULAR PURPOSE.
// See the Mulan PSL v2 for more details.

using Furion.DependencyInjection;
using System.Collections.Generic;

namespace Furion.UnifyResult
{
    /// <summary>
    /// 规范化状态码选项
    /// </summary>
    [SuppressSniffer]
    public sealed class UnifyResultStatusCodesOptions
    {
        /// <summary>
        /// 设置返回 200 状态码列表
        /// <para>默认：401，403，404，如果设置为 null，则标识所有状态码都返回 200 </para>
        /// </summary>
        public int[] Return200StatusCodes { get; set; } = new[] { 401, 403, 404 };

        /// <summary>
        /// 适配（篡改）Http 状态码（只支持短路状态码，比如 401，403，404，500 等）
        /// </summary>
        public IDictionary<int, int> AdaptStatusCodes { get; set; }
    }
}