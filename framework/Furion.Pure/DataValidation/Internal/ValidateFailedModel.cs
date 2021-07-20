// Copyright (c) 2020-2021 百小僧, Baiqian Co.,Ltd.
// Furion is licensed under Mulan PSL v2.
// You can use this software according to the terms and conditions of the Mulan PSL v2.
// You may obtain a copy of Mulan PSL v2 at:
//             http://license.coscl.org.cn/MulanPSL2
// THIS SOFTWARE IS PROVIDED ON AN "AS IS" BASIS, WITHOUT WARRANTIES OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO NON-INFRINGEMENT, MERCHANTABILITY OR FIT FOR A PARTICULAR PURPOSE.
// See the Mulan PSL v2 for more details.

using Furion.DependencyInjection;

namespace Furion.DataValidation
{
    /// <summary>
    /// 验证失败模型
    /// </summary>
    [SuppressSniffer]
    public sealed class ValidateFailedModel
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="field"></param>
        /// <param name="messages"></param>
        public ValidateFailedModel(string field, string[] messages)
        {
            Field = field;
            Messages = messages;
        }

        /// <summary>
        /// 出错字段
        /// </summary>
        public string Field { get; set; }

        /// <summary>
        /// 错误列表
        /// </summary>
        public string[] Messages { get; set; }
    }
}