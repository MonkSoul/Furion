using System;
using System.Threading;

namespace Furion.Snowflake
{
    /// <summary>
    /// 默认实现
    /// </summary>
    public class DefaultIDGenerator : IIDGenerator
    {
        /// <summary>
        /// 雪花算法接口实例
        /// </summary>
        private ISnowflakeWorker InternalSnowflakeWorker { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="options"></param>
        public DefaultIDGenerator(IDGeneratorOptions options)
        {
            if (options == null)
            {
                throw new ApplicationException("options error.");
            }

            if (options.BaseTime < DateTime.Now.AddYears(-50) || options.BaseTime > DateTime.Now)
            {
                throw new ApplicationException("BaseTime error.");
            }

            if (options.SeqBitLength + options.WorkerIdBitLength > 22)
            {
                throw new ApplicationException("error：WorkerIdBitLength + SeqBitLength <= 22.");
            }

            var maxWorkerIdNumber = Math.Pow(2, options.WorkerIdBitLength) - 1;
            if (options.WorkerId < 0 || options.WorkerId > maxWorkerIdNumber)
            {
                throw new ApplicationException("WorkerId error. (range:[1, " + maxWorkerIdNumber + "].");
            }

            if (options.SeqBitLength < 2 || options.SeqBitLength > 21)
            {
                throw new ApplicationException("SeqBitLength error. (range:[2, 21]).");
            }

            var maxSeqNumber = Math.Pow(2, options.SeqBitLength) - 1;
            if (options.MaxSeqNumber < 0 || options.MaxSeqNumber > maxSeqNumber)
            {
                throw new ApplicationException("MaxSeqNumber error. (range:[1, " + maxSeqNumber + "].");
            }

            var maxValue = maxSeqNumber; // maxSeqNumber - 1;
            if (options.MinSeqNumber < 1 || options.MinSeqNumber > maxValue)
            {
                throw new ApplicationException("MinSeqNumber error. (range:[1, " + maxValue + "].");
            }

            InternalSnowflakeWorker = options.Method switch
            {
                1 => new SnowflakeWorkerM1(options),
                2 => new SnowflakeWorkerM2(options),
                _ => new SnowflakeWorkerM1(options),
            };

            if (options.Method == 1)
            {
                Thread.Sleep(500);
            }
        }

        /// <summary>
        /// 生成雪花 ID 过程中的异步事件
        /// </summary>
        public Action<OverCostActionArg> GenIdActionAsync
        {
            get => InternalSnowflakeWorker.GenAction;
            set => InternalSnowflakeWorker.GenAction = value;
        }

        /// <summary>
        /// 生成新的 long 类型数据
        /// </summary>
        /// <returns></returns>
        public long NewLong()
        {
            return InternalSnowflakeWorker.NextId();
        }
    }
}