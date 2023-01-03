import {
  IconArrowUp,
  IconCalendarClock,
  IconHelpCircle
} from "@douyinfe/semi-icons";
import { BackTop, Button, Layout, Nav } from "@douyinfe/semi-ui";
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
              logo: (
                <IconCalendarClock style={{ height: "36px", fontSize: 36 }} />
              ),
            }}
          >
            <Nav.Footer>
              <Button
                theme="borderless"
                icon={<IconHelpCircle size="large" />}
                style={{
                  color: "var(--semi-color-text-2)",
                  marginRight: "12px",
                }}
                onClick={() =>
                  window.open("https://furion.baiqian.ltd/docs/job")
                }
              />
            </Nav.Footer>
          </Nav>
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
        <BackTop />
        <BackTop
          style={{
            display: "flex",
            alignItems: "center",
            justifyContent: "center",
            height: 30,
            width: 30,
            borderRadius: "100%",
            backgroundColor: "#0077fa",
            color: "#fff",
            bottom: 100,
          }}
        >
          <IconArrowUp />
        </BackTop>
      </Content>
    </Layout>
  );
}

export default App;
