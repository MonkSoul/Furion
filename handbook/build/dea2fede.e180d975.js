(window.webpackJsonp=window.webpackJsonp||[]).push([[95],{169:function(e,n,t){"use strict";t.r(n),t.d(n,"frontMatter",(function(){return s})),t.d(n,"metadata",(function(){return b})),t.d(n,"toc",(function(){return u})),t.d(n,"default",(function(){return p}));var a=t(3),o=t(7),r=(t(0),t(191)),i=(t(189),t(198)),c=t(199),l=t(192),s={id:"tran",title:"9.26 \u4e8b\u52a1\u548c\u5de5\u4f5c\u5355\u5143",sidebar_label:"9.26 \u4e8b\u52a1\u548c\u5de5\u4f5c\u5355\u5143"},b={unversionedId:"tran",id:"tran",isDocsHomePage:!1,title:"9.26 \u4e8b\u52a1\u548c\u5de5\u4f5c\u5355\u5143",description:"9.26.1 \u4e8b\u52a1",source:"@site/docs\\tran.mdx",slug:"/tran",permalink:"/furion/docs/tran",editUrl:"https://gitee.com/monksoul/Furion/tree/master/handbook/docs/tran.mdx",version:"current",lastUpdatedBy:"\u767e\u5c0f\u50e7",lastUpdatedAt:1612020493,sidebar_label:"9.26 \u4e8b\u52a1\u548c\u5de5\u4f5c\u5355\u5143",sidebar:"docs",previous:{title:"9.25. \u5b9e\u4f53\u6570\u636e\u76d1\u542c\u5668",permalink:"/furion/docs/dbcontext-entitytrigger"},next:{title:"9.27 \u8bfb\u5199\u5206\u79bb/\u4e3b\u4ece\u590d\u5236",permalink:"/furion/docs/dbcontext-read-write"}},u=[{value:"9.26.1 \u4e8b\u52a1",id:"9261-\u4e8b\u52a1",children:[]},{value:"9.26.2 \u5de5\u4f5c\u5355\u5143",id:"9262-\u5de5\u4f5c\u5355\u5143",children:[]},{value:"9.26.3 \u5982\u4f55\u4f7f\u7528",id:"9263-\u5982\u4f55\u4f7f\u7528",children:[{value:"9.26.3.1 \u81ea\u52a8\u7ba1\u7406",id:"92631-\u81ea\u52a8\u7ba1\u7406",children:[]},{value:"9.26.3.2 \u624b\u52a8\u7ba1\u7406",id:"92632-\u624b\u52a8\u7ba1\u7406",children:[]}]},{value:"9.26.4 \u5de5\u4f5c\u5355\u5143\u7279\u6027\u8bf4\u660e",id:"9264-\u5de5\u4f5c\u5355\u5143\u7279\u6027\u8bf4\u660e",children:[{value:"9.26.4.1 <code>[UnitOfWork]</code>",id:"92641-unitofwork",children:[]}]},{value:"9.26.5 \u53cd\u9988\u4e0e\u5efa\u8bae",id:"9265-\u53cd\u9988\u4e0e\u5efa\u8bae",children:[]}],d={toc:u};function p(e){var n=e.components,t=Object(o.a)(e,["components"]);return Object(r.b)("wrapper",Object(a.a)({},d,t,{components:n,mdxType:"MDXLayout"}),Object(r.b)(l.a,{mdxType:"JoinGroup"}),Object(r.b)("h2",{id:"9261-\u4e8b\u52a1"},"9.26.1 \u4e8b\u52a1"),Object(r.b)("p",null,"\u4e8b\u52a1\u6307\u4f5c\u4e3a\u5355\u4e2a\u903b\u8f91\u5de5\u4f5c\u5355\u5143\u6267\u884c\u7684\u4e00\u7cfb\u5217\u64cd\u4f5c\uff0c\u8981\u4e48",Object(r.b)("strong",{parentName:"p"},"\u5b8c\u5168\u5730\u6267\u884c\uff0c\u8981\u4e48\u5b8c\u5168\u5730\u4e0d\u6267\u884c"),"\u3002"),Object(r.b)("p",null,"\u7b80\u5355\u7684\u8bf4\uff0c\u4e8b\u52a1\u5c31\u662f\u5e76\u53d1\u63a7\u5236\u7684\u5355\u4f4d\uff0c\u662f\u7528\u6237\u5b9a\u4e49\u7684\u4e00\u4e2a\u64cd\u4f5c\u5e8f\u5217\u3002 \u800c\u4e00\u4e2a\u903b\u8f91\u5de5\u4f5c\u5355\u5143\u8981\u6210\u4e3a\u4e8b\u52a1\uff0c\u5c31\u5fc5\u987b\u6ee1\u8db3 ",Object(r.b)("inlineCode",{parentName:"p"},"ACID")," \u5c5e\u6027\u3002"),Object(r.b)("ul",null,Object(r.b)("li",{parentName:"ul"},Object(r.b)("inlineCode",{parentName:"li"},"A"),"\uff1a\u539f\u5b50\u6027\uff08Atomicity\uff09\uff1a\u4e8b\u52a1\u4e2d\u7684\u64cd\u4f5c\u8981\u4e48\u90fd\u4e0d\u505a\uff0c\u8981\u4e48\u5c31\u5168\u505a"),Object(r.b)("li",{parentName:"ul"},Object(r.b)("inlineCode",{parentName:"li"},"C"),"\uff1a\u4e00\u81f4\u6027\uff08Consistency\uff09\uff1a\u4e8b\u52a1\u6267\u884c\u7684\u7ed3\u679c\u5fc5\u987b\u662f\u4ece\u6570\u636e\u5e93\u4ece\u4e00\u4e2a\u4e00\u81f4\u6027\u72b6\u6001\u8f6c\u6362\u5230\u53e6\u4e00\u4e2a\u4e00\u81f4\u6027\u72b6\u6001"),Object(r.b)("li",{parentName:"ul"},Object(r.b)("inlineCode",{parentName:"li"},"I"),"\uff1a\u9694\u79bb\u6027\uff08Isolation\uff09\uff1a\u4e00\u4e2a\u4e8b\u52a1\u7684\u6267\u884c\u4e0d\u80fd\u88ab\u5176\u4ed6\u4e8b\u52a1\u5e72\u6270"),Object(r.b)("li",{parentName:"ul"},Object(r.b)("inlineCode",{parentName:"li"},"D"),"\uff1a\u6301\u4e45\u6027\uff08Durability\uff09\uff1a\u4e00\u4e2a\u4e8b\u52a1\u4e00\u65e6\u63d0\u4ea4\uff0c\u5b83\u5bf9\u6570\u636e\u5e93\u4e2d\u6570\u636e\u7684\u6539\u53d8\u5c31\u5e94\u8be5\u662f\u6c38\u4e45\u6027\u7684")),Object(r.b)("h2",{id:"9262-\u5de5\u4f5c\u5355\u5143"},"9.26.2 \u5de5\u4f5c\u5355\u5143"),Object(r.b)("p",null,"\u7b80\u5355\u6765\u8bf4\uff0c\u5c31\u662f\u4e3a\u4e86\u4fdd\u8bc1\u4e00\u6b21\u5b8c\u6574\u7684\u529f\u80fd\u64cd\u4f5c\u6240\u4ea7\u751f\u7684\u4e00\u4e9b\u5217\u63d0\u4ea4\u6570\u636e\u7684\u5b8c\u6574\u6027\uff0c\u8d77\u7740\u4e8b\u52a1\u7684\u4f5c\u7528\u3002\u5728\u8ba1\u7b97\u673a\u9886\u57df\u4e2d\uff0c\u5de5\u4f5c\u5355\u5143\u901a\u5e38\u7528 ",Object(r.b)("inlineCode",{parentName:"p"},"UnitOfWork")," \u540d\u79f0\u8868\u793a\u3002"),Object(r.b)("p",null,"\u901a\u5e38\u6211\u4eec\u4fdd\u8bc1\u7528\u6237\u7684\u6bcf\u4e00\u6b21\u8bf7\u6c42\u90fd\u662f\u5904\u4e8e\u5728\u4e00\u4e2a\u529f\u80fd\u5355\u5143\u4e2d\uff0c\u4e5f\u5c31\u662f\u5de5\u4f5c\u5355\u5143\u3002"),Object(r.b)("h2",{id:"9263-\u5982\u4f55\u4f7f\u7528"},"9.26.3 \u5982\u4f55\u4f7f\u7528"),Object(r.b)("h3",{id:"92631-\u81ea\u52a8\u7ba1\u7406"},"9.26.3.1 \u81ea\u52a8\u7ba1\u7406"),Object(r.b)("p",null,"\u5728 ",Object(r.b)("inlineCode",{parentName:"p"},"Furion")," \u6846\u67b6\u4e2d\uff0c\u6211\u4eec\u53ea\u9700\u8981\u5728\u63a7\u5236\u5668 Action \u4e2d\u8d34 ",Object(r.b)("inlineCode",{parentName:"p"},"[UnitOfWork]")," \u7279\u6027\u5373\u53ef\u5f00\u542f\u5de5\u4f5c\u5355\u5143\u6a21\u5f0f\uff0c\u4fdd\u8bc1\u4e86\u6bcf\u4e00\u6b21\u8bf7\u6c42\u90fd\u662f\u4e00\u4e2a ",Object(r.b)("inlineCode",{parentName:"p"},"\u5de5\u4f5c\u5355\u5143"),"\uff0c\u8981\u4e48\u540c\u65f6\u6210\u529f\uff0c\u8981\u4e48\u540c\u65f6\u5931\u8d25\u3002"),Object(r.b)("h3",{id:"92632-\u624b\u52a8\u7ba1\u7406"},"9.26.3.2 \u624b\u52a8\u7ba1\u7406"),Object(r.b)(i.a,{defaultValue:"one",values:[{label:"\u793a\u4f8b\u4e00",value:"one"},{label:"\u793a\u4f8b\u4e8c",value:"two"},{label:"\u793a\u4f8b\u4e09",value:"three"}],mdxType:"Tabs"},Object(r.b)(c.a,{value:"one",mdxType:"TabItem"},Object(r.b)("pre",null,Object(r.b)("code",Object(a.a)({parentName:"pre"},{className:"language-cs"}),'// \u5f00\u542f\u4e8b\u52a1\nusing (var transaction = _testRepository.Database.BeginTransaction())\n{\n    try\n    {\n        _testRepository.Insert(new Blog { Url = "http://blogs.msdn.com/dotnet" });\n        _testRepository.SaveChanges();\n\n        _testRepository.Insert(new Blog { Url = "http://blogs.msdn.com/visualstudio" });\n        _testRepository.SaveChanges();\n\n        var blogs = _testRepository.Entity\n                .OrderBy(b => b.Url)\n                .ToList();\n\n        // \u63d0\u4ea4\u4e8b\u52a1\n        transaction.Commit();\n     }\n     catch (Exception)\n     {\n        // \u56de\u6eda\u4e8b\u52a1\n        transaction.RollBack();\n     }\n}\n'))),Object(r.b)(c.a,{value:"two",mdxType:"TabItem"},Object(r.b)("pre",null,Object(r.b)("code",Object(a.a)({parentName:"pre"},{className:"language-cs"}),'var options = new DbContextOptionsBuilder<HoaDbContext>()\n    .UseSqlServer(new SqlConnection(connectionString))\n    .Options;\n\n// \u521b\u5efa\u8fde\u63a5\u5b57\u7b26\u4e32\nusing (var context1 = new HoaDbContext(options))\n{\n    // \u5f00\u542f\u4e8b\u52a1\n    using (var transaction = context1.Database.BeginTransaction())\n    {\n        try\n        {\n            _testRepository.Insert(new Blog { Url = "http://blogs.msdn.com/dotnet" });\n            _testRepository.SaveChanges();\n\n            context1.Blogs.Add(new Blog { Url = "http://blogs.msdn.com/dotnet" });\n            context1.SaveChanges();\n\n            // \u521b\u5efa\u65b0\u7684\u8fde\u63a5\u5bf9\u8c61\n            using (var context2 = new HoaDbContext(options))\n            {\n                // \u5171\u4eab\u8fde\u63a5\u4e8b\u52a1\n                context2.Database.UseTransaction(transaction.GetDbTransaction());\n\n                var blogs = context2.Blogs\n                    .OrderBy(b => b.Url)\n                    .ToList();\n            }\n\n            // \u63d0\u4ea4\u4e8b\u52a1\n            transaction.Commit();\n        }\n        catch (Exception)\n        {\n            // \u56de\u6eda\u4e8b\u52a1\n            transaction.RollBack();\n        }\n    }\n}\n'))),Object(r.b)(c.a,{value:"three",mdxType:"TabItem"},Object(r.b)("pre",null,Object(r.b)("code",Object(a.a)({parentName:"pre"},{className:"language-cs"}),'// \u5f00\u542f\u5206\u5e03\u5f0f\u4e8b\u52a1\nusing (var scope = new TransactionScope(\n    TransactionScopeOption.Required,\n    new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))\n{\n    using (var connection = new SqlConnection(connectionString))\n    {\n        connection.Open();\n\n        try\n        {\n            // \u8fd9\u91cc\u662f Ado.NET \u64cd\u4f5c\n            var command = connection.CreateCommand();\n            command.CommandText = "DELETE FROM dbo.Blogs";\n            command.ExecuteNonQuery();\n\n            // \u521b\u5efaEF Core \u6570\u636e\u5e93\u4e0a\u4e0b\u6587\n            var options = new DbContextOptionsBuilder<BloggingContext>()\n                .UseSqlServer(connection)\n                .Options;\n            using (var context = new BloggingContext(options))\n            {\n                context.Blogs.Add(new Blog { Url = "http://blogs.msdn.com/dotnet" });\n                context.SaveChanges();\n            }\n\n            // \u6846\u67b6\u5c01\u88c5\u7684\u4ed3\u50a8\n            _testRepository.Insert(new Blog { Url = "http://blogs.msdn.com/dotnet" });\n            _testRepository.SaveChanges();\n\n           // \u63d0\u4ea4\u4e8b\u52a1\n            scope.Complete();\n        }\n        catch (System.Exception)\n        {\n            // \u81ea\u52a8\u56de\u6eda\n        }\n    }\n}\n')))),Object(r.b)("h2",{id:"9264-\u5de5\u4f5c\u5355\u5143\u7279\u6027\u8bf4\u660e"},"9.26.4 \u5de5\u4f5c\u5355\u5143\u7279\u6027\u8bf4\u660e"),Object(r.b)("h3",{id:"92641-unitofwork"},"9.26.4.1 ",Object(r.b)("inlineCode",{parentName:"h3"},"[UnitOfWork]")),Object(r.b)("p",null,Object(r.b)("inlineCode",{parentName:"p"},"[UnitOfWork]")," \u7279\u6027\u7528\u6765\u6807\u8bb0\u4e8b\u52a1\u4fe1\u606f\uff0c\u5982\u4f5c\u7528\u8303\u56f4\uff0c\u9694\u79bb\u7ea7\u522b\u7b49\u3002"),Object(r.b)("ul",null,Object(r.b)("li",{parentName:"ul"},Object(r.b)("inlineCode",{parentName:"li"},"Enabled"),"\uff1a\u662f\u5426\u542f\u52a8\u5de5\u4f5c\u5355\u5143\uff0c\u9ed8\u8ba4 ",Object(r.b)("inlineCode",{parentName:"li"},"true")),Object(r.b)("li",{parentName:"ul"},Object(r.b)("inlineCode",{parentName:"li"},"ScopeOption"),"\uff1a\u5b9a\u4e49\u4e8b\u52a1\u8303\u56f4\u884c\u4e3a\uff0c\u9ed8\u8ba4 ",Object(r.b)("inlineCode",{parentName:"li"},"TransactionScopeOption.Required")),Object(r.b)("li",{parentName:"ul"},Object(r.b)("inlineCode",{parentName:"li"},"IsolationLevel"),"\uff1a\u8bbe\u7f6e\u4e8b\u52a1\u9694\u79bb\u7ea7\u522b\uff0c\u9ed8\u8ba4 ",Object(r.b)("inlineCode",{parentName:"li"},"IsolationLevel.ReadCommitted"),";"),Object(r.b)("li",{parentName:"ul"},Object(r.b)("inlineCode",{parentName:"li"},"AsyncFlowOption"),"\uff1a\u5141\u8bb8\u8de8\u7ebf\u7a0b\u8fde\u7eed\u4efb\u52a1\u7684\u4e8b\u52a1\u6d41\uff0c\u5982\u6709\u5f02\u6b65\u64cd\u4f5c\u9700\u5f00\u542f\u8be5\u9009\u9879\uff0c\u9ed8\u8ba4\u5f00\u542f")),Object(r.b)("div",{className:"admonition admonition-important alert alert--info"},Object(r.b)("div",Object(a.a)({parentName:"div"},{className:"admonition-heading"}),Object(r.b)("h5",{parentName:"div"},Object(r.b)("span",Object(a.a)({parentName:"h5"},{className:"admonition-icon"}),Object(r.b)("svg",Object(a.a)({parentName:"span"},{xmlns:"http://www.w3.org/2000/svg",width:"14",height:"16",viewBox:"0 0 14 16"}),Object(r.b)("path",Object(a.a)({parentName:"svg"},{fillRule:"evenodd",d:"M7 2.3c3.14 0 5.7 2.56 5.7 5.7s-2.56 5.7-5.7 5.7A5.71 5.71 0 0 1 1.3 8c0-3.14 2.56-5.7 5.7-5.7zM7 1C3.14 1 0 4.14 0 8s3.14 7 7 7 7-3.14 7-7-3.14-7-7-7zm1 3H6v5h2V4zm0 6H6v2h2v-2z"})))),"\u7279\u522b\u6ce8\u610f")),Object(r.b)("div",Object(a.a)({parentName:"div"},{className:"admonition-content"}),Object(r.b)("p",{parentName:"div"},"\u4e00\u65e6\u65b9\u6cd5\u8d34\u4e86 ",Object(r.b)("inlineCode",{parentName:"p"},"[UnitOfWork(false)]")," \u7279\u6027\u540e\uff0c\u90a3\u4e48\u8be5\u65b9\u6cd5\u4e0d\u518d\u542f\u7528\u5de5\u4f5c\u5355\u5143\u6a21\u5f0f\uff0c\u4e5f\u5c31\u662f\u4e0d\u5305\u542b\u4e8b\u52a1\uff0c\u4e5f\u4e0d\u4f1a\u81ea\u52a8\u63d0\u4ea4\u6570\u636e\u5e93\u3002\u614e\u7528!"))),Object(r.b)("h2",{id:"9265-\u53cd\u9988\u4e0e\u5efa\u8bae"},"9.26.5 \u53cd\u9988\u4e0e\u5efa\u8bae"),Object(r.b)("div",{className:"admonition admonition-note alert alert--secondary"},Object(r.b)("div",Object(a.a)({parentName:"div"},{className:"admonition-heading"}),Object(r.b)("h5",{parentName:"div"},Object(r.b)("span",Object(a.a)({parentName:"h5"},{className:"admonition-icon"}),Object(r.b)("svg",Object(a.a)({parentName:"span"},{xmlns:"http://www.w3.org/2000/svg",width:"14",height:"16",viewBox:"0 0 14 16"}),Object(r.b)("path",Object(a.a)({parentName:"svg"},{fillRule:"evenodd",d:"M6.3 5.69a.942.942 0 0 1-.28-.7c0-.28.09-.52.28-.7.19-.18.42-.28.7-.28.28 0 .52.09.7.28.18.19.28.42.28.7 0 .28-.09.52-.28.7a1 1 0 0 1-.7.3c-.28 0-.52-.11-.7-.3zM8 7.99c-.02-.25-.11-.48-.31-.69-.2-.19-.42-.3-.69-.31H6c-.27.02-.48.13-.69.31-.2.2-.3.44-.31.69h1v3c.02.27.11.5.31.69.2.2.42.31.69.31h1c.27 0 .48-.11.69-.31.2-.19.3-.42.31-.69H8V7.98v.01zM7 2.3c-3.14 0-5.7 2.54-5.7 5.68 0 3.14 2.56 5.7 5.7 5.7s5.7-2.55 5.7-5.7c0-3.15-2.56-5.69-5.7-5.69v.01zM7 .98c3.86 0 7 3.14 7 7s-3.14 7-7 7-7-3.12-7-7 3.14-7 7-7z"})))),"\u4e0e\u6211\u4eec\u4ea4\u6d41")),Object(r.b)("div",Object(a.a)({parentName:"div"},{className:"admonition-content"}),Object(r.b)("p",{parentName:"div"},"\u7ed9 Furion \u63d0 ",Object(r.b)("a",Object(a.a)({parentName:"p"},{href:"https://gitee.com/monksoul/Furion/issues/new?issue"}),"Issue"),"\u3002"))),Object(r.b)("hr",null),Object(r.b)("div",{className:"admonition admonition-note alert alert--secondary"},Object(r.b)("div",Object(a.a)({parentName:"div"},{className:"admonition-heading"}),Object(r.b)("h5",{parentName:"div"},Object(r.b)("span",Object(a.a)({parentName:"h5"},{className:"admonition-icon"}),Object(r.b)("svg",Object(a.a)({parentName:"span"},{xmlns:"http://www.w3.org/2000/svg",width:"14",height:"16",viewBox:"0 0 14 16"}),Object(r.b)("path",Object(a.a)({parentName:"svg"},{fillRule:"evenodd",d:"M6.3 5.69a.942.942 0 0 1-.28-.7c0-.28.09-.52.28-.7.19-.18.42-.28.7-.28.28 0 .52.09.7.28.18.19.28.42.28.7 0 .28-.09.52-.28.7a1 1 0 0 1-.7.3c-.28 0-.52-.11-.7-.3zM8 7.99c-.02-.25-.11-.48-.31-.69-.2-.19-.42-.3-.69-.31H6c-.27.02-.48.13-.69.31-.2.2-.3.44-.31.69h1v3c.02.27.11.5.31.69.2.2.42.31.69.31h1c.27 0 .48-.11.69-.31.2-.19.3-.42.31-.69H8V7.98v.01zM7 2.3c-3.14 0-5.7 2.54-5.7 5.68 0 3.14 2.56 5.7 5.7 5.7s5.7-2.55 5.7-5.7c0-3.15-2.56-5.69-5.7-5.69v.01zM7 .98c3.86 0 7 3.14 7 7s-3.14 7-7 7-7-3.12-7-7 3.14-7 7-7z"})))),"\u4e86\u89e3\u66f4\u591a")),Object(r.b)("div",Object(a.a)({parentName:"div"},{className:"admonition-content"}),Object(r.b)("p",{parentName:"div"},"\u60f3\u4e86\u89e3\u66f4\u591a ",Object(r.b)("inlineCode",{parentName:"p"},"\u4e8b\u52a1")," \u77e5\u8bc6\u53ef\u67e5\u9605 ",Object(r.b)("a",Object(a.a)({parentName:"p"},{href:"https://docs.microsoft.com/zh-cn/ef/core/saving/transactions"}),"EF Core - \u4f7f\u7528\u4e8b\u52a1")," \u7ae0\u8282\u3002"))))}p.isMDXComponent=!0},189:function(e,n,t){"use strict";t.d(n,"b",(function(){return r})),t.d(n,"a",(function(){return i}));var a=t(21),o=t(190);function r(){const{siteConfig:{baseUrl:e="/",url:n}={}}=Object(a.default)();return{withBaseUrl:(t,a)=>function(e,n,t,{forcePrependBaseUrl:a=!1,absolute:r=!1}={}){if(!t)return t;if(t.startsWith("#"))return t;if(Object(o.b)(t))return t;if(a)return n+t;const i=t.startsWith(n)?t:n+t.replace(/^\//,"");return r?e+i:i}(n,e,t,a)}}function i(e,n={}){const{withBaseUrl:t}=r();return t(e,n)}},190:function(e,n,t){"use strict";function a(e){return!0===/^(\w*:|\/\/)/.test(e)}function o(e){return void 0!==e&&!a(e)}t.d(n,"b",(function(){return a})),t.d(n,"a",(function(){return o}))},191:function(e,n,t){"use strict";t.d(n,"a",(function(){return u})),t.d(n,"b",(function(){return m}));var a=t(0),o=t.n(a);function r(e,n,t){return n in e?Object.defineProperty(e,n,{value:t,enumerable:!0,configurable:!0,writable:!0}):e[n]=t,e}function i(e,n){var t=Object.keys(e);if(Object.getOwnPropertySymbols){var a=Object.getOwnPropertySymbols(e);n&&(a=a.filter((function(n){return Object.getOwnPropertyDescriptor(e,n).enumerable}))),t.push.apply(t,a)}return t}function c(e){for(var n=1;n<arguments.length;n++){var t=null!=arguments[n]?arguments[n]:{};n%2?i(Object(t),!0).forEach((function(n){r(e,n,t[n])})):Object.getOwnPropertyDescriptors?Object.defineProperties(e,Object.getOwnPropertyDescriptors(t)):i(Object(t)).forEach((function(n){Object.defineProperty(e,n,Object.getOwnPropertyDescriptor(t,n))}))}return e}function l(e,n){if(null==e)return{};var t,a,o=function(e,n){if(null==e)return{};var t,a,o={},r=Object.keys(e);for(a=0;a<r.length;a++)t=r[a],n.indexOf(t)>=0||(o[t]=e[t]);return o}(e,n);if(Object.getOwnPropertySymbols){var r=Object.getOwnPropertySymbols(e);for(a=0;a<r.length;a++)t=r[a],n.indexOf(t)>=0||Object.prototype.propertyIsEnumerable.call(e,t)&&(o[t]=e[t])}return o}var s=o.a.createContext({}),b=function(e){var n=o.a.useContext(s),t=n;return e&&(t="function"==typeof e?e(n):c(c({},n),e)),t},u=function(e){var n=b(e.components);return o.a.createElement(s.Provider,{value:n},e.children)},d={inlineCode:"code",wrapper:function(e){var n=e.children;return o.a.createElement(o.a.Fragment,{},n)}},p=o.a.forwardRef((function(e,n){var t=e.components,a=e.mdxType,r=e.originalType,i=e.parentName,s=l(e,["components","mdxType","originalType","parentName"]),u=b(t),p=a,m=u["".concat(i,".").concat(p)]||u[p]||d[p]||r;return t?o.a.createElement(m,c(c({ref:n},s),{},{components:t})):o.a.createElement(m,c({ref:n},s))}));function m(e,n){var t=arguments,a=n&&n.mdxType;if("string"==typeof e||a){var r=t.length,i=new Array(r);i[0]=p;var c={};for(var l in n)hasOwnProperty.call(n,l)&&(c[l]=n[l]);c.originalType=e,c.mdxType="string"==typeof e?e:a,i[1]=c;for(var s=2;s<r;s++)i[s]=t[s];return o.a.createElement.apply(null,i)}return o.a.createElement.apply(null,t)}p.displayName="MDXCreateElement"},192:function(e,n,t){"use strict";t.d(n,"a",(function(){return i}));var a=t(0),o=t.n(a),r=t(189);t(55);function i(){const[e,n]=Object(a.useState)(!1);return o.a.createElement("div",{className:"furion-join-group"},e&&o.a.createElement("img",{src:Object(r.a)("img/dotnetchina2.jpg")}),o.a.createElement("button",{onClick:()=>n(!e)},"\u52a0\u5165QQ\u4ea4\u6d41\u7fa4"))}},193:function(e,n,t){"use strict";function a(e){var n,t,o="";if("string"==typeof e||"number"==typeof e)o+=e;else if("object"==typeof e)if(Array.isArray(e))for(n=0;n<e.length;n++)e[n]&&(t=a(e[n]))&&(o&&(o+=" "),o+=t);else for(n in e)e[n]&&(o&&(o+=" "),o+=n);return o}n.a=function(){for(var e,n,t=0,o="";t<arguments.length;)(e=arguments[t++])&&(n=a(e))&&(o&&(o+=" "),o+=n);return o}},195:function(e,n,t){"use strict";var a=t(0),o=t(196);n.a=function(){const e=Object(a.useContext)(o.a);if(null==e)throw new Error("`useUserPreferencesContext` is used outside of `Layout` Component.");return e}},196:function(e,n,t){"use strict";var a=t(0);const o=Object(a.createContext)(void 0);n.a=o},198:function(e,n,t){"use strict";var a=t(0),o=t.n(a),r=t(195),i=t(193),c=t(56),l=t.n(c);const s=37,b=39;n.a=function(e){const{lazy:n,block:t,defaultValue:c,values:u,groupId:d,className:p}=e,{tabGroupChoices:m,setTabGroupChoices:O}=Object(r.a)(),[j,v]=Object(a.useState)(c),f=a.Children.toArray(e.children);if(null!=d){const e=m[d];null!=e&&e!==j&&u.some(n=>n.value===e)&&v(e)}const g=e=>{v(e),null!=d&&O(d,e)},h=[];return o.a.createElement("div",null,o.a.createElement("ul",{role:"tablist","aria-orientation":"horizontal",className:Object(i.a)("tabs",{"tabs--block":t},p)},u.map(({value:e,label:n})=>o.a.createElement("li",{role:"tab",tabIndex:0,"aria-selected":j===e,className:Object(i.a)("tabs__item",l.a.tabItem,{"tabs__item--active":j===e}),key:e,ref:e=>h.push(e),onKeyDown:e=>{((e,n,t)=>{switch(t.keyCode){case b:((e,n)=>{const t=e.indexOf(n)+1;e[t]?e[t].focus():e[0].focus()})(e,n);break;case s:((e,n)=>{const t=e.indexOf(n)-1;e[t]?e[t].focus():e[e.length-1].focus()})(e,n)}})(h,e.target,e)},onFocus:()=>g(e),onClick:()=>{g(e)}},n))),n?Object(a.cloneElement)(f.filter(e=>e.props.value===j)[0],{className:"margin-vert--md"}):o.a.createElement("div",{className:"margin-vert--md"},f.map((e,n)=>Object(a.cloneElement)(e,{key:n,hidden:e.props.value!==j}))))}},199:function(e,n,t){"use strict";var a=t(3),o=t(0),r=t.n(o);n.a=function({children:e,hidden:n,className:t}){return r.a.createElement("div",Object(a.a)({role:"tabpanel"},{hidden:n,className:t}),e)}}}]);