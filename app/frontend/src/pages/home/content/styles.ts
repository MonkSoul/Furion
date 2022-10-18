import { Breadcrumb, Layout } from "@douyinfe/semi-ui";
import styled from "styled-components";

export const Content = styled(Layout.Content)`
  padding: 24px;
  background-color: var(--semi-color-bg-0);
`;

export const Container = styled.div`
  border-radius: 10px;
  border: 1px solid var(--semi-color-border);
  height: 376px;
  padding: 32px;
`;

export const StyledBreadcrumb = styled(Breadcrumb)`
  margin-bottom: 24px;
`;
