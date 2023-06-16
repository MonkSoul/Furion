import React from "react";
import FloatBar from "../components/FloatBar";

function Root({ children }) {
  return (
    <>
      <FloatBar />
      {children}
    </>
  );
}

export default Root;
