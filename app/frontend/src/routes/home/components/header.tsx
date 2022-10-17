/**
 * 首页顶部菜单
 */

import {
  IconBell,
  IconHelpCircle,
  IconHome,
  IconLive,
  IconMoon,
  IconSetting
} from "@douyinfe/semi-icons";
import { Avatar, Button, Layout, Nav } from "@douyinfe/semi-ui";
import useAppState from "../../../shared/states/app.state";

function HomeHeader() {
  const { Header } = Layout;
  const { switchMode } = useAppState();

  return (
    <Header style={{ backgroundColor: "var(--semi-color-bg-1)" }}>
      <Nav mode="horizontal">
        <Nav.Item itemKey="Home" text="首页" icon={<IconHome size="large" />} />
        <Nav.Item itemKey="Live" text="直播" icon={<IconLive size="large" />} />
        <Nav.Item
          itemKey="Setting"
          text="设置"
          icon={<IconSetting size="large" />}
        />
        <Nav.Footer>
          <Button
            onClick={switchMode}
            theme="borderless"
            icon={<IconMoon size="large" />}
            style={{
              color: "var(--semi-color-text-2)",
              marginRight: "12px",
            }}
          />
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
            F
          </Avatar>
        </Nav.Footer>
      </Nav>
    </Header>
  );
}

export default HomeHeader;
