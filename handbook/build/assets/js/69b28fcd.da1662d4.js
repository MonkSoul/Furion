"use strict";(self.webpackChunkfurion=self.webpackChunkfurion||[]).push([[1186],{3905:function(e,n,t){t.d(n,{Zo:function(){return c},kt:function(){return m}});var r=t(7294);function a(e,n,t){return n in e?Object.defineProperty(e,n,{value:t,enumerable:!0,configurable:!0,writable:!0}):e[n]=t,e}function i(e,n){var t=Object.keys(e);if(Object.getOwnPropertySymbols){var r=Object.getOwnPropertySymbols(e);n&&(r=r.filter((function(n){return Object.getOwnPropertyDescriptor(e,n).enumerable}))),t.push.apply(t,r)}return t}function o(e){for(var n=1;n<arguments.length;n++){var t=null!=arguments[n]?arguments[n]:{};n%2?i(Object(t),!0).forEach((function(n){a(e,n,t[n])})):Object.getOwnPropertyDescriptors?Object.defineProperties(e,Object.getOwnPropertyDescriptors(t)):i(Object(t)).forEach((function(n){Object.defineProperty(e,n,Object.getOwnPropertyDescriptor(t,n))}))}return e}function s(e,n){if(null==e)return{};var t,r,a=function(e,n){if(null==e)return{};var t,r,a={},i=Object.keys(e);for(r=0;r<i.length;r++)t=i[r],n.indexOf(t)>=0||(a[t]=e[t]);return a}(e,n);if(Object.getOwnPropertySymbols){var i=Object.getOwnPropertySymbols(e);for(r=0;r<i.length;r++)t=i[r],n.indexOf(t)>=0||Object.prototype.propertyIsEnumerable.call(e,t)&&(a[t]=e[t])}return a}var l=r.createContext({}),p=function(e){var n=r.useContext(l),t=n;return e&&(t="function"==typeof e?e(n):o(o({},n),e)),t},c=function(e){var n=p(e.components);return r.createElement(l.Provider,{value:n},e.children)},u={inlineCode:"code",wrapper:function(e){var n=e.children;return r.createElement(r.Fragment,{},n)}},d=r.forwardRef((function(e,n){var t=e.components,a=e.mdxType,i=e.originalType,l=e.parentName,c=s(e,["components","mdxType","originalType","parentName"]),d=p(t),m=a,v=d["".concat(l,".").concat(m)]||d[m]||u[m]||i;return t?r.createElement(v,o(o({ref:n},c),{},{components:t})):r.createElement(v,o({ref:n},c))}));function m(e,n){var t=arguments,a=n&&n.mdxType;if("string"==typeof e||a){var i=t.length,o=new Array(i);o[0]=d;var s={};for(var l in n)hasOwnProperty.call(n,l)&&(s[l]=n[l]);s.originalType=e,s.mdxType="string"==typeof e?e:a,o[1]=s;for(var p=2;p<i;p++)o[p]=t[p];return r.createElement.apply(null,o)}return r.createElement.apply(null,t)}d.displayName="MDXCreateElement"},3675:function(e,n,t){t.r(n),t.d(n,{assets:function(){return u},contentTitle:function(){return p},default:function(){return v},frontMatter:function(){return l},metadata:function(){return c},toc:function(){return d}});var r=t(3117),a=t(102),i=(t(7294),t(3905)),o=t(4996),s=["components"],l={id:"event-bus-old",title:"22. \u4e8b\u4ef6\u603b\u7ebf",sidebar_label:"22. \u4e8b\u4ef6\u603b\u7ebf"},p=void 0,c={unversionedId:"event-bus-old",id:"event-bus-old",title:"22. \u4e8b\u4ef6\u603b\u7ebf",description:"",source:"@site/docs/event-bus-old.mdx",sourceDirName:".",slug:"/event-bus-old",permalink:"/furion/docs/event-bus-old",draft:!1,editUrl:"https://gitee.com/dotnetchina/Furion/tree/net6/handbook/docs/event-bus-old.mdx",tags:[],version:"current",lastUpdatedBy:"MonkSoul",lastUpdatedAt:1651894493,formattedLastUpdatedAt:"May 7, 2022",frontMatter:{id:"event-bus-old",title:"22. \u4e8b\u4ef6\u603b\u7ebf",sidebar_label:"22. \u4e8b\u4ef6\u603b\u7ebf"}},u={},d=[{value:"22.1 \u4ec0\u4e48\u662f\u4e8b\u4ef6\u603b\u7ebf",id:"221-\u4ec0\u4e48\u662f\u4e8b\u4ef6\u603b\u7ebf",level:2},{value:"22.2 <code>MessageCenter</code> \u6d88\u606f\u4e2d\u5fc3",id:"222-messagecenter-\u6d88\u606f\u4e2d\u5fc3",level:2},{value:"22.2.1 \u6ce8\u518c <code>\u8f7b\u91cf\u7ea7\u4e8b\u4ef6\u603b\u7ebf\u670d\u52a1</code>",id:"2221-\u6ce8\u518c-\u8f7b\u91cf\u7ea7\u4e8b\u4ef6\u603b\u7ebf\u670d\u52a1",level:3},{value:"22.2.2 \u5b9a\u4e49\u8ba2\u9605\u5904\u7406\u7a0b\u5e8f",id:"2222-\u5b9a\u4e49\u8ba2\u9605\u5904\u7406\u7a0b\u5e8f",level:3},{value:"22.2.3 \u53d1\u5e03\u6d88\u606f",id:"2223-\u53d1\u5e03\u6d88\u606f",level:3},{value:"22.2.4 \u76f4\u63a5\u8ba2\u9605\u6d88\u606f",id:"2224-\u76f4\u63a5\u8ba2\u9605\u6d88\u606f",level:3},{value:"22.3 \u540c\u6b65\u65b9\u5f0f\u6267\u884c",id:"223-\u540c\u6b65\u65b9\u5f0f\u6267\u884c",level:2},{value:"22.4 \u5173\u4e8e\u4f9d\u8d56\u6ce8\u5165",id:"224-\u5173\u4e8e\u4f9d\u8d56\u6ce8\u5165",level:2},{value:"22.5 \u53cd\u9988\u4e0e\u5efa\u8bae",id:"225-\u53cd\u9988\u4e0e\u5efa\u8bae",level:2}],m={toc:d};function v(e){var n=e.components,t=(0,a.Z)(e,s);return(0,i.kt)("wrapper",(0,r.Z)({},m,t,{components:n,mdxType:"MDXLayout"}),(0,i.kt)("admonition",{title:"v2.20+ \u7248\u672c\u8bf4\u660e",type:"warning"},(0,i.kt)("p",{parentName:"admonition"},(0,i.kt)("strong",{parentName:"p"},"\u5728 ",(0,i.kt)("inlineCode",{parentName:"strong"},"Furion v2.20+")," \u7248\u672c\u540e\u91c7\u7528 ",(0,i.kt)("a",{parentName:"strong",href:"https://gitee.com/dotnetchina/Jaina"},"Jaina")," \u4e8b\u4ef6\u603b\u7ebf\u66ff\u6362\u539f\u6709\u7684 ",(0,i.kt)("inlineCode",{parentName:"strong"},"EventBus")),"\uff0c",(0,i.kt)("strong",{parentName:"p"},"\ud83d\ude36",(0,i.kt)("a",{parentName:"strong",href:"./event-bus"},"\u67e5\u770b\u65b0\u6587\u6863")))),(0,i.kt)("h2",{id:"221-\u4ec0\u4e48\u662f\u4e8b\u4ef6\u603b\u7ebf"},"22.1 \u4ec0\u4e48\u662f\u4e8b\u4ef6\u603b\u7ebf"),(0,i.kt)("p",null,"\u4e8b\u4ef6\u603b\u7ebf\u662f\u5bf9\u53d1\u5e03-\u8ba2\u9605\u6a21\u5f0f\u7684\u4e00\u79cd\u5b9e\u73b0\u3002\u5b83\u662f\u4e00\u79cd\u96c6\u4e2d\u5f0f\u4e8b\u4ef6\u5904\u7406\u673a\u5236\uff0c\u5141\u8bb8\u4e0d\u540c\u7684\u7ec4\u4ef6\u4e4b\u95f4\u8fdb\u884c\u5f7c\u6b64\u901a\u4fe1\u800c\u53c8\u4e0d\u9700\u8981\u76f8\u4e92\u4f9d\u8d56\uff0c\u8fbe\u5230\u4e00\u79cd\u89e3\u8026\u7684\u76ee\u7684\u3002"),(0,i.kt)("p",null,"\u6211\u4eec\u6765\u770b\u770b\u4e8b\u4ef6\u603b\u7ebf\u7684\u5904\u7406\u6d41\u7a0b\uff1a"),(0,i.kt)("img",{src:(0,o.Z)("img/event1.png")}),(0,i.kt)("h2",{id:"222-messagecenter-\u6d88\u606f\u4e2d\u5fc3"},"22.2 ",(0,i.kt)("inlineCode",{parentName:"h2"},"MessageCenter")," \u6d88\u606f\u4e2d\u5fc3"),(0,i.kt)("p",null,"\u5728 ",(0,i.kt)("inlineCode",{parentName:"p"},"Furion")," \u6846\u67b6\u4e2d\uff0c\u5b9e\u73b0\u4e86\u4e00\u79cd\u8f7b\u91cf\u7ea7\u7684\u4e8b\u4ef6\u603b\u7ebf\u5b9e\u73b0\u673a\u5236\uff1a",(0,i.kt)("inlineCode",{parentName:"p"},"MessageCenter"),"\uff08\u6d88\u606f\u4e2d\u5fc3\uff09\uff0c",(0,i.kt)("inlineCode",{parentName:"p"},"MessageCenter")," \u91c7\u7528\u5b57\u7b26\u4e32\u6d88\u606f\u673a\u5236\u8fdb\u884c\u5e7f\u64ad\uff0c \u53ef\u4ee5\u5728\u7edd\u5927\u591a\u6570\u4e2d\u5c0f\u578b\u9879\u76ee\u4e2d\u53d1\u6325\u4f5c\u7528\uff0c\u7f3a\u70b9\u662f\u6d88\u606f\u5904\u7406\u662f\u5728\u4e3b\u7ebf\u7a0b\u4e2d\u5b8c\u6210\u5e76\u4e14\u6d88\u606f\u4e0d\u652f\u6301\u5206\u5e03\u5f0f\u5b58\u50a8\u3002"),(0,i.kt)("p",null,"\u53e6\u5916\uff0c",(0,i.kt)("inlineCode",{parentName:"p"},"MessageCenter")," \u652f\u6301\u5355\u64ad\u3001\u591a\u64ad\u53d1\u5e03\u53ca\u591a\u8ba2\u9605\u3002\u5982\u56fe\u6240\u793a\uff1a"),(0,i.kt)("img",{src:(0,o.Z)("img/event2.png")}),(0,i.kt)("h3",{id:"2221-\u6ce8\u518c-\u8f7b\u91cf\u7ea7\u4e8b\u4ef6\u603b\u7ebf\u670d\u52a1"},"22.2.1 \u6ce8\u518c ",(0,i.kt)("inlineCode",{parentName:"h3"},"\u8f7b\u91cf\u7ea7\u4e8b\u4ef6\u603b\u7ebf\u670d\u52a1")),(0,i.kt)("p",null,"\u5982\u679c\u60f3\u4f7f\u7528 ",(0,i.kt)("inlineCode",{parentName:"p"},"MessageCenter")," \u8f7b\u91cf\u7ea7\u4e8b\u4ef6\u603b\u7ebf\uff0c\u53ea\u9700\u8981\u5728 ",(0,i.kt)("inlineCode",{parentName:"p"},"Startup.cs")," \u4e2d\u6ce8\u518c\u670d\u52a1\u5373\u53ef\uff0c\u5982\uff1a"),(0,i.kt)("pre",null,(0,i.kt)("code",{parentName:"pre",className:"language-cs",metastring:"showLineNumbers  {3}",showLineNumbers:!0,"":!0,"{3}":!0},"public void ConfigureServices(IServiceCollection services)\n{\n    services.AddSimpleEventBus();\n}\n")),(0,i.kt)("h3",{id:"2222-\u5b9a\u4e49\u8ba2\u9605\u5904\u7406\u7a0b\u5e8f"},"22.2.2 \u5b9a\u4e49\u8ba2\u9605\u5904\u7406\u7a0b\u5e8f"),(0,i.kt)("p",null,(0,i.kt)("inlineCode",{parentName:"p"},"MessageCenter")," \u662f\u6839\u636e ",(0,i.kt)("inlineCode",{parentName:"p"},"MesseageId")," \u6d88\u606f Id \u6765\u89e6\u53d1\u5bf9\u5e94\u7684\u5904\u7406\u7a0b\u5e8f\u7684\uff0c\u6240\u4ee5\u6211\u4eec\u9700\u8981\u5b9a\u4e49 ",(0,i.kt)("inlineCode",{parentName:"p"},"\u8ba2\u9605\u5904\u7406\u7a0b\u5e8f\u7c7b"),"\uff0c\u8be5\u7c7b\u9700\u5b9e\u73b0 ",(0,i.kt)("inlineCode",{parentName:"p"},"ISubscribeHandler")," \u63a5\u53e3\uff0c\u5982\uff1a"),(0,i.kt)("pre",null,(0,i.kt)("code",{parentName:"pre",className:"language-cs",metastring:"showLineNumbers  {1,4-5,11-13}",showLineNumbers:!0,"":!0,"{1,4-5,11-13}":!0},'public class UserChangeSubscribeHandler : ISubscribeHandler\n{\n    // \u5b9a\u4e49\u4e00\u6761\u6d88\u606f\n    [SubscribeMessage("create:user")]\n    public void CreateUser(string eventId, object payload)\n    {\n        Console.WriteLine("\u6211\u662f"+eventId);\n    }\n\n    // \u591a\u6761\u6d88\u606f\u5171\u7528\u540c\u4e00\u4e2a\u5904\u7406\u7a0b\u5e8f\n    [SubscribeMessage("delete:user")]\n    [SubscribeMessage("remove:user")]\n    public void RemoveUser(string eventId, object payload)\n    {\n        Console.WriteLine("\u6211\u662f"+eventId);\n    }\n\n    // \u652f\u6301\u5f02\u6b65\n    [SubscribeMessage("delete:user")]\n    public async Task SupportAsync(string eventId, object payload)\n    {\n        await MethodAsync();\n    }\n}\n')),(0,i.kt)("h3",{id:"2223-\u53d1\u5e03\u6d88\u606f"},"22.2.3 \u53d1\u5e03\u6d88\u606f"),(0,i.kt)("p",null,"\u5b9a\u4e49\u597d\u8ba2\u9605\u5904\u7406\u7a0b\u5e8f\u540e\uff0c\u6211\u4eec\u5c31\u53ef\u4ee5\u5728\u7a0b\u5e8f\u4efb\u4f55\u5730\u65b9\u8fdb\u884c\u5e7f\u64ad\u6d88\u606f\uff0c\u4e8b\u4ef6\u603b\u7ebf\u4f1a\u81ea\u52a8\u6839\u636e ",(0,i.kt)("inlineCode",{parentName:"p"},"\u6d88\u606f Id")," \u89e6\u53d1\u5bf9\u5e94\u7684\u5904\u7406\u7a0b\u5e8f\u65b9\u6cd5\uff1a"),(0,i.kt)("pre",null,(0,i.kt)("code",{parentName:"pre",className:"language-cs",metastring:"showLineNumbers",showLineNumbers:!0},'MessageCenter.Send("create:user", new User {}); // => \u6253\u5370\uff1a\u6211\u662fcreate:user\n\nMessageCenter.Send("delete:user", new User {}); // => \u6253\u5370\uff1a\u6211\u662fdelete:user\nMessageCenter.Send("remove:user", new User {}); // => \u6253\u5370\uff1a\u6211\u662fremove:user\n')),(0,i.kt)("h3",{id:"2224-\u76f4\u63a5\u8ba2\u9605\u6d88\u606f"},"22.2.4 \u76f4\u63a5\u8ba2\u9605\u6d88\u606f"),(0,i.kt)("p",null,"\u5728\u4e0a\u9762\u7684\u4f8b\u5b50\u4e2d\uff0c\u6211\u4eec\u9700\u8981\u521b\u5efa ",(0,i.kt)("inlineCode",{parentName:"p"},"ISubscribeHandler")," \u7684\u6d3e\u751f\u7c7b\u8fdb\u884c\u76f8\u5173\u914d\u7f6e\u624d\u80fd\u5b9e\u73b0\u8ba2\u9605\u5904\u7406\u3002"),(0,i.kt)("p",null,"\u5728\u67d0\u4e9b\u7279\u6b8a\u60c5\u51b5\u4e0b\uff0c\u53ef\u80fd\u53ea\u9700\u8981\u8ba2\u9605\u4e00\u6b21\u5373\u53ef\u3002\u6240\u4ee5\uff0c\u5728 ",(0,i.kt)("inlineCode",{parentName:"p"},"Furion")," \u6846\u67b6\u4e2d\uff0c\u4e3a\u4e86\u66f4\u7b80\u4fbf\u7684\u8ba2\u9605\u6d88\u606f\uff0c\u4e5f\u63d0\u4f9b\u4e86 ",(0,i.kt)("inlineCode",{parentName:"p"},"MessageCenter.Subscribe<T>")," \u9759\u6001\u65b9\u6cd5\uff0c\u5982\uff1a"),(0,i.kt)("pre",null,(0,i.kt)("code",{parentName:"pre",className:"language-cs",metastring:"showLineNumbers",showLineNumbers:!0},'MessageCenter.Subscribe<User>("create:user", (i,p) => {\n    // do something\u3002\u3002\u3002\n});\n')),(0,i.kt)("h2",{id:"223-\u540c\u6b65\u65b9\u5f0f\u6267\u884c"},"22.3 \u540c\u6b65\u65b9\u5f0f\u6267\u884c"),(0,i.kt)("p",null,"\u9ed8\u8ba4\u60c5\u51b5\u4e0b\uff0c\u4e8b\u4ef6\u603b\u7ebf\u603b\u662f\u91c7\u7528\u65b0\u7ebf\u7a0b\u65b9\u5f0f\u6267\u884c\uff0c\u4f46\u662f\u6211\u4eec\u53ef\u4ee5\u914d\u7f6e\u4e3a\u540c\u6b65\u65b9\u5f0f\uff0c\u5982\uff1a"),(0,i.kt)("pre",null,(0,i.kt)("code",{parentName:"pre",className:"language-cs",metastring:"showLineNumbers",showLineNumbers:!0},'MessageCenter.Send("create:user", isSync: true);\n')),(0,i.kt)("h2",{id:"224-\u5173\u4e8e\u4f9d\u8d56\u6ce8\u5165"},"22.4 \u5173\u4e8e\u4f9d\u8d56\u6ce8\u5165"),(0,i.kt)("p",null,"\u5728 ",(0,i.kt)("inlineCode",{parentName:"p"},"Furion")," \u6846\u67b6\u4e2d\uff0c\u4e8b\u4ef6\u603b\u7ebf\u662f\u4e0d\u652f\u6301\u6784\u9020\u51fd\u6570\u6ce8\u5165\u7684\uff0c\u800c\u4e14\u6784\u9020\u51fd\u6570\u4e5f\u53ea\u4f1a\u6267\u884c\u4e00\u6b21\u3002\u6240\u4ee5\u9700\u8981\u7528\u5230\u670d\u52a1\uff0c\u5e94\u8be5\u901a\u8fc7\u9759\u6001\u7c7b\u89e3\u6790\uff0c",(0,i.kt)("inlineCode",{parentName:"p"},"App.GetService<xx>()")," \u6216 ",(0,i.kt)("inlineCode",{parentName:"p"},"Db.GetRepository<XX>()"),"\u3002"),(0,i.kt)("pre",null,(0,i.kt)("code",{parentName:"pre",className:"language-cs",metastring:"showLineNumbers  {5,12-20}",showLineNumbers:!0,"":!0,"{5,12-20}":!0},'public class UserChangeSubscribeHandler : ISubscribeHandler\n{\n    public UserChangeSubscribeHandler()\n    {\n        // \u4e0d\u652f\u6301\u8fd9\u91cc\u89e3\u6790\u670d\u52a1\uff01\uff01\uff01\uff01\uff01\uff01\uff01\uff01\uff01\uff01\uff01\n    }\n\n    // \u5b9a\u4e49\u4e00\u6761\u6d88\u606f\n    [SubscribeMessage("create:user")]\n    public void CreateUser(string eventId, object payload)\n    {\n        // \u521b\u5efa\u4e00\u4e2a\u4f5c\u7528\u57df\uff0c\u5bf9\u8c61\u4f7f\u7528\u5b8c\u6210\u81ea\u52a8\u91ca\u653e\n        Scoped.Create((_, scope) =>\n        {\n            var services = scope.ServiceProvider;\n\n            var repository = Db.GetRepository<Person>(services);    // services \u4f20\u9012\u8fdb\u53bb\n            var someService = App.GetService<ISomeService>(services);   // services \u4f20\u9012\u8fdb\u53bb\n            var otherService = services.GetService<IOtherService>();    // \u76f4\u63a5\u7528 services \u89e3\u6790\n        });\n    }\n}\n')),(0,i.kt)("admonition",{type:"note"},(0,i.kt)("mdxAdmonitionTitle",{parentName:"admonition"},"\u5173\u4e8e ",(0,i.kt)("inlineCode",{parentName:"mdxAdmonitionTitle"},"App.GetService<TService>()")," \u89e3\u6790\u670d\u52a1"),(0,i.kt)("p",{parentName:"admonition"},"\u5728\u9ad8\u9891\u5b9a\u65f6\u4efb\u52a1\u4e2d\u8c03\u7528",(0,i.kt)("inlineCode",{parentName:"p"},"App.GetService(TService)"),"\uff0c\u53ef\u80fd\u4f1a\u51fa\u73b0\u5185\u5b58\u65e0\u6cd5\u56de\u6536\u7684\u60c5\u51b5\uff0c\u5efa\u8bae\u59cb\u7ec8\u4f7f\u7528",(0,i.kt)("inlineCode",{parentName:"p"},"scope.ServiceProvider.GetService(TService)"),"\u6765\u83b7\u53d6",(0,i.kt)("inlineCode",{parentName:"p"},"TService"))),(0,i.kt)("admonition",{title:"\u6570\u636e\u5e93\u64cd\u4f5c\u6ce8\u610f",type:"important"},(0,i.kt)("p",{parentName:"admonition"},"\u5982\u679c\u4f5c\u7528\u57df\u4e2d\u5bf9",(0,i.kt)("strong",{parentName:"p"},"\u6570\u636e\u5e93\u6709\u4efb\u4f55\u53d8\u66f4\u64cd\u4f5c"),"\uff0c\u9700\u624b\u52a8\u8c03\u7528 ",(0,i.kt)("inlineCode",{parentName:"p"},"SaveChanges")," \u6216\u5e26 ",(0,i.kt)("inlineCode",{parentName:"p"},"Now")," \u7ed3\u5c3e\u7684\u65b9\u6cd5\u3002\u4e5f\u53ef\u4ee5\u4f7f\u7528 ",(0,i.kt)("inlineCode",{parentName:"p"},"Scoped.CreateUow(handler)")," \u4ee3\u66ff\u3002")),(0,i.kt)("admonition",{title:"\u5173\u4e8e\u4f9d\u8d56\u6ce8\u5165",type:"warning"},(0,i.kt)("p",{parentName:"admonition"},(0,i.kt)("inlineCode",{parentName:"p"},"ISubscribeHandler")," \u63a5\u53e3\u4e3b\u8981\u662f\u7528\u6765\u67e5\u627e\u5b9a\u4e49\u4e8b\u4ef6\u5bf9\u8c61\u7684\uff0c\u4e5f\u5c31\u662f\u5b83\u7684\u5b9e\u73b0\u7c7b\u5e76\u672a\u63d0\u4f9b\u4f9d\u8d56\u6ce8\u5165\u529f\u80fd\uff0c\u6240\u4ee5\u5728\u5b9e\u73b0\u7c7b\u5e76\u4e0d\u652f\u6301\u6784\u9020\u51fd\u6570\u6ce8\u5165\u4f9d\u8d56\u9879\u3002")),(0,i.kt)("h2",{id:"225-\u53cd\u9988\u4e0e\u5efa\u8bae"},"22.5 \u53cd\u9988\u4e0e\u5efa\u8bae"),(0,i.kt)("admonition",{title:"\u4e0e\u6211\u4eec\u4ea4\u6d41",type:"note"},(0,i.kt)("p",{parentName:"admonition"},"\u7ed9 Furion \u63d0 ",(0,i.kt)("a",{parentName:"p",href:"https://gitee.com/dotnetchina/Furion/issues/new?issue"},"Issue"),"\u3002")))}v.isMDXComponent=!0}}]);