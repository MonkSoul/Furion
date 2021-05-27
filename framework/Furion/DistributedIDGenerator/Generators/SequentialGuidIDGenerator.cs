// -----------------------------------------------------------------------------
// 让 .NET 开发更简单，更通用，更流行。
// Copyright © 2020-2021 Furion, 百小僧, Baiqian Co.,Ltd.
//
// 框架名称：Furion
// 框架作者：百小僧
// 框架版本：2.7.3
// 源码地址：Gitee： https://gitee.com/dotnetchina/Furion
//          Github：https://github.com/monksoul/Furion
// 开源协议：Apache-2.0（https://gitee.com/dotnetchina/Furion/blob/master/LICENSE）
// -----------------------------------------------------------------------------

using Furion.DependencyInjection;
using System;
using System.Security.Cryptography;

namespace Furion.DistributedIDGenerator
{
    /// <summary>
    /// 连续 GUID ID 生成器
    /// </summary>
    public class SequentialGuidIDGenerator : IDistributedIDGenerator, ISingleton
    {
        /// <summary>
        /// 生成强随机字符串提供器
        /// </summary>
        private static readonly RNGCryptoServiceProvider RandomGenerator = new();

        /// <summary>
        /// 生成逻辑
        /// </summary>
        /// <param name="idGeneratorOptions"></param>
        /// <returns></returns>
        public object Create(object idGeneratorOptions = null)
        {
            var options = (idGeneratorOptions ?? new SequentialGuidSettings()
            {
                GuidType = SequentialGuidType.SequentialAsString
            }) as SequentialGuidSettings;

            var randomBytes = new byte[10];
            RandomGenerator.GetBytes(randomBytes);

            var timestamp = DateTime.UtcNow.Ticks / 10000L;

            var timestampBytes = BitConverter.GetBytes(timestamp);

            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(timestampBytes);
            }

            var guidBytes = new byte[16];

            switch (options.GuidType)
            {
                // 生成字符串类型
                case SequentialGuidType.SequentialAsString:
                case SequentialGuidType.SequentialAsBinary:

                    Buffer.BlockCopy(timestampBytes, 2, guidBytes, 0, 6);
                    Buffer.BlockCopy(randomBytes, 0, guidBytes, 6, 10);

                    if (options.GuidType == SequentialGuidType.SequentialAsString && BitConverter.IsLittleEndian)
                    {
                        Array.Reverse(guidBytes, 0, 4);
                        Array.Reverse(guidBytes, 4, 2);
                    }

                    break;
                // 序列化部分在末尾部分
                case SequentialGuidType.SequentialAtEnd:

                    Buffer.BlockCopy(randomBytes, 0, guidBytes, 0, 10);
                    Buffer.BlockCopy(timestampBytes, 2, guidBytes, 10, 6);
                    break;
            }

            return new Guid(guidBytes);
        }
    }
}