using System;

namespace Furion.Snowflake
{
    /// <summary>
    /// 雪花 ID 生成器接口
    /// </summary>
    public interface IIDGenerator
    {
        /// <summary>
        /// 生成雪花 ID 过程中的异步事件
        /// </summary>
        Action<OverCostActionArg> GenIdActionAsync { get; set; }

        /// <summary>
        /// 生成新的 long 类型数据
        /// </summary>
        /// <returns></returns>
        long NewLong();
    }
}