import React from "react";
import clsx from "clsx";
import Layout from "@theme/Layout";
import Link from "@docusaurus/Link";
import useDocusaurusContext from "@docusaurus/useDocusaurusContext";
import useBaseUrl from "@docusaurus/useBaseUrl";
import styles from "./styles.module.css";

const features = [
  {
    title: <>.NET 5 新起点</>,
    imageUrl: "img/undraw_docusaurus_mountain.svg",
    description: (
      <>
        .NET 5 是 .NET 的重要且令人兴奋的新方向。你会看到 .NET
        变得更加简单，但也有更广泛的功能和实用程序。
        <strong>
          所有新的开发和功能都将是 .NET 5 的一部分，包括新的 C# 版本
        </strong>
        。
      </>
    ),
  },
  {
    title: <>“六极” 架构</>,
    imageUrl: "img/undraw_docusaurus_tree.svg",
    description: (
      <>
        Fur 在设计之初就秉承着 “六极” ：
        <strong>
          极易入门、极速开发、极少依赖、极少配置、极其灵活、极易维护
        </strong>{" "}
        的设计思想，在架构设计上做了大量的优化，支持各个能力阶层技术员极速上手。
      </>
    ),
  },
  {
    title: <>冲一杯咖啡的时间</>,
    imageUrl: "img/undraw_docusaurus_react.svg",
    description: (
      <>
        Fur
        除了独具创新的设计理念和灵活的架构设计以外，同时还结合了主流的敏捷开发模式打造的一款极速开发框架。
        <strong>只需冲制一杯咖啡的时间便可完成工作</strong>。
      </>
    ),
  },
];

function Feature({ imageUrl, title, description }) {
  const imgUrl = useBaseUrl(imageUrl);
  return (
    <div className={clsx("col col--4", styles.feature)}>
      {imgUrl && (
        <div className="text--center">
          <img className={styles.featureImage} src={imgUrl} alt={title} />
        </div>
      )}
      <h3 className={clsx(styles.featureTitle)}>{title}</h3>
      <p>{description}</p>
    </div>
  );
}

function Home() {
  const context = useDocusaurusContext();
  const { siteConfig = {} } = context;
  return (
    <Layout
      title={`Fur 是 .NET 5 平台下企业应用开发最佳实践框架。 ${siteConfig.title}`}
      description="Fur 是 .NET 5 平台下企业应用开发最佳实践框架。"
    >
      <header className={clsx("hero hero--primary", styles.heroBanner)}>
        <div className="container">
          <h1 className="hero__title">
            <img
              src={useBaseUrl("img/furlogo.png")}
              height="80"
              style={{ margin: "0 auto", display: "block" }}
            />
          </h1>
          <p className={clsx("hero__subtitle", styles.subtitle)}>
            {siteConfig.tagline}
          </p>
          <div className={styles.buttons}>
            <Link
              className={clsx(
                "button button--outline button--secondary button--lg",
                styles.getStarted
              )}
              to={useBaseUrl("docs/get-start")}
            >
              一分钟入门
            </Link>
          </div>
        </div>
      </header>
      <main>
        {features && features.length > 0 && (
          <section className={styles.features}>
            <div className="container">
              <div className="row">
                {features.map((props, idx) => (
                  <Feature key={idx} {...props} />
                ))}
              </div>
            </div>
          </section>
        )}
      </main>
    </Layout>
  );
}

export default Home;
