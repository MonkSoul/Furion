import useBaseUrl from "@docusaurus/useBaseUrl";
import React, { useContext } from "react";
import classes from "./Donate.module.css";
import GlobalContext from "./GlobalContext";

export default function Donate({ style }) {
  const { setDonate } = useContext(GlobalContext);

  return (
    <>
      <div
        className={classes.donate}
        style={{
          ...(style || {}),
        }}
        title="您的支持是我们坚持完善下去的动力！"
        onClick={() => setDonate(true)}
      >
        <div style={{ position: "relative", marginRight: 9 }}>
          <img
            src={useBaseUrl("img/donateme.png")}
            style={{
              height: "100%",
              maxHeight: "100%",
              display: "block",
              minWidth: 70,
            }}
            alt="赞助 Furion"
          />
          <span
            style={{
              position: "absolute",
              top: 25,
              left: 0,
              right: 0,
              fontSize: 12,
              zIndex: 1,
              textAlign: "center",
              color: "white",
              fontWeight: "bold",
              backgroundColor: "rgba(0, 0, 0, 0.4)",
            }}
          >
            查看大图
          </span>
        </div>
        <div
          style={{
            display: "flex",
            flex: 1,
            flexDirection: "column",
            justifyContent: "space-between",
            padding: "5px 0 5px 0",
          }}
        >
          <h3
            style={{
              fontWeight: 500,
              fontSize: 17,
              // color: "#0e1011",
              margin: "4px 0 0 0 ",
              textAlign: "left",
              background: "linear-gradient(to right, red, blue)",
              backgroundClip: "text",
              WebkitBackgroundClip: "text",
              color: "transparent",
              whiteSpace: "nowrap",
              cursor: "pointer",
            }}
          >
            谢谢您对 Furion 的认可！
          </h3>
          <div
            style={{
              display: "flex",
              justifyContent: "space-between",
              whiteSpace: "nowrap",
              marginTop: 10,
            }}
          >
            <span style={{ fontSize: 12, color: "#ccc" }}>微信：ibaiqian</span>
            <a
              href="https://gitee.com/dotnetchina/Furion"
              style={{ marginRight: 6 }}
              target="_blank"
            >
              <img
                src="https://gitee.com/dotnetchina/Furion/badge/star.svg?theme=white"
                alt="star"
              ></img>
            </a>
          </div>
        </div>
      </div>
    </>
  );
}
