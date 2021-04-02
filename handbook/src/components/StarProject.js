import React, { useState } from "react";
import useBaseUrl from "@docusaurus/useBaseUrl";
import "./StarProject.css";

export default function StarProject() {
  const [show, setShow] = useState(true);

  return (
    <div className="furion-star-project">
      <button className="furion-star-close" onClick={() => setShow(false)}>
        关闭
      </button>
      {show ? (
        <a
          href="https://gitee.com/dotnetchina/OpenAuth.Net"
          target="_blank"
          title="dotNET China 精选项目第 03 届"
        >
          <img src={useBaseUrl("img/OpenAuth.NET.png")} />
        </a>
      ) : (
        <></>
      )}
    </div>
  );
}
