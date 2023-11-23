import BrowserOnly from "@docusaurus/BrowserOnly";
import Link from "@docusaurus/Link";
import useBaseUrl from "@docusaurus/useBaseUrl";
import Modal from "@uiw/react-modal";
import React, { useContext, useEffect, useState } from "react";
import Assistance from "../components/Assistance";
import FloatBar from "../components/FloatBar";
import GlobalContext from "../components/GlobalContext";
import Vip from "../components/Vip";
import VipDesc from "../components/VipDesc.mdx";
import VipImageList from "../components/VipImageList";

function Root({ children }) {
  const [donate, setDonate] = useState(false);
  const [showVip, setVip] = useState(false);
  const [adv, setAdv] = useState(true);
  const [drawer, showDrawer] = useState(true); // 弹窗
  const [rightVip, setRightVip] = useState(false);
  const [topVip, setTopVip] = useState(true);

  const onClosed = () => {
    setDonate(false);
  };

  useEffect(() => {
    if (!drawer) {
      setTimeout(() => {
        setTopVip(false);
      }, 5000);
    }
  }, [drawer]);

  return (
    <GlobalContext.Provider
      value={{
        donate,
        setDonate,
        setVip,
        adv,
        setAdv,
        drawer,
        showDrawer,
        rightVip,
        setRightVip,
        topVip,
        setTopVip,
      }}
    >
      {showVip && <Vip />}
      <FloatBar />
      {/* {!drawer && topVip && <TopBanner />} */}
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
        style={{
          color: "#1c1e21",
        }}
        bodyStyle={{ fontSize: 15, color: "#1c1e21" }}
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
                  作者微信：ibaiqian
                </div>
              </div>
            </div>
          </div>
        </div>
        <VipImageList padding={10} />
        {/* <div
          style={{
            marginTop: 20,
            textAlign: "center",
            fontSize: 18,
          }}
        >
          ⭐️{" "}
          <Link
            to={useBaseUrl("docs/subscribe")}
            style={{
              color: "red",
              fontWeight: "bold",
              textDecoration: "underline",
            }}
            onClick={() => setDonate(false)}
          >
            开通 VIP 服务仅需 499 元/年，尊享 365 天项目无忧
          </Link>{" "}
          ⭐️
        </div> */}
      </Modal>

      <BrowserOnly children={() => <VipShow />} />
    </GlobalContext.Provider>
  );
}

function VipShow() {
  const { drawer, showDrawer, setVip, setTopVip } = useContext(GlobalContext);

  return (
    <Modal
      title="⭐️ 开通 VIP 服务仅需 499 元/年 ⭐️"
      isOpen={drawer}
      useButton={false}
      icon="pay"
      type="primary"
      onClosed={() => {
        showDrawer(false);
        setVip(true);
      }}
      style={{
        color: "#1c1e21",
      }}
      bodyStyle={{ fontSize: 15, color: "#1c1e21" }}
      // isCloseButtonShown={false}
      // maskClosable={false}
    >
      <div>
        <img
          src={useBaseUrl("img/cmp-vip.jpeg")}
          style={{ borderRadius: 5, display: "block" }}
        />
      </div>
      <br />
      <VipDesc />
      <Assistance
        style={{ margin: 0 }}
        onClick={() => {
          setTopVip(false);
          showDrawer(false);
        }}
      />
    </Modal>
  );
}

export default Root;
