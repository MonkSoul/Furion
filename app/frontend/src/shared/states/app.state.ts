/**
 * App.tsx 全局状态
 */

import create from "zustand";

/**
 * App 状态
 */
export interface AppState {
  /**
   * 亮色/暗色模式
   */
  mode: string;
  /**
   * 切换亮色/暗色模式
   */
  switchMode: () => void;
}

/**
 * App 状态 Hooks
 */
const useAppState = create<AppState>((set) => ({
  mode: "semi-always-light",
  switchMode: () =>
    set((state) => ({
      mode:
        state.mode === "semi-always-dark"
          ? "semi-always-light"
          : "semi-always-dark",
    })),
}));

export default useAppState;
