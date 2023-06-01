/**
 * 当前版本：v1.1.0
 * 使用描述：https://editor.swagger.io 代码生成 typescript-angular 辅组工具库
 */

import {
  HttpClientModule,
  HttpErrorResponse,
  HttpEvent,
  HttpHandler,
  HttpHeaders,
  HttpInterceptor,
  HttpRequest,
  HttpResponse,
  HttpResponseBase,
  HTTP_INTERCEPTORS
} from "@angular/common/http";
import { Injectable, NgModule } from "@angular/core";
import { finalize, Observable, tap } from "rxjs";
import { ApiModule, Configuration } from "./api-services";
import { environment } from "./environments/environment";

/**
 * 接口服务器配置
 */
export const serveConfig = new Configuration({
  basePath: !environment.production
    ? "https://localhost:44342" // 开发环境服务器接口地址
    : "http://furion.baiqian.ltd", // 生产环境服务器接口地址
});

// token 键定义
export const accessTokenKey = "access-token";
export const refreshAccessTokenKey = `x-${accessTokenKey}`;

// 清除 token
export const clearAccessTokens = () => {
  window.localStorage.removeItem(accessTokenKey);
  window.localStorage.removeItem(refreshAccessTokenKey);

  // 这里可以添加清除更多 Key =========================================
};

// 错误处理
export const throwError = (message: string) => {
  throw new Error(message);
};

/**
 * 定义客户端请求拦截器
 */
@Injectable()
export class ClientHttpInterceptor implements HttpInterceptor {
  /**
   * 构造函数
   */
  constructor() {}

  /**
   * 实现拦截逻辑
   * @param req 请求对象
   * @param next 调用下一个中间件
   * @returns 响应对象
   */
  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    // 构建请求拦截信息
    const update: { headers: HttpHeaders } = { headers: req.headers };

    // 获取本地的 token
    const accessToken = window.localStorage.getItem(accessTokenKey);
    if (accessToken) {
      // 将 token 添加到请求报文头中
      update.headers = update.headers.set(
        "Authorization",
        `Bearer ${accessToken}`
      );

      // 判断 accessToken 是否过期
      const jwt: any = decryptJWT(accessToken);
      const exp = getJWTDate(jwt.exp as number);

      // token 已经过期
      if (new Date() >= exp) {
        // 获取刷新 token
        const refreshAccessToken = window.localStorage.getItem(
          refreshAccessTokenKey
        );

        // 携带刷新 token
        if (refreshAccessToken) {
          update.headers = update.headers.set(
            "X-Authorization",
            `Bearer ${refreshAccessToken}`
          );
        }
      }
    }

    // 克隆一份请求再修改
    const wrapReq = req.clone(update);

    // 这里编写请求拦截代码 =========================================

    return next.handle(wrapReq).pipe(
      tap(
        (event) => {
          if (event instanceof HttpResponse) {
            const res = event as HttpResponse<any>;

            // 检查并存储授权信息
            checkAndStoreAuthentication(res);

            // 处理规范化结果错误
            var serve = res.body;
            if (serve && serve.hasOwnProperty("errors") && serve.errors) {
              // 处理规范化 401 授权问题
              if (serve.errors === "401 Unauthorized") {
                clearAccessTokens();
              }

              throwError(
                !serve.errors
                  ? "Request Error."
                  : typeof serve.errors === "string"
                  ? serve.errors
                  : JSON.stringify(serve.errors)
              );
              return;
            }

            // 这里编写响应拦截代码 =========================================
          }
        },
        (error) => {
          if (error instanceof HttpErrorResponse) {
            // 获取响应对象并解析状态码
            const res = error as HttpErrorResponse;
            const status = res.status;

            // 检查并存储授权信息
            checkAndStoreAuthentication(res);

            // 检查 401 权限
            if (status === 401) {
              clearAccessTokens();
            }
          }

          // 这里编写响应错误代码
        }
      ),
      finalize(() => {
        // 这里编写请求完成或错误处理 =========================================
      })
    );
  }
}

/**
 * 导出 ServeModule
 * 依赖 HttpClientModule，ClientHttpInterceptor
 */
@NgModule({
  imports: [HttpClientModule, ApiModule.forRoot(() => serveConfig)],
  declarations: [],
  exports: [],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: ClientHttpInterceptor,
      multi: true,
    },
  ],
})
export class ServeModule {}

/**
 * 检查并存储授权信息
 * @param res 响应对象
 */
export function checkAndStoreAuthentication(res: HttpResponseBase): void {
  // 读取响应报文头 token 信息
  var accessToken = res.headers.get(accessTokenKey);
  var refreshAccessToken = res.headers.get(refreshAccessTokenKey);

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
}

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
 * 解密 JWT token 的信息
 * @param token jwt token 字符串
 * @returns <any>object
 */
export function decryptJWT(token: string): any {
  token = token.replace(/_/g, "/").replace(/-/g, "+");
  var json = decodeURIComponent(escape(window.atob(token.split(".")[1])));
  return JSON.parse(json);
}

/**
 * 将 JWT 时间戳转换成 Date
 * @description 主要针对 `exp`，`iat`，`nbf`
 * @param timestamp 时间戳
 * @returns Date 对象
 */
export function getJWTDate(timestamp: number): Date {
  return new Date(timestamp * 1000);
}
