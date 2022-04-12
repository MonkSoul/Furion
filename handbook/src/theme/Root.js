import React from "react";
import JoinGroup from "../components/JoinGroup";
import GiveMeStar from "../components/GiveMeStar";
import StarProject from "../components/StarProject";
import ZhiCai from "../components/ZhiCai";
import Support from "../components/Support";
import "animate.css";
import Playground from "../components/Playground";

function Root({ children }) {
  return (
    <>
      <GiveMeStar />
      {/* <ZhiCai /> */}
      {children}
      {/* <JoinGroup /> */}
      {/* <StarProject /> */}
      <Support />
      <Playground />
    </>
  );
}

export default Root;
