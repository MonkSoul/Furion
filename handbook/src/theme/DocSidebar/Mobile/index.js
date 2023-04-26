import {
  NavbarSecondaryMenuFiller,
  ThemeClassNames,
} from "@docusaurus/theme-common";
import { useNavbarMobileSidebar } from "@docusaurus/theme-common/internal";
import useBaseUrl from "@docusaurus/useBaseUrl";
import DocSidebarItems from "@theme/DocSidebarItems";
import clsx from "clsx";
import React, { useState } from "react";
import sponsors from "../../../data/sponsor";

// eslint-disable-next-line react/function-component-definition
const DocSidebarMobileSecondaryMenu = ({ sidebar, path }) => {
  const mobileSidebar = useNavbarMobileSidebar();
  const [show, setShow] = useState(true);
  const sponsor = sponsors.find((u) => u.id == 100);

  return (
    <>
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
      <div>
        <SponsorItem
          key={sponsor.url}
          title={sponsor.title}
          url={sponsor.url}
          picture={"img/xxyd2.jpeg"}
          top={true}
          last={false}
          tag={sponsor.tag}
          style={{ marginBottom: 0 }}
        />
      </div>
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
  var tops = sponsors.filter((u) => u.top);
  var unTops = sponsors.filter((u) => !u.top);

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
      {tops.map((item) => (
        <SponsorItem
          key={item.url}
          title={item.title}
          url={item.url}
          picture={item.picture}
          top={true}
          last={false}
          tag={item.tag}
        />
      ))}

      {unTops.map(({ picture, url, title, tag }, i) => (
        <SponsorItemSmart
          key={url}
          title={title}
          url={url}
          picture={picture}
          i={i}
          tag={tag}
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
          合作微信号：ibaiqian
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

function SponsorItem({ picture, url, last, title, top, tag, style }) {
  const inlineStyle = {
    display: "block",
    marginBottom: last ? null : "0.5em",
    position: "relative",
    alignItems: "center",
    boxSizing: "border-box",
    border: top ? "2px solid rgb(255, 176, 46)" : undefined,
  };
  return (
    <a
      href={url}
      target="_blank"
      title={title}
      style={Object.assign(inlineStyle, style)}
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
        {tag}
      </span>
    </a>
  );
}

function SponsorItemSmart({ picture, url, title, tag, i }) {
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

export default React.memo(DocSidebarMobile);
