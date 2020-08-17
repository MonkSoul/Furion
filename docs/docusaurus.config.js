module.exports = {
  title: "Fur",
  tagline: "小僧不才，略懂皮毛（Fur）。",
  url: "https://your-docusaurus-test-site.com",
  baseUrl: "/fur/",
  onBrokenLinks: "throw",
  favicon: "img/favicon.ico",
  organizationName: "facebook", // Usually your GitHub org/user name.
  projectName: "docusaurus", // Usually your repo name.
  themeConfig: {
    prism: {
      additionalLanguages: ["powershell","csharp"],
    },
    navbar: {
      title: "Fur",
      logo: {
        alt: "Fur Logo",
        src: "img/logo.svg",
      },
      hideOnScroll: true,
      items: [
        {
          type: "docsVersionDropdown",
          position: "left",
        },
        {
          to: "docs/",
          activeBasePath: "docs",
          label: "手册",
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
          title: "手册",
          items: [
            {
              label: "快速入门",
              to: "docs/",
            },
            {
              label: "文档指南",
              to: "docs/doc2/",
            },
          ],
        },
        {
          title: "社区",
          items: [
            {
              label: "Stack Overflow",
              href: "https://stackoverflow.com/questions/tagged/docusaurus",
            },
            {
              label: "Discord",
              href: "https://discordapp.com/invite/docusaurus",
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
              label: "GitHub",
              href: "https://github.com/facebook/docusaurus",
            },
          ],
        },
      ],
      copyright: `Copyright © ${new Date().getFullYear()} Fur, Baiqian. Built with Docusaurus.`,
    },
  },
  presets: [
    [
      "@docusaurus/preset-classic",
      {
        docs: {
          // It is recommended to set document id as docs home page (`docs/` path).
          homePageId: "configuration",
          sidebarPath: require.resolve("./sidebars.js"),
          // Please change this to your repo.
          editUrl:
            "https://gitee.com/monksoul/Fur/tree/alpha/docs/",
        },
        blog: {
          showReadingTime: true,
          // Please change this to your repo.
          editUrl:
            "https://gitee.com/monksoul/Fur/tree/alpha/docs/",
        },
        theme: {
          customCss: require.resolve("./src/css/custom.css"),
        },
      },
    ],
  ],
};
