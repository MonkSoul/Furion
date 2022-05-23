import globalAxios, { AxiosInstance } from "axios";
import { Configuration } from "../api-services";
import { BaseAPI, BASE_PATH } from "../api-services/base";

// 接口服务器配置
export const serveConfig = new Configuration({
  basePath:
    process.env.NODE_ENV !== "production"
      ? "https://localhost:44342"
      : "https://furion.icu",
});

// jwt token 键定义，清除 token 操作
const accessTokenKey = "access-token";
const refreshAccessTokenKey = `x-${accessTokenKey}`;
const clearAccessTokens = () => {
  window.localStorage.removeItem(accessTokenKey);
  window.localStorage.removeItem(refreshAccessTokenKey);
};

// axios 实例配置
export const axiosInstance: AxiosInstance = globalAxios;
// axios 请求拦截
axiosInstance.interceptors.request.use(
  (conf) => {
    conf.headers!["Authorization"] = `Bearer ${window.localStorage.getItem(
      accessTokenKey
    )}`;
    conf.headers!["X-Authorization"] = `Bearer ${window.localStorage.getItem(
      refreshAccessTokenKey
    )}`;

    // 这里编写请求拦截代码

    return conf;
  },
  (error) => {
    // 这里编写请求错误代码

    return Promise.reject(error);
  }
);

// axios 响应拦截
axiosInstance.interceptors.response.use(
  (res) => {
    // 获取状态码和返回数据
    var status = res.status;
    var serve = res.data;

    // 处理 401
    if (status === 401) {
      clearAccessTokens();
    }

    // 处理规范化结果 400/500 错误
    if (serve && serve.hasOwnProperty("errors")) {
      throw new Error(JSON.stringify(serve.errors || "[500] Server Error."));
    }

    // 读取响应头授权信息
    var accessToken = res.headers[accessTokenKey];
    var refreshAccessToken = res.headers[refreshAccessTokenKey];

    // 判断是否是 401 状态码或无效 token
    if (accessToken === "invalid_token") {
      clearAccessTokens();
    }
    // 判断是否存在刷新 token
    else if (
      refreshAccessToken &&
      accessToken &&
      accessToken !== "invalid_token"
    ) {
      window.localStorage.setItem(accessTokenKey, accessToken);
      window.localStorage.setItem(refreshAccessTokenKey, refreshAccessToken);
    }

    // 这里编写响应拦截代码

    return res;
  },
  (error) => {
    // 这里编写响应错误代码

    return Promise.reject(error);
  }
);

// 处理 Promise 并返回 [err,res] 数组
export function feature<T, U = Error>(
  promise: Promise<T>,
  errorExt?: object
): Promise<[U, undefined] | [null, T]> {
  return promise
    .then<[null, T]>((data: T) => [null, data])
    .catch<[U, undefined]>((err: U) => {
      if (errorExt) {
        const parsedError = Object.assign({}, err, errorExt);
        return [parsedError, undefined];
      }

      return [err, undefined];
    });
}

// 获取特定类型 API 实例
export function getAPI<T extends BaseAPI>(
  apiType: new (
    configuration?: Configuration,
    basePath?: string,
    axiosInstance?: AxiosInstance
  ) => T
) {
  return new apiType(serveConfig, BASE_PATH, axiosInstance);
}
