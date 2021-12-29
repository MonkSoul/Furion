import React from "react";
import "./GiveMeStar.css";

export default function GiveMeStar() {
  return (
    <>
      <div className="furion-give-me-star">
        {/* <Typist
          cursor={{ show: false, hideWhenDone: true, hideWhenDoneDelay: 0 }}
        > */}
        {/* ⭐️ 你给的 Star，胜过所有读过的诗
          <a href="https://gitee.com/dotnetchina/Furion" target="_blank">
            <img
              className="furion-gitee-star"
              src="https://gitee.com/dotnetchina/Furion/badge/star.svg?theme=gvp"
              alt="star"
            ></img>
          </a>{" "}
          ⭐️ */}
        自己造轮子是一件苦差事。 现在，您可以专注于业务开发，仅需集成 ⭐️<a href="https://gitee.com/dotnetchina/Furion" style={{ textDecoration: 'underline', color: 'yellow' }} target="_blank">Furion</a>⭐️ 即可。
        {/* </Typist> */}
      </div>
    </>
  );
}
