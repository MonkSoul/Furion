import {
  IconBell,
  IconBytedanceLogo,
  IconHelpCircle,
  IconHistogram,
  IconHome,
  IconLive,
  IconSetting
} from "@douyinfe/semi-icons";
import {
  Avatar,
  Breadcrumb,
  Button,
  Layout,
  Nav,
  Skeleton
} from "@douyinfe/semi-ui";
import "./App.css";

function App() {
  const { Header, Footer, Sider, Content } = Layout;
  return (
    <Layout
      style={{ border: "1px solid var(--semi-color-border)", height: "100vh" }}
    >
      <Sider style={{ backgroundColor: "var(--semi-color-bg-1)" }}>
        <Nav
          defaultSelectedKeys={["Home"]}
          style={{ maxWidth: 220, height: "100%" }}
          items={[
            { itemKey: "Home", text: "首页", icon: <IconHome size="large" /> },
            {
              itemKey: "Histogram",
              text: "基础数据",
              icon: <IconHistogram size="large" />,
            },
            {
              itemKey: "Live",
              text: "测试功能",
              icon: <IconLive size="large" />,
            },
            {
              itemKey: "Setting",
              text: "设置",
              icon: <IconSetting size="large" />,
              items: ["用户管理", "日志管理", "其他设置"],
            },
          ]}
          header={{
            logo: (
              <img
                src="//lf1-cdn-tos.bytescm.com/obj/ttfe/ies/semi/webcast_logo.svg"
                alt=""
              />
            ),
            text: "Furion 通用后台",
          }}
          footer={{
            collapseButton: true,
          }}
        />
      </Sider>
      <Layout>
        <Header style={{ backgroundColor: "var(--semi-color-bg-1)" }}>
          <Nav mode="horizontal">
            <Nav.Item
              itemKey="Home"
              text="首页"
              icon={<IconHome size="large" />}
            />
            <Nav.Item
              itemKey="Live"
              text="直播"
              icon={<IconLive size="large" />}
            />
            <Nav.Item
              itemKey="Setting"
              text="设置"
              icon={<IconSetting size="large" />}
            />
            <Nav.Footer>
              <Button
                theme="borderless"
                icon={<IconBell size="large" />}
                style={{
                  color: "var(--semi-color-text-2)",
                  marginRight: "12px",
                }}
              />
              <Button
                theme="borderless"
                icon={<IconHelpCircle size="large" />}
                style={{
                  color: "var(--semi-color-text-2)",
                  marginRight: "12px",
                }}
              />
              <Avatar color="orange" size="small">
                YJ
              </Avatar>
            </Nav.Footer>
          </Nav>
        </Header>
        <Content
          style={{
            padding: "24px",
            backgroundColor: "var(--semi-color-bg-0)",
          }}
        >
          <Breadcrumb
            style={{
              marginBottom: "24px",
            }}
            routes={[
              "首页",
              "当这个页面标题很长时需要省略",
              "上一页",
              "详情页",
            ]}
          />
          <div
            style={{
              borderRadius: "10px",
              border: "1px solid var(--semi-color-border)",
              height: "376px",
              padding: "32px",
            }}
          >
            <Skeleton
              placeholder={<Skeleton.Paragraph rows={2} />}
              loading={true}
            >
              <p>Hi, Bytedance dance dance.</p>
              <p>Hi, Bytedance dance dance.</p>
            </Skeleton>
          </div>
        </Content>
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
            <span>Copyright © 2019 ByteDance. All Rights Reserved. </span>
          </span>
          <span>
            <span style={{ marginRight: "24px" }}>平台客服</span>
            <span>反馈建议</span>
          </span>
        </Footer>
      </Layout>
    </Layout>
  );
}
export default App;
