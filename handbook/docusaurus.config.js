module.exports = {
  title: "Fur",
  tagline: "Fur 是 .NET 5 平台下企业应用开发最佳实践框架。",
  url: "https://chinadot.net",
  baseUrl: "/fur/",
  onBrokenLinks: "throw",
  favicon: "img/favicon.ico",
  organizationName: "Baiqian Co.,Ltd.",
  projectName: "Fur",
  themeConfig: {
    prism: {
      additionalLanguages: ["powershell", "csharp", "sql"],
    },
    navbar: {
      title: "Fur",
      logo: {
        alt: "Fur Logo",
        src: "img/logo.png",
      },
      hideOnScroll: false,
      items: [
        {
          type: "docsVersionDropdown",
          position: "left",
        },
        {
          to: "docs",
          activeBasePath: "docs",
          label: "文档",
          position: "left",
        },
        { to: "blog", label: "博客", position: "left" },
        { label: "配置", position: "left" },
        { label: "视频", position: "right" },
        {
          label: "工具",
          position: "right",
          items: [
            {
              label: "代码生成器",
            },
          ],
        },
        {
          href: "https://gitee.com/monksoul/Fur/board",
          label: "看板",
          position: "right",
        },
        {
          label: "仓库",
          position: "right",
          items: [
            {
              label: "Gitee",
              href: "https://gitee.com/monksoul/Fur",
            },
            {
              label: "GitHub",
              href: "https://github.com/MonkSoul/Fur",
            },
          ],
        },
      ],
    },
    footer: {
      style: "dark",
      links: [
        {
          title: "文档",
          items: [
            {
              label: "入门",
              to: "docs",
            },
            {
              label: "指南",
              to: "docs",
            },
          ],
        },
        {
          title: "社区",
          items: [
            {
              label: "讨论",
              href: "https://gitee.com/monksoul/Fur/issues",
            },
            {
              label: "看板",
              href: "https://gitee.com/monksoul/Fur/board",
            },
          ],
        },
        {
          title: "更多",
          items: [
            {
              label: "博客",
              to: "blog",
            },
            {
              label: "仓库",
              href: "https://gitee.com/monksoul/Fur",
            },
          ],
        },
      ],
      copyright: `Copyright © ${new Date().getFullYear()} Fur, Baiqian Co.,Ltd.`,
    },
  },
  presets: [
    [
      "@docusaurus/preset-classic",
      {
        docs: {
          sidebarPath: require.resolve("./sidebars.js"),
          editUrl: "https://gitee.com/monksoul/Fur/tree/main/handbook/",
        },
        blog: {
          showReadingTime: true,
          editUrl: "https://gitee.com/monksoul/Fur/tree/main/handbook/",
        },
        theme: {
          customCss: require.resolve("./src/css/custom.css"),
        },
      },
    ],
  ],
};
