module.exports = {
  docs: [
    {
      type: "category",
      label: "1. 序言",
      items: [
        "introduce",
        "author",
        "source",
        "case",
        "donate",
        "upgrade",
        "course",
        "target",
        "position",
        // "pillar-of-humiliation"
      ],
    },
    {
      type: "category",
      label: "2. 入门指南",
      items: [
        "serverun",
        "get-start",
        "get-start-net6",
        "get-start-net7",
        "template",
        "reference",
        "inject",
        "net5-to-net6",
        "net6-to-net7",
        "globalusing",
        "jsonschema",
        "vsfast",
        "nuget-local",
      ],
    },
    {
      type: "category",
      label: "3. 应用启动",
      items: ["appstartup", "component"],
    },
    {
      type: "category",
      label: "4. 配置与选项",
      items: ["configuration", "options"],
    },
    {
      type: "category",
      label: "5. Web 应用开发",
      items: [
        "dynamic-api-controller",
        "httpcontext",
        "filter",
        "audit",
        "middleware",
        "clientapi",
      ],
    },
    {
      type: "doc",
      id: "specification-document",
    },
    {
      type: "doc",
      id: "friendly-exception",
    },
    {
      type: "doc",
      id: "data-validation",
    },
    {
      type: "category",
      label: "9. 数据库操作指南（EFCore）",
      items: [
        "dbcontext-start",
        "dbcontext",
        "dbcontext-locator",
        "entity",
        "dbcontext-repository",
        "dbcontext-add",
        "dbcontext-update",
        "dbcontext-add-or-update",
        "dbcontext-delete",
        "dbcontext-batch",
        "dbcontext-query",
        "dbcontext-hight-query",
        "dbcontext-view",
        "dbcontext-proc",
        "dbcontext-function",
        "dbcontext-sql",
        "dbcontext-sql-template",
        "dbcontext-sql-proxy",
        "dbcontext-multi-database",
        "dbcontext-db-first",
        "dbcontext-code-first",
        "dbcontext-seed-data",
        "dbcontext-audit",
        "dbcontext-filter",
        "dbcontext-Interceptor",
        "dbcontext-entitytrigger",
        "tran",
        "dbcontext-read-write",
        "split-db",
        "efcore-recommend",
      ],
    },
    {
      type: "category",
      label: "10. SqlSugar 或其他 ORM",
      items: ["sqlsugar", "dapper", "mongodb"],
    },
    {
      type: "doc",
      id: "saas",
    },
    {
      type: "doc",
      id: "dependency-injection",
    },
    {
      type: "doc",
      id: "object-mapper",
    },
    {
      type: "doc",
      id: "cache",
    },
    {
      type: "doc",
      id: "auth-control",
    },
    {
      type: "doc",
      id: "cors",
    },
    {
      type: "doc",
      id: "view-engine",
    },
    {
      type: "doc",
      id: "logging",
    },
    {
      type: "doc",
      id: "http",
    },
    {
      type: "doc",
      id: "encryption",
    },
    {
      type: "doc",
      id: "local-language",
    },
    {
      type: "doc",
      id: "event-bus",
    },
    {
      type: "doc",
      id: "json-serialization",
    },
    {
      type: "doc",
      id: "signalr",
    },
    {
      type: "doc",
      id: "process-service",
    },
    {
      type: "category",
      label: "26. 定时任务 (Schedule)",
      items: ["job", "cron", "task-queue"],
    },
    {
      type: "doc",
      id: "idgenerator",
    },
    {
      type: "doc",
      id: "module-dev",
    },
    {
      type: "doc",
      id: "clayobj",
    },
    {
      type: "doc",
      id: "sensitive-detection",
    },
    {
      type: "doc",
      id: "file-provider",
    },
    {
      type: "doc",
      id: "sesssion-state",
    },
    {
      type: "doc",
      id: "ipc",
    },
    {
      type: "category",
      label: "34. 托管/部署/发布",
      items: [
        "deploy-iis",
        "deploy-docker",
        "deploy-nginx",
        "virtual-deploy",
        "singlefile",
        "pm2",
      ],
    },
    {
      type: "category",
      label: "35. 持续部署集成",
      items: ["deploy-docker-auto", "devops"],
    },
    {
      type: "category",
      label: "36. 测试指南",
      items: ["unittest", "performance", "benchmark", "bingfa"],
    },
    {
      type: "doc",
      id: "dotnet-tools",
    },
    {
      type: "doc",
      id: "contribute",
    },
  ],
  settings: [
    {
      type: "doc",
      id: "settings/appsettings",
    },
    {
      type: "doc",
      id: "settings/corsaccessorsettings",
    },
    {
      type: "doc",
      id: "settings/validationTypemessagesettings",
    },
    {
      type: "doc",
      id: "settings/dependencyinjectionsettings",
    },
    {
      type: "doc",
      id: "settings/dynamicapicontrollersettings",
    },
    {
      type: "doc",
      id: "settings/friendlyexceptionsettings",
    },
    {
      type: "doc",
      id: "settings/specificationdocumentsettings",
    },
    {
      type: "doc",
      id: "settings/localizationsettings",
    },
    {
      type: "doc",
      id: "settings/jwtsettings",
    },
    {
      type: "doc",
      id: "settings/unifyresultsettings",
    },
  ],
  global: [
    {
      type: "doc",
      id: "global/app",
    },
    {
      type: "doc",
      id: "global/db",
    },
    {
      type: "doc",
      id: "global/datavalidator",
    },
    {
      type: "doc",
      id: "global/oops",
    },
    {
      type: "doc",
      id: "global/linqexpression",
    },
    {
      type: "doc",
      id: "global/shttp",
    },
    {
      type: "doc",
      id: "global/jsonserializer",
    },
    {
      type: "doc",
      id: "global/l",
    },
    {
      type: "doc",
      id: "global/messagecenter",
    },
    {
      type: "doc",
      id: "global/json",
    },
    {
      type: "doc",
      id: "global/scoped",
    },
    {
      type: "doc",
      id: "global/sparetime",
    },
    {
      type: "doc",
      id: "global/fs",
    },
    {
      type: "doc",
      id: "global/jwt",
    },
    {
      type: "doc",
      id: "global/tp",
    },
    {
      type: "doc",
      id: "global/log",
    },
    {
      type: "doc",
      id: "global/schedular",
    },
    {
      type: "doc",
      id: "global/taskqueued",
    },
  ],
};
