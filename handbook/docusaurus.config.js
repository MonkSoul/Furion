module.exports = {
  title: "Furion",
  tagline: "让 .NET 开发更简单，更通用，更流行。",
  url: "https://furion.baiqian.ltd",
  baseUrl: "/",
  onBrokenLinks: "throw",
  onBrokenMarkdownLinks: "warn",
  favicon: "img/favicon.ico",
  organizationName: "Baiqian Co.,Ltd",
  projectName: "Furion",
  scripts: [],
  themeConfig: {
    zoom: {
      selector:
        ".markdown :not(em) > img,.markdown > img, article img[loading]",
      background: {
        light: "rgb(255, 255, 255)",
        dark: "rgb(50, 50, 50)",
      },
      // options you can specify via https://github.com/francoischalifour/medium-zoom#usage
      config: {},
    },
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
              label: "📝 查看日志（v4.8.5.5）",
              href: "/docs/upgrade",
            },
            {
              label: "🚀 路线图",
              href: "/docs/target",
            },
          ],
        },
        // {
        //   to: "docs/net6-to-net7",
        //   activeBasePath: "docs/net6-to-net7",
        //   label: ".NET7🚀",
        //   position: "left",
        // },
        {
          label: "源码",
          position: "right",
          items: [
            {
              label: "Gitee（主库）",
              href: "https://gitee.com/dotnetchina/Furion",
            },
            {
              label: "GitHub",
              href: "https://github.com/MonkSoul/Furion",
            },
            {
              label: "Nuget",
              href: "https://www.nuget.org/profiles/monk.soul",
            },
          ],
        },
        {
          label: "社区",
          position: "right",
          href: "https://gitee.com/dotnetchina",
        },
        {
          label: "案例",
          position: "right",
          to: "docs/case",
          activeBasePath: "docs/case",
        },
        {
          label: "赞助",
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
              to: "docs/serverun",
            },
            {
              label: "手册",
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
      copyright: `Copyright © 2020-present 百小僧, Baiqian Co.,Ltd and Contributors.`,
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
          editUrl: "https://gitee.com/dotnetchina/Furion/tree/v4/handbook/",
          showLastUpdateTime: true,
          showLastUpdateAuthor: true,
          sidebarCollapsible: true,
          sidebarCollapsed: true,
        },
        blog: {
          showReadingTime: true,
          editUrl: "https://gitee.com/dotnetchina/Furion/tree/v4/handbook/",
        },
        theme: {
          customCss: require.resolve("./src/css/custom.css"),
        },
      },
    ],
  ],
  plugins: [require.resolve("docusaurus-plugin-image-zoom")],
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
