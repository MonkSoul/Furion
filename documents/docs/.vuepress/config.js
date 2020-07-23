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
          title: "数据库操作指南",
          path: "/handbook/database-accessor/",
          sidebarDepth: 3,
          children: [
            {
              title: "工作单元与事务",
              path: "/handbook/database-accessor/unitofwork-transaction",
            },
          ],
        },
      ],
    },
  },
};
