/**
 * 首页布局
 */

import { Layout } from "@douyinfe/semi-ui";
import useAppState from "../../shared/states/app.state";
import HomeContent from "./components/content";
import HomeFooter from "./components/footer";
import HomeHeader from "./components/header";
import HomeSider from "./components/sider";

function Home() {
  // 读取全局状态
  const { mode } = useAppState();

  return (
    <Layout
      className={mode}
      style={{ border: "1px solid var(--semi-color-border)", height: "100vh" }}
    >
      <HomeSider />
      <Layout>
        <HomeHeader />
        <HomeContent />
        <HomeFooter />
      </Layout>
    </Layout>
  );
}

export default Home;
