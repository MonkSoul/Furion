/**
 * 首页布局
 */

import { Layout } from "@douyinfe/semi-ui";
import useAppState from "../../shared/states/app.state";
import HomeContent from "./content";
import HomeFooter from "./footer";
import HomeHeader from "./header";
import HomeSider from "./sider";
import { StyledLayout } from "./styles";

function Home() {
  // 读取全局状态
  const { mode } = useAppState();

  return (
    <StyledLayout className={mode}>
      <HomeSider />
      <Layout>
        <HomeHeader />
        <HomeContent />
        <HomeFooter />
      </Layout>
    </StyledLayout>
  );
}

export default Home;
