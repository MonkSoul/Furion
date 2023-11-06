import Link from "@docusaurus/Link";
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
          right: 5,
          top: 5,
          cursor: "pointer",
          borderRadius: "50%",
          backgroundColor: "rgba(0,0,0,0.4)",
          color: "#f1f1f1",
          width: 20,
          height: 20,
          textAlign: "center",
          lineHeight: "20px",
          fontSize: 12,
        }}
        title="关闭"
        onClick={() => setVip(false)}
      >
        X
      </div>
      <Link
        to={useBaseUrl("docs/subscribe")}
        style={{ display: "block" }}
        title="👍 2023年12月01日前仅需 499元/年享有 VIP 服务"
      >
        <img
          src={useBaseUrl("img/cmp-vip.png")}
          style={{ display: "block", width: 200, height: 200 }}
        />
      </Link>
    </div>
  );
}
