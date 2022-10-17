/**
 * 首页侧边栏菜单
 */
import { Layout, Nav } from "@douyinfe/semi-ui";

import {
  IconHistogram,
  IconHome,
  IconLive,
  IconSetting
} from "@douyinfe/semi-icons";
import styled from "styled-components";

const Sider = styled(Layout.Sider)`
  background-color: var(--semi-color-bg-1);
`;

const StyledNav = styled(Nav)`
  height: 100%;
`;

function HomeSider() {
  return (
    <Sider>
      <StyledNav
        defaultSelectedKeys={["Home"]}
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
  );
}

export default HomeSider;
