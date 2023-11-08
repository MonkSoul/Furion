import Link from "@docusaurus/Link";
import useBaseUrl from "@docusaurus/useBaseUrl";
import React from "react";
import classes from "./Assistance.module.css";

export default function Assistance({ style = {} }) {
  const count = 296;

  return (
    <Link
      className={classes.ass}
      to={useBaseUrl("docs/subscribe")}
      style={{ ...style }}
    >
      <div className={classes.title}>开通 VIP 服务助力 Furion v5 发布</div>
      <div className={classes.progress}>
        <div className={classes.number}>{count}</div>
        <div className={classes.percent}>
          <div
            className={classes.current}
            style={{
              width: (count / 1000) * 100 + "%",
            }}
          ></div>
        </div>
        <div className={classes.number}>1000</div>
      </div>
    </Link>
  );
}
