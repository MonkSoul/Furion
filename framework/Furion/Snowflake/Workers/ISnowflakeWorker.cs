using System;

namespace Furion.Snowflake
{
    /// <summary>
    /// 雪花算法接口
    /// </summary>
    internal interface ISnowflakeWorker
    {
        /// <summary>
        /// 雪花 ID 生成事件
        /// </summary>
        Action<OverCostActionArg> GenAction { get; set; }

        /// <summary>
        /// 下一个 雪花 ID
        /// </summary>
        /// <returns></returns>
        long NextId();
    }
}