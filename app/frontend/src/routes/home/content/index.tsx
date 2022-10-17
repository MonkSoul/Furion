import { Skeleton } from "@douyinfe/semi-ui";
import { useEffect, useState } from "react";
import { HelloApi } from "../../../shared/api-services";
import { feature, getAPI } from "../../../shared/axios-utils";
import { Container, Content, StyledBreadcrumb as Breadcrumb } from "./styles";

function HomeContent() {
  const [hello, setHello] = useState<string | undefined | null>();

  useEffect(() => {
    const loadHello = async () => {
      const [err, res] = await feature(getAPI(HelloApi).apiHelloSayPost());
      if (!err) {
        setHello(res.data.data);
      }
    };

    loadHello();
  }, []);

  return (
    <Content>
      <Breadcrumb
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
