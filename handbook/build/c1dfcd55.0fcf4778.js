(window.webpackJsonp=window.webpackJsonp||[]).push([[80],{155:function(e,t,n){"use strict";n.r(t),n.d(t,"frontMatter",(function(){return o})),n.d(t,"metadata",(function(){return b})),n.d(t,"toc",(function(){return l})),n.d(t,"default",(function(){return d}));var a=n(3),r=n(7),c=(n(0),n(191)),i=(n(189),n(192)),o={id:"deploy-docker",title:"27.2 \u5728 Docker \u90e8\u7f72",sidebar_label:"27.2 \u5728 Docker \u90e8\u7f72"},b={unversionedId:"deploy-docker",id:"deploy-docker",isDocsHomePage:!1,title:"27.2 \u5728 Docker \u90e8\u7f72",description:"27.2.1 \u5173\u4e8e Docker \u90e8\u7f72",source:"@site/docs\\deploy-docker.mdx",slug:"/deploy-docker",permalink:"/furion/docs/deploy-docker",editUrl:"https://gitee.com/monksoul/Furion/tree/master/handbook/docs/deploy-docker.mdx",version:"current",lastUpdatedBy:"\u767e\u5c0f\u50e7",lastUpdatedAt:1612020493,sidebar_label:"27.2 \u5728 Docker \u90e8\u7f72",sidebar:"docs",previous:{title:"27.1 \u5728 IIS \u90e8\u7f72",permalink:"/furion/docs/deploy-iis"},next:{title:"27.3 \u5728 Nginx \u90e8\u7f72",permalink:"/furion/docs/deploy-nginx"}},l=[{value:"27.2.1 \u5173\u4e8e <code>Docker</code> \u90e8\u7f72",id:"2721-\u5173\u4e8e-docker-\u90e8\u7f72",children:[]},{value:"27.2.2 \u4e24\u79cd\u65b9\u5f0f\u6784\u5efa",id:"2722-\u4e24\u79cd\u65b9\u5f0f\u6784\u5efa",children:[{value:"27.2.2.1 \u53d1\u5e03\u540e\u6784\u5efa",id:"27221-\u53d1\u5e03\u540e\u6784\u5efa",children:[]},{value:"27.2.2.2 \u7f16\u8bd1+\u6784\u5efa+\u53d1\u5e03",id:"27222-\u7f16\u8bd1\u6784\u5efa\u53d1\u5e03",children:[]}]},{value:"27.2.3 \u53cd\u9988\u4e0e\u5efa\u8bae",id:"2723-\u53cd\u9988\u4e0e\u5efa\u8bae",children:[]}],p={toc:l};function d(e){var t=e.components,n=Object(r.a)(e,["components"]);return Object(c.b)("wrapper",Object(a.a)({},p,n,{components:t,mdxType:"MDXLayout"}),Object(c.b)(i.a,{mdxType:"JoinGroup"}),Object(c.b)("h2",{id:"2721-\u5173\u4e8e-docker-\u90e8\u7f72"},"27.2.1 \u5173\u4e8e ",Object(c.b)("inlineCode",{parentName:"h2"},"Docker")," \u90e8\u7f72"),Object(c.b)("p",null,"\u5728 ",Object(c.b)("inlineCode",{parentName:"p"},"Docker")," \u4e2d\u90e8\u7f72\u7f51\u7ad9\u6709\u4e24\u79cd\u65b9\u5f0f\uff1a"),Object(c.b)("ul",null,Object(c.b)("li",{parentName:"ul"},Object(c.b)("inlineCode",{parentName:"li"},"\u53d1\u5e03\u540e\u6784\u5efa"),"\uff1a\u6b64\u65b9\u5f0f\u662f\u5148\u53d1\u5e03\u7f51\u7ad9\u540e\u5728\u518d\u6784\u5efa\u955c\u50cf\uff0c\u8fd9\u6837\u53ef\u4ee5\u51cf\u5c11\u4e0d\u5fc5\u8981\u7684\u6784\u5efa\u5c42\uff0c\u800c\u4e14\u8fd8\u80fd\u7f29\u51cf\u955c\u50cf\u5927\u5c0f\u3002",Object(c.b)("strong",{parentName:"li"},"\uff08\u63a8\u8350\uff09")),Object(c.b)("li",{parentName:"ul"},Object(c.b)("inlineCode",{parentName:"li"},"\u7f16\u8bd1+\u6784\u5efa+\u53d1\u5e03"),"\uff1a\u4e5f\u5c31\u662f\u8bf4\u5728 ",Object(c.b)("inlineCode",{parentName:"li"},"Dockerfile")," \u4e2d\u914d\u7f6e\u7f51\u7ad9\u4ece\u6784\u5efa\u5230\u53d1\u5e03\u7684\u5b8c\u6574\u8fc7\u7a0b\uff0c\u6b64\u65b9\u5f0f\u4f1a\u901f\u5ea6\u6162\uff0c\u800c\u4e14\u4f1a\u4ea7\u751f\u5197\u4f59\u5c42\uff0c\u589e\u52a0\u955c\u50cf\u5927\u5c0f\u3002")),Object(c.b)("h2",{id:"2722-\u4e24\u79cd\u65b9\u5f0f\u6784\u5efa"},"27.2.2 \u4e24\u79cd\u65b9\u5f0f\u6784\u5efa"),Object(c.b)("h3",{id:"27221-\u53d1\u5e03\u540e\u6784\u5efa"},"27.2.2.1 \u53d1\u5e03\u540e\u6784\u5efa"),Object(c.b)("ul",null,Object(c.b)("li",{parentName:"ul"},"\ud83d\udc49 \u53d1\u5e03\u7f51\u7ad9")),Object(c.b)("p",null,"\u9996\u5148\u5728 ",Object(c.b)("inlineCode",{parentName:"p"},"Visual Studio")," \u6216 ",Object(c.b)("inlineCode",{parentName:"p"},"dotnet cli")," \u4e2d\u53d1\u5e03\u7f51\u7ad9\uff0c\u53ef\u4ee5\u53c2\u8003 ",Object(c.b)("a",Object(a.a)({parentName:"p"},{href:"deploy-iis#2611-%E5%8F%91%E5%B8%83%E7%BD%91%E7%AB%99"}),"\u5728 IIS \u90e8\u7f72-\u53d1\u5e03\u7f51\u7ad9")),Object(c.b)("ul",null,Object(c.b)("li",{parentName:"ul"},"\ud83d\udc49 \u7f16\u5199 ",Object(c.b)("inlineCode",{parentName:"li"},"Dockerfile"))),Object(c.b)("pre",null,Object(c.b)("code",Object(a.a)({parentName:"pre"},{className:"language-bash"}),'FROM mcr.microsoft.com/dotnet/aspnet:5.0.1\nWORKDIR /app\nEXPOSE 80\nEXPOSE 443\n\nCOPY . .\nENTRYPOINT ["dotnet", "Furion.Web.Entry.dll"]\n')),Object(c.b)("ul",null,Object(c.b)("li",{parentName:"ul"},"\ud83d\udc49 \u5c06 ",Object(c.b)("inlineCode",{parentName:"li"},"Dockerfile")," \u6587\u4ef6\u62f7\u8d1d\u5230\u53d1\u5e03\u6839\u76ee\u5f55")),Object(c.b)("p",null,"\u5c06\u7f16\u5199\u597d\u7684 ",Object(c.b)("inlineCode",{parentName:"p"},"Dockerfile")," \u6587\u4ef6\uff08\u6ce8\u610f ",Object(c.b)("inlineCode",{parentName:"p"},"D")," \u5927\u5199\uff09\u62f7\u8d1d\u5230\u53d1\u5e03\u7f51\u7ad9\u7684\u6839\u76ee\u5f55\u4e0b\u3002"),Object(c.b)("ul",null,Object(c.b)("li",{parentName:"ul"},"\ud83d\udc49 \u6784\u5efa ",Object(c.b)("inlineCode",{parentName:"li"},"Docker")," \u955c\u50cf")),Object(c.b)("p",null,"\u5728\u7f51\u7ad9\u53d1\u5e03\u540e\u7684\u8def\u5f84\u6839\u76ee\u5f55\u4e0b\uff08\u5fc5\u987b\u542b ",Object(c.b)("inlineCode",{parentName:"p"},"Dockerfile"),"\uff09\u6253\u5f00 ",Object(c.b)("inlineCode",{parentName:"p"},"CMD/PowerShell")," \u53ea\u9700\u6784\u5efa\u547d\u4ee4\uff1a"),Object(c.b)("pre",null,Object(c.b)("code",Object(a.a)({parentName:"pre"},{className:"language-bash"}),"docker build -t \u7f51\u7ad9\u540d\u79f0:\u7f51\u7ad9\u7248\u672c\u53f7 .\n")),Object(c.b)("div",{className:"admonition admonition-important alert alert--info"},Object(c.b)("div",Object(a.a)({parentName:"div"},{className:"admonition-heading"}),Object(c.b)("h5",{parentName:"div"},Object(c.b)("span",Object(a.a)({parentName:"h5"},{className:"admonition-icon"}),Object(c.b)("svg",Object(a.a)({parentName:"span"},{xmlns:"http://www.w3.org/2000/svg",width:"14",height:"16",viewBox:"0 0 14 16"}),Object(c.b)("path",Object(a.a)({parentName:"svg"},{fillRule:"evenodd",d:"M7 2.3c3.14 0 5.7 2.56 5.7 5.7s-2.56 5.7-5.7 5.7A5.71 5.71 0 0 1 1.3 8c0-3.14 2.56-5.7 5.7-5.7zM7 1C3.14 1 0 4.14 0 8s3.14 7 7 7 7-3.14 7-7-3.14-7-7-7zm1 3H6v5h2V4zm0 6H6v2h2v-2z"})))),"\u7279\u522b\u6ce8\u610f")),Object(c.b)("div",Object(a.a)({parentName:"div"},{className:"admonition-content"}),Object(c.b)("p",{parentName:"div"},"\u540e\u7aef\u7684 ",Object(c.b)("inlineCode",{parentName:"p"},".")," \u4e0d\u80fd\u7701\u7565"))),Object(c.b)("ul",null,Object(c.b)("li",{parentName:"ul"},"\ud83d\udc49 \u542f\u52a8\u955c\u50cf")),Object(c.b)("pre",null,Object(c.b)("code",Object(a.a)({parentName:"pre"},{className:"language-bash"}),"docker run --name \u5bb9\u5668\u540d\u79f0 -p 5000:80 --restart=always -d \u7f51\u7ad9\u540d\u79f0:\u7f51\u7ad9\u7248\u672c\u53f7\n")),Object(c.b)("div",{className:"admonition admonition-tip alert alert--success"},Object(c.b)("div",Object(a.a)({parentName:"div"},{className:"admonition-heading"}),Object(c.b)("h5",{parentName:"div"},Object(c.b)("span",Object(a.a)({parentName:"h5"},{className:"admonition-icon"}),Object(c.b)("svg",Object(a.a)({parentName:"span"},{xmlns:"http://www.w3.org/2000/svg",width:"12",height:"16",viewBox:"0 0 12 16"}),Object(c.b)("path",Object(a.a)({parentName:"svg"},{fillRule:"evenodd",d:"M6.5 0C3.48 0 1 2.19 1 5c0 .92.55 2.25 1 3 1.34 2.25 1.78 2.78 2 4v1h5v-1c.22-1.22.66-1.75 2-4 .45-.75 1-2.08 1-3 0-2.81-2.48-5-5.5-5zm3.64 7.48c-.25.44-.47.8-.67 1.11-.86 1.41-1.25 2.06-1.45 3.23-.02.05-.02.11-.02.17H5c0-.06 0-.13-.02-.17-.2-1.17-.59-1.83-1.45-3.23-.2-.31-.42-.67-.67-1.11C2.44 6.78 2 5.65 2 5c0-2.2 2.02-4 4.5-4 1.22 0 2.36.42 3.22 1.19C10.55 2.94 11 3.94 11 5c0 .66-.44 1.78-.86 2.48zM4 14h5c-.23 1.14-1.3 2-2.5 2s-2.27-.86-2.5-2z"})))),"\u53d1\u5e03\u5230 ",Object(c.b)("inlineCode",{parentName:"h5"},"hub.docker.com"))),Object(c.b)("div",Object(a.a)({parentName:"div"},{className:"admonition-content"}),Object(c.b)("p",{parentName:"div"},"\u5982\u679c\u9700\u8981\u5c06\u8be5\u7f51\u7ad9\u7684\u955c\u50cf\u516c\u5f00\u51fa\u53bb\uff0c\u90a3\u4e48\u53ef\u4ee5\u53d1\u5e03\u5230 ",Object(c.b)("inlineCode",{parentName:"p"},"hub.docker.com")," \u4e2d\u3002\u53d1\u5e03\u6b65\u9aa4\u5982\u4e0b\uff1a"),Object(c.b)("ul",{parentName:"div"},Object(c.b)("li",{parentName:"ul"},"\ud83d\udc49 \u4e3a\u955c\u50cf\u6253 ",Object(c.b)("inlineCode",{parentName:"li"},"tag")," \u6807\u7b7e")),Object(c.b)("pre",{parentName:"div"},Object(c.b)("code",Object(a.a)({parentName:"pre"},{className:"language-bash"}),"docker tag \u7f51\u7ad9\u540d\u79f0:\u7f51\u7ad9\u7248\u672c\u53f7 docker\u8d26\u53f7\u540d/\u7f51\u7ad9\u540d\u79f0:\u7f51\u7ad9\u7248\u672c\u53f7\n")),Object(c.b)("p",{parentName:"div"},"\u5982\uff1a"),Object(c.b)("pre",{parentName:"div"},Object(c.b)("code",Object(a.a)({parentName:"pre"},{className:"language-bash"}),"docker tag furion:v1.8.0 monksoul/furion:v1.8.0\n")),Object(c.b)("ul",{parentName:"div"},Object(c.b)("li",{parentName:"ul"},"\ud83d\udc49 \u767b\u5f55 ",Object(c.b)("inlineCode",{parentName:"li"},"docker"))),Object(c.b)("pre",{parentName:"div"},Object(c.b)("code",Object(a.a)({parentName:"pre"},{className:"language-bash"}),"docker login\n")),Object(c.b)("ul",{parentName:"div"},Object(c.b)("li",{parentName:"ul"},"\ud83d\udc49 \u63a8\u9001\u5230 ",Object(c.b)("inlineCode",{parentName:"li"},"hub.docker.com"))),Object(c.b)("pre",{parentName:"div"},Object(c.b)("code",Object(a.a)({parentName:"pre"},{className:"language-bash"}),"docker push docker\u8d26\u53f7\u540d/\u7f51\u7ad9\u540d\u79f0:\u7f51\u7ad9\u7248\u672c\u53f7\n")),Object(c.b)("p",{parentName:"div"},"\u5982\uff1a"),Object(c.b)("pre",{parentName:"div"},Object(c.b)("code",Object(a.a)({parentName:"pre"},{className:"language-bash"}),"docker push monksoul/furion:v1.8.0\n")))),Object(c.b)("h3",{id:"27222-\u7f16\u8bd1\u6784\u5efa\u53d1\u5e03"},"27.2.2.2 \u7f16\u8bd1+\u6784\u5efa+\u53d1\u5e03"),Object(c.b)("p",null,"\u6b64\u65b9\u5f0f\u53ef\u4ee5\u5077\u61d2\uff0c\u4f46\u662f\u4e0d\u592a\u63a8\u8350\uff0c\u4e0d\u8fc7\u5728\u67d0\u4e9b\u573a\u666f\u4e0b\u975e\u5e38\u6709\u7528\uff0c\u5c31\u662f\u96c6\u6210 ",Object(c.b)("inlineCode",{parentName:"p"},"Devops")," \u5de5\u5177\u94fe\u53ef\u4ee5\u505a\u5230\u4e00\u6b65\u5230\u4f4d\u3002"),Object(c.b)("ul",null,Object(c.b)("li",{parentName:"ul"},"\ud83d\udc49 \u7f16\u5199 ",Object(c.b)("inlineCode",{parentName:"li"},"Dockerfile"))),Object(c.b)("p",null,"\u8fd9\u79cd\u65b9\u5f0f\u53ea\u9700\u8981\u628a ",Object(c.b)("inlineCode",{parentName:"p"},"Dockerfile")," \u5185\u5bb9\u66ff\u6362\u6210\u4ee5\u4e0b\u5373\u53ef\uff1a"),Object(c.b)("pre",null,Object(c.b)("code",Object(a.a)({parentName:"pre"},{className:"language-bash",metastring:"{6}","{6}":!0}),'FROM mcr.microsoft.com/dotnet/sdk:5.0.1 AS build\nWORKDIR /source\n\n# Download Source\nRUN git init\nRUN git remote add -t master -m master origin \u4f60\u7684\u6e90\u7801Git\u5730\u5740\nRUN git config core.sparseCheckout true\nRUN echo samples >> .git/info/sparse-checkout\nRUN git pull --depth 1 origin main\n\n# Restore And Publish\nWORKDIR /source/samples\nRUN dotnet restore\nRUN dotnet publish -c release -o /app --no-restore\n\n# Run\nFROM mcr.microsoft.com/dotnet/aspnet:5.0.1\nWORKDIR /app\nCOPY --from=build /app ./\nEXPOSE 80\nEXPOSE 443\nENTRYPOINT ["dotnet", "Furion.Web.Entry.dll"]\n')),Object(c.b)("ul",null,Object(c.b)("li",{parentName:"ul"},"\ud83d\udc49 \u5728 ",Object(c.b)("inlineCode",{parentName:"li"},"Dockerfile")," \u6240\u5728\u8def\u5f84\u6784\u5efa")),Object(c.b)("p",null,"\u63a5\u4e0b\u6765\u7684\u6b65\u9aa4\u548c\u4e0a\u8ff0\u6b65\u9aa4\u4e00\u81f4\uff0c\u4e0d\u518d\u91cd\u590d\u7f16\u5199"),Object(c.b)("h2",{id:"2723-\u53cd\u9988\u4e0e\u5efa\u8bae"},"27.2.3 \u53cd\u9988\u4e0e\u5efa\u8bae"),Object(c.b)("div",{className:"admonition admonition-note alert alert--secondary"},Object(c.b)("div",Object(a.a)({parentName:"div"},{className:"admonition-heading"}),Object(c.b)("h5",{parentName:"div"},Object(c.b)("span",Object(a.a)({parentName:"h5"},{className:"admonition-icon"}),Object(c.b)("svg",Object(a.a)({parentName:"span"},{xmlns:"http://www.w3.org/2000/svg",width:"14",height:"16",viewBox:"0 0 14 16"}),Object(c.b)("path",Object(a.a)({parentName:"svg"},{fillRule:"evenodd",d:"M6.3 5.69a.942.942 0 0 1-.28-.7c0-.28.09-.52.28-.7.19-.18.42-.28.7-.28.28 0 .52.09.7.28.18.19.28.42.28.7 0 .28-.09.52-.28.7a1 1 0 0 1-.7.3c-.28 0-.52-.11-.7-.3zM8 7.99c-.02-.25-.11-.48-.31-.69-.2-.19-.42-.3-.69-.31H6c-.27.02-.48.13-.69.31-.2.2-.3.44-.31.69h1v3c.02.27.11.5.31.69.2.2.42.31.69.31h1c.27 0 .48-.11.69-.31.2-.19.3-.42.31-.69H8V7.98v.01zM7 2.3c-3.14 0-5.7 2.54-5.7 5.68 0 3.14 2.56 5.7 5.7 5.7s5.7-2.55 5.7-5.7c0-3.15-2.56-5.69-5.7-5.69v.01zM7 .98c3.86 0 7 3.14 7 7s-3.14 7-7 7-7-3.12-7-7 3.14-7 7-7z"})))),"\u4e0e\u6211\u4eec\u4ea4\u6d41")),Object(c.b)("div",Object(a.a)({parentName:"div"},{className:"admonition-content"}),Object(c.b)("p",{parentName:"div"},"\u7ed9 Furion \u63d0 ",Object(c.b)("a",Object(a.a)({parentName:"p"},{href:"https://gitee.com/monksoul/Furion/issues/new?issue"}),"Issue"),"\u3002"))))}d.isMDXComponent=!0},189:function(e,t,n){"use strict";n.d(t,"b",(function(){return c})),n.d(t,"a",(function(){return i}));var a=n(21),r=n(190);function c(){const{siteConfig:{baseUrl:e="/",url:t}={}}=Object(a.default)();return{withBaseUrl:(n,a)=>function(e,t,n,{forcePrependBaseUrl:a=!1,absolute:c=!1}={}){if(!n)return n;if(n.startsWith("#"))return n;if(Object(r.b)(n))return n;if(a)return t+n;const i=n.startsWith(t)?n:t+n.replace(/^\//,"");return c?e+i:i}(t,e,n,a)}}function i(e,t={}){const{withBaseUrl:n}=c();return n(e,t)}},190:function(e,t,n){"use strict";function a(e){return!0===/^(\w*:|\/\/)/.test(e)}function r(e){return void 0!==e&&!a(e)}n.d(t,"b",(function(){return a})),n.d(t,"a",(function(){return r}))},191:function(e,t,n){"use strict";n.d(t,"a",(function(){return d})),n.d(t,"b",(function(){return m}));var a=n(0),r=n.n(a);function c(e,t,n){return t in e?Object.defineProperty(e,t,{value:n,enumerable:!0,configurable:!0,writable:!0}):e[t]=n,e}function i(e,t){var n=Object.keys(e);if(Object.getOwnPropertySymbols){var a=Object.getOwnPropertySymbols(e);t&&(a=a.filter((function(t){return Object.getOwnPropertyDescriptor(e,t).enumerable}))),n.push.apply(n,a)}return n}function o(e){for(var t=1;t<arguments.length;t++){var n=null!=arguments[t]?arguments[t]:{};t%2?i(Object(n),!0).forEach((function(t){c(e,t,n[t])})):Object.getOwnPropertyDescriptors?Object.defineProperties(e,Object.getOwnPropertyDescriptors(n)):i(Object(n)).forEach((function(t){Object.defineProperty(e,t,Object.getOwnPropertyDescriptor(n,t))}))}return e}function b(e,t){if(null==e)return{};var n,a,r=function(e,t){if(null==e)return{};var n,a,r={},c=Object.keys(e);for(a=0;a<c.length;a++)n=c[a],t.indexOf(n)>=0||(r[n]=e[n]);return r}(e,t);if(Object.getOwnPropertySymbols){var c=Object.getOwnPropertySymbols(e);for(a=0;a<c.length;a++)n=c[a],t.indexOf(n)>=0||Object.prototype.propertyIsEnumerable.call(e,n)&&(r[n]=e[n])}return r}var l=r.a.createContext({}),p=function(e){var t=r.a.useContext(l),n=t;return e&&(n="function"==typeof e?e(t):o(o({},t),e)),n},d=function(e){var t=p(e.components);return r.a.createElement(l.Provider,{value:t},e.children)},s={inlineCode:"code",wrapper:function(e){var t=e.children;return r.a.createElement(r.a.Fragment,{},t)}},u=r.a.forwardRef((function(e,t){var n=e.components,a=e.mdxType,c=e.originalType,i=e.parentName,l=b(e,["components","mdxType","originalType","parentName"]),d=p(n),u=a,m=d["".concat(i,".").concat(u)]||d[u]||s[u]||c;return n?r.a.createElement(m,o(o({ref:t},l),{},{components:n})):r.a.createElement(m,o({ref:t},l))}));function m(e,t){var n=arguments,a=t&&t.mdxType;if("string"==typeof e||a){var c=n.length,i=new Array(c);i[0]=u;var o={};for(var b in t)hasOwnProperty.call(t,b)&&(o[b]=t[b]);o.originalType=e,o.mdxType="string"==typeof e?e:a,i[1]=o;for(var l=2;l<c;l++)i[l]=n[l];return r.a.createElement.apply(null,i)}return r.a.createElement.apply(null,n)}u.displayName="MDXCreateElement"},192:function(e,t,n){"use strict";n.d(t,"a",(function(){return i}));var a=n(0),r=n.n(a),c=n(189);n(55);function i(){const[e,t]=Object(a.useState)(!1);return r.a.createElement("div",{className:"furion-join-group"},e&&r.a.createElement("img",{src:Object(c.a)("img/dotnetchina2.jpg")}),r.a.createElement("button",{onClick:()=>t(!e)},"\u52a0\u5165QQ\u4ea4\u6d41\u7fa4"))}}}]);