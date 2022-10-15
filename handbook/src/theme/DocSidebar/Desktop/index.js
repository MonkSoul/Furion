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
  return (
    <div
      className={clsx(
        styles.sidebar,
        hideOnScroll && styles.sidebarWithHideableNavbar,
        isHidden && styles.sidebarHidden
      )}
    >
      {hideOnScroll && <Logo tabIndex={-1} className={styles.sidebarLogo} />}
      <Sponsor />
      <Content path={path} sidebar={sidebar} />
      {hideableSidebar && <CollapseButton onClick={onCollapse} />}
    </div>
  );
}

function Sponsor() {
  const [show, setShow] = useState(true);

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
      {sponsors.map(({ picture, url, title, top }, i) =>
        show ? (
          <SponsorItem
            key={url}
            title={title}
            url={url}
            picture={picture}
            top={top}
            last={sponsors.length - 1 == i}
          />
        ) : (
          <SponsorItemSmart
            key={url}
            title={title}
            url={url}
            picture={picture}
            i={i}
            top={top}
            last={sponsors.length - 1 == i}
          />
        )
      )}
      <div
        style={{
          display: "flex",
          justifyContent: "space-between",
          alignItems: "center",
          padding: "5px 0",
        }}
      >
        <span
          style={{
            color: "#999",
            fontSize: 13,
            fontWeight: "bold",
            cursor: "pointer",
            userSelect: "none",
          }}
          onClick={() => setShow((s) => !s)}
        >
          {show ? (
            <>
              <b style={{ color: "#723cff" }}>开</b>|关
            </>
          ) : (
            <>
              开|<b style={{ color: "#723cff" }}>关</b>
            </>
          )}
        </span>
        <a
          href="mailto:monksoul@outlook.com"
          style={{ color: "#723cff", fontSize: 13, fontWeight: "bold" }}
          title="monksoul@outlook.com"
        >
          💖成为赞助商
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
        广告
      </span>
    </a>
  );
}

function SponsorItemSmart({ picture, url, last, title, i, top }) {
  if (top) {
    return (
      <SponsorItem
        key={url}
        title={title}
        url={url}
        picture={picture}
        top={top}
        last={last}
      />
    );
  }

  return (
    <a
      href={url}
      target="_blank"
      title={title}
      style={{
        display: "inline-block",
        position: "relative",
        width: 138,
        marginRight: i % 2 == 0 ? 0 : 8,
        position: "relative",
        boxSizing: "border-box",
        border: top ? "2px solid rgb(255, 176, 46)" : undefined,
      }}
    >
      <img src={useBaseUrl(picture)} style={{ display: "block", width: 138 }} />
      {top && (
        <span style={{ position: "absolute", zIndex: 10, top: -16, right: -8 }}>
          👑
        </span>
      )}
    </a>
  );
}

export default React.memo(DocSidebarDesktop);
