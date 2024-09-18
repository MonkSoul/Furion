import CryptoJS from "crypto-js";

/**
 * 生成 KSort 签名
 * @param appId APP_ID
 * @param appKey APP_KEY
 * @param command 命令
 * @param data 数据
 * @param timestamp 时间戳（可选）
 * @returns
 */
export function signatureByKSort(
  appId,
  appKey,
  command,
  data,
  timestamp = null
) {
  // 序列化数据
  const input = typeof data === "string" ? data : JSON.stringify(data);

  // 生成时间戳
  if (!timestamp) {
    timestamp =
      new Date().getTime() - new Date("1970-01-01T00:00:00Z").getTime();
  }

  const sData = {
    app_id: appId,
    app_key: appKey,
    command,
    data: input,
    timestamp,
  };

  // 获取对象的所有键并对键进行排序
  const keys = Object.keys(sData);
  keys.sort();

  // 生成签名
  let result = "";
  for (let i = 0; i < keys.length; i++) {
    const key = keys[i];
    const value = sData[key];
    result += key + "=" + value;

    if (i < keys.length - 1) {
      result += ",";
    }
  }

  // MD5 小写加密
  const hash = CryptoJS.MD5(result);
  const hexString = hash.toString(CryptoJS.enc.Hex);
  var signature = hexString.toLowerCase();

  return {
    app_id: appId,
    app_key: appKey,
    command,
    data: input,
    timestamp,
    signature,
  };
}
