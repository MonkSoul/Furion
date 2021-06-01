import React, { useState } from "react";
import useBaseUrl from "@docusaurus/useBaseUrl";
import "./JoinGroup.css";

export default function JoinGroup() {
  const [show, setShow] = useState(false);

  return (
    <div className="furion-join-group">
      {show && <img src={useBaseUrl("img/dotnetchina2.jpg")} />}
      <button onClick={() => setShow(!show)}>
        {show ? "QQ 群号：592844340" : "加入 dotNET China 交流群"}
      </button>
    </div>
  );
}
