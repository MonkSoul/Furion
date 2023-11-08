import useBaseUrl from "@docusaurus/useBaseUrl";
import React, { useContext } from "react";
import GlobalContext from "./GlobalContext";

export default function SpecDonate({ style }) {
  const { setDonate } = useContext(GlobalContext);

  return (
    // <a
    //   style={{
    //     minHeight: 120,
    //     backgroundColor: "#f0f0f0",
    //     display: "flex",
    //     flexDirection: "column",
    //     alignItems: "center",
    //     justifyContent: "center",
    //     borderRadius: 8,
    //     marginBottom: 20,
    //     textDecoration: "none",
    //     boxSizing: "border-box",
    //     padding: 20,
    //     userSelect: "none",
    //     cursor: "pointer",
    //   }}
    //   onClick={() => setDonate(true)}
    // >
    //   <h3
    //     style={{
    //       fontWeight: 500,
    //       fontSize: 30,
    //       margin: "4px 0 0 0 ",
    //       textAlign: "left",
    //       background: "linear-gradient(to right, red, blue)",
    //       backgroundClip: "text",
    //       WebkitBackgroundClip: "text",
    //       color: "transparent",
    //       whiteSpace: "nowrap",
    //       cursor: "pointer",
    //       marginBottom: 5,
    //     }}
    //   >
    //     特别赞助（虚席以待）
    //   </h3>
    //   <div>
    //     如果 Furion 对您有所帮助，并且您希望 Furion 能够继续发展下去，请考虑
    //     ⌈赞助⌋ 我们。
    //   </div>
    // </a>
    <a
      href="http://github.crmeb.net/u/furion"
      target="_blank"
      style={{
        display: "block",
        marginBottom: 20,
        textDecoration: "none",
      }}
      title="CRMEB 专注开源电商系统研发"
    >
      <img
        src={useBaseUrl("img/crmeb-spec.jpg")}
        style={{ display: "block", pointerEvents: "none" }}
      />
    </a>
  );
}
