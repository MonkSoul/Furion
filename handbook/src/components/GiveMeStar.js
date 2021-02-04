import React, { useEffect } from "react";
import useBaseUrl from "@docusaurus/useBaseUrl";
import "./GiveMeStar.css";

export default function GiveMeStar() {
  return (
    <div className="furion-give-me-star">
      ⭐️ 如果你喜欢{" "}
      <a href="https://gitee.com/monksoul/Furion" target="_blank">
        Furion
      </a>{" "}
      ，可以在{" "}
      <a href="https://gitee.com/monksoul/Furion" target="_blank">
        Gitee
      </a>{" "}
      中给个
      <a href="https://gitee.com/monksoul/Furion" target="_blank">
        <img
          className="furion-gitee-star"
          src="https://gitee.com/monksoul/Furion/badge/star.svg?theme=gvp"
          alt="star"
        ></img>
      </a>{" "}
      ⭐️
    </div>
  );
}
