import clsx from "clsx";
import React, { useState } from "react";
import classes from "./Playground.module.css";

export default function Playground() {
  const [show, setShow] = useState(false);

  return (
    <>
      <div
        className={classes.button}
        onClick={() => setShow(!show)}
        children="演练场"
      />
      {show && (
        <div className={clsx(classes.modal)} onClick={() => setShow(false)}>
          <iframe
            className={clsx(
              classes.playground,
              "animate__animated",
              "animate__fadeInUp"
            )}
            width="100%"
            height="100%"
            src="https://replit.com/@MonkSoul/FurionPlayground?embed=1#Services/DatabaseApiService.cs"
            scrolling="no"
            frameBorder="no"
          />
        </div>
      )}
    </>
  );
}
