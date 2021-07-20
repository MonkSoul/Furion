import React, { useState } from "react";
import useBaseUrl from "@docusaurus/useBaseUrl";
import "./StarProject.css";

export default function StarProject() {
  const [show, setShow] = useState(true);

  return (
    <div className="furion-star-project">
      <a href="https://gitee.com/monksoul" target="_blank">
        <img src={useBaseUrl("img/icon.png")} title="点击了解百小僧" />
      </a>
    </div>
  );
}
