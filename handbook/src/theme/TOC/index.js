import useBaseUrl from "@docusaurus/useBaseUrl";
import TOCItems from "@theme/TOCItems";
import clsx from "clsx";
import React from "react";
import styles from "./styles.module.css";

// Using a custom className
// This prevents TOCInline/TOCCollapsible getting highlighted by mistake
const LINK_CLASS_NAME = "table-of-contents__link toc-highlight";
const LINK_ACTIVE_CLASS_NAME = "table-of-contents__link--active";
export default function TOC({ className, ...props }) {
  return (
    <div className={clsx(styles.tableOfContents, "thin-scrollbar", className)}>
      <DotNETChina />
      <TOCItems
        {...props}
        linkClassName={LINK_CLASS_NAME}
        linkActiveClassName={LINK_ACTIVE_CLASS_NAME}
      />
    </div>
  );
}

function DotNETChina() {
  return (
    <>
      <a
        href="https://gitee.com/dotnetchina"
        target="_blank"
        style={{ display: "block", position: "relative" }}
        title="了解 dotNET China 组织"
      >
        <img
          src={useBaseUrl("img/chinadotnet.png")}
          style={{ display: "block", width: "90%", margin: "0 auto" }}
        />
      </a>
      <div style={{ position: "relative" }}>
        <img
          title="微信扫描关注 Furion 官方公众号"
          src={useBaseUrl("img/weixin_qrcode.jpg")}
          style={{ display: "block", margin: "0 auto" }}
        />
        <div
          style={{
            textAlign: "center",
            color: "#c9c9c9",
            position: "absolute",
            width: "100%",
            bottom: -8,
            fontSize: 12,
            left: 0,
            whiteSpace: "nowrap",
          }}
        >
          关注 Furion 公众号订阅最新资讯
        </div>
      </div>
    </>
  );
}
