import Link from "@docusaurus/Link";
import { useColorMode } from "@docusaurus/theme-common";
import useBaseUrl from "@docusaurus/useBaseUrl";
import useDocusaurusContext from "@docusaurus/useDocusaurusContext";
import Layout from "@theme/Layout";
import components from "@theme/MDXComponents";
import Modal from "@uiw/react-modal";
import React, { useState } from "react";
import urls from "../data/urls";
import DockerIcon from "./docker.svg";
import "./index.css";
import "./index.own.css";
import KubernetesIcon from "./kubernetes.svg";
import LinuxIcon from "./linux.svg";
import MacOSIcon from "./macos.svg";
import WindowIcon from "./windows.svg";

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
      <Links />
      <Bifa />
      <DDos />
    </Layout>
  );
}

function Banner() {
  return (
    <div className="furion-banner">
      <div className="furion-banner-container">
        <div className="furion-banner-item">
          <div className="furion-banner-project">
            Furion{" "}
            <span
              style={{ fontSize: 14, fontWeight: "normal", color: "#8759ff" }}
            >
              [ˈfjʊəriən] <sup style={{ opacity: 0.6 }}>英</sup>{" "}
              <span style={{ opacity: 0.4, fontSize: 12 }}>|</span> [ˈfjʊriən]{" "}
              <sup style={{ opacity: 0.6 }}>美</sup>
            </span>
          </div>
          <div style={{ color: "#82aaff", position: "relative", fontSize: 14 }}>
            您的痛点，Furion 已阅已历；Furion 的惊喜，您且慢慢享受。
          </div>
          <div className="furion-banner-description">
            让 .NET 开发更简单，更通用，更流行。
          </div>
          <div>
            <ul className="furion-banner-spec">
              <li>MIT 宽松开源协议，商用项目首选</li>
              <li>支持 .NET5/6/7/8+，没有历史包袱</li>
              <li>极少依赖，只依赖两个第三方包</li>
              <li>代码无侵入性，兼容原生写法</li>
              <li>
                <a
                  href={useBaseUrl("docs/whyfurion")}
                  style={{ color: "#fff", textDecoration: "underline" }}
                >
                  了解 「选择 Furion 的十大理由」
                </a>
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
              <span className="furion-version">v4.8.8.48</span>
            </Link>
            <Link
              className="furion-try-demo"
              to={useBaseUrl("docs/subscribe")}
              title="企业服务"
            >
              企业服务
              {/* <span className="furion-version">专业团队</span> */}
            </Link>
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
            <div style={{ color: "#723cff" }}>12000 +</div>
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
            <div style={{ color: "#3fbbfe" }}>4200 +</div>
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
            <div style={{ color: "#1fd898" }}>10,972,714</div>
            <span className={isDarkTheme ? " dark" : ""}>Downloads</span>
          </div>
        </div>
      </div>
    </div>
  );
}

function WhoUse() {
  return (
    <div className="furion-whouse">
      <div className="furion-who-custom">
        {/* <h1>等待下一个幸运儿，会是你吗？</h1> */}
        <div className="furion-custom-img">
          <a href="https://gitee.com/dotnetchina" target="_blank">
            <img src={useBaseUrl("img/chinadotnet.png")} id="dotnet-china" />
          </a>
        </div>
        <div className="furion-custom-img">
          <img
            src={useBaseUrl("img/custom1.png")}
            height="100"
            style={{ height: 100, maxWidth: "100%" }}
          />
        </div>
        <div className="furion-custom-img">
          <img
            src={useBaseUrl("img/custom2.png")}
            height="100"
            style={{ height: 100, maxWidth: "100%" }}
          />
        </div>
        <div className="furion-custom-img">
          <img
            src={useBaseUrl("img/custom3.png")}
            height="100"
            style={{ height: 100, maxWidth: "100%" }}
          />
        </div>
        <div className="furion-custom-img">
          <img
            src={useBaseUrl("img/custom4.jpg")}
            height="100"
            style={{ height: 100, maxWidth: "100%" }}
          />
        </div>
        <div className="furion-custom-img">
          <a href="http://www.hezongsoft.net" target="_blank">
            <img
              src={useBaseUrl("img/custom5.png")}
              height="100"
              style={{ height: 100, maxWidth: "100%" }}
            />
          </a>
        </div>
        <div className="furion-custom-img">
          <a href="http://www.dilon.vip/" target="_blank">
            <img
              src={useBaseUrl("img/custom6.png")}
              height="100"
              style={{ height: 100, maxWidth: "100%" }}
            />
          </a>
        </div>
        <div className="furion-custom-img">
          <img
            src={useBaseUrl("img/c10.jpg")}
            height="100"
            style={{ height: 100, maxWidth: "100%" }}
          />
        </div>
        <div className="furion-custom-img">
          <img
            src={useBaseUrl("img/jjd.png")}
            height="100"
            style={{ height: 100, maxWidth: "100%" }}
          />
        </div>
        <div className="furion-custom-img">
          <a href="http://www.samfeng.cn/" target="_blank">
            <img
              src={useBaseUrl("img/custom10.png")}
              height="100"
              style={{ height: 100, maxWidth: "100%" }}
            />
          </a>
        </div>
        <div className="furion-custom-img">
          <a href="http://www.ketaisoft.com/" target="_blank">
            <img
              src={useBaseUrl("img/custom11.png")}
              height="100"
              style={{ height: 100, maxWidth: "100%" }}
            />
          </a>
        </div>
      </div>
      <div className="furion-who-des">
        <div style={{ maxWidth: 350 }}>
          <div></div>
          <h1>他们都在用</h1>
          <p>
            Furion 已在 NuGet 平台超 1100
            万次下载。从小型企业到企业的解决方案及知名企业。他们在简单软件和复杂管理系统的开发方面都信任我们。
          </p>
          <a
            className="furion-get-start"
            href="mailto:monksoul@outlook.com"
            target="_blank"
          >
            立即添加您的公司
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

function DDos() {
  const [visible, setVisible] = useState(() => {
    var currentDate = new Date();
    var lastDate = new Date("2023-10-26 00:00:00");
    return currentDate < lastDate;
  });

  const onClosed = () => {
    setVisible(false);
  };

  return (
    <div>
      <Modal
        title="关于 DDoS 攻击"
        isOpen={visible}
        confirmText="知道啦"
        icon="reddit"
        type="primary"
        onClosed={onClosed}
      >
        <i>好吧，官网被 DDoS 攻击了</i>。
        <br />
        <br />
        事情是这样的，早在 <u>2023年10月10日</u>，官网迎来了首次 DDoS
        攻击，云服务器因 CDN 超量和全站加速损失了1000
        多元。当时没太放在心上，简单的把对应的 IP
        添加到了黑名单规则中，于是攻击消停了，舒舒服服地过完风平浪静且没有任何波澜的一周。🫠
        <br />
        <br />
        就在前天 <u>2023年10月21日</u>，很平常的周末， DDoS
        攻击来了个二次进宫。这套路我老熟了 {"->"} 添加 IP
        到黑名单列表。没错，当时这样做确实是解决了（机智如我~）😁
        <br />
        <br />
        于是，来到昨天 <u>2023年10月22日</u>
        晚，
        <u>
          还是很平常的一天，无聊中静待着命运的惊喜（原来是惊吓！）。果不其然！DDoS
          攻击它~它它~又来了
        </u>
        ，这一次云服务器直接损失了4000 多元。🤣😂
        <br />
        <br />
        <u>此情此景，我的内心只有一句独白：“好嗨哦！感觉人生已经达到了高潮~”</u>
        。
        {/* <img src={useBaseUrl("img/support.png")} style={{ maxWidth: "100%" }} /> */}
        <br />
        <br />
        于是，本着 ⌈事不过三⌋ 的处理原则，我妥协了：
        <br />
        <br />
        <b>
          搞 DDoS 攻击的爷啊~，Furion 是基于 MIT 开源协议的项目，
          <u>真的是不赚钱（严格来说是亏钱的）</u>，
          三年来完全是用爱发电，牺牲了不知多少私人时间，投入了不少成本，如今有这等规模真不容易。再者
          Furion
          每年投入的云服务器还有其他杂七杂八（域名、CDN、全站加速、OSS...）费用大概在
          8000 多元，这些都是我们团队自个掏钱维持的。
          <u>所以，高抬贵手~，别盯着它，别折腾它啦。</u>🤞
        </b>
        <br />
        <br />
        <i>注：此弹窗保留三天，三天后不再显示，多多理解~</i>（2023.10.23）
      </Modal>
    </div>
  );
}

export default Home;
