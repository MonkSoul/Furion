import { IconHelpCircle } from "@douyinfe/semi-icons";
import { Button, Card, Col, Divider, Row } from "@douyinfe/semi-ui";
import { ButtonProps } from "@douyinfe/semi-ui/lib/es/button";
import styled from "styled-components";

export const Container = styled.div`
  width: 100vw;
  height: 100vh;
`;

export const StyledRow = styled(Row)`
  height: 100%;
`;

export const BannerContainer = styled(Col)`
  height: 100%;
  background-color: rgb(35, 91, 218);
  display: flex;
  justify-content: center;
  align-items: center;
  flex-direction: column;
`;

export const BannerTitle = styled.div`
  margin: 0 auto;
  width: 349px;
  font-size: 24px;
  font-weight: 500;
  font-stretch: normal;
  font-style: normal;
  line-height: 1.5;
  letter-spacing: normal;
  color: #fff;
  position: relative;
  text-align: left;
  top: 100px;
`;

export const LoginContainer = styled(Col)`
  height: 100%;
  display: flex;
  justify-content: center;
  align-items: center;
`;

export const LoginCard = styled(Card)`
  position: relative;
  width: 444px;
  height: 550px;
  padding-left: 20px;
  padding-right: 20px;
  padding-top: 20px;
  box-shadow: 0 4px 8px rgb(31 35 41 / 3%), 0 3px 6px -6px rgb(31 35 41 / 5%),
    0 6px 18px 6px rgb(31 35 41 / 3%);
`;

export const LoginTitle = styled.div`
  font-size: 22px;
  color: #1f2329;
  font-weight: 600;
  line-height: 30px;
  white-space: pre-wrap;
  margin-bottom: 12px;
`;

export const Submit = styled(Button)<ButtonProps>`
  margin-top: 24px;
`;

export const Support = styled.div`
  position: absolute;
  left: 40px;
  right: 40px;
  bottom: 0;
  height: 80px;
`;

export const StyledDivider = styled(Divider)`
  color: #8f959e !important;

  & span {
    font-weight: normal !important;
  }
`;

export const PasswordTip = styled(IconHelpCircle)`
  color: var(--semi-color-text-2);
`;
