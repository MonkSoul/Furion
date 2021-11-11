import React from "react";
import "./GiveMeStar.css";
import Typist from "react-typist";

export default function GiveMeStar() {
  return (
    <>
      <div className="furion-give-me-star">
        <Typist
          cursor={{ show: false, hideWhenDone: true, hideWhenDoneDelay: 0 }}
        >
          ⭐️ 你给的 Star，胜过所有读过的诗
          <a href="https://gitee.com/dotnetchina/Furion" target="_blank">
            <img
              className="furion-gitee-star"
              src="https://gitee.com/dotnetchina/Furion/badge/star.svg?theme=gvp"
              alt="star"
            ></img>
          </a>{" "}
          ⭐️
        </Typist>
      </div>
    </>
  );
}
