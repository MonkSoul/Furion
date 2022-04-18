import React, { useState } from "react";
import clsx from "clsx";
import IconFont from "./iconfonts";
import classes from "./Support.module.css";
import useBaseUrl from "@docusaurus/useBaseUrl";

const langClassName = clsx(
  classes.lang,
  "animate__animated",
  "animate__zoomIn"
);

export default function Support() {
  const [state, setState] = useState(false);
  return (
    <div className={clsx(classes.container)}>
      <div className={classes.title} onClick={() => setState((f) => !f)}>
        <IconFont className={classes.icon} name="fuwu" color="#fff" />
        <span>请作者喝杯咖啡</span>
        <span className={classes.toggle}>
          <IconFont
            className={classes.icon}
            name={state ? "up" : "down"}
            color="#fff"
            size={14}
          />
        </span>
      </div>
      {state && (
        <div
          className={clsx(
            classes.content,
            "animate__animated",
            "animate__fadeIn"
          )}
        >
          如需长期<span className={classes.span}>项目技术外包</span>
          可添加此微信
          <img
            className={classes.img}
            src="https://img.shields.io/badge/%E5%BE%AE%E4%BF%A1%E5%8F%B7-ibaiqian-yellow?cacheSeconds=10800"
            alt="微信号：ibaiqian"
            title="微信号：ibaiqian"
          />
          <br />
          <span className={langClassName}>C#</span>
          <span className={langClassName}>.NET6</span>
          <span className={langClassName}>React</span>
          <span className={langClassName}>Node</span>
          <span className={langClassName}>TypeScript</span>
          <span className={langClassName}>Web</span>
          <span className={langClassName}>Rust</span>
          <span className={langClassName}>桌面</span>
          <span className={langClassName}>Tauri</span>
          <span className={langClassName}>React Native</span>
          <span className={langClassName}>小程序</span>
          <div
            style={{
              marginTop: 10,
              borderTop: "1px solid #dedede",
              paddingTop: 5,
              marginBottom: 5,
              color: "rgb(114, 60, 255)",
            }}
          >
            如果觉得 Furion 能够帮助到您，可以请作者喝杯咖啡。
          </div>
          <img src={useBaseUrl("img/support.png")} style={{ width: "100%" }} />
        </div>
      )}
    </div>
  );
}
