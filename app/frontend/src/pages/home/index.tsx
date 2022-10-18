import { Layout } from "@douyinfe/semi-ui";
import useAppState from "../../shared/states/app.state";
import { default as Content } from "./content";
import { default as Footer } from "./footer";
import { default as Header } from "./header";
import { default as Sider } from "./sider";
import { Container } from "./styles";

function Home() {
  // 读取全局状态
  const { mode } = useAppState();

  return (
    <Container className={mode}>
      <Sider />
      <Layout>
        <Header />
        <Content />
        <Footer />
      </Layout>
    </Container>
  );
}

export default Home;
