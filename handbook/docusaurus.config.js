module.exports = {
  title: "Furion",
  tagline: "让 .NET 开发更简单，更通用，更流行。",
  url: "https://furion.pro",
  baseUrl: "/furion/",
  onBrokenLinks: "throw",
  onBrokenMarkdownLinks: "warn",
  favicon: "img/favicon.ico",
  organizationName: "Baiqian Co.,Ltd.",
  projectName: "Furion",
  themeConfig: {
    prism: {
      additionalLanguages: ["powershell", "csharp", "sql"],
    },
    algolia: {
      apiKey: "97723a21d903df7ca5ea618b160f03c6",
      indexName: "furion",
      contextualSearch: true,
    },
    navbar: {
      title: "Furion",
      logo: {
        alt: "Furion Logo",
        src: "img/furionlogo.png",
      },
      hideOnScroll: true,
      items: [
        // {
        //   type: "docsVersionDropdown",
        //   position: "left",
        // },
        {
          to: "docs",
          activeBasePath: "docs",
          label: "文档",
          position: "left",
        },
        {
          to: "docs/global/app",
          activeBasePath: "docs/global",
          label: "静态类",
          position: "left",
        },
        {
          to: "docs/settings/appsettings",
          activeBasePath: "docs/settings",
          label: "配置",
          position: "left",
        },
        { to: "blog", label: "博客", position: "left" },
        // {
        //   label: "视频",
        //   position: "right",
        //   href: "https://space.bilibili.com/695987967",
        // },
        // {
        //   label: "工具",
        //   position: "right",
        //   items: [
        //     {
        //       label: "代码生成器",
        //     },
        //   ],
        // },
        // {
        //   href: "https://gitee.com/monksoul/Furion/board",
        //   label: "看板",
        //   position: "right",
        // },

        {
          label: "源码",
          position: "right",
          items: [
            {
              label: "Gitee",
              href: "https://gitee.com/monksoul/Furion",
            },
            {
              label: "GitHub",
              href: "https://github.com/MonkSoul/Furion",
            },
          ],
        },
        { label: "社区", position: "right", href: "https://www.chinadot.net" },
        {
          label: "合作",
          position: "right",
          to: "docs/cooperate",
          activeBasePath: "docs/cooperate",
        },
        {
          label: "💰赞助",
          position: "right",
          to: "docs/donate",
          activeBasePath: "docs/donate",
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
              to: "docs/get-start",
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
              href: "https://gitee.com/monksoul/Furion/issues",
            },
            {
              label: "看板",
              href: "https://gitee.com/monksoul/Furion/board",
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
              href: "https://gitee.com/monksoul/Furion",
            },
          ],
        },
      ],
      copyright: `Copyright © ${
        new Date().getFullYear() - 1
      }-${new Date().getFullYear()} Furion, Baiqian Co.,Ltd.`,
    },
  },
  presets: [
    [
      "@docusaurus/preset-classic",
      {
        docs: {
          sidebarPath: require.resolve("./sidebars.js"),
          editUrl: "https://gitee.com/monksoul/Furion/tree/master/handbook/",
          showLastUpdateTime: true,
          showLastUpdateAuthor: true,
        },
        blog: {
          showReadingTime: true,
          editUrl: "https://gitee.com/monksoul/Furion/tree/master/handbook/",
        },
        theme: {
          customCss: require.resolve("./src/css/custom.css"),
        },
      },
    ],
  ],
};
