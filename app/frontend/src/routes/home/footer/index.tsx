import { Footer, Logo, Span } from "./styles";

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
