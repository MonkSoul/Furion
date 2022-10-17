/**
 * 首页底部导航条
 */

import { IconBytedanceLogo } from "@douyinfe/semi-icons";
import { Layout } from "@douyinfe/semi-ui";
import styled from "styled-components";

const Footer = styled(Layout.Footer)`
  display: flex;
  justify-content: space-between;
  padding: 20px;
  color: var(--semi-color-text-2);
  background: rgba(var(--semi-grey-0), 1);
`;

const Span = styled.span`
  display: flex;
  align-items: center;
`;

const Logo = styled(IconBytedanceLogo)`
  margin-right: 8px;
`;

function HomeFooter() {
  return (
    <Footer>
      <Span>
        <Logo size="large" />
        <span>
          Copyright (c) 2020-2022 百小僧, Baiqian Co.,Ltd and Contributors.
        </span>
      </Span>
      <span>
        <span>v0.1.0-alpha</span>
      </span>
    </Footer>
  );
}

export default HomeFooter;
