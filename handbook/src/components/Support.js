import React, { useState } from "react";
import styled from "styled-components";
import clsx from "clsx";
import IconFont from "./iconfonts";

const Container = styled.div`
  position: fixed;
  flex-direction: column;
  right: 5px;
  bottom: 5px;
  z-index: 100;
  border-radius: 3px;
  width: 260px;
  font-size: 14px;
  overflow: hidden;
  line-height: 24px;
  box-shadow: 0 8px 16px -4px rgb(9 30 66 / 25%), 0 0 0 1px rgb(9 30 66 / 8%);
`;

const Title = styled.div`
  padding: 5px 12px;
  display: flex;
  align-items: center;
  background-color: rgb(0, 148, 0);
  color: #fff;
  cursor: pointer;
  font-weight: bold;
  position: relative;
`;

const Content = styled.div`
  padding: 12px;
  background-color: rgb(230, 246, 230);
`;

const WeiXinImage = styled.img`
  margin: 3px 0 0 8px;
  vertical-align: middle;
  position: relative;
  top: -4px;
`;

const BoldSpan = styled.span`
  font-weight: bold;
  margin: 0 3px;
`;

const Lang = styled.span`
  background: #0000000d;
  color: #000;
  display: inline-block;
  padding: 0 10px;
  font-size: 12px;
  border-radius: 12px;
  margin: 8px 8px 0 0;
  box-shadow: 0 4px 8px -4px rgb(9 30 66 / 25%), 0 0 0 1px rgb(9 30 66 / 8%);
  cursor: pointer;

  &:hover {
    background-color: rgb(252, 213, 63);
  }
`;

const Icon = styled(IconFont)`
  margin-right: 5px;
`;

const ToggleButton = styled.span`
  position: absolute;
  right: 8px;
  height: 24px;
  width: 24px;
  border-radius: 50%;
  display: flex;
  justify-content: center;
  align-items: center;
  cursor: pointer;
  transition: all 0.2s;
  font-weight: bold;

  &:hover {
    background-color: rgba(255, 255, 255, 0.3);
    transform: rotate(90deg);
  }

  & svg {
    position: relative;
    left: 2px;
  }
`;

const langClassName = clsx("animate__animated", "animate__zoomIn");

export default function Support() {
  const [state, setState] = useState(true);
  return (
    <Container className={clsx("animate__animated", "animate__swing")}>
      <Title onClick={() => setState((f) => !f)}>
        <Icon name="fuwu" color="#fff" />
        <span>服务支持</span>
        <ToggleButton>
          <Icon name={state ? "up" : "down"} color="#fff" size={14} />
        </ToggleButton>
      </Title>
      {state && (
        <Content className={clsx("animate__animated", "animate__fadeIn")}>
          如需长期<BoldSpan>技术支持</BoldSpan>或<BoldSpan>技术外包</BoldSpan>
          可添加此微信
          <WeiXinImage
            src="https://img.shields.io/badge/%E5%BE%AE%E4%BF%A1%E5%8F%B7-ibaiqian-yellow?cacheSeconds=10800"
            alt="微信号：ibaiqian"
            title="微信号：ibaiqian"
          />
          <br />
          <Lang className={langClassName}>C#</Lang>
          <Lang className={langClassName}>.NET6</Lang>
          <Lang className={langClassName}>React</Lang>
          <Lang className={langClassName}>Node</Lang>
          <Lang className={langClassName}>TypeScript</Lang>
          <Lang className={langClassName}>Web</Lang>
          <Lang className={langClassName}>Rust</Lang>
        </Content>
      )}
    </Container>
  );
}
