import "animate.css";
import Playground from "../components/Playground";
import React from "react";

function Root({ children }) {
  return (
    <>
      {/* <GiveMeStar /> */}
      {/* <ZhiCai /> */}
      {children}
      {/* <JoinGroup /> */}
      {/* <StarProject /> */}
      {/* <Support /> */}
      <Playground />
    </>
  );
}

export default Root;
