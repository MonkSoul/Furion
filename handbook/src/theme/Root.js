import "animate.css";
import React from "react";
import GiveMeStar from "../components/GiveMeStar";

function Root({ children }) {
  return (
    <>
      <GiveMeStar />
      {/* <zhicai /> */}
      {children}
      {/* <JoinGroup /> */}
      {/* <StarProject /> */}
      {/* <Support /> */}
      {/* <Playground /> */}
    </>
  );
}

export default Root;
