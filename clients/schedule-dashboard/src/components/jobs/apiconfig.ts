import { CachePolicies } from "use-http";

/**
 * 接口配置
 */
const apiconfig = {
  hostAddress:
    process.env.NODE_ENV !== "production"
      ? "https://localhost:5001/schedule/api"
      : "/schedule/api",
  options: {
    headers: {
      Accept: `application/json`,
    },
    cachePolicy: CachePolicies.NO_CACHE,
  },
};

export default apiconfig;
