import Link from "@docusaurus/Link";
import useBaseUrl from "@docusaurus/useBaseUrl";
import { useContext } from "react";
import GlobalContext from "./GlobalContext";

export default function VipImageList({ padding = 5 }) {
  const { setDonate } = useContext(GlobalContext);

  return (
    <Link
      to={useBaseUrl("/docs/subscribe")}
      style={{
        display: "flex",
        width: "100%",
        boxSizing: "border-box",
        alignItems: "center",
        justifyContent: "space-between",
        paddingLeft: padding,
      }}
      title="⭐️ 开通 VIP 服务仅需 499 元/年，尊享 365 天项目无忧 ⭐️"
      onClick={() => setDonate(false)}
    >
      {Array.from({ length: 10 }, (_, i) => (
        <Image index={i + 1} padding={padding} />
      ))}
    </Link>
  );
}

function Image({ index, padding }) {
  return (
    <div
      style={{
        flex: 1,
        margin: `${padding}px ${padding}px 5px 0`,
        userSelect: "none",
        pointerEvents: "none",
      }}
    >
      <img
        src={useBaseUrl("img/vip" + index + ".jpeg")}
        style={{
          width: "100%",
          height: "100%",
          display: "block",
          borderRadius: 8,
        }}
      />
    </div>
  );
}
