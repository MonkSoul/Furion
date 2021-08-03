import React from "react";
import JoinGroup from "../components/JoinGroup";
import GiveMeStar from "../components/GiveMeStar";
import StarProject from "../components/StarProject";

function Root({ children }) {
  return (
    <>
      <GiveMeStar />
      {children}
      {/* <JoinGroup /> */}
      {/* <StarProject /> */}
    </>
  );
}

export default Root;
