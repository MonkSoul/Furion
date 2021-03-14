using System;

namespace Furion.Snowflake
{
    /// <summary>
    /// 雪花 ID 生成器接口
    /// </summary>
    public interface IIDGenerator
    {
        /// <summary>
        /// 生成过程中产生的事件
        /// </summary>
        Action<OverCostActionArg> GenIdActionAsync { get; set; }

        /// <summary>
        /// 生成新的long型Id
        /// </summary>
        /// <returns></returns>
        long NewLong();
    }
}