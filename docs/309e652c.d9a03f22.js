(window.webpackJsonp=window.webpackJsonp||[]).push([[24],{189:function(e,t,a){"use strict";a.d(t,"b",(function(){return c})),a.d(t,"a",(function(){return b}));var n=a(21),r=a(190);function c(){const{siteConfig:{baseUrl:e="/",url:t}={}}=Object(n.default)();return{withBaseUrl:(a,n)=>function(e,t,a,{forcePrependBaseUrl:n=!1,absolute:c=!1}={}){if(!a)return a;if(a.startsWith("#"))return a;if(Object(r.b)(a))return a;if(n)return t+a;const b=a.startsWith(t)?a:t+a.replace(/^\//,"");return c?e+b:b}(t,e,a,n)}}function b(e,t={}){const{withBaseUrl:a}=c();return a(e,t)}},190:function(e,t,a){"use strict";function n(e){return!0===/^(\w*:|\/\/)/.test(e)}function r(e){return void 0!==e&&!n(e)}a.d(t,"b",(function(){return n})),a.d(t,"a",(function(){return r}))},191:function(e,t,a){"use strict";a.d(t,"a",(function(){return m})),a.d(t,"b",(function(){return d}));var n=a(0),r=a.n(n);function c(e,t,a){return t in e?Object.defineProperty(e,t,{value:a,enumerable:!0,configurable:!0,writable:!0}):e[t]=a,e}function b(e,t){var a=Object.keys(e);if(Object.getOwnPropertySymbols){var n=Object.getOwnPropertySymbols(e);t&&(n=n.filter((function(t){return Object.getOwnPropertyDescriptor(e,t).enumerable}))),a.push.apply(a,n)}return a}function i(e){for(var t=1;t<arguments.length;t++){var a=null!=arguments[t]?arguments[t]:{};t%2?b(Object(a),!0).forEach((function(t){c(e,t,a[t])})):Object.getOwnPropertyDescriptors?Object.defineProperties(e,Object.getOwnPropertyDescriptors(a)):b(Object(a)).forEach((function(t){Object.defineProperty(e,t,Object.getOwnPropertyDescriptor(a,t))}))}return e}function o(e,t){if(null==e)return{};var a,n,r=function(e,t){if(null==e)return{};var a,n,r={},c=Object.keys(e);for(n=0;n<c.length;n++)a=c[n],t.indexOf(a)>=0||(r[a]=e[a]);return r}(e,t);if(Object.getOwnPropertySymbols){var c=Object.getOwnPropertySymbols(e);for(n=0;n<c.length;n++)a=c[n],t.indexOf(a)>=0||Object.prototype.propertyIsEnumerable.call(e,a)&&(r[a]=e[a])}return r}var l=r.a.createContext({}),p=function(e){var t=r.a.useContext(l),a=t;return e&&(a="function"==typeof e?e(t):i(i({},t),e)),a},m=function(e){var t=p(e.components);return r.a.createElement(l.Provider,{value:t},e.children)},s={inlineCode:"code",wrapper:function(e){var t=e.children;return r.a.createElement(r.a.Fragment,{},t)}},O=r.a.forwardRef((function(e,t){var a=e.components,n=e.mdxType,c=e.originalType,b=e.parentName,l=o(e,["components","mdxType","originalType","parentName"]),m=p(a),O=n,d=m["".concat(b,".").concat(O)]||m[O]||s[O]||c;return a?r.a.createElement(d,i(i({ref:t},l),{},{components:a})):r.a.createElement(d,i({ref:t},l))}));function d(e,t){var a=arguments,n=t&&t.mdxType;if("string"==typeof e||n){var c=a.length,b=new Array(c);b[0]=O;var i={};for(var o in t)hasOwnProperty.call(t,o)&&(i[o]=t[o]);i.originalType=e,i.mdxType="string"==typeof e?e:n,b[1]=i;for(var l=2;l<c;l++)b[l]=a[l];return r.a.createElement.apply(null,b)}return r.a.createElement.apply(null,a)}O.displayName="MDXCreateElement"},192:function(e,t,a){"use strict";a.d(t,"a",(function(){return b}));var n=a(0),r=a.n(n),c=a(189);a(55);function b(){const[e,t]=Object(n.useState)(!1);return r.a.createElement("div",{className:"furion-join-group"},e&&r.a.createElement("img",{src:Object(c.a)("img/dotnetchina2.jpg")}),r.a.createElement("button",{onClick:()=>t(!e)},"\u52a0\u5165QQ\u4ea4\u6d41\u7fa4"))}},97:function(e,t,a){"use strict";a.r(t),a.d(t,"frontMatter",(function(){return i})),a.d(t,"metadata",(function(){return o})),a.d(t,"toc",(function(){return l})),a.d(t,"default",(function(){return m}));var n=a(3),r=a(7),c=(a(0),a(191)),b=a(192),i={id:"template",title:"2.2 \u5b98\u65b9\u811a\u624b\u67b6",sidebar_label:"2.2 \u5b98\u65b9\u811a\u624b\u67b6"},o={unversionedId:"template",id:"template",isDocsHomePage:!1,title:"2.2 \u5b98\u65b9\u811a\u624b\u67b6",description:"\u811a\u624b\u67b6\u4e0d\u662f\u5b89\u88c5\u5728\u9879\u76ee\u4e2d\u7684\uff01 \u800c\u662f\u901a\u8fc7 CMD \u6216 PowerShell \u5b89\u88c5\u5230\u64cd\u4f5c\u7cfb\u7edf\u4e2d\u7684\u3002",source:"@site/docs\\template.mdx",slug:"/template",permalink:"/docs/template",editUrl:"https://gitee.com/monksoul/Furion/tree/master/handbook/docs/template.mdx",version:"current",lastUpdatedBy:"\u767e\u5c0f\u50e7",lastUpdatedAt:1612020493,sidebar_label:"2.2 \u5b98\u65b9\u811a\u624b\u67b6",sidebar:"docs",previous:{title:"2.1 \u4e00\u5206\u949f\u4e0a\u624b",permalink:"/docs/get-start"},next:{title:"2.3 \u6846\u67b6\u9879\u76ee\u5f15\u7528",permalink:"/docs/reference"}},l=[{value:"2.2.1 \u811a\u624b\u67b6",id:"221-\u811a\u624b\u67b6",children:[]},{value:"2.2.2 \u5b89\u88c5\u811a\u624b\u67b6",id:"222-\u5b89\u88c5\u811a\u624b\u67b6",children:[]},{value:"2.2.3 \u4f7f\u7528\u811a\u624b\u67b6",id:"223-\u4f7f\u7528\u811a\u624b\u67b6",children:[]},{value:"2.2.4 \u811a\u624b\u67b6\u66f4\u65b0",id:"224-\u811a\u624b\u67b6\u66f4\u65b0",children:[]},{value:"2.2.5 \u53cd\u9988\u4e0e\u5efa\u8bae",id:"225-\u53cd\u9988\u4e0e\u5efa\u8bae",children:[]}],p={toc:l};function m(e){var t=e.components,a=Object(r.a)(e,["components"]);return Object(c.b)("wrapper",Object(n.a)({},p,a,{components:t,mdxType:"MDXLayout"}),Object(c.b)(b.a,{mdxType:"JoinGroup"}),Object(c.b)("div",{className:"admonition admonition-important alert alert--info"},Object(c.b)("div",Object(n.a)({parentName:"div"},{className:"admonition-heading"}),Object(c.b)("h5",{parentName:"div"},Object(c.b)("span",Object(n.a)({parentName:"h5"},{className:"admonition-icon"}),Object(c.b)("svg",Object(n.a)({parentName:"span"},{xmlns:"http://www.w3.org/2000/svg",width:"14",height:"16",viewBox:"0 0 14 16"}),Object(c.b)("path",Object(n.a)({parentName:"svg"},{fillRule:"evenodd",d:"M7 2.3c3.14 0 5.7 2.56 5.7 5.7s-2.56 5.7-5.7 5.7A5.71 5.71 0 0 1 1.3 8c0-3.14 2.56-5.7 5.7-5.7zM7 1C3.14 1 0 4.14 0 8s3.14 7 7 7 7-3.14 7-7-3.14-7-7-7zm1 3H6v5h2V4zm0 6H6v2h2v-2z"})))),"\u7279\u522b\u8bf4\u660e")),Object(c.b)("div",Object(n.a)({parentName:"div"},{className:"admonition-content"}),Object(c.b)("p",{parentName:"div"},Object(c.b)("strong",{parentName:"p"},"\u811a\u624b\u67b6\u4e0d\u662f\u5b89\u88c5\u5728\u9879\u76ee\u4e2d\u7684\uff01")," \u800c\u662f\u901a\u8fc7 ",Object(c.b)("inlineCode",{parentName:"p"},"CMD")," \u6216 ",Object(c.b)("inlineCode",{parentName:"p"},"PowerShell")," \u5b89\u88c5\u5230\u64cd\u4f5c\u7cfb\u7edf\u4e2d\u7684\u3002"))),Object(c.b)("h2",{id:"221-\u811a\u624b\u67b6"},"2.2.1 \u811a\u624b\u67b6"),Object(c.b)("p",null,Object(c.b)("inlineCode",{parentName:"p"},"Furion")," \u5b98\u65b9\u63d0\u4f9b\u4e86\u591a\u79cd ",Object(c.b)("inlineCode",{parentName:"p"},"Web")," \u5e94\u7528\u7c7b\u578b\u7684\u811a\u624b\u67b6\uff0c\u65b9\u4fbf\u5927\u5bb6\u5feb\u901f\u521b\u5efa\u591a\u5c42\u67b6\u6784\u9879\u76ee\u3002\u76ee\u524d\u652f\u6301\u4ee5\u4e0b\u5e94\u7528\u811a\u624b\u67b6\uff1a"),Object(c.b)("table",null,Object(c.b)("thead",{parentName:"table"},Object(c.b)("tr",{parentName:"thead"},Object(c.b)("th",Object(n.a)({parentName:"tr"},{align:"center"}),"\u6a21\u677f\u7c7b\u578b"),Object(c.b)("th",Object(n.a)({parentName:"tr"},{align:null}),"\u540d\u79f0"),Object(c.b)("th",Object(n.a)({parentName:"tr"},{align:null}),"\u7248\u672c"),Object(c.b)("th",Object(n.a)({parentName:"tr"},{align:null}),"\u5173\u952e\u8bcd"),Object(c.b)("th",Object(n.a)({parentName:"tr"},{align:null}),"\u63cf\u8ff0"))),Object(c.b)("tbody",{parentName:"table"},Object(c.b)("tr",{parentName:"tbody"},Object(c.b)("td",Object(n.a)({parentName:"tr"},{align:"center"}),Object(c.b)("a",Object(n.a)({parentName:"td"},{href:"https://www.nuget.org/packages/Furion.Template.Mvc/"}),Object(c.b)("img",Object(n.a)({parentName:"a"},{src:"https://shields.io/badge/-Nuget-yellow?cacheSeconds=604800",alt:"nuget"})))),Object(c.b)("td",Object(n.a)({parentName:"tr"},{align:null}),"Furion.Template.Mvc"),Object(c.b)("td",Object(n.a)({parentName:"tr"},{align:null}),Object(c.b)("a",Object(n.a)({parentName:"td"},{href:"https://www.nuget.org/packages/Furion.Template.Mvc/"}),Object(c.b)("img",Object(n.a)({parentName:"a"},{src:"https://img.shields.io/nuget/v/Furion.Template.Mvc.svg?cacheSeconds=10800",alt:"nuget"})))),Object(c.b)("td",Object(n.a)({parentName:"tr"},{align:null}),"\ud83d\udc49 ",Object(c.b)("strong",{parentName:"td"},"furionmvc")),Object(c.b)("td",Object(n.a)({parentName:"tr"},{align:null}),"Mvc \u6a21\u677f")),Object(c.b)("tr",{parentName:"tbody"},Object(c.b)("td",Object(n.a)({parentName:"tr"},{align:"center"}),Object(c.b)("a",Object(n.a)({parentName:"td"},{href:"https://www.nuget.org/packages/Furion.Template.Api/"}),Object(c.b)("img",Object(n.a)({parentName:"a"},{src:"https://shields.io/badge/-Nuget-yellow?cacheSeconds=604800",alt:"nuget"})))),Object(c.b)("td",Object(n.a)({parentName:"tr"},{align:null}),"Furion.Template.Api"),Object(c.b)("td",Object(n.a)({parentName:"tr"},{align:null}),Object(c.b)("a",Object(n.a)({parentName:"td"},{href:"https://www.nuget.org/packages/Furion.Template.Api/"}),Object(c.b)("img",Object(n.a)({parentName:"a"},{src:"https://img.shields.io/nuget/v/Furion.Template.Api.svg?cacheSeconds=10800",alt:"nuget"})))),Object(c.b)("td",Object(n.a)({parentName:"tr"},{align:null}),"\ud83d\udc49 ",Object(c.b)("strong",{parentName:"td"},"furionapi")),Object(c.b)("td",Object(n.a)({parentName:"tr"},{align:null}),"WebApi \u6a21\u677f")),Object(c.b)("tr",{parentName:"tbody"},Object(c.b)("td",Object(n.a)({parentName:"tr"},{align:"center"}),Object(c.b)("a",Object(n.a)({parentName:"td"},{href:"https://www.nuget.org/packages/Furion.Template.App/"}),Object(c.b)("img",Object(n.a)({parentName:"a"},{src:"https://shields.io/badge/-Nuget-yellow?cacheSeconds=604800",alt:"nuget"})))),Object(c.b)("td",Object(n.a)({parentName:"tr"},{align:null}),"Furion.Template.App"),Object(c.b)("td",Object(n.a)({parentName:"tr"},{align:null}),Object(c.b)("a",Object(n.a)({parentName:"td"},{href:"https://www.nuget.org/packages/Furion.Template.App/"}),Object(c.b)("img",Object(n.a)({parentName:"a"},{src:"https://img.shields.io/nuget/v/Furion.Template.App.svg?cacheSeconds=10800",alt:"nuget"})))),Object(c.b)("td",Object(n.a)({parentName:"tr"},{align:null}),"\ud83d\udc49 ",Object(c.b)("strong",{parentName:"td"},"furionapp")),Object(c.b)("td",Object(n.a)({parentName:"tr"},{align:null}),"Mvc/WebApi \u6a21\u677f")),Object(c.b)("tr",{parentName:"tbody"},Object(c.b)("td",Object(n.a)({parentName:"tr"},{align:"center"}),Object(c.b)("a",Object(n.a)({parentName:"td"},{href:"https://www.nuget.org/packages/Furion.Template.Razor/"}),Object(c.b)("img",Object(n.a)({parentName:"a"},{src:"https://shields.io/badge/-Nuget-yellow?cacheSeconds=604800",alt:"nuget"})))),Object(c.b)("td",Object(n.a)({parentName:"tr"},{align:null}),"Furion.Template.Razor"),Object(c.b)("td",Object(n.a)({parentName:"tr"},{align:null}),Object(c.b)("a",Object(n.a)({parentName:"td"},{href:"https://www.nuget.org/packages/Furion.Template.Razor/"}),Object(c.b)("img",Object(n.a)({parentName:"a"},{src:"https://img.shields.io/nuget/v/Furion.Template.Razor.svg?cacheSeconds=10800",alt:"nuget"})))),Object(c.b)("td",Object(n.a)({parentName:"tr"},{align:null}),"\ud83d\udc49 ",Object(c.b)("strong",{parentName:"td"},"furionrazor")),Object(c.b)("td",Object(n.a)({parentName:"tr"},{align:null}),"RazorPages \u6a21\u677f")),Object(c.b)("tr",{parentName:"tbody"},Object(c.b)("td",Object(n.a)({parentName:"tr"},{align:"center"}),Object(c.b)("a",Object(n.a)({parentName:"td"},{href:"https://www.nuget.org/packages/Furion.Template.RazorWithWebApi/"}),Object(c.b)("img",Object(n.a)({parentName:"a"},{src:"https://shields.io/badge/-Nuget-yellow?cacheSeconds=604800",alt:"nuget"})))),Object(c.b)("td",Object(n.a)({parentName:"tr"},{align:null}),"Furion.Template.RazorWithWebApi"),Object(c.b)("td",Object(n.a)({parentName:"tr"},{align:null}),Object(c.b)("a",Object(n.a)({parentName:"td"},{href:"https://www.nuget.org/packages/Furion.Template.RazorWithWebApi/"}),Object(c.b)("img",Object(n.a)({parentName:"a"},{src:"https://img.shields.io/nuget/v/Furion.Template.RazorWithWebApi.svg?cacheSeconds=10800",alt:"nuget"})))),Object(c.b)("td",Object(n.a)({parentName:"tr"},{align:null}),"\ud83d\udc49 ",Object(c.b)("strong",{parentName:"td"},"furionrazorapi")),Object(c.b)("td",Object(n.a)({parentName:"tr"},{align:null}),"RazorPages/WebApi \u6a21\u677f")),Object(c.b)("tr",{parentName:"tbody"},Object(c.b)("td",Object(n.a)({parentName:"tr"},{align:"center"}),Object(c.b)("a",Object(n.a)({parentName:"td"},{href:"https://www.nuget.org/packages/Furion.Template.Blazor/"}),Object(c.b)("img",Object(n.a)({parentName:"a"},{src:"https://shields.io/badge/-Nuget-yellow?cacheSeconds=604800",alt:"nuget"})))),Object(c.b)("td",Object(n.a)({parentName:"tr"},{align:null}),"Furion.Template.Blazor"),Object(c.b)("td",Object(n.a)({parentName:"tr"},{align:null}),Object(c.b)("a",Object(n.a)({parentName:"td"},{href:"https://www.nuget.org/packages/Furion.Template.Blazor/"}),Object(c.b)("img",Object(n.a)({parentName:"a"},{src:"https://img.shields.io/nuget/v/Furion.Template.Blazor.svg?cacheSeconds=10800",alt:"nuget"})))),Object(c.b)("td",Object(n.a)({parentName:"tr"},{align:null}),"\ud83d\udc49 ",Object(c.b)("strong",{parentName:"td"},"furionblazor")),Object(c.b)("td",Object(n.a)({parentName:"tr"},{align:null}),"Blazor \u6a21\u677f")),Object(c.b)("tr",{parentName:"tbody"},Object(c.b)("td",Object(n.a)({parentName:"tr"},{align:"center"}),Object(c.b)("a",Object(n.a)({parentName:"td"},{href:"https://www.nuget.org/packages/Furion.Template.BlazorWithWebApi/"}),Object(c.b)("img",Object(n.a)({parentName:"a"},{src:"https://shields.io/badge/-Nuget-yellow?cacheSeconds=604800",alt:"nuget"})))),Object(c.b)("td",Object(n.a)({parentName:"tr"},{align:null}),"Furion.Template.BlazorWithWebApi"),Object(c.b)("td",Object(n.a)({parentName:"tr"},{align:null}),Object(c.b)("a",Object(n.a)({parentName:"td"},{href:"https://www.nuget.org/packages/Furion.Template.BlazorWithWebApi/"}),Object(c.b)("img",Object(n.a)({parentName:"a"},{src:"https://img.shields.io/nuget/v/Furion.Template.BlazorWithWebApi.svg?cacheSeconds=10800",alt:"nuget"})))),Object(c.b)("td",Object(n.a)({parentName:"tr"},{align:null}),"\ud83d\udc49 ",Object(c.b)("strong",{parentName:"td"},"furionblazorapi")),Object(c.b)("td",Object(n.a)({parentName:"tr"},{align:null}),"Blazor/WebApi \u6a21\u677f")))),Object(c.b)("h2",{id:"222-\u5b89\u88c5\u811a\u624b\u67b6"},"2.2.2 \u5b89\u88c5\u811a\u624b\u67b6"),Object(c.b)("p",null,"\u6253\u5f00 ",Object(c.b)("inlineCode",{parentName:"p"},"CMD")," \u6216 ",Object(c.b)("inlineCode",{parentName:"p"},"Powershell")," \u6267\u884c\u6a21\u677f\u5b89\u88c5\u547d\u4ee4\uff1a"),Object(c.b)("pre",null,Object(c.b)("code",Object(n.a)({parentName:"pre"},{className:"language-bash"}),"dotnet new --install Furion.Template.Mvc::1.11.0\n")),Object(c.b)("h2",{id:"223-\u4f7f\u7528\u811a\u624b\u67b6"},"2.2.3 \u4f7f\u7528\u811a\u624b\u67b6"),Object(c.b)("pre",null,Object(c.b)("code",Object(n.a)({parentName:"pre"},{className:"language-bash"}),"dotnet new furionmvc -n \u9879\u76ee\u540d\u79f0\n")),Object(c.b)("p",null,"\u8fd9\u6837\u5c31\u53ef\u4ee5\u751f\u6210\u9879\u76ee\u4ee3\u7801\u4e86\uff0c",Object(c.b)("strong",{parentName:"p"},"\u751f\u6210\u4e4b\u540e\u63a8\u8350\u5c06\u6240\u6709\u7684 ",Object(c.b)("inlineCode",{parentName:"strong"},"nuget")," \u5305\u66f4\u65b0\u5230\u6700\u65b0\u7248\u672c\u3002")),Object(c.b)("div",{className:"admonition admonition-important alert alert--info"},Object(c.b)("div",Object(n.a)({parentName:"div"},{className:"admonition-heading"}),Object(c.b)("h5",{parentName:"div"},Object(c.b)("span",Object(n.a)({parentName:"h5"},{className:"admonition-icon"}),Object(c.b)("svg",Object(n.a)({parentName:"span"},{xmlns:"http://www.w3.org/2000/svg",width:"14",height:"16",viewBox:"0 0 14 16"}),Object(c.b)("path",Object(n.a)({parentName:"svg"},{fillRule:"evenodd",d:"M7 2.3c3.14 0 5.7 2.56 5.7 5.7s-2.56 5.7-5.7 5.7A5.71 5.71 0 0 1 1.3 8c0-3.14 2.56-5.7 5.7-5.7zM7 1C3.14 1 0 4.14 0 8s3.14 7 7 7 7-3.14 7-7-3.14-7-7-7zm1 3H6v5h2V4zm0 6H6v2h2v-2z"})))),"\u7279\u522b\u63d0\u9192")),Object(c.b)("div",Object(n.a)({parentName:"div"},{className:"admonition-content"}),Object(c.b)("p",{parentName:"div"},Object(c.b)("inlineCode",{parentName:"p"},"furionmvc")," \u5bf9\u5e94\u7684\u662f\u4e0a\u9762\u5217\u8868\u7684 ",Object(c.b)("inlineCode",{parentName:"p"},"\u5173\u952e\u5b57"),"\uff0c\u6211\u4eec\u4e5f\u53ef\u4ee5\u901a\u8fc7 ",Object(c.b)("inlineCode",{parentName:"p"},"dotnet new --list")," \u67e5\u770b\u3002"),Object(c.b)("p",{parentName:"div"},"\u60f3\u4e86\u89e3\u66f4\u591a\u53ef\u4ee5\u4f7f\u7528 ",Object(c.b)("inlineCode",{parentName:"p"},"dotnet new \u5173\u952e\u5b57 --help")," \u67e5\u770b\u66f4\u591a\u53c2\u6570\u3002"))),Object(c.b)("h2",{id:"224-\u811a\u624b\u67b6\u66f4\u65b0"},"2.2.4 \u811a\u624b\u67b6\u66f4\u65b0"),Object(c.b)("p",null,"\u53ea\u9700\u8981\u91cd\u65b0\u5b89\u88c5\u6700\u65b0\u7248\u66ff\u6362\u5373\u53ef\uff0c\u5982\uff1a"),Object(c.b)("pre",null,Object(c.b)("code",Object(n.a)({parentName:"pre"},{className:"language-bash"}),"dotnet new --install Furion.Template.Mvc::1.x.x\n")),Object(c.b)("p",null,"\u4e0d\u5e26\u7248\u672c\u53f7\u603b\u662f\u5b89\u88c5\u6700\u65b0\u7684\u7248\u672c\u3002"),Object(c.b)("h2",{id:"225-\u53cd\u9988\u4e0e\u5efa\u8bae"},"2.2.5 \u53cd\u9988\u4e0e\u5efa\u8bae"),Object(c.b)("div",{className:"admonition admonition-note alert alert--secondary"},Object(c.b)("div",Object(n.a)({parentName:"div"},{className:"admonition-heading"}),Object(c.b)("h5",{parentName:"div"},Object(c.b)("span",Object(n.a)({parentName:"h5"},{className:"admonition-icon"}),Object(c.b)("svg",Object(n.a)({parentName:"span"},{xmlns:"http://www.w3.org/2000/svg",width:"14",height:"16",viewBox:"0 0 14 16"}),Object(c.b)("path",Object(n.a)({parentName:"svg"},{fillRule:"evenodd",d:"M6.3 5.69a.942.942 0 0 1-.28-.7c0-.28.09-.52.28-.7.19-.18.42-.28.7-.28.28 0 .52.09.7.28.18.19.28.42.28.7 0 .28-.09.52-.28.7a1 1 0 0 1-.7.3c-.28 0-.52-.11-.7-.3zM8 7.99c-.02-.25-.11-.48-.31-.69-.2-.19-.42-.3-.69-.31H6c-.27.02-.48.13-.69.31-.2.2-.3.44-.31.69h1v3c.02.27.11.5.31.69.2.2.42.31.69.31h1c.27 0 .48-.11.69-.31.2-.19.3-.42.31-.69H8V7.98v.01zM7 2.3c-3.14 0-5.7 2.54-5.7 5.68 0 3.14 2.56 5.7 5.7 5.7s5.7-2.55 5.7-5.7c0-3.15-2.56-5.69-5.7-5.69v.01zM7 .98c3.86 0 7 3.14 7 7s-3.14 7-7 7-7-3.12-7-7 3.14-7 7-7z"})))),"\u4e0e\u6211\u4eec\u4ea4\u6d41")),Object(c.b)("div",Object(n.a)({parentName:"div"},{className:"admonition-content"}),Object(c.b)("p",{parentName:"div"},"\u7ed9 Furion \u63d0 ",Object(c.b)("a",Object(n.a)({parentName:"p"},{href:"https://gitee.com/monksoul/Furion/issues/new?issue"}),"Issue"),"\u3002"))),Object(c.b)("hr",null),Object(c.b)("div",{className:"admonition admonition-note alert alert--secondary"},Object(c.b)("div",Object(n.a)({parentName:"div"},{className:"admonition-heading"}),Object(c.b)("h5",{parentName:"div"},Object(c.b)("span",Object(n.a)({parentName:"h5"},{className:"admonition-icon"}),Object(c.b)("svg",Object(n.a)({parentName:"span"},{xmlns:"http://www.w3.org/2000/svg",width:"14",height:"16",viewBox:"0 0 14 16"}),Object(c.b)("path",Object(n.a)({parentName:"svg"},{fillRule:"evenodd",d:"M6.3 5.69a.942.942 0 0 1-.28-.7c0-.28.09-.52.28-.7.19-.18.42-.28.7-.28.28 0 .52.09.7.28.18.19.28.42.28.7 0 .28-.09.52-.28.7a1 1 0 0 1-.7.3c-.28 0-.52-.11-.7-.3zM8 7.99c-.02-.25-.11-.48-.31-.69-.2-.19-.42-.3-.69-.31H6c-.27.02-.48.13-.69.31-.2.2-.3.44-.31.69h1v3c.02.27.11.5.31.69.2.2.42.31.69.31h1c.27 0 .48-.11.69-.31.2-.19.3-.42.31-.69H8V7.98v.01zM7 2.3c-3.14 0-5.7 2.54-5.7 5.68 0 3.14 2.56 5.7 5.7 5.7s5.7-2.55 5.7-5.7c0-3.15-2.56-5.69-5.7-5.69v.01zM7 .98c3.86 0 7 3.14 7 7s-3.14 7-7 7-7-3.12-7-7 3.14-7 7-7z"})))),"\u4e86\u89e3\u66f4\u591a")),Object(c.b)("div",Object(n.a)({parentName:"div"},{className:"admonition-content"}),Object(c.b)("p",{parentName:"div"},"\u60f3\u4e86\u89e3\u66f4\u591a ",Object(c.b)("inlineCode",{parentName:"p"},"\u6a21\u677f\u77e5\u8bc6")," \u77e5\u8bc6\u53ef\u67e5\u9605 ",Object(c.b)("a",Object(n.a)({parentName:"p"},{href:"https://docs.microsoft.com/zh-cn/dotnet/core/tools/dotnet-new"}),"dotnet-new \u6a21\u677f")," \u7ae0\u8282\u3002"))))}m.isMDXComponent=!0}}]);