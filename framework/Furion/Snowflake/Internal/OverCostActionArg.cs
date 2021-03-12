namespace Furion.Snowflake
{
    /// <summary>
    /// 漂移事件参数
    /// </summary>
    public class OverCostActionArg
    {
        /// <summary>
        /// 事件类型
        /// </summary>
        public int ActionType { get; set; }

        /// <summary>
        /// 时间戳
        /// </summary>
        public long TimeTick { get; set; }

        /// <summary>
        /// 机器码
        /// </summary>
        public ushort WorkerId { get; set; }

        /// <summary>
        /// 漂移次数
        /// </summary>
        public int OverCostCountInOneTerm { get; set; }

        /// <summary>
        /// Id 产量
        /// </summary>
        public int GenCountInOneTerm { get; set; }

        /// <summary>
        /// 漂移周期次序
        /// </summary>
        public int TermIndex { get; set; }

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