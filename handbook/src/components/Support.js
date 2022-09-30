import useBaseUrl from "@docusaurus/useBaseUrl";
import clsx from "clsx";
import React, { useState } from "react";
import IconFont from "./iconfonts";
import classes from "./Support.module.css";

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
        <span>最美的诗，莫过于指尖的温存</span>
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
          {/* 如需长期<span className={classes.span}>项目技术外包</span>
          可添加此微信
          <img
            className={classes.img}
            src="https://img.shields.io/badge/%E5%BE%AE%E4%BF%A1%E5%8F%B7-ibaiqian-yellow?cacheSeconds=10800"
            alt="微信号：ibaiqian"
            title="微信号：ibaiqian"
          />
          <br /> */}
          {/* <span className={langClassName}>C#</span>
          <span className={langClassName}>.NET6</span>
          <span className={langClassName}>React</span>
          <span className={langClassName}>Node</span>
          <span className={langClassName}>TypeScript</span>
          <span className={langClassName}>Web</span>
          <span className={langClassName}>Rust</span>
          <span className={langClassName}>桌面</span>
          <span className={langClassName}>Tauri</span>
          <span className={langClassName}>React Native</span>
          <span className={langClassName}>小程序</span> */}
          <div
            style={{
              padding: "0 12px",
              color: "rgb(114, 60, 255)",
            }}
          >
            如果你觉得 Furion
            确实有用，并且实实在在的帮助到了你，那么可以考虑支持一下我们。
          </div>
          <img src={useBaseUrl("img/support.png")} style={{ width: "100%" }} />
        </div>
      )}
    </div>
  );
}
