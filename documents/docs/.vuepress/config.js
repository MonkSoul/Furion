module.exports = {
  title: "Fur",
  description: "小僧不才，略懂皮毛（Fur）",
  base: "/fur/",
  dest: "dist",
  markdown: {
    lineNumbers: true,
    toc: {
      includeLevel: [1, 2, 3, 4],
    },
  },
  themeConfig: {
    displayAllHeaders: true,
    lastUpdated: "最后更新于",
    smoothScroll: true,
    nav: [
      {
        text: "入门指南",
        link: "/guide/",
      },
      {
        text: "开发手册",
        link: "/handbook/",
      },
      {
        text: "框架配置",
        link: "/config/",
      },
      {
        text: "视频教程",
        link: "https://gitee.com/monksoul/Fur",
      },
      {
        text: "任务看板",
        link: "https://gitee.com/monksoul/Fur/board",
      },
      {
        text: "便捷工具",
        items: [{ text: "代码生成器", link: "/tools/code-generate" }],
      },
      {
        text: "开源地址",
        items: [
          { text: "Gitee", link: "https://gitee.com/monksoul/Fur" },
          { text: "Github", link: "https://github.com/MonkSoul/Fur" },
        ],
      },
    ],
    sidebar: {
      "/handbook/": [
        {
          title: "配置与选项",
          path: "/handbook/configuration-options/configuration",
          sidebarDepth: 3,
          children: [
            {
              title: "配置",
              path: "/handbook/configuration-options/configuration",
            },
            {
              title: "选项",
              path: "/handbook/configuration-options/options",
            },
          ],
        },
        {
          title: "数据库操作",
          path: "/handbook/database-accessor/dbcontext",
          sidebarDepth: 3,
          children: [
            {
              title: "数据库上下文",
              path: "/handbook/database-accessor/dbcontext",
            },
          ],
        },
      ],
    },
  },
};
