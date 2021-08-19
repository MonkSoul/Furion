import React, { useState } from "react";
import useBaseUrl from "@docusaurus/useBaseUrl";
import "./JoinGroup.css";

export default function JoinGroup() {
  // const [show, setShow] = useState(false);

  return (
    <div className="furion-join-group">
      <a
        href="https://gitee.com/dotnetchina/Furion/tree/net6-dev/"
        target="_blank"
      >
        ✨ <span style={{ color: "yellow" }}>.NET6</span> 版Furion可
        <span style={{ color: "yellow" }}>正式</span>使用 ✨
      </a>
    </div>
  );
}
