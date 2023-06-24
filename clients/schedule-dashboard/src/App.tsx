import {
  IconArrowUp,
  IconCalendarClock,
  IconHelpCircle,
  IconMoon,
  IconSun,
} from "@douyinfe/semi-icons";
import { BackTop, Button, Layout, Nav, Tooltip } from "@douyinfe/semi-ui";
import { useState } from "react";
import Jobs from "./components/jobs";

function App() {
  const { Header, Content } = Layout;
  const [mode, setMode] = useState("light");

  const switchMode = () => {
    const body = document.body;
    if (body.hasAttribute("theme-mode")) {
      body.removeAttribute("theme-mode");
      setMode("light");
    } else {
      body.setAttribute("theme-mode", "dark");
      setMode("dark");
    }
  };

  return (
    <Layout
      style={{
        border: "1px solid var(--semi-color-border)",
        height: "100vh",
      }}
    >
      <Header style={{ backgroundColor: "var(--semi-color-bg-1)" }}>
        <div>
          <Nav
            mode="horizontal"
            defaultSelectedKeys={["Home"]}
            header={{
              text: "Schedule Dashboard",
              logo: (
                <IconCalendarClock
                  style={{
                    height: "36px",
                    fontSize: 36,
                    color: mode === "light" ? "black" : "white",
                  }}
                />
              ),
            }}
          >
            <Nav.Footer>
              <Tooltip
                content={mode === "light" ? "切换到暗色模式" : "切换到亮色模式"}
              >
                <Button
                  theme="borderless"
                  icon={
                    mode === "light" ? (
                      <IconMoon size="large" />
                    ) : (
                      <IconSun size="large" />
                    )
                  }
                  style={{
                    color: "var(--semi-color-text-2)",
                    marginRight: "12px",
                  }}
                  onClick={() => switchMode()}
                />
              </Tooltip>
              <Tooltip content={"查看文档"}>
                <Button
                  theme="borderless"
                  icon={<IconHelpCircle size="large" />}
                  style={{
                    color: "var(--semi-color-text-2)",
                    marginRight: "12px",
                  }}
                  onClick={() =>
                    window.open("http://furion.baiqian.ltd/docs/job")
                  }
                />
              </Tooltip>
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
