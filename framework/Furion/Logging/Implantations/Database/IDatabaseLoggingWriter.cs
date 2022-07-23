// Copyright (c) 2020-2022 百小僧, Baiqian Co.,Ltd.
// Furion is licensed under Mulan PSL v2.
// You can use this software according to the terms and conditions of the Mulan PSL v2.
// You may obtain a copy of Mulan PSL v2 at:
//             https://gitee.com/dotnetchina/Furion/blob/master/LICENSE
// THIS SOFTWARE IS PROVIDED ON AN "AS IS" BASIS, WITHOUT WARRANTIES OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO NON-INFRINGEMENT, MERCHANTABILITY OR FIT FOR A PARTICULAR PURPOSE.
// See the Mulan PSL v2 for more details.

namespace Furion.Logging;

/// <summary>
/// 数据库日志写入器
/// </summary>
public interface IDatabaseLoggingWriter
{
    /// <summary>
    /// 写入数据库
    /// </summary>
    /// <param name="logMsg">结构化日志消息</param>
    /// <param name="flush">清除缓冲区</param>
    void Write(LogMessage logMsg, bool flush);
}