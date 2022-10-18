import { Button, Layout } from "@douyinfe/semi-ui";
import { ButtonProps } from "@douyinfe/semi-ui/lib/es/button";
import styled from "styled-components";

export const Header = styled(Layout.Header)`
  background-color: var(--semi-color-bg-1);
`;

export const NavButton = styled(Button)<ButtonProps>`
  color: var(--semi-color-text-2) !important;
  margin-right: 12px;
`;
