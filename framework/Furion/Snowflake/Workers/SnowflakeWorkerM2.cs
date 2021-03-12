using System;

namespace Furion.Snowflake
{
    /// <summary>
    /// 常规雪花算法
    /// </summary>
    internal class SnowflakeWorkerM2 : SnowflakeWorkerM1
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="options"></param>
        public SnowflakeWorkerM2(IDGeneratorOptions options) : base(options)
        {
        }

        /// <summary>
        /// 下一个雪花 ID
        /// </summary>
        /// <returns></returns>
        public override long NextId()
        {
            lock (_SyncLock)
            {
                long currentTimeTick = GetCurrentTimeTick();

                if (_LastTimeTick == currentTimeTick)
                {
                    if (_CurrentSeqNumber++ > MaxSeqNumber)
                    {
                        _CurrentSeqNumber = MinSeqNumber;
                        currentTimeTick = GetNextTimeTick();
                    }
                }
                else
                {
                    _CurrentSeqNumber = MinSeqNumber;
                }

                if (currentTimeTick < _LastTimeTick)
                {
                    throw new Exception(string.Format("Time error for {0} milliseconds", _LastTimeTick - currentTimeTick));
                }

                _LastTimeTick = currentTimeTick;
                var result = ((currentTimeTick << _TimestampShift) + ((long)WorkerId << SeqBitLength) + (uint)_CurrentSeqNumber);

                return result;
            }
        }
    }
}