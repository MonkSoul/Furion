import useBaseUrl from "@docusaurus/useBaseUrl";
import { useContext } from "react";
import GlobalContext from "./GlobalContext";

export default function Vip({}) {
  const { setVip } = useContext(GlobalContext);

  return (
    <div
      style={{
        position: "fixed",
        width: 200,
        height: 200,
        zIndex: 999999999,
        bottom: 10,
        right: 0,
        borderRadius: 5,
        overflow: "hidden",
      }}
    >
      <div
        style={{
          position: "absolute",
          zIndex: 2,
          right: 10,
          top: 10,
          cursor: "pointer",
        }}
        title="关闭"
        onClick={() => setVip(false)}
      >
        X
      </div>
      <a
        href="/docs/subscribe"
        target="_blank"
        style={{ display: "block" }}
        title="👍 2023 年 12 月 01 日前仅需 499元/年享有 VIP 服务"
      >
        <img
          src={useBaseUrl("img/cmp-vip.png")}
          style={{ display: "block", width: 200, height: 200 }}
        />
      </a>
    </div>
  );
}
