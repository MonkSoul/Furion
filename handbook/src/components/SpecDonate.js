import React from "react";

export default function SpecDonate({ style }) {
  return (
    <a
      href="/?donate=1"
      style={{
        minHeight: 120,
        backgroundColor: "#f0f0f0",
        display: "flex",
        flexDirection: "column",
        alignItems: "center",
        justifyContent: "center",
        borderRadius: 8,
        marginBottom: 20,
        textDecoration: "none",
        boxSizing: "border-box",
        padding: 20,
        userSelect:"none"
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
          marginBottom: 5,
        }}
      >
        特别赞助（虚席以待）
      </h3>
      <div>
        如果 Furion 对您有所帮助，并且您希望 Furion 能够继续发展下去，请考虑
        ⌈赞助⌋ 我们。
      </div>
    </a>
  );
}
