import React from "react";
import JoinGroup from "../components/JoinGroup";
import GiveMeStar from "../components/GiveMeStar";

function Root({ children }) {
  return (
    <>
      <GiveMeStar />
      {children}
      <JoinGroup />
    </>
  );
}

export default Root;
