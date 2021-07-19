import React, { useState } from "react";
import useBaseUrl from "@docusaurus/useBaseUrl";
import "./StarProject.css";

export default function StarProject() {
  const [show, setShow] = useState(true);

  return (
    <div className="furion-star-project" onClick={() => setShow(!show)}>
      {show && (
        <div className="furion-star-title" style={{ color: "white" }}>
          微信公众号
        </div>
      )}
      <button className="furion-star-close">{show ? "收起" : "展开"}</button>
      <img
        src={useBaseUrl("img/monksoul.jpg")}
        style={{ height: show ? null : 100 }}
        title="打开微信扫一扫关注百小僧公众号"
      />
      {show && (
        <div className="furion-star-title" style={{ color: "yellow" }}>
          百小僧别吹牛
        </div>
      )}
    </div>
  );
}
