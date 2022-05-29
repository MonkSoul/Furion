/**
 * https://editor.swagger.io 代码生成 typescript-axios 辅组工具库
 * 适配 axios 版本：v0.21.1
 * 视频教程：https://www.bilibili.com/video/BV1EW4y1C71D
 */

import globalAxios, { AxiosInstance } from "axios";
import { Configuration } from "./api-services";
import { BaseAPI, BASE_PATH } from "./api-services/base";

// 如果是 Angular 项目，则取消下面注释即可
// import { environment } from './environments/environment';

/**
 * 接口服务器配置
 */
export const serveConfig = new Configuration({
  // 如果是 Angular 项目，则取消下面注释，并删除 process.env.NODE_ENV !== "production"
  // basePath: !environment.production
  basePath:
    process.env.NODE_ENV !== "production"
      ? "https://localhost:44342" // 开发环境服务器接口地址
      : "https://furion.icu", // 生产环境服务器接口地址
});

// token 键定义
const accessTokenKey = "access-token";
const refreshAccessTokenKey = `x-${accessTokenKey}`;

// 清除 token
const clearAccessTokens = () => {
  window.localStorage.removeItem(accessTokenKey);
  window.localStorage.removeItem(refreshAccessTokenKey);
};

/**
 * axios 默认实例
 */
export const axiosInstance: AxiosInstance = globalAxios;

// 这里可以配置 axios 更多选项 =========================================

// axios 请求拦截
axiosInstance.interceptors.request.use(
  (conf) => {
    // 将 token 添加到请求报文头中
    conf.headers!["Authorization"] = `Bearer ${window.localStorage.getItem(
      accessTokenKey
    )}`;
    conf.headers!["X-Authorization"] = `Bearer ${window.localStorage.getItem(
      refreshAccessTokenKey
    )}`;

    // 这里编写请求拦截代码 =========================================

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

    // 处理未进行规范化处理的
    if (status >= 400) {
      throw new Error(res.statusText || "Request Error.");
    }

    // 处理规范化结果错误
    if (serve && serve.hasOwnProperty("errors") && serve.errors) {
      throw new Error(JSON.stringify(serve.errors || "Request Error."));
    }

    // 读取响应报文头 token 信息
    var accessToken = res.headers[accessTokenKey];
    var refreshAccessToken = res.headers[refreshAccessTokenKey];

    // 判断是否是无效 token
    if (accessToken === "invalid_token") {
      clearAccessTokens();
    }
    // 判断是否存在刷新 token，如果存在则存储在本地
    else if (
      refreshAccessToken &&
      accessToken &&
      accessToken !== "invalid_token"
    ) {
      window.localStorage.setItem(accessTokenKey, accessToken);
      window.localStorage.setItem(refreshAccessTokenKey, refreshAccessToken);
    }

    // 这里编写响应拦截代码 =========================================

    return res;
  },
  (error) => {
    // 这里编写响应错误代码

    return Promise.reject(error);
  }
);

/**
 * 包装 Promise 并返回 [Error, any]
 * @param promise Promise 方法
 * @param errorExt 自定义错误信息（拓展）
 * @returns [Error, any]
 */
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

/**
 * 获取/创建服务 API 实例
 * @param apiType BaseAPI 派生类型
 * @returns 服务API 实例
 */
export function getAPI<T extends BaseAPI>(
  apiType: new (
    configuration?: Configuration,
    basePath?: string,
    axiosInstance?: AxiosInstance
  ) => T
) {
  return new apiType(serveConfig, BASE_PATH, axiosInstance);
}

/**
 * 解密 JWT token 的信息
 * @param token jwt token 字符串
 * @returns <any>object
 */
export function decryptJWT(token: string): any {
  token = token.replace(/\_/g, "/").replace(/\-/g, "+");
  var json = decodeURIComponent(escape(window.atob(token.split(".")[1])));
  return JSON.parse(json);
}
