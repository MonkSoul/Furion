import { useState } from "react";
import { PublicUser } from "../api-services";
import { clearAccessTokens } from "../axios-utils";
import AuthContext from "./AuthContext";

/**
 * 授权提供器
 * @param param0
 * @returns
 */
export default function AuthProvider({
  children,
}: {
  children: React.ReactNode;
}) {
  /**
   * 读取用户信息
   */
  const [user, setUser] = useState<PublicUser | undefined>(() => {
    const user_store = window.localStorage.getItem("user_info");
    return !user_store ? undefined : (JSON.parse(user_store) as PublicUser);
  });

  /**
   * 初始化登录方法
   * @param data
   * @param callback
   */
  const signin = (data: PublicUser, callback: VoidFunction) => {
    window.localStorage.setItem("user_info", JSON.stringify(data));
    setUser(data);
    callback();
  };

  /**
   * 初始化退出方法
   * @param callback
   */
  const signout = (callback: VoidFunction) => {
    window.localStorage.removeItem("user_info");
    callback();
    clearAccessTokens();
  };

  return (
    <AuthContext.Provider
      value={{
        user,
        signin,
        signout,
      }}
    >
      {children}
    </AuthContext.Provider>
  );
}
