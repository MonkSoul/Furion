using System.Data;
using System.Text.Json;

namespace Furion.DataEncryption;

/// <summary>
/// KSort 加密（数据签名）
/// </summary>
[SuppressSniffer]
public class KSortEncryption
{
    private static DateTime _timeStampStartTime = new(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

    /// <summary>
    /// 数据加密（签名）
    /// </summary>
    /// <param name="appId">APP_ID</param>
    /// <param name="appKey">APP_KEY</param>
    /// <param name="command">命令</param>
    /// <param name="data">序列化后的字符串</param>
    /// <param name="timestamp">时间戳</param>
    /// <returns><see cref="object"/></returns>
    public static KSortSignature Encrypt(string appId, string appKey, string command, string data, long? timestamp = null)
    {
        ArgumentNullException.ThrowIfNull(appId);
        ArgumentNullException.ThrowIfNull(appKey);
        ArgumentNullException.ThrowIfNull(command);

        timestamp ??= (long)(DateTime.Now.ToUniversalTime() - _timeStampStartTime).TotalMilliseconds;

        var dic = new Dictionary<string, object>
        {
            {"app_id", appId },
            {"app_key", appKey },
            {"command", command },
            {"data", data },
            {"timestamp", timestamp },
         };

        // ksort 排序
        var sortedDic = dic.OrderBy(kvp => kvp.Key);

        // 半角逗号连接
        var output = string.Join(",", sortedDic.Select(kvp => $"{kvp.Key}={kvp.Value}"));

        // utf8 编码，32位小写
        var signature = MD5Encryption.Encrypt(output);

        return new KSortSignature
        {
            app_id = appId,
            app_key = appKey,
            command = command,
            data = data,
            timestamp = timestamp.Value,
            signature = signature
        };
    }

    /// <summary>
    /// 比较数据签名
    /// </summary>
    /// <param name="kSortSignature"></param>
    /// <returns></returns>
    public static bool Compare(KSortSignature kSortSignature)
    {
        ArgumentNullException.ThrowIfNull(kSortSignature);

        return Encrypt(kSortSignature.app_id, kSortSignature.app_key, kSortSignature.command, kSortSignature.data, kSortSignature.timestamp) == kSortSignature;
    }

    /// <summary>
    /// 比较数据签名
    /// </summary>
    /// <param name="signatureData">签名数据</param>
    /// <returns></returns>
    public static bool Compare(string signatureData)
    {
        var kSortSignature = JsonSerializer.Deserialize<KSortSignature>(signatureData);
        ArgumentNullException.ThrowIfNull(kSortSignature);

        return Encrypt(kSortSignature.app_id, kSortSignature.app_key, kSortSignature.command, kSortSignature.data, kSortSignature.timestamp) == kSortSignature;
    }
}

/// <summary>
/// KSort 签名类
/// </summary>
public class KSortSignature : IEquatable<KSortSignature>
{
    /// <summary>
    /// APP_ID
    /// </summary>
    public string app_id { get; set; }

    /// <summary>
    /// APP_KEY
    /// </summary>
    public string app_key { get; set; }

    /// <summary>
    /// 命令
    /// </summary>
    public string command { get; set; }

    /// <summary>
    /// 序列化的字符串
    /// </summary>
    public string data { get; set; }

    /// <summary>
    /// 时间戳
    /// </summary>
    public long timestamp { get; set; }

    /// <summary>
    /// 签名
    /// </summary>
    public string signature { get; set; }

    /// <inheritdoc />
    public bool Equals(KSortSignature other)
    {
        if (other == null) return false;

        return app_id == other.app_id &&
               app_key == other.app_key &&
               command == other.command &&
               data == other.data &&
               timestamp == other.timestamp &&
               signature == other.signature;
    }

    /// <inheritdoc />
    public override bool Equals(object obj)
    {
        return this.Equals(obj as KSortSignature);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(app_id, app_key, command, data, timestamp, signature);
    }

    /// <inheritdoc />
    public static bool operator ==(KSortSignature lhs, KSortSignature rhs)
    {
        if (ReferenceEquals(lhs, rhs)) return true;
        if (lhs is null || rhs is null) return false;
        return lhs.Equals(rhs);
    }

    /// <inheritdoc />
    public static bool operator !=(KSortSignature lhs, KSortSignature rhs)
    {
        return !(lhs == rhs);
    }
}