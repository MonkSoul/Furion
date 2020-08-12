using System;

namespace Fur.FeatureApiController
{
    /// <summary>
    /// 参数位置
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    public class SeatAttribute : Attribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="seat"></param>
        public SeatAttribute(Seat seat = Seat.ActionEnd)
            => Seat = seat;

        /// <summary>
        /// 参数位置
        /// </summary>
        public Seat Seat { get; set; }
    }
}