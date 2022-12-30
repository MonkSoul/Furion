import { Layout, Nav } from "@douyinfe/semi-ui";
import Jobs from "./components/jobs";

function App() {
  const { Header, Content } = Layout;

  return (
    <Layout style={{ border: "1px solid var(--semi-color-border)" }}>
      <Header style={{ backgroundColor: "var(--semi-color-bg-1)" }}>
        <div>
          <Nav
            mode="horizontal"
            defaultSelectedKeys={["Home"]}
            header={{
              text: "Schedule Dashboard",
            }}
          />
        </div>
      </Header>
      <Content
        style={{
          padding: "24px",
          backgroundColor: "var(--semi-color-bg-0)",
        }}
      >
        <div
          style={{
            borderRadius: "10px",
            border: "1px solid var(--semi-color-border)",
          }}
        >
          <Jobs />
        </div>
      </Content>
    </Layout>
  );
}

export default App;
