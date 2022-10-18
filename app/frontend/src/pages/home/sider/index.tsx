import {
  IconHistogram,
  IconHome,
  IconLive,
  IconSetting
} from "@douyinfe/semi-icons";
import { Sider, StyledNav as Nav } from "./styles";

function HomeSider() {
  return (
    <Sider>
      <Nav
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
