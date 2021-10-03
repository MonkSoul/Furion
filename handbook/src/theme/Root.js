import React from "react";
import JoinGroup from "../components/JoinGroup";
import GiveMeStar from "../components/GiveMeStar";
import StarProject from "../components/StarProject";
import ZhiCai from "../components/ZhiCai";

function Root({ children }) {
  return (
    <>
      <GiveMeStar />
      <ZhiCai />
      {children}
      {/* <JoinGroup /> */}
      {/* <StarProject /> */}
    </>
  );
}

export default Root;
