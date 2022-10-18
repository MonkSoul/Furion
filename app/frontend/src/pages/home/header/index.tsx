import {
  IconBell,
  IconHelpCircle,
  IconHome,
  IconLive,
  IconMoon,
  IconSetting
} from "@douyinfe/semi-icons";
import { Avatar, Dropdown, Nav } from "@douyinfe/semi-ui";
import useAuth from "../../../shared/hooks/useAuth";
import useAppState from "../../../shared/states/app.state";
import { Header, NavButton } from "./styles";

function HomeHeader() {
  const { switchMode } = useAppState();
  const auth = useAuth();

  return (
    <Header>
      <Nav mode="horizontal">
        <Nav.Item itemKey="Home" text="首页" icon={<IconHome size="large" />} />
        <Nav.Item itemKey="Live" text="直播" icon={<IconLive size="large" />} />
        <Nav.Item
          itemKey="Setting"
          text="设置"
          icon={<IconSetting size="large" />}
        />
        <Nav.Footer>
          <NavButton
            onClick={switchMode}
            theme="borderless"
            icon={<IconMoon size="large" />}
          />
          <NavButton theme="borderless" icon={<IconBell size="large" />} />
          <NavButton
            theme="borderless"
            icon={<IconHelpCircle size="large" />}
          />

          <Dropdown
            trigger={"click"}
            position={"bottomLeft"}
            render={
              <Dropdown.Menu>
                <Dropdown.Item onClick={() => auth.signout(() => {})}>
                  退出系统
                </Dropdown.Item>
              </Dropdown.Menu>
            }
          >
            <Avatar color="orange" size="small">
              F
            </Avatar>
          </Dropdown>
        </Nav.Footer>
      </Nav>
    </Header>
  );
}

export default HomeHeader;
