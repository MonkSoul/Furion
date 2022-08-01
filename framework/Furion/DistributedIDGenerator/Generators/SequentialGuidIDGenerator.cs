// MIT License
//
// Copyright (c) 2020-2022 百小僧, Baiqian Co.,Ltd.
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using System.Security.Cryptography;

namespace Furion.DistributedIDGenerator;

/// <summary>
/// 连续 GUID ID 生成器
/// <para>代码参考自：https://github.com/PomeloFoundation/Pomelo.EntityFrameworkCore.MySql/blob/ebe011a6f1b2a2a9709fe558cfc7ed3215b55c37/src/EFCore.MySql/ValueGeneration/Internal/MySqlSequentialGuidValueGenerator.cs </para>
/// </summary>
public class SequentialGuidIDGenerator : IDistributedIDGenerator, ISingleton
{
    /// <summary>
    /// 随机数生成器
    /// </summary>
    private static readonly RandomNumberGenerator _rng = RandomNumberGenerator.Create();

    /// <summary>
    /// 生成逻辑
    /// </summary>
    /// <param name="idGeneratorOptions"></param>
    /// <returns></returns>
    public object Create(object idGeneratorOptions = null)
    {
        // According to RFC 4122:
        // dddddddd-dddd-Mddd-Ndrr-rrrrrrrrrrrr
        // - M = RFC version, in this case '4' for random UUID
        // - N = RFC variant (plus other bits), in this case 0b1000 for variant 1
        // - d = nibbles based on UTC date/time in ticks
        // - r = nibbles based on random bytes

        var options = idGeneratorOptions as SequentialGuidSettings;

        var randomBytes = new byte[7];
        _rng.GetBytes(randomBytes);
        var ticks = (ulong)(options?.TimeNow == null ? DateTimeOffset.UtcNow : options.TimeNow.Value).Ticks;

        var uuidVersion = (ushort)4;
        var uuidVariant = (ushort)0b1000;

        var ticksAndVersion = (ushort)((ticks << 48 >> 52) | (ushort)(uuidVersion << 12));
        var ticksAndVariant = (byte)((ticks << 60 >> 60) | (byte)(uuidVariant << 4));

        if (options?.LittleEndianBinary16Format == true)
        {
            var guidBytes = new byte[16];
            var tickBytes = BitConverter.GetBytes(ticks);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(tickBytes);
            }

            Buffer.BlockCopy(tickBytes, 0, guidBytes, 0, 6);
            guidBytes[6] = (byte)(ticksAndVersion << 8 >> 8);
            guidBytes[7] = (byte)(ticksAndVersion >> 8);
            guidBytes[8] = ticksAndVariant;
            Buffer.BlockCopy(randomBytes, 0, guidBytes, 9, 7);

            return new Guid(guidBytes);
        }

        var guid = new Guid((uint)(ticks >> 32), (ushort)(ticks << 32 >> 48), ticksAndVersion,
            ticksAndVariant,
            randomBytes[0],
            randomBytes[1],
            randomBytes[2],
            randomBytes[3],
            randomBytes[4],
            randomBytes[5],
            randomBytes[6]);

        return guid;
    }
}