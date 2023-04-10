using Furion.DataEncryption;
using Furion.DataEncryption.Extensions;
using Furion.DynamicApiController;

namespace Furion.TestProject;

/// <summary>
/// 数据加解密集成测试
/// </summary>
public class DataEncryptionTests : IDynamicApiController
{
    /// <summary>
    /// 测试 MD5 加密
    /// </summary>
    /// <returns></returns>
    public string TestMD5Encrypt(string text, bool uppercase)
    {
        var data1 = MD5Encryption.Encrypt(text, uppercase);
        var data2 = text.ToMD5Encrypt(uppercase);

        var isEqual = data1 == data2;
        if (!isEqual) throw new Exception("静态加密和字符串拓展加密不相等");

        return data1;
    }

    /// <summary>
    /// 比较文本和加密后字符串
    /// </summary>
    /// <returns></returns>
    public bool TestMD5Compare(string text, string hash, bool uppercase)
    {
        return MD5Encryption.Compare(text, hash, uppercase);
    }
}