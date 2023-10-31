import useBaseUrl from "@docusaurus/useBaseUrl";
import Modal from "@uiw/react-modal";
import React, { useState } from "react";

export default function Donate({ style }) {
  const [visible, setVisible] = useState(() => {
    var params = new URLSearchParams(window.location.search);
    var paramValue = params.get("donate");
    return Number(paramValue) === 1;
  });

  const onClosed = () => {
    var params = new URLSearchParams(window.location.search);
    setVisible(false);
    params.delete("donate");
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
          cursor: "pointer",
          ...(style || {}),
        }}
        title="您的支持是我们坚持完善下去的动力！"
        onClick={() => setVisible(true)}
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
            }}
          >
            <span style={{ fontSize: 12, color: "#ccc" }}>
              微信号：ibaiqian
            </span>
            <a
              href="javascript:void(0)"
              style={{
                color: "#723cff",
                fontSize: 13,
                marginRight: 10,
              }}
              title="monksoul@outlook.com"
            >
              成为赞助商
            </a>
          </div>
        </div>
      </div>

      <Modal
        title="赞助 Furion"
        isOpen={visible}
        useButton={false}
        icon="pay"
        type="primary"
        onClosed={onClosed}
        maxWidth={700}
        minWidth={700}
      >
        如果 Furion 对您有所帮助，并且您希望 Furion 能够继续发展下去，请考虑{" "}
        <a
          href="/docs/donate"
          style={{
            color: "#723cff",
            fontSize: 13,
          }}
          title="monksoul@outlook.com"
        >
          ⌈赞助⌋
        </a>{" "}
        我们。
        <div
          style={{
            display: "flex",
            flexDirection: "row",
            marginTop: 25,
            whiteSpace: "nowrap",
            lineHeight: "26px",
          }}
        >
          <div style={{ flex: 1 }}>
            <h3 style={{ textAlign: "center" }}>个人微信扫码赞助</h3>
            <img
              src={useBaseUrl("img/support.png")}
              style={{ width: "100%" }}
            />
          </div>
          <div style={{ flex: 1 }}>
            <h3 style={{ textAlign: "center" }}>品牌商友情赞助</h3>
            <div style={{ padding: "20px 10px 0 25px" }}>
              <div>
                <h4>特别赞助</h4>
                <ul>
                  <li>15,000/年 10,000/半年</li>
                  <li style={{ color: "red" }}>
                    Gitee/Github 仓库 README.md 展示
                  </li>
                  <li style={{ color: "red" }}>
                    文档页顶部和底部 ⌈大横幅⌋ 展示
                  </li>
                  <li>官网首页 ⌈特别赞助⌋ 展示</li>
                  <li>文档页目录导航顶部 ⌈大图⌋ 展示</li>
                </ul>
              </div>
              <div>
                <h4>铂金赞助</h4>
                <ul>
                  <li>7,500/年 5,000/半年</li>
                  <li>官网首页 ⌈铂金赞助⌋ 展示</li>
                  <li>文档页目录导航顶部 ⌈大图⌋ 展示</li>
                </ul>
              </div>
              <div>
                <h4>金牌赞助</h4>
                <ul>
                  <li>5,000/年</li>
                  <li>官网首页 ⌈金牌赞助⌋ 展示</li>
                  <li>文档页目录导航顶部 ⌈小图⌋ 展示</li>
                </ul>
              </div>
              <div>
                <hr />
                <div style={{ textAlign: "right", color: "gray" }}>
                  微信号：ibaiqian
                </div>
              </div>
            </div>
          </div>
        </div>
      </Modal>
    </>
  );
}
