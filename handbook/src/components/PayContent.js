import useBaseUrl from "@docusaurus/useBaseUrl";
import React from "react";

export function PayContent() {
  return (
    <div>
      <div
        style={{
          padding: 10,
          textAlign: "center",
          fontSize: 16,
          marginTop: 5,
          fontWeight: 500,
        }}
      >
        开通 VIP 服务仅需 499 元/年
      </div>
      <img
        src={useBaseUrl("img/cmp-donate.jpg")}
        style={{ width: 300, display: "block" }}
      />
    </div>
  );
}
