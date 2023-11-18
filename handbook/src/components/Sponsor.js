import useBaseUrl from "@docusaurus/useBaseUrl";
import React from "react";
import sponsors from "../data/sponsor";
import Donate from "./Donate";

export function Sponsor() {
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
      <Donate />
    </div>
  );
}

const sponsorItemStyle = {
  display: "block",
  position: "relative",
  alignItems: "center",
  boxSizing: "border-box",
  backgroundColor: "#fff",
};

const sponsorTagStyle = {
  position: "absolute",
  display: "block",
  right: 0,
  bottom: 0,
  zIndex: 5,
  fontSize: 12,
  backgroundColor: "rgba(0,0,0,0.8)",
  padding: "0 5px",
  color: "#25c2a0",
};

export function SponsorItem({ picture, url, last, title, top, tag, style }) {
  return (
    <a
      href={url}
      target="_blank"
      title={title}
      style={{
        ...sponsorItemStyle,
        marginBottom: last ? null : "0.5em",
        border: top ? "2px solid rgb(255, 176, 46)" : undefined,
        ...style,
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
      <span style={sponsorTagStyle}>{tag}</span>
    </a>
  );
}

const sponsorSmartStyle = {
  display: "inline-block",
  position: "relative",
  width: "48.5%",
  position: "relative",
  boxSizing: "border-box",
  backgroundColor: "#fff",
};

export function SponsorItemSmart({ picture, url, title, tag, i }) {
  return (
    <a
      href={url}
      target="_blank"
      title={title}
      style={{ ...sponsorSmartStyle, marginRight: i % 2 !== 0 ? 0 : 8 }}
    >
      <img
        src={useBaseUrl(picture)}
        style={{ display: "block", width: "100%" }}
        loading="lazy"
      />
    </a>
  );
}

export const closeStyle = {
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
};
