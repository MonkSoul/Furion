import React from "react";
import styles from "./FloatBar.module.css";
import useBaseUrl from "@docusaurus/useBaseUrl";

export default function FloatBar() {
  return (
    <div className={styles.floatbar}>
      <div className={styles.qrcode}>
        <img
          title="微信扫码关注 Furion 官方公众号"
          src={useBaseUrl("img/weixin_qrcode.jpg")}
          style={{ display: "block" }}
        />
        <div>❤️ 关注 Furion 微信公众号有惊喜哦！</div>
      </div>
      <div
        style={{
          display: "flex",
          flexDirection: "row-reverse",
          alignItems: "flex-start",
        }}
      >
        <div className={styles.title}>🫠 遇到问题</div>
        <div className={styles.extend}>
          <Item
            title="问题反馈"
            description="到 Furion 开源仓库反馈"
            url="https://gitee.com/dotnetchina/Furion/issues"
          />
          <Item
            title="成为赞助商"
            description="支持 Furion 的开源事业"
            url="http://furion.baiqian.ltd/docs/donate"
          />
        </div>
      </div>
    </div>
  );
}

function Item({ title, description, url }) {
  return (
    <div className={styles.item} onClick={() => window.open(url, "_blank")}>
      <div style={{ flex: 1 }}>
        <div className={styles.itemTitle}>{title}</div>
        <div className={styles.itemDesc}>{description}</div>
      </div>
      <div className={styles.jiantou}></div>
    </div>
  );
}
