/**
 * 首页布局
 */

import { Layout } from "@douyinfe/semi-ui";
import styled from "styled-components";
import useAppState from "../../shared/states/app.state";
import HomeContent from "./components/content";
import HomeFooter from "./components/footer";
import HomeHeader from "./components/header";
import HomeSider from "./components/sider";

const StyledLayout = styled(Layout)`
  border: 1px solid var(--semi-color-border);
  height: 100vh;
`;

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
