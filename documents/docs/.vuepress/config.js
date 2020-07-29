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
          title: "数据库操作",
          path: "/handbook/database-accessor/",
          sidebarDepth: 3,
          children: [
            {
              title: "数据库上下文",
              path: "/handbook/database-accessor/dbcontext",
            },
            {
              title: "数据库上下文定位器",
              path: "/handbook/database-accessor/dbcontext-locator",
            },
            {
              title: "数据库上下文池",
              path: "/handbook/database-accessor/dbcontextpool",
            },
            {
              title: "实体配置",
              path: "/handbook/database-accessor/dbentity",
            },
            {
              title: "数据仓储",
              path: "/handbook/database-accessor/repository",
            },
            {
              title: "查询操作",
              path: "/handbook/database-accessor/query",
            },
            {
              title: "新增操作",
              path: "/handbook/database-accessor/insert",
            },
            {
              title: "更新操作",
              path: "/handbook/database-accessor/update",
            },
            {
              title: "删除操作",
              path: "/handbook/database-accessor/delete",
            },
            {
              title: "视图操作",
              path: "/handbook/database-accessor/view",
            },
            {
              title: "存储过程操作",
              path: "/handbook/database-accessor/procedure",
            },
            {
              title: "函数操作",
              path: "/handbook/database-accessor/function",
            },
            {
              title: "查询筛选器",
              path: "/handbook/database-accessor/queryfilter",
            },
            {
              title: "种子数据",
              path: "/handbook/database-accessor/seeddata",
            },
            {
              title: "工作单元/事务",
              path: "/handbook/database-accessor/unitofwork-transaction",
            },
            {
              title: "多上下文操作",
              path: "/handbook/database-accessor/multiple-dbcontext",
            },
            {
              title: "切面上下文",
              path: "/handbook/database-accessor/tangent-context",
            },
            {
              title: "多租户模式",
              path: "/handbook/database-accessor/multiple-tenants",
            },
            {
              title: "主从库/读写分离",
              path: "/handbook/database-accessor/master-slave",
            },
            {
              title: "数据库监听",
              path: "/handbook/database-accessor/sql-profiler",
            },
            {
              title: "数据迁移",
              path: "/handbook/database-accessor/code-first",
            },
            {
              title: "反向工程",
              path: "/handbook/database-accessor/db-first",
            },
            {
              title: "审计日志",
              path: "/handbook/database-accessor/audit-log",
            },
          ],
        },
      ],
    },
  },
};
