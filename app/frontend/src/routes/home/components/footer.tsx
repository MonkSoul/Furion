/**
 * 首页底部导航条
 */

import { IconBytedanceLogo } from "@douyinfe/semi-icons";
import { Layout } from "@douyinfe/semi-ui";

function HomeFooter() {
  const { Footer } = Layout;
  return (
    <Footer
      style={{
        display: "flex",
        justifyContent: "space-between",
        padding: "20px",
        color: "var(--semi-color-text-2)",
        backgroundColor: "rgba(var(--semi-grey-0), 1)",
      }}
    >
      <span
        style={{
          display: "flex",
          alignItems: "center",
        }}
      >
        <IconBytedanceLogo size="large" style={{ marginRight: "8px" }} />
        <span>
          Copyright (c) 2020-2022 百小僧, Baiqian Co.,Ltd and Contributors.
        </span>
      </span>
      <span>
        <span>v0.1.0-alpha</span>
      </span>
    </Footer>
  );
}

export default HomeFooter;
