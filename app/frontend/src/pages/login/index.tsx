import { IconHelpCircle } from "@douyinfe/semi-icons";
import { Checkbox, Form, Tooltip } from "@douyinfe/semi-ui";
import { useNavigate } from "react-router-dom";
import { ReactComponent as IconLogin } from "../../shared/assets/images/login.svg";
import {
  BannerContainer,
  BannerTitle,
  Container,
  LoginCard,
  LoginContainer,
  LoginTitle,
  StyledDivider,
  StyledRow,
  Submit,
  Support
} from "./styles";

function Login() {
  return (
    <Container>
      <StyledRow>
        <LoginForm />
        <Banner />
      </StyledRow>
    </Container>
  );
}

function LoginForm() {
  const navigate = useNavigate();

  return (
    <LoginContainer span={16}>
      <LoginCard>
        <LoginTitle>欢迎使用 Furion</LoginTitle>
        <Form layout="vertical">
          <Form.Input
            size="large"
            field="userName"
            label="用户名"
            placeholder="手机号/电子邮箱/用户名"
          />
          <Form.Input
            mode="password"
            size="large"
            field="password"
            label={{
              text: "密码",
              extra: (
                <Tooltip content="详情">
                  <IconHelpCircle
                    style={{ color: "var(--semi-color-text-2)" }}
                  />
                </Tooltip>
              ),
            }}
            placeholder="请输入密码"
          />
          <Checkbox value="false">记住我</Checkbox>
          <Submit
            block
            size="large"
            type="secondary"
            onClick={() => navigate("/")}
          >
            登录
          </Submit>
        </Form>
        <Support>
          <StyledDivider margin="12px" align="center">
            v0.1.0-alpha
          </StyledDivider>
        </Support>
      </LoginCard>
    </LoginContainer>
  );
}

function Banner() {
  return (
    <BannerContainer span={8}>
      <BannerTitle>
        Furion 整合企业管理通用功能于一体，让 .NET 开发更简单，更通用，更流行。
      </BannerTitle>
      <IconLogin width={350} />
    </BannerContainer>
  );
}

export default Login;
