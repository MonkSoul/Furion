/**
 * 首页内容
 */

import { Breadcrumb, Layout, Skeleton } from "@douyinfe/semi-ui";
import { useEffect, useState } from "react";
import { HelloApi } from "../../../shared/api-services";
import { getAPI } from "../../../shared/axios-utils";

function HomeContent() {
  const { Content } = Layout;
  const [hello, setHello] = useState<string | undefined | null>();

  useEffect(() => {
    getAPI(HelloApi)
      .apiHelloSayPost()
      .then((res) => {
        setHello(res.data.data);
      });
  }, []);

  return (
    <Content
      style={{
        padding: "24px",
        backgroundColor: "var(--semi-color-bg-0)",
      }}
    >
      <Breadcrumb
        style={{
          marginBottom: "24px",
        }}
        routes={["首页", "当这个页面标题很长时需要省略", "上一页", "详情页"]}
      />
      <div
        style={{
          borderRadius: "10px",
          border: "1px solid var(--semi-color-border)",
          height: "376px",
          padding: "32px",
        }}
      >
        <Skeleton placeholder={<Skeleton.Paragraph rows={2} />} loading={true}>
          <p>Hi, Bytedance dance dance.</p>
          <p>Hi, Bytedance dance dance.</p>
        </Skeleton>
        {hello}
      </div>
    </Content>
  );
}

export default HomeContent;
