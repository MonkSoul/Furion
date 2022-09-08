module.exports = {
  title: "Furion",
  tagline: "让 .NET 开发更简单，更通用，更流行。",
  url: "https://dotnetchina.gitee.io",
  baseUrl: "/furion/",
  onBrokenLinks: "throw",
  onBrokenMarkdownLinks: "warn",
  favicon: "img/favicon.ico",
  organizationName: "Baiqian Co.,Ltd",
  projectName: "Furion",
  scripts: ["/furion/script/baidutongji.js"],
  themeConfig: {
    docs: {
      sidebar: {
        hideable: true,
        autoCollapseCategories: true,
      },
    },
    prism: {
      additionalLanguages: ["powershell", "csharp", "sql"],
      // theme: require("prism-react-renderer/themes/github"),
      // darkTheme: require("prism-react-renderer/themes/dracula"),
    },
    // algolia: {
    //   appId: "XYY4NGVXSA",
    //   apiKey: "957b35892d68e9ac86c35c96d89dcedf",
    //   indexName: "furion",
    //   contextualSearch: true,
    // },
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
        {
          label: "更新日志",
          position: "left",
          items: [
            {
              label: "📝 查看日志",
              href: "/docs/upgrade",
            },
            {
              label: "🚀 路线图",
              href: "/docs/target",
            },
          ],
        },
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
        //   href: "https://gitee.com/dotnetchina/Furion/board",
        //   label: "看板",
        //   position: "right",
        // },
        // {
        //   label: "耻辱柱",
        //   position: "right",
        //   to: "docs/pillar-of-humiliation",
        //   activeBasePath: "docs/pillar-of-humiliation",
        // },
        {
          label: "源码",
          position: "right",
          items: [
            {
              label: "Gitee",
              href: "https://gitee.com/dotnetchina/Furion",
            },
            {
              label: "GitHub",
              href: "https://github.com/MonkSoul/Furion",
            },
          ],
        },
        {
          label: "社区",
          position: "right",
          href: "https://gitee.com/dotnetchina",
        },
        {
          label: "案例✨",
          position: "right",
          to: "docs/case",
          activeBasePath: "docs/case",
        },
        {
          label: "支持",
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
              href: "https://gitee.com/dotnetchina/Furion/issues",
            },
            {
              label: "看板",
              href: "https://gitee.com/dotnetchina/Furion/board",
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
              href: "https://gitee.com/dotnetchina/Furion",
            },
          ],
        },
      ],
      copyright: `Copyright © 2020-${new Date().getFullYear()} 百小僧, Baiqian Co.,Ltd.`,
      logo: {
        src: "img/chinadotnet.png",
        href: "https://gitee.com/dotnetchina",
      },
    },
  },
  presets: [
    [
      "@docusaurus/preset-classic",
      {
        docs: {
          sidebarPath: require.resolve("./sidebars.js"),
          editUrl: "https://gitee.com/dotnetchina/Furion/tree/net6/handbook/",
          showLastUpdateTime: true,
          showLastUpdateAuthor: true,
          // sidebarCollapsible: true,
        },
        blog: {
          showReadingTime: true,
          editUrl: "https://gitee.com/dotnetchina/Furion/tree/net6/handbook/",
        },
        theme: {
          customCss: require.resolve("./src/css/custom.css"),
        },
      },
    ],
  ],
  themes: [
    [
      "@easyops-cn/docusaurus-search-local",
      {
        hashed: true,
        language: ["en", "zh"],
        highlightSearchTermsOnTargetPage: true,
        explicitSearchResultPath: true,
      },
    ],
  ],
};
