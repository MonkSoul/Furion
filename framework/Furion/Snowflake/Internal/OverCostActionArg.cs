using Furion.DependencyInjection;

namespace Furion.Snowflake
{
    /// <summary>
    /// Id生成时回调参数
    /// </summary>
    [SkipScan]
    public class OverCostActionArg
    {
        /// <summary>
        /// 事件类型
        /// 1-开始，2-结束，8-漂移
        /// </summary>
        public virtual int ActionType { get; set; }

        /// <summary>
        /// 时间戳
        /// </summary>
        public virtual long TimeTick { get; set; }

        /// <summary>
        /// 机器码
        /// </summary>
        public virtual ushort WorkerId { get; set; }

        /// <summary>
        /// 漂移计算次数
        /// </summary>
        public virtual int OverCostCountInOneTerm { get; set; }

        /// <summary>
        /// 漂移期间生产ID个数
        /// </summary>
        public virtual int GenCountInOneTerm { get; set; }

        /// <summary>
        /// 漂移周期
        /// </summary>
        public virtual int TermIndex { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="workerId"></param>
        /// <param name="timeTick"></param>
        /// <param name="actionType"></param>
        /// <param name="overCostCountInOneTerm"></param>
        /// <param name="genCountWhenOverCost"></param>
        /// <param name="index"></param>
        public OverCostActionArg(ushort workerId, long timeTick, int actionType = 0, int overCostCountInOneTerm = 0, int genCountWhenOverCost = 0, int index = 0)
        {
            ActionType = actionType;
            TimeTick = timeTick;
            WorkerId = workerId;
            OverCostCountInOneTerm = overCostCountInOneTerm;
            GenCountInOneTerm = genCountWhenOverCost;
            TermIndex = index;
        }
    }
}