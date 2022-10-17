/**
 * 首页内容
 */

import { Breadcrumb, Layout, Skeleton } from "@douyinfe/semi-ui";
import { useEffect, useState } from "react";
import styled from "styled-components";
import { HelloApi } from "../../../shared/api-services";
import { getAPI } from "../../../shared/axios-utils";

const Content = styled(Layout.Content)`
  padding: 24px;
  background-color: var(--semi-color-bg-0);
`;

const Container = styled.div`
  border-radius: 10px;
  border: 1px solid var(--semi-color-border);
  height: 376px;
  padding: 32px;
`;

const StyledBreadcrumb = styled(Breadcrumb)`
  margin-bottom: 24px;
`;

function HomeContent() {
  const [hello, setHello] = useState<string | undefined | null>();

  useEffect(() => {
    getAPI(HelloApi)
      .apiHelloSayPost()
      .then((res) => {
        setHello(res.data.data);
      });
  }, []);

  return (
    <Content>
      <StyledBreadcrumb
        routes={["首页", "当这个页面标题很长时需要省略", "上一页", "详情页"]}
      />
      <Container>
        <Skeleton placeholder={<Skeleton.Paragraph rows={2} />} loading={true}>
          <p>Hi, Bytedance dance dance.</p>
          <p>Hi, Bytedance dance dance.</p>
        </Skeleton>
        {hello}
      </Container>
    </Content>
  );
}

export default HomeContent;
