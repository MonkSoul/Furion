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
          ⭐️ 如果你喜欢{" "}
          <a href="https://gitee.com/dotnetchina/Furion" target="_blank">
            Furion
          </a>{" "}
          ，可以在{" "}
          <a href="https://gitee.com/dotnetchina/Furion" target="_blank">
            Gitee
          </a>{" "}
          中给个
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
