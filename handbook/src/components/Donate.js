import useBaseUrl from "@docusaurus/useBaseUrl";
import Modal from "@uiw/react-modal";
import React, { useState } from "react";

export default function Donate() {
  const [visible, setVisible] = useState(false);

  const onClosed = () => {
    setVisible(false);
  };

  return (
    <>
      <div
        style={{
          display: "flex",
          padding: "5px",
          boxSizing: "border-box",
          backgroundColor: "#f4f8fa",
          height: 80,
          borderRadius: 5,
        }}
      >
        <div
          style={{ position: "relative", marginRight: 10, cursor: "pointer" }}
          onClick={() => setVisible(true)}
        >
          <img
            src={useBaseUrl("img/donateme.png")}
            style={{
              height: "100%",
              maxHeight: "100%",
              display: "block",
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
              fontSize: 18,
              letterSpacing: 1.8,
              color: "#0e1011",
              margin: "4px 0 0 0 ",
            }}
          >
            微信扫码赞助 Furion
          </h3>
          <div
            style={{
              display: "flex",
              justifyContent: "space-between",
            }}
          >
            <span style={{ fontSize: 12, color: "#ccc" }}>
              微信号：ibaiqian
            </span>
            <a
              href="/docs/donate"
              style={{ color: "#723cff", fontSize: 13, fontWeight: "bold" }}
              title="monksoul@outlook.com"
            >
              成为赞助商
            </a>
          </div>
        </div>
      </div>

      <Modal
        title="微信扫码赞助 Furion"
        isOpen={visible}
        useButton={false}
        icon="pay"
        type="primary"
        onClosed={onClosed}
        width={360}
      >
        如果 Furion 对您有所帮助，并且您希望 Furion
        能够继续发展下去，请考虑赞助我们。
        <br />
        <br />
        <img src={useBaseUrl("img/support.png")} style={{ width: "100%" }} />
      </Modal>
    </>
  );
}
