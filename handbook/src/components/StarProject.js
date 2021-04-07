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
          href="https://gitee.com/dotnetchina/SqlSugar"
          target="_blank"
          title="dotNET China 精选项目第 04 届"
        >
          <img src={useBaseUrl("img/SqlSugar.png")} />
        </a>
      ) : (
        <></>
      )}
    </div>
  );
}
