/**
 * Copyright (c) Facebook, Inc. and its affiliates.
 *
 * This source code is licensed under the MIT license found in the
 * LICENSE file in the root directory of this source tree.
 */
import React from "react";
import clsx from "clsx";
import {
  NavbarSecondaryMenuFiller,
  ThemeClassNames,
  useNavbarMobileSidebar,
} from "@docusaurus/theme-common";
import DocSidebarItems from "@theme/DocSidebarItems";

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
  return (
    <div
      style={{
        margin: "0.5em",
        marginTop: "0",
        display: "block",
        textAlign: "center",
        borderBottom: "1px solid #dedede",
        paddingBottom: "0.2em",
      }}
    >
      {sponsors.map(({ picture, url, title }, i) => (
        <SponsorItem
          key={url}
          title={title}
          url={url}
          picture={picture}
          last={sponsors.length - 1 == i}
        />
      ))}
      <div>
        <a
          href="mailto:monksoul@outlook.com"
          style={{ color: "#723cff", fontSize: 13, fontWeight: "bold" }}
          title="monksoul@outlook.com"
        >
          成为赞助商
        </a>
      </div>
    </div>
  );
}

function SponsorItem({ picture, url, last, title }) {
  return (
    <a
      href={url}
      target="_blank"
      title={title}
      style={{
        display: "block",
        marginBottom: last ? null : "0.5em",
        position: "relative",
      }}
    >
      <img
        src={useBaseUrl(picture)}
        style={{ display: "block", width: "100%" }}
      />
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
        赞助商广告
      </span>
    </a>
  );
}

export default React.memo(DocSidebarMobile);
