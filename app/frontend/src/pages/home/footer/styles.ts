import { IconBytedanceLogo } from "@douyinfe/semi-icons";
import { Layout } from "@douyinfe/semi-ui";
import styled from "styled-components";

export const Footer = styled(Layout.Footer)`
  display: flex;
  justify-content: space-between;
  padding: 20px;
  color: var(--semi-color-text-2);
  background: rgba(var(--semi-grey-0), 1);
`;

export const Span = styled.span`
  display: flex;
  align-items: center;
`;

export const Logo = styled(IconBytedanceLogo)`
  margin-right: 8px;
`;
