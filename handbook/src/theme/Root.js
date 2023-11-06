import Link from "@docusaurus/Link";
import useBaseUrl from "@docusaurus/useBaseUrl";
import Modal from "@uiw/react-modal";
import React, { useState } from "react";
import FloatBar from "../components/FloatBar";
import GlobalContext from "../components/GlobalContext";
import Vip from "../components/Vip";

function Root({ children }) {
  const [donate, setDonate] = useState(false);
  const [showVip, setVip] = useState(true);

  const onClosed = () => {
    setDonate(false);
  };

  return (
    <GlobalContext.Provider
      value={{
        donate,
        setDonate,
        setVip,
      }}
    >
      {showVip && <Vip />}
      <FloatBar />
      {children}

      <Modal
        title="开源不易，请考虑赞助 Furion"
        isOpen={donate}
        useButton={false}
        icon="pay"
        type="primary"
        onClosed={onClosed}
        maxWidth={700}
        minWidth={700}
      >
        如果 Furion 对您有所帮助，并且您希望 Furion 能够继续发展下去，请考虑{" "}
        <Link
          to={useBaseUrl("docs/donate")}
          style={{
            color: "#723cff",
            fontSize: 13,
          }}
          title="monksoul@outlook.com"
          onClick={() => setDonate(false)}
        >
          ⌈赞助⌋
        </Link>{" "}
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
                  微信：ibaiqian
                </div>
              </div>
            </div>
          </div>
        </div>
        <div
          style={{
            marginTop: 20,
            textAlign: "center",
            fontSize: 18,
          }}
        >
          👍{" "}
          <Link
            to={useBaseUrl("docs/subscribe")}
            style={{
              color: "red",
              fontWeight: "bold",
              textDecoration: "underline",
            }}
            onClick={() => setDonate(false)}
          >
            2023年12月01日前仅需 499元/年享有 VIP 服务
          </Link>
        </div>
      </Modal>
    </GlobalContext.Provider>
  );
}

export default Root;
