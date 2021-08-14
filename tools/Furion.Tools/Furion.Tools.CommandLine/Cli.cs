using System;
using System.Collections.Generic;
using System.Linq;

namespace Furion.Tools.CommandLine
{
    /// <summary>
    /// Cli 常用方法
    /// </summary>
    public static partial class Cli
    {
        /// <summary>
        /// 参数元数据集合
        /// </summary>
        public static IEnumerable<ArgumentMetadata> ArgumentMetadatas { get; internal set; }

        /// <summary>
        /// 判断参数是否定义
        /// </summary>
        /// <param name="argumentName"></param>
        /// <param name="handler"></param>
        public static void Check(string argumentName, Action<ArgumentMetadata> handler)
        {
            if (handler == null) throw new ArgumentNullException(nameof(handler));

            var argumentMetadata = ArgumentMetadatas.FirstOrDefault(u => u.ShortName.ToString() == argumentName || u.LongName == argumentName || u.Property.Name == argumentName);
            if (argumentMetadata != null && argumentMetadata.IsTransmission) handler(argumentMetadata);
        }

        /// <summary>
        /// 判断是否空参数
        /// </summary>
        /// <param name="handler"></param>
        /// <returns></returns>
        public static void CheckEmpty(Action handler)
        {
            if (handler == null) throw new ArgumentNullException(nameof(handler));
            if (Arguments == null || Arguments.ArgumentDictionary.Count == 0) handler();
        }
    }
}