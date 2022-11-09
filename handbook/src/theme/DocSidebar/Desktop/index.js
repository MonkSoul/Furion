/**
 * Copyright (c) Facebook, Inc. and its affiliates.
 *
 * This source code is licensed under the MIT license found in the
 * LICENSE file in the root directory of this source tree.
 */
import { useThemeConfig } from "@docusaurus/theme-common";
import useBaseUrl from "@docusaurus/useBaseUrl";
import CollapseButton from "@theme/DocSidebar/Desktop/CollapseButton";
import Content from "@theme/DocSidebar/Desktop/Content";
import Logo from "@theme/Logo";
import clsx from "clsx";
import React, { useState } from "react";
import sponsors from "../../../data/sponsor";
import styles from "./styles.module.css";

function DocSidebarDesktop({ path, sidebar, onCollapse, isHidden }) {
  const {
    navbar: { hideOnScroll },
    hideableSidebar,
  } = useThemeConfig();
  const [show, setShow] = useState(true);

  return (
    <div
      className={clsx(
        styles.sidebar,
        hideOnScroll && styles.sidebarWithHideableNavbar,
        isHidden && styles.sidebarHidden
      )}
    >
      {hideOnScroll && <Logo tabIndex={-1} className={styles.sidebarLogo} />}
      {show && (
        <>
          <Sponsor />
          <span
            style={{
              margin: "0 auto",
              display: "inline-block",
              position: "relative",
              top: 5,
              marginTop: -28,
              cursor: "pointer",
              borderRadius: "50%",
              width: 28,
              height: 28,
              minWidth: 28,
              minHeight: 28,
              display: "flex",
              alignItems: "center",
              justifyContent: "center",
              boxSizing: "border-box",
              userSelect: "none",
              fontSize: 12,
              backgroundColor: "#3fbbfe",
              color: "#fff",
              fontWeight: "bold",
            }}
            onClick={() => setShow((s) => !s)}
          >
            收
          </span>
        </>
      )}
      <Content path={path} sidebar={sidebar} />
      {hideableSidebar && <CollapseButton onClick={onCollapse} />}
    </div>
  );
}

function randomNum(minNum, maxNum) {
  switch (arguments.length) {
    case 1:
      return parseInt(Math.random() * minNum + 1, 10);
    case 2:
      return parseInt(Math.random() * (maxNum - minNum + 1) + minNum, 10);
    default:
      return 0;
  }
}

function Sponsor() {
  const topIndex = randomNum(0, sponsors.length - 1);
  const topData = sponsors[topIndex];

  return (
    <div
      style={{
        margin: "0.5em",
        display: "block",
        borderBottom: "1px solid #dedede",
        paddingBottom: "0.2em",
        clear: "both",
      }}
    >
      <SponsorItem
        title={topData.title}
        url={topData.url}
        picture={topData.picture}
        top={true}
        last={false}
      />

      {sponsors
        .filter((item, i) => i !== topIndex)
        .map(({ picture, url, title }, i) => (
          <SponsorItemSmart
            key={url}
            title={title}
            url={url}
            picture={picture}
            i={i}
          />
        ))}
      {/* <div
        style={{
          display: "flex",
          justifyContent: "space-between",
          alignItems: "center",
        }}
      >
        <a href="https://gitee.com/dotnetchina/Furion/stargazers">
          <img
            src="https://gitee.com/dotnetchina/Furion/badge/star.svg?theme=gvp"
            alt="star"
          ></img>
        </a>
        <a href="https://gitee.com/dotnetchina/Furion">
          <img
            src="https://gitee.com/dotnetchina/Furion/widgets/widget_5.svg"
            alt="Fork me on Gitee"
            height={20}
          ></img>
        </a>
        <a href="https://gitee.com/dotnetchina/Furion/members">
          <img
            src="https://gitee.com/dotnetchina/Furion/badge/fork.svg?theme=gvp"
            alt="fork"
          ></img>
        </a>
      </div> */}
      <div
        style={{
          display: "flex",
          justifyContent: "space-between",
          alignItems: "center",
          padding: "5px 0",
        }}
      >
        <span style={{ fontSize: 12, color: "#ccc" }}>
          置顶广告采用随机方式
        </span>
        <a
          href="/docs/donate"
          style={{ color: "#723cff", fontSize: 13, fontWeight: "bold" }}
          title="monksoul@outlook.com"
        >
          成为赞助商
        </a>
      </div>
    </div>
  );
}

function SponsorItem({ picture, url, last, title, top }) {
  return (
    <a
      href={url}
      target="_blank"
      title={title}
      style={{
        display: "block",
        marginBottom: last ? null : "0.5em",
        position: "relative",
        alignItems: "center",
        boxSizing: "border-box",
        border: top ? "2px solid rgb(255, 176, 46)" : undefined,
      }}
    >
      <img
        src={useBaseUrl(picture)}
        style={{ display: "block", width: "100%" }}
        loading="lazy"
      />
      {top && (
        <span style={{ position: "absolute", zIndex: 10, top: -16, right: -8 }}>
          👑
        </span>
      )}
      <span
        style={{
          position: "absolute",
          display: "block",
          right: 0,
          bottom: 0,
          zIndex: 10,
          fontSize: 12,
          backgroundColor: "rgba(0,0,0,0.8)",
          padding: "0 5px",
        }}
      >
        赞助商
      </span>
    </a>
  );
}

function SponsorItemSmart({ picture, url, title, i }) {
  return (
    <a
      href={url}
      target="_blank"
      title={title}
      style={{
        display: "inline-block",
        position: "relative",
        width: "48.5%",
        marginRight: i % 2 !== 0 ? 0 : 8,
        position: "relative",
        boxSizing: "border-box",
      }}
    >
      <img
        src={useBaseUrl(picture)}
        style={{ display: "block", width: "100%" }}
        loading="lazy"
      />
    </a>
  );
}

export default React.memo(DocSidebarDesktop);
