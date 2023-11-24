import Link from "@docusaurus/Link";
import { useColorMode } from "@docusaurus/theme-common";
import useBaseUrl from "@docusaurus/useBaseUrl";
import useDocusaurusContext from "@docusaurus/useDocusaurusContext";
import Layout from "@theme/Layout";
import components from "@theme/MDXComponents";
import Tooltip from "@uiw/react-tooltip";
import React, { useContext } from "react";
import Assistance from "../components/Assistance";
import Donate from "../components/Donate";
import GlobalContext from "../components/GlobalContext";
import SpecDonate from "../components/SpecDonate";
import urls from "../data/urls";
import DockerIcon from "./docker.svg";
import "./index.css";
import "./index.own.css";
import KubernetesIcon from "./kubernetes.svg";
import LinuxIcon from "./linux.svg";
import MacOSIcon from "./macos.svg";
import NetConf from "./netconf-bot.svg";
import WindowIcon from "./windows.svg";

const count = "已有 344 位用户开通 VIP 服务";

function Home() {
  const context = useDocusaurusContext();
  const { siteConfig = {} } = context;

  return (
    <Layout
      title={`让 .NET 开发更简单，更通用，更流行。 ${siteConfig.title}`}
      description="让 .NET 开发更简单，更通用，更流行。"
    >
      <Banner />
      <Gitee />
      <ProccessOn />
      <WhoUse />
      <FuChi />
      <Links />
      <Bifa />
    </Layout>
  );
}

function Banner() {
  return (
    <div className="furion-banner">
      <div className="furion-banner-container">
        <img
          src={useBaseUrl("img/netconf-drone.gif")}
          className="furioin-index-gif"
          style={{ pointerEvents: "none" }}
        />
        <div className="furion-banner-item">
          <NetConf
            style={{ position: "absolute", zIndex: 1, top: 200, left: 130 }}
          />
          <div
            className="furion-banner-project"
            style={{ position: "relative", zIndex: 2 }}
          >
            Furion{" "}
            <span
              style={{ fontSize: 14, fontWeight: "normal", color: "#8759ff" }}
            >
              [ˈfjʊəriən] <sup style={{ opacity: 0.6 }}>英</sup>{" "}
              <span style={{ opacity: 0.4, fontSize: 12 }}>|</span> [ˈfjʊriən]{" "}
              <sup style={{ opacity: 0.6 }}>美</sup>
            </span>
          </div>
          <div
            style={{
              color: "#82aaff",
              position: "relative",
              fontSize: 14,
              zIndex: 2,
            }}
          >
            您的痛点，Furion 已阅已历；Furion 的惊喜，您且慢慢享受。
          </div>
          <div
            className="furion-banner-description"
            style={{ position: "relative", zIndex: 2 }}
          >
            让 .NET 开发更简单，更通用，更流行。
          </div>
          <div style={{ position: "relative", zIndex: 2 }}>
            <ul className="furion-banner-spec">
              <li>MIT 宽松开源协议，商用项目首选</li>
              <li>支持 .NET5/6/7/8+，没有历史包袱</li>
              <li>极少依赖，只依赖两个第三方包</li>
              <li>代码无侵入性，兼容原生写法</li>
              <li>
                <Link
                  to={useBaseUrl("docs/whyfurion")}
                  style={{ color: "#fff", textDecoration: "underline" }}
                >
                  了解 「选择 Furion 的十大理由」
                </Link>
              </li>
            </ul>
          </div>
          <div className="furion-support-platform">受支持平台：</div>
          <div className="furion-support-icons">
            <span>
              <WindowIcon height="39" width="39" />
            </span>
            <span>
              <LinuxIcon height="39" width="39" />
            </span>
            <span>
              <MacOSIcon height="39" width="39" />
            </span>
            <span>
              <DockerIcon height="39" width="39" />
            </span>
            <span>
              <KubernetesIcon height="39" width="39" />
            </span>
          </div>

          <div className="furion-get-start-btn">
            <Link
              className="furion-get-start"
              to={useBaseUrl("docs/category/getstart")}
            >
              入门指南
              <span className="furion-version">v4.9.1.7</span>
            </Link>
            <Tooltip content={count} placement="bottomRight" isOpen>
              <Link
                className="furion-try-demo"
                to={useBaseUrl("docs/subscribe")}
                title="VIP 服务"
              >
                VIP 服务
                <span className="furion-version">499元/年</span>
              </Link>
            </Tooltip>
          </div>
        </div>
        <div className="furion-banner-item">
          <SystemWindow style={{ float: "right" }}>
            <CodeSection
              language="cs"
              // section="schema"
              source={`
// highlight-next-line
Serve.Run();

public class FurionAppService : IDynamicApiController
{
    private readonly IRepository<User> _userRepository;
    // highlight-next-line
    public FurionAppService(IRepository<User> userRepository)
    {
        _userRepository = userRepository;
    }

    // highlight-next-line
    [IfException(1000, ErrorMessage = "用户ID: {0} 不存在")]
    public async Task<UserDto> GetUser([Range(1, int.MaxValue)] int userId)
    {
        var user = await _userRepository.FindOrDefaultAsync(userId);
        // highlight-next-line
        _ = user ?? throw Oops.Oh(1000, userId);
        return user.Adapt<UserDto>();
    }

    public async Task<RemoteData> GetRemote(string id)
    {
      // highlight-next-line
        var data = await $"http://furion.baiqian.ltd/data?id={id}".GetAsAsync<RemoteData>();
        return data;
    }
}
`}
            />
          </SystemWindow>
        </div>
      </div>
    </div>
  );
}

function Gitee() {
  const { colorMode, setLightTheme, setDarkTheme } = useColorMode();
  const isDarkTheme = colorMode === "dark";

  return (
    <div className="furion-content">
      <p className={"furion-small-title" + (isDarkTheme ? " dark" : "")}>
        MIT 宽松开源协议/商用项目首选
      </p>
      <h1 className={"furion-big-title" + (isDarkTheme ? " dark" : "")}>
        ⭐️ MIT 开源协议，代码在 Gitee/GitHub 平台托管 ⭐️
      </h1>
      <div className="furion-gitee-log">
        <div
          className="furion-log-item"
          style={{ border: "6px solid #723cff" }}
        >
          <div
            className={"furion-log-jiao" + (isDarkTheme ? " dark" : "")}
          ></div>
          <div className="furion-log-number">
            <div style={{ color: "#723cff" }}>12,000 +</div>
            <span className={isDarkTheme ? " dark" : ""}>Stars</span>
          </div>
        </div>
        <div
          className="furion-log-item"
          style={{ border: "6px solid #3fbbfe" }}
        >
          <div
            className={"furion-log-jiao" + (isDarkTheme ? " dark" : "")}
          ></div>
          <div className="furion-log-number">
            <div style={{ color: "#3fbbfe" }}>4,200 +</div>
            <span className={isDarkTheme ? " dark" : ""}>Forks</span>
          </div>
        </div>
        <div
          className="furion-log-item"
          style={{ border: "6px solid #1fd898" }}
        >
          <div
            className={"furion-log-jiao" + (isDarkTheme ? " dark" : "")}
          ></div>
          <div className="furion-log-number">
            <div style={{ color: "#1fd898" }}>11,648,355</div>
            <span className={isDarkTheme ? " dark" : ""}>Downloads</span>
          </div>
        </div>
      </div>
    </div>
  );
}

function WhoUse() {
  const { setDonate } = useContext(GlobalContext);

  return (
    <div className="furion-whouse">
      <div className="furion-who-custom">
        <h1>特别赞助</h1>
        <div className="furion-donate-wrap" style={{ width: "100%" }}>
          <div
            className="furion-custom-img"
            style={{ width: "100%", maxWidth: 958 }}
          >
            <SpecDonate />
          </div>
        </div>
        <h2>铂金赞助</h2>
        <div className="furion-donate-wrap">
          <div className="furion-custom-img">
            <a href="http://github.crmeb.net/u/furion" target="_blank">
              <img
                src={useBaseUrl("img/crmeb.jpg")}
                height="100"
                style={{ height: 80, maxWidth: "100%" }}
              />
            </a>
          </div>
          <div className="furion-custom-img">
            <a
              href="https://gitee.com/dromara/MaxKey?from=furion"
              target="_blank"
            >
              <img
                src={useBaseUrl("img/maxkey.png")}
                height="100"
                style={{ height: 80, maxWidth: "100%" }}
              />
            </a>
          </div>
        </div>
        <h3>金牌赞助</h3>
        <div className="furion-donate-wrap">
          <div className="furion-custom-img">
            <a href="https://www.gadmin8.com?from=furion" target="_blank">
              <img
                src={useBaseUrl("img/tpflow.png")}
                height="100"
                style={{ height: 50, maxWidth: "100%" }}
              />
            </a>
          </div>
          <div className="furion-custom-img">
            <a href="https://www.coreshop.cn?from=furion" target="_blank">
              <img
                src={useBaseUrl("img/coreshop.gif")}
                height="100"
                style={{ height: 50, maxWidth: "100%" }}
              />
            </a>
          </div>
          <div className="furion-custom-img">
            <a href="https://www.diygw.com?from=furion" target="_blank">
              <img
                src={useBaseUrl("img/lk.jpg")}
                height="100"
                style={{ height: 50, maxWidth: "100%" }}
              />
            </a>
          </div>
        </div>
      </div>
      <div className="furion-who-des">
        <div className="furion-who-des-text">
          <div></div>
          <h1>请考虑赞助 Furion</h1>
          <p>
            Furion 是一个 MIT 许可的开源项目，在 NuGet 平台获得超 1160
            万次下载。从小型企业到企业的解决方案及知名企业，他们在简单软件和复杂管理系统的开发方面都信任我们。
            <br />
            <br />
            如果 Furion 对您有所帮助，并且您希望 Furion 能够继续发展下去，请考虑
            ⌈赞助⌋ 我们。
          </p>
          <br />
          <Assistance style={{ margin: 0 }} />
          <br />
          <Donate />
          <a
            className="furion-get-start"
            style={{ cursor: "pointer" }}
            onClick={() => setDonate(true)}
          >
            成为赞助商
          </a>
        </div>
      </div>
    </div>
  );
}

function Links() {
  const { colorMode, setLightTheme, setDarkTheme } = useColorMode();
  const isDarkTheme = colorMode === "dark";
  return (
    <div className="furion-links">
      <p className={"furion-small-title" + (isDarkTheme ? " dark" : "")}>
        友情链接
      </p>
      <h1 className={"furion-big-title" + (isDarkTheme ? " dark" : "")}>
        更多优秀的项目/网站
      </h1>
      <div className="furion-links-content">
        {urls.map((item, i) => (
          <a href={item.url} key={i} target="_blank" title={item.title}>
            {item.text}
          </a>
        ))}
      </div>
    </div>
  );
}

function ProccessOn() {
  const { colorMode, setLightTheme, setDarkTheme } = useColorMode();
  const isDarkTheme = colorMode === "dark";
  return (
    <div className="furion-proccesson">
      <p className={"furion-small-title" + (isDarkTheme ? " dark" : "")}>
        功能模块
      </p>
      <h1 className={"furion-big-title" + (isDarkTheme ? " dark" : "")}>
        麻雀虽小五脏俱全
      </h1>
      <div className="furion-proccesson-content">
        <iframe
          id="embed_dom"
          name="embed_dom"
          frameBorder="0"
          style={{ display: "block", width: "100%", height: "600px" }}
          src="https://www.processon.com/embed/60a7489f6376891bafa2d353"
        ></iframe>
      </div>
    </div>
  );
}

function CodeSection(props) {
  let { language, replace, section, source } = props;

  source = source.replace(/\/\/ <.*?\n/g, "");

  if (replace) {
    for (const [pattern, value] of Object.entries(replace)) {
      source = source.replace(new RegExp(pattern, "gs"), value);
    }
  }

  source = source.trim();
  if (!source.includes("\n")) {
    source += "\n";
  }

  return (
    <components.pre>
      <components.code
        children={source}
        className={`language-${language}`}
        mdxType="code"
        originalType="code"
        parentName="pre"
      />
    </components.pre>
  );
}

function SystemWindow(systemWindowProps) {
  const { children, className, ...props } = systemWindowProps;
  return (
    <div
      {...props}
      className={"system-window blue-accent preview-border " + className}
    >
      <div className="system-top-bar">
        <span
          className="system-top-bar-circle"
          style={{ backgroundColor: "#8759ff" }}
        />
        <span
          className="system-top-bar-circle"
          style={{ backgroundColor: "#3fc4fe" }}
        />
        <span
          className="system-top-bar-circle"
          style={{ backgroundColor: "#42ffac" }}
        />
      </div>
      {children}
    </div>
  );
}

function Bifa() {
  return (
    <div className="furion-bifa">
      <div className="furion-wzi-title">
        <b>Furion</b>
        历经三年打磨
      </div>
      <Wzi>网友笔伐过</Wzi>
      <Wzi>用户捧杀过</Wzi>
      <Wzi>
        内心<span>反复放弃</span>过
      </Wzi>
      <Wzi>
        最终<span>化茧成蝶</span>
      </Wzi>
      <Wzi>
        为<span>祖国信创</span>添砖加瓦
      </Wzi>
    </div>
  );
}

function Wzi(props) {
  return <div className="furion-wzi">{props.children}</div>;
}

function FuChi() {
  const { colorMode, setLightTheme, setDarkTheme } = useColorMode();
  const isDarkTheme = colorMode === "dark";
  return (
    <div className="furion-links furion-fuchi-wrap">
      <p className={"furion-small-title" + (isDarkTheme ? " dark" : "")}>
        开发者扶持计划
      </p>
      <h1 className={"furion-big-title" + (isDarkTheme ? " dark" : "")}>
        如果 C# 不能成就我们，那我们就成就 C#。
      </h1>
      <div
        className="furion-fuchi"
        style={{
          display: "flex",
          justifyContent: "center",
          alignItems: "center",
          overflow: "hidden",
        }}
      >
        <div
          className="furion-fuchi-content"
          style={{
            fontFamily: "Muli",
            fontSize: "1em",
            lineHeight: 1.75,
            textAlign: "left",
            color: "#444444",
            flex: 1,
          }}
        >
          为了回馈那些为 Furion
          提供问题解决、拉取请求以及开源案例的开发者，Furion 将推出全新的{" "}
          <span style={{ color: "#723cff" }}>"开发者扶持计划"</span>
          。该计划将使用从 VIP 服务中获得的一部分资金来资助这些开发者。
          <br />
          <br />
          我们由衷感谢各位的支持。我们团队愿意将 VIP
          服务收到的一部分资金用来赞助那些为 Furion 框架贡献的人。
          <br />
          <br />
          <Assistance style={{ margin: 0 }} />
          <a className="furion-get-start" style={{ cursor: "pointer" }}>
            即将到来...
          </a>
        </div>
        <div style={{ flex: 1 }}>
          <img
            src={useBaseUrl("img/cmp-support.jpg")}
            style={{ width: "100%" }}
          />
        </div>
      </div>
    </div>
  );
}

export default Home;
