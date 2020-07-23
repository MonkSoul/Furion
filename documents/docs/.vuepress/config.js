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
        text: "指南",
        link: "/guide/",
      },
      {
        text: "手册",
        link: "/handbook/",
      },
      {
        text: "工具",
        items: [{ text: "代码生成器", link: "/tools/code-generate" }],
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
