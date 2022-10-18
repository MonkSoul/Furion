import React from "react";
import { PublicUser } from "../api-services";

/**
 * 授权类型
 */
export interface AuthContextType {
  /**
   * 用户信息
   */
  user?: PublicUser;
  /**
   * 登录系统
   */
  signin: (user: PublicUser, callback: VoidFunction) => void;
  /**
   * 退出系统
   */
  signout: (callback: VoidFunction) => void;
}

/**
 * 创建授权上下文
 */
const AuthContext = React.createContext<AuthContextType>(null!);

export default AuthContext;
