import React from "react";

export default function SpecDonate({ style }) {
  return (
    <a
      href="/?donate=1"
      style={{
        height: 120,
        backgroundColor: "#f0f0f0",
        display: "flex",
        alignItems: "center",
        justifyContent: "center",
        borderRadius: 8,
        marginBottom: 20,
        textDecoration:"none"
      }}
    >
      <h3
        style={{
          fontWeight: 500,
          fontSize: 30,
          margin: "4px 0 0 0 ",
          textAlign: "left",
          background: "linear-gradient(to right, red, blue)",
          backgroundClip: "text",
          WebkitBackgroundClip: "text",
          color: "transparent",
          whiteSpace: "nowrap",
          cursor: "pointer",
        }}
      >
        特别赞助（虚席以待）
      </h3>
    </a>
  );
}
