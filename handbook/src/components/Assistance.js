import Link from "@docusaurus/Link";
import useBaseUrl from "@docusaurus/useBaseUrl";
import Tooltip from "@uiw/react-tooltip";
import React from "react";
import classes from "./Assistance.module.css";

export default function Assistance({ style = {}, onClick }) {
  const count = 344;
  const tip = "已有 " + count + " 位用户开通 VIP 服务";

  return (
    <Tooltip content={tip} placement="top" autoAdjustOverflow>
      <Link
        className={classes.ass}
        to={useBaseUrl("docs/subscribe")}
        style={{ ...style }}
        onClick={onClick}
      >
        <div className={classes.title}>开通 VIP 服务尊享一对一技术指导</div>
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
    </Tooltip>
  );
}
