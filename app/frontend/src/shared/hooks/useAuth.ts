import React from "react";
import AuthContext from "../contexts/AuthContext";

/**
 * 获取用户授权信息
 * @returns
 */
export default function useAuth() {
  return React.useContext(AuthContext);
}
