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
          href="https://gitee.com/dotnetchina#-%E6%AF%8F%E5%91%A8%E7%B2%BE%E9%80%89%E9%A1%B9%E7%9B%AE-"
          target="_blank"
          title="dotNET China 精选项目第 07 期"
        >
          <img src={useBaseUrl("img/SunnyUI.png")} />
        </a>
      ) : (
        <></>
      )}
    </div>
  );
}
