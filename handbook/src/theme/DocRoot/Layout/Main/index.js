import Link from "@docusaurus/Link";
import { useColorMode } from "@docusaurus/theme-common";
import { useDocsSidebar } from "@docusaurus/theme-common/internal";
import useBaseUrl from "@docusaurus/useBaseUrl";
import clsx from "clsx";
import React from "react";
import styles from "./styles.module.css";

export default function DocRootLayoutMain({
  hiddenSidebarContainer,
  children,
}) {
  const sidebar = useDocsSidebar();
  return (
    <main
      className={clsx(
        styles.docMainContainer,
        (hiddenSidebarContainer || !sidebar) && styles.docMainContainerEnhanced
      )}
      style={{ flexDirection: "column" }}
    >
      {/* <Cases /> */}
      <Notice />
      <div
        className={clsx(
          "container padding-top--md padding-bottom--lg",
          styles.docItemWrapper,
          hiddenSidebarContainer && styles.docItemWrapperEnhanced
        )}
      >
        {children}
      </div>
    </main>
  );
}

function Cases({}) {
  const { colorMode, setLightTheme, setDarkTheme } = useColorMode();
  const isDarkTheme = colorMode === "dark";

  return (
    <div className={clsx(styles.cases, isDarkTheme && styles.caseDark)}>
      <Item
        url="https://gitee.com/zuohuaijun/Admin.NET/commits/next"
        title="Admin.NET"
        logoUrl={useBaseUrl("img/furionlogo.png")}
      />
      <Item url="https://www.donet5.com/" title="SqlSugar" />
    </div>
  );
}

function Item({ url, title, logoUrl, desc }) {
  return (
    <a href={url} target="_blank" className={styles.caseItem} title={desc}>
      {logoUrl && <img src={logoUrl} />}
      <span>{title}</span>
    </a>
  );
}

function Notice() {
  const { colorMode, setLightTheme, setDarkTheme } = useColorMode();
  const isDarkTheme = colorMode === "dark";

  return (
    <div className={clsx(styles.notice, isDarkTheme && styles.noticeDark)}>
      ⭐️ 开通 VIP 服务仅需 <s>899 元</s>(<b>限时特价 599 元</b>)，尊享 365
      天项目无忧{" "}
      <Link to={useBaseUrl("/docs/subscribe")} className={styles.tip}>
        立即开通
      </Link>{" "}
      ⭐️
    </div>
  );
}
