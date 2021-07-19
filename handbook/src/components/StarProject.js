import React, { useState } from "react";
import useBaseUrl from "@docusaurus/useBaseUrl";
import "./StarProject.css";

export default function StarProject() {
  const [show, setShow] = useState(true);

  return (
    <div className="furion-star-project">
      <a
        className="furion-star-title"
        href="https://gitee.com/monksoul"
        target="_blank"
        style={{ color: "white" }}
        title="打开微信扫一扫关注百小僧公众号"
      >
        微信公众号
      </a>
      <button className="furion-star-close" onClick={() => setShow(!show)}>
        {show ? "收起" : "展开"}
      </button>
      {show && (
        <a
          href="https://gitee.com/monksoul"
          target="_blank"
          title="打开微信扫一扫关注百小僧公众号"
        >
          <img src={useBaseUrl("img/monksoul.jpg")} />
        </a>
      )}
      <a
        className="furion-star-title"
        style={{ color: "yellow" }}
        href="https://gitee.com/monksoul"
        target="_blank"
        title="打开微信扫一扫关注百小僧公众号"
      >
        百小僧别吹牛
      </a>
    </div>
  );
}
