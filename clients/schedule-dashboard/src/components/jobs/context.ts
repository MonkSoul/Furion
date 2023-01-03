import { createContext } from "react";

/**
 * 全局上下文
 */
const GlobalContext = createContext<{ rate: number }>({ rate: 300 });

export default GlobalContext;
