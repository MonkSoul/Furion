import "animate.css";
import React from "react";
import GiveMeStar from "../components/GiveMeStar";
import Playground from "../components/Playground";

function Root({ children }) {
  return (
    <>
      <GiveMeStar />
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
