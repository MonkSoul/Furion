import { Footer, Logo, Span } from "./styles";

function HomeFooter() {
  return (
    <Footer>
      <Span>
        <Logo size="large" />
        <span>
          Copyright © 2020-present 百小僧, 百签科技（广东）有限公司 and Contributors.
        </span>
      </Span>
      <span>
        <span>v0.1.0-alpha</span>
      </span>
    </Footer>
  );
}

export default HomeFooter;
