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
          href="https://gitee.com/dotnetchina/MiniExcel"
          target="_blank"
          title="dotNET China 精选项目第 06 期"
        >
          <img src={useBaseUrl("img/MiniExcel.png")} />
        </a>
      ) : (
        <></>
      )}
    </div>
  );
}
