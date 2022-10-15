import {
  NavbarSecondaryMenuFiller,
  ThemeClassNames
} from "@docusaurus/theme-common";
import { useNavbarMobileSidebar } from "@docusaurus/theme-common/internal";
import DocSidebarItems from "@theme/DocSidebarItems";
import clsx from "clsx";
import React, { useState } from "react";

import useBaseUrl from "@docusaurus/useBaseUrl";
import sponsors from "../../../data/sponsor";

// eslint-disable-next-line react/function-component-definition
const DocSidebarMobileSecondaryMenu = ({ sidebar, path }) => {
  const mobileSidebar = useNavbarMobileSidebar();
  return (
    <>
      <Sponsor />
      <ul className={clsx(ThemeClassNames.docs.docSidebarMenu, "menu__list")}>
        <DocSidebarItems
          items={sidebar}
          activePath={path}
          onItemClick={(item) => {
            // Mobile sidebar should only be closed if the category has a link
            if (item.type === "category" && item.href) {
              mobileSidebar.toggle();
            }
            if (item.type === "link") {
              mobileSidebar.toggle();
            }
          }}
          level={1}
        />
      </ul>
    </>
  );
};
function DocSidebarMobile(props) {
  return (
    <NavbarSecondaryMenuFiller
      component={DocSidebarMobileSecondaryMenu}
      props={props}
    />
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

export default React.memo(DocSidebarMobile);
