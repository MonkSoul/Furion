"use strict";(self.webpackChunkfurion=self.webpackChunkfurion||[]).push([[1866],{3905:function(e,n,t){t.d(n,{Zo:function(){return m},kt:function(){return c}});var i=t(7294);function r(e,n,t){return n in e?Object.defineProperty(e,n,{value:t,enumerable:!0,configurable:!0,writable:!0}):e[n]=t,e}function o(e,n){var t=Object.keys(e);if(Object.getOwnPropertySymbols){var i=Object.getOwnPropertySymbols(e);n&&(i=i.filter((function(n){return Object.getOwnPropertyDescriptor(e,n).enumerable}))),t.push.apply(t,i)}return t}function a(e){for(var n=1;n<arguments.length;n++){var t=null!=arguments[n]?arguments[n]:{};n%2?o(Object(t),!0).forEach((function(n){r(e,n,t[n])})):Object.getOwnPropertyDescriptors?Object.defineProperties(e,Object.getOwnPropertyDescriptors(t)):o(Object(t)).forEach((function(n){Object.defineProperty(e,n,Object.getOwnPropertyDescriptor(t,n))}))}return e}function l(e,n){if(null==e)return{};var t,i,r=function(e,n){if(null==e)return{};var t,i,r={},o=Object.keys(e);for(i=0;i<o.length;i++)t=o[i],n.indexOf(t)>=0||(r[t]=e[t]);return r}(e,n);if(Object.getOwnPropertySymbols){var o=Object.getOwnPropertySymbols(e);for(i=0;i<o.length;i++)t=o[i],n.indexOf(t)>=0||Object.prototype.propertyIsEnumerable.call(e,t)&&(r[t]=e[t])}return r}var s=i.createContext({}),p=function(e){var n=i.useContext(s),t=n;return e&&(t="function"==typeof e?e(n):a(a({},n),e)),t},m=function(e){var n=p(e.components);return i.createElement(s.Provider,{value:n},e.children)},d={inlineCode:"code",wrapper:function(e){var n=e.children;return i.createElement(i.Fragment,{},n)}},u=i.forwardRef((function(e,n){var t=e.components,r=e.mdxType,o=e.originalType,s=e.parentName,m=l(e,["components","mdxType","originalType","parentName"]),u=p(t),c=r,k=u["".concat(s,".").concat(c)]||u[c]||d[c]||o;return t?i.createElement(k,a(a({ref:n},m),{},{components:t})):i.createElement(k,a({ref:n},m))}));function c(e,n){var t=arguments,r=n&&n.mdxType;if("string"==typeof e||r){var o=t.length,a=new Array(o);a[0]=u;var l={};for(var s in n)hasOwnProperty.call(n,s)&&(l[s]=n[s]);l.originalType=e,l.mdxType="string"==typeof e?e:r,a[1]=l;for(var p=2;p<o;p++)a[p]=t[p];return i.createElement.apply(null,a)}return i.createElement.apply(null,t)}u.displayName="MDXCreateElement"},8889:function(e,n,t){t.r(n),t.d(n,{assets:function(){return d},contentTitle:function(){return p},default:function(){return k},frontMatter:function(){return s},metadata:function(){return m},toc:function(){return u}});var i=t(3117),r=t(102),o=(t(7294),t(3905)),a=t(4996),l=["components"],s={id:"dbcontext-db-first",title:"9.19 \u6570\u636e\u5e93\u751f\u6210\u6a21\u578b",sidebar_label:"9.19 \u6570\u636e\u5e93\u751f\u6210\u6a21\u578b (Db First)"},p=void 0,m={unversionedId:"dbcontext-db-first",id:"dbcontext-db-first",title:"9.19 \u6570\u636e\u5e93\u751f\u6210\u6a21\u578b",description:"",source:"@site/docs/dbcontext-db-first.mdx",sourceDirName:".",slug:"/dbcontext-db-first",permalink:"/furion/docs/dbcontext-db-first",draft:!1,editUrl:"https://gitee.com/dotnetchina/Furion/tree/net6/handbook/docs/dbcontext-db-first.mdx",tags:[],version:"current",lastUpdatedBy:"MonkSoul",lastUpdatedAt:1653276586,formattedLastUpdatedAt:"May 23, 2022",frontMatter:{id:"dbcontext-db-first",title:"9.19 \u6570\u636e\u5e93\u751f\u6210\u6a21\u578b",sidebar_label:"9.19 \u6570\u636e\u5e93\u751f\u6210\u6a21\u578b (Db First)"},sidebar:"docs",previous:{title:"9.18 \u591a\u79cd\u6570\u636e\u5e93\u64cd\u4f5c \u2728",permalink:"/furion/docs/dbcontext-multi-database"},next:{title:"9.20 \u6a21\u578b\u751f\u6210\u6570\u636e\u5e93 (Code First)",permalink:"/furion/docs/dbcontext-code-first"}},d={},u=[{value:"9.19.1 \u6570\u636e\u5e93\u5f00\u53d1\u65b9\u5f0f",id:"9191-\u6570\u636e\u5e93\u5f00\u53d1\u65b9\u5f0f",level:2},{value:"9.19.2 \u64cd\u4f5c\u6307\u5357",id:"9192-\u64cd\u4f5c\u6307\u5357",level:2},{value:"9.19.2.1 \u6253\u5f00 <code>\u7a0b\u5e8f\u5305\u7ba1\u7406\u63a7\u5236\u53f0</code>",id:"91921-\u6253\u5f00-\u7a0b\u5e8f\u5305\u7ba1\u7406\u63a7\u5236\u53f0",level:3},{value:"9.19.2.2 \u5207\u6362\u9ed8\u8ba4\u9879\u76ee",id:"91922-\u5207\u6362\u9ed8\u8ba4\u9879\u76ee",level:3},{value:"9.19.2.3 \u8f93\u5165 <code>cli.ps1</code> \u547d\u4ee4",id:"91923-\u8f93\u5165-clips1-\u547d\u4ee4",level:3},{value:"9.19.3 \u754c\u9762\u64cd\u4f5c\u6a21\u5f0f",id:"9193-\u754c\u9762\u64cd\u4f5c\u6a21\u5f0f",level:2},{value:"9.19.3.1 \u542f\u52a8\u754c\u9762\u64cd\u4f5c",id:"91931-\u542f\u52a8\u754c\u9762\u64cd\u4f5c",level:3},{value:"9.19.3.2 \u52a0\u8f7d\u6570\u636e\u5e93\u8868",id:"91932-\u52a0\u8f7d\u6570\u636e\u5e93\u8868",level:3},{value:"9.19.3.3 \u9009\u62e9\u8868\u6216\u89c6\u56fe\u751f\u6210",id:"91933-\u9009\u62e9\u8868\u6216\u89c6\u56fe\u751f\u6210",level:3},{value:"9.19.3.4 \u9009\u62e9\u4fdd\u5b58\u76ee\u5f55",id:"91934-\u9009\u62e9\u4fdd\u5b58\u76ee\u5f55",level:3},{value:"9.19.3.5 \u751f\u6210\u6700\u7ec8\u5b9e\u4f53\u4ee3\u7801",id:"91935-\u751f\u6210\u6700\u7ec8\u5b9e\u4f53\u4ee3\u7801",level:3},{value:"9.19.4 \u547d\u4ee4\u53c2\u6570\u914d\u7f6e",id:"9194-\u547d\u4ee4\u53c2\u6570\u914d\u7f6e",level:2},{value:"9.19.5 \u53cd\u9988\u4e0e\u5efa\u8bae",id:"9195-\u53cd\u9988\u4e0e\u5efa\u8bae",level:2}],c={toc:u};function k(e){var n=e.components,t=(0,r.Z)(e,l);return(0,o.kt)("wrapper",(0,i.Z)({},c,t,{components:n,mdxType:"MDXLayout"}),(0,o.kt)("admonition",{title:"\u89c6\u9891\u6559\u7a0b",type:"tip"},(0,o.kt)("p",{parentName:"admonition"},(0,o.kt)("a",{parentName:"p",href:"https://www.bilibili.com/video/BV1Rt4y1W7oJ"},"https://www.bilibili.com/video/BV1Rt4y1W7oJ"))),(0,o.kt)("admonition",{title:"\u5173\u4e8e\u811a\u672c",type:"tip"},(0,o.kt)("p",{parentName:"admonition"},"\u5728\u9605\u8bfb\u4e0b\u9762\u6587\u6863\u4e4b\u524d\uff0c\u5fc5\u987b\u628a",(0,o.kt)("inlineCode",{parentName:"p"},"Furion")," \u6e90\u7801\u6587\u4ef6\u5939\u4e0b\u7684 ",(0,o.kt)("inlineCode",{parentName:"p"},"tools/cli.ps1")," \u6587\u4ef6\u62f7\u8d1d\u5230\u672c\u5730\u4e2d\u3002\u800c\u4e14 ",(0,o.kt)("inlineCode",{parentName:"p"},"Fur.Web.Entry")," \u5c42\u9700\u8981\u5b89\u88c5 ",(0,o.kt)("inlineCode",{parentName:"p"},"Microsoft.EntityFrameworkCore.Tools")," \u5305\u3002")),(0,o.kt)("admonition",{title:"\u7cfb\u7edf\u63d0\u793a\u7981\u6b62\u8fd0\u884c\u811a\u672c\u89e3\u51b3",type:"important"},(0,o.kt)("p",{parentName:"admonition"},"\u5982\u679c\u51fa\u73b0 ",(0,o.kt)("inlineCode",{parentName:"p"},"cli.ps1")," \u65e0\u6cd5\u8fd0\u884c\u7684\u60c5\u51b5\uff0c\u5982\u63d0\u793a\uff1a",(0,o.kt)("inlineCode",{parentName:"p"},"\u201c\u56e0\u4e3a\u5728\u6b64\u7cfb\u7edf\u4e0a\u7981\u6b62\u8fd0\u884c\u811a\u672c\u201d"),"\uff0c\u53ea\u9700\u8981\u6253\u5f00\u7cfb\u7edf\u7ba1\u7406\u5458 ",(0,o.kt)("inlineCode",{parentName:"p"},"CMD/Powershell")," \u6267\u884c\uff1a",(0,o.kt)("inlineCode",{parentName:"p"},"set-ExecutionPolicy RemoteSigned")," \u547d\u4ee4\u5e76\u6839\u636e\u64cd\u4f5c\u63d0\u793a\u8f93\u5165 ",(0,o.kt)("inlineCode",{parentName:"p"},"A")," \u5373\u53ef\u3002"),(0,o.kt)("p",{parentName:"admonition"},"\u4e4b\u540e\u91cd\u542f ",(0,o.kt)("inlineCode",{parentName:"p"},"Visual Studio")," \u5de5\u5177\u3002")),(0,o.kt)("h2",{id:"9191-\u6570\u636e\u5e93\u5f00\u53d1\u65b9\u5f0f"},"9.19.1 \u6570\u636e\u5e93\u5f00\u53d1\u65b9\u5f0f"),(0,o.kt)("p",null,(0,o.kt)("inlineCode",{parentName:"p"},"Furion")," \u63d0\u4f9b\u4e24\u79cd\u4e3b\u8981\u65b9\u6cd5\u6765 ",(0,o.kt)("strong",{parentName:"p"},"\u4fdd\u6301\u5b9e\u4f53\u6a21\u578b\u548c\u6570\u636e\u5e93\u67b6\u6784\u540c\u6b65"),"\u3002"),(0,o.kt)("p",null,"\u81f3\u4e8e\u6211\u4eec\u5e94\u8be5\u9009\u7528\u54ea\u4e2a\u65b9\u6cd5\uff0c\u8bf7\u786e\u5b9a\u4f60\u662f\u5e0c\u671b\u4ee5\u5b9e\u4f53\u6a21\u578b\u4e3a\u51c6\u8fd8\u662f\u4ee5\u6570\u636e\u5e93\u4e3a\u51c6\uff1a"),(0,o.kt)("ul",null,(0,o.kt)("li",{parentName:"ul"},(0,o.kt)("p",{parentName:"li"},"\u5982\u679c\u5e0c\u671b ",(0,o.kt)("strong",{parentName:"p"},"\u4ee5\u5b9e\u4f53\u6a21\u578b\u4e3a\u51c6"),"\uff0c\u8bf7\u4f7f\u7528\u6b63\u5411\u5de5\u7a0b\uff08Code First\uff09\u3002 \u5bf9\u5b9e\u4f53\u6a21\u578b\u8fdb\u884c\u66f4\u6539\u65f6\uff0c\u6b64\u65b9\u6cd5\u4f1a\u4ee5\u589e\u91cf\u65b9\u5f0f\u5c06\u76f8\u5e94\u67b6\u6784\u66f4\u6539\u5e94\u7528\u5230\u6570\u636e\u5e93\uff0c\u4ee5\u4f7f\u6570\u636e\u5e93\u4fdd\u6301\u4e0e\u5b9e\u4f53\u6a21\u578b\u517c\u5bb9\u3002")),(0,o.kt)("li",{parentName:"ul"},(0,o.kt)("p",{parentName:"li"},"\u5982\u679c\u5e0c\u671b ",(0,o.kt)("strong",{parentName:"p"},"\u4ee5\u6570\u636e\u5e93\u67b6\u6784\u4e3a\u51c6"),"\uff0c\u8bf7\u4f7f\u7528\u53cd\u5411\u5de5\u7a0b\uff08Database First\uff09\u3002 \u4f7f\u7528\u6b64\u65b9\u6cd5\uff0c\u53ef\u901a\u8fc7\u5c06\u6570\u636e\u5e93\u67b6\u6784\u53cd\u5411\u5de5\u7a0b\u5230\u5b9e\u4f53\u6a21\u578b\u6765\u751f\u6210\u76f8\u5e94\u7684\u5b9e\u4f53\u7c7b\u578b\u3002"))),(0,o.kt)("p",null,"\u672c\u7ae0\u8282\u662f ",(0,o.kt)("strong",{parentName:"p"},"\u53cd\u5411\u5de5\u7a0b\uff08Database First\uff09")," \u7684\u76f8\u5173\u5185\u5bb9\u3002"),(0,o.kt)("h2",{id:"9192-\u64cd\u4f5c\u6307\u5357"},"9.19.2 \u64cd\u4f5c\u6307\u5357"),(0,o.kt)("admonition",{title:"\u64cd\u4f5c\u4e4b\u524d\u6ce8\u610f\u4e8b\u9879",type:"important"},(0,o.kt)("p",{parentName:"admonition"},"\u76ee\u524d ",(0,o.kt)("inlineCode",{parentName:"p"},"Furion Tools")," \u751f\u6210\u5de5\u5177",(0,o.kt)("strong",{parentName:"p"},"\u9ed8\u8ba4\u4e0d\u652f\u6301\u4efb\u4f55\u6570\u636e\u5e93"),"\u751f\u6210\uff0c\u6240\u4ee5\u5982\u9700\u751f\u6210\u7279\u5b9a\u6570\u636e\u5e93\u7684\u4ee3\u7801\uff0c\u53ea\u9700\u8981\u5728 ",(0,o.kt)("inlineCode",{parentName:"p"},"Furion.EntityFrameworkCore.Core")," \u5b89\u88c5\u5bf9\u5e94\u7684\u6570\u636e\u5e93\u5305\u5373\u53ef\uff1a"),(0,o.kt)("p",{parentName:"admonition"},"\u5404\u4e2a\u6570\u636e\u5e93\u7684\u5305\u53ef\u67e5\u9605\uff1a",(0,o.kt)("a",{parentName:"p",href:"dbcontext-multi-database"},"\u591a\u6570\u636e\u5e93\u64cd\u4f5c-\u6570\u636e\u5e93\u63d0\u4f9b\u5668\u5bf9\u5e94\u5305")),(0,o.kt)("p",{parentName:"admonition"},"\u53e6\u5916\uff0c\u53ea\u6709 ",(0,o.kt)("inlineCode",{parentName:"p"},"SqlServer")," \u6570\u636e\u5e93\u652f\u6301\u53ef\u89c6\u5316 ",(0,o.kt)("inlineCode",{parentName:"p"},"GUI")," \u64cd\u4f5c\uff0c\u5176\u4ed6\u7684\u53ea\u80fd\u547d\u4ee4\u884c\u64cd\u4f5c\u3002")),(0,o.kt)("h3",{id:"91921-\u6253\u5f00-\u7a0b\u5e8f\u5305\u7ba1\u7406\u63a7\u5236\u53f0"},"9.19.2.1 \u6253\u5f00 ",(0,o.kt)("inlineCode",{parentName:"h3"},"\u7a0b\u5e8f\u5305\u7ba1\u7406\u63a7\u5236\u53f0")),(0,o.kt)("p",null,"\u6ce8\u610f\uff1a\u5f00\u59cb\u4e4b\u524d\u5148\u628a ",(0,o.kt)("inlineCode",{parentName:"p"},"Furion.Web.Entry")," \u8bbe\u4e3a\u542f\u52a8\u9879\u76ee\u3002"),(0,o.kt)("img",{src:(0,a.Z)("img/dbfirst1.png")}),(0,o.kt)("h3",{id:"91922-\u5207\u6362\u9ed8\u8ba4\u9879\u76ee"},"9.19.2.2 \u5207\u6362\u9ed8\u8ba4\u9879\u76ee"),(0,o.kt)("p",null,"\u5c06 ",(0,o.kt)("inlineCode",{parentName:"p"},"\u7a0b\u5e8f\u5305\u7ba1\u7406\u63a7\u5236\u53f0")," \u9ed8\u8ba4\u9879\u76ee\u8bbe\u7f6e\u4e3a ",(0,o.kt)("inlineCode",{parentName:"p"},"Furion.Core"),"\uff0c\u5982\u679c\u60a8\u662f\u5176\u4ed6\u540d\u5b57\uff0c\u5219\u5207\u6362\u5bf9\u5e94\u5373\u53ef\u3002"),(0,o.kt)("img",{src:(0,a.Z)("img/dbfirst2.png")}),(0,o.kt)("h3",{id:"91923-\u8f93\u5165-clips1-\u547d\u4ee4"},"9.19.2.3 \u8f93\u5165 ",(0,o.kt)("inlineCode",{parentName:"h3"},"cli.ps1")," \u547d\u4ee4"),(0,o.kt)("pre",null,(0,o.kt)("code",{parentName:"pre",className:"language-shell",metastring:"showLineNumbers",showLineNumbers:!0},"PM> Show-Command ../tools/cli.ps1\n")),(0,o.kt)("img",{src:(0,a.Z)("img/df10.png")}),(0,o.kt)("admonition",{title:"\u5168\u547d\u4ee4\u65b9\u5f0f\uff08\u63a8\u8350\uff09\u2764\ufe0f",type:"important"},(0,o.kt)("p",{parentName:"admonition"},"\u9664\u4e86\u91c7\u7528 ",(0,o.kt)("inlineCode",{parentName:"p"},"Show-Command")," \u65b9\u5f0f\u4ee5\u5916\uff0c\u8fd8\u53ef\u4ee5\u76f4\u63a5\u6267\u884c\u547d\u4ee4\uff0c\u5982\uff1a"),(0,o.kt)("pre",{parentName:"admonition"},(0,o.kt)("code",{parentName:"pre",className:"language-shell",metastring:"showLineNumbers",showLineNumbers:!0},'&"./tools/cli.ps1" -DbProvider "Microsoft.EntityFrameworkCore.SqlServer" -CoreProject "XXX.Core" -EntryProject "XXX.Web.Entry" -ConnectionName "Default"\n'))),(0,o.kt)("admonition",{title:"\u5c0f\u63d0\u793a",type:"note"},(0,o.kt)("p",{parentName:"admonition"},"\u5982\u679c\u4f7f\u7528\u7684\u662f ",(0,o.kt)("inlineCode",{parentName:"p"},"SqlServer")," \u6570\u636e\u5e93\uff0c\u5219\u9ed8\u8ba4\u4e0d\u9700\u8981\u6307\u5b9a ",(0,o.kt)("inlineCode",{parentName:"p"},"-DbProvider")," \u53c2\u6570\u3002")),(0,o.kt)("p",null,"\u5982\u679c\u4e0d\u6e05\u695a\u5f53\u524d\u8fd0\u884c\u73af\u5883\u7684\u8def\u5f84\uff0c\u53ef\u4ee5\u8f93\u5165 ",(0,o.kt)("inlineCode",{parentName:"p"},"pwd")," \u67e5\u770b\u3002"),(0,o.kt)("admonition",{title:"\u5173\u4e8e\u6570\u636e\u5e93\u547d\u540d",type:"important"},(0,o.kt)("p",{parentName:"admonition"},"\u5982\u679c\u9700\u8981\u4fdd\u6301\u548c\u6570\u636e\u5e93\u4e00\u6a21\u4e00\u6837\u7684\u547d\u540d\uff0c\u5219\u4f7f\u7528 ",(0,o.kt)("inlineCode",{parentName:"p"},"-UseDatabaseNames")," \u53c2\u6570\u6307\u5b9a\uff0c\u5982\uff1a"),(0,o.kt)("pre",{parentName:"admonition"},(0,o.kt)("code",{parentName:"pre",className:"language-cs",metastring:"showLineNumbers",showLineNumbers:!0},' &"../tools/cli.ps1" -UseDatabaseNames\n'))),(0,o.kt)("img",{src:(0,a.Z)("img/dbfirst3.png")}),(0,o.kt)("admonition",{title:"\u7b49\u5f85\u8f93\u5165",type:"important"},(0,o.kt)("p",{parentName:"admonition"},"\u6267\u884c\u4e0a\u9762\u547d\u4ee4\u540e\uff0c\u6b64\u65f6 ",(0,o.kt)("inlineCode",{parentName:"p"},"Cli")," \u6709\u4e00\u4e2a\u7b49\u5f85\u8f93\u5165\u63d0\u793a\uff1a"),(0,o.kt)("pre",{parentName:"admonition"},(0,o.kt)("code",{parentName:"pre",className:"language-shell",metastring:"showLineNumbers",showLineNumbers:!0},"Furion Tools v1.0.0 \u8bf7\u952e\u5165\u64cd\u4f5c\u7c7b\u578b\uff1a[G] \u754c\u9762\u64cd\u4f5c\uff0c[\u4efb\u610f\u5b57\u7b26] \u547d\u4ee4\u884c\u64cd\u4f5c\nFurion Tools v1.0.0 \u60a8\u7684\u8f93\u5165\u662f:\n")),(0,o.kt)("p",{parentName:"admonition"},(0,o.kt)("strong",{parentName:"p"},"\u8f93\u5165\u5927\u5199 ",(0,o.kt)("inlineCode",{parentName:"strong"},"G")," \u8fdb\u5165\u754c\u9762\u64cd\u4f5c\u6a21\u5f0f\uff0c\u5176\u4ed6\u4efb\u610f\u5b57\u7b26\u8fdb\u5165\u547d\u4ee4\u884c\u64cd\u4f5c\u6a21\u5f0f\u3002"))),(0,o.kt)("admonition",{title:"\u6ce8\u610f\u4e8b\u9879",type:"caution"},(0,o.kt)("p",{parentName:"admonition"},"\u76ee\u524d\u53ea\u6709 ",(0,o.kt)("inlineCode",{parentName:"p"},"Sql Server")," \u6570\u636e\u5e93\u624d\u652f\u6301 ",(0,o.kt)("inlineCode",{parentName:"p"},"GUI \u754c\u9762\u64cd\u4f5c\u6a21\u5f0f"),"\uff0c\u5176\u4ed6\u6570\u636e\u5e93\u8bf7\u4f7f\u7528\u547d\u4ee4\u884c\u6a21\u5f0f\u3002")),(0,o.kt)("h2",{id:"9193-\u754c\u9762\u64cd\u4f5c\u6a21\u5f0f"},"9.19.3 \u754c\u9762\u64cd\u4f5c\u6a21\u5f0f"),(0,o.kt)("h3",{id:"91931-\u542f\u52a8\u754c\u9762\u64cd\u4f5c"},"9.19.3.1 \u542f\u52a8\u754c\u9762\u64cd\u4f5c"),(0,o.kt)("p",null,"\u5f53\u6211\u4eec\u8f93\u5165 ",(0,o.kt)("inlineCode",{parentName:"p"},"G")," \u65f6\uff0c\u5c06\u6253\u5f00 ",(0,o.kt)("inlineCode",{parentName:"p"},"GUI")," \u754c\u9762\u64cd\u4f5c\u6a21\u5f0f\uff0c\u5982\uff1a"),(0,o.kt)("img",{src:(0,a.Z)("img/dbfirst4.png")}),(0,o.kt)("p",null,"\u8fd9\u65f6\uff0c",(0,o.kt)("inlineCode",{parentName:"p"},"Furion Tools")," \u4f1a\u81ea\u52a8\u67e5\u627e\u6240\u6709\u6570\u636e\u5e93\u914d\u7f6e\u8fde\u63a5\u5b57\u7b26\u4e32\u7684 ",(0,o.kt)("inlineCode",{parentName:"p"},".json")," \u6587\u4ef6\uff1a"),(0,o.kt)("admonition",{title:"\u6ce8\u610f\u4e8b\u9879",type:"important"},(0,o.kt)("p",{parentName:"admonition"},"\u6570\u636e\u5e93\u8fde\u63a5\u5b57\u7b26\u4e32\u914d\u7f6e\u9879\u9700\u5199\u5230 ",(0,o.kt)("inlineCode",{parentName:"p"},"json")," \u914d\u7f6e\u6587\u4ef6\u4e2d\uff0c\u4e14\u6839\u8282\u70b9\u9700\u8981\u5199\u4e3a ",(0,o.kt)("inlineCode",{parentName:"p"},"ConnectionStrings")," \u3002")),(0,o.kt)("pre",null,(0,o.kt)("code",{parentName:"pre",className:"language-json",metastring:"showLineNumbers  {12-13}",showLineNumbers:!0,"":!0,"{12-13}":!0},'{\n  "Logging": {\n    "LogLevel": {\n      "Default": "Information",\n      "Microsoft": "Warning",\n      "Microsoft.Hosting.Lifetime": "Information",\n      "Microsoft.EntityFrameworkCore": "Information"\n    }\n  },\n  "AllowedHosts": "*",\n  "ConnectionStrings": {\n    "DbConnectionString": "Server=localhost;Database=Furion;User=sa;Password=000000;MultipleActiveResultSets=True;",\n    "Sqlite3ConnectionString": "Data Source=./Furion.db"\n  }\n}\n')),(0,o.kt)("h3",{id:"91932-\u52a0\u8f7d\u6570\u636e\u5e93\u8868"},"9.19.3.2 \u52a0\u8f7d\u6570\u636e\u5e93\u8868"),(0,o.kt)("p",null,"\u9009\u62e9\u8fde\u63a5\u5b57\u7b26\u4e32\u4e4b\u540e\uff0c\u70b9\u51fb ",(0,o.kt)("inlineCode",{parentName:"p"},"\u52a0\u8f7d\u6570\u636e\u5e93\u8868\u548c\u89c6\u56fe")," \u6309\u94ae\u52a0\u8f7d\u3002"),(0,o.kt)("img",{src:(0,a.Z)("img/dbfirst5.png")}),(0,o.kt)("h3",{id:"91933-\u9009\u62e9\u8868\u6216\u89c6\u56fe\u751f\u6210"},"9.19.3.3 \u9009\u62e9\u8868\u6216\u89c6\u56fe\u751f\u6210"),(0,o.kt)("p",null,"\u52a0\u8f7d\u8868\u5b8c\u6210\u540e\uff0c\u53ef\u4ee5\u9009\u62e9\u60a8\u8981\u751f\u6210\u7684\u8868\u6216\u89c6\u56fe\uff0c",(0,o.kt)("strong",{parentName:"p"},"\u652f\u6301\u591a\u9009")),(0,o.kt)("img",{src:(0,a.Z)("img/dbfirst6.png")}),(0,o.kt)("p",null,"\u70b9\u51fb\u5e95\u90e8\u6309\u94ae ",(0,o.kt)("inlineCode",{parentName:"p"},"\u7acb\u5373\u751f\u6210")),(0,o.kt)("h3",{id:"91934-\u9009\u62e9\u4fdd\u5b58\u76ee\u5f55"},"9.19.3.4 \u9009\u62e9\u4fdd\u5b58\u76ee\u5f55"),(0,o.kt)("p",null,"\u70b9\u51fb ",(0,o.kt)("inlineCode",{parentName:"p"},"\u7acb\u5373\u751f\u6210")," \u6309\u94ae\u540e\uff0c\u4f1a\u5f39\u51fa\u5b9e\u4f53\u4fdd\u5b58\u9009\u62e9\u76ee\u5f55\u8d44\u6e90\u7ba1\u7406\u5668\uff0c\u9ed8\u8ba4\u5b9e\u4f53\u53ea\u80fd\u4fdd\u5b58\u5728 ",(0,o.kt)("inlineCode",{parentName:"p"},"Furion.Core")," \u5c42\uff1a"),(0,o.kt)("img",{src:(0,a.Z)("img/dbfirst7.png")}),(0,o.kt)("p",null,"\u70b9\u51fb\u786e\u5b9a\u540e\u5c31\u53ef\u5b8c\u6210\u6240\u6709\u751f\u6210\u64cd\u4f5c\u3002"),(0,o.kt)("h3",{id:"91935-\u751f\u6210\u6700\u7ec8\u5b9e\u4f53\u4ee3\u7801"},"9.19.3.5 \u751f\u6210\u6700\u7ec8\u5b9e\u4f53\u4ee3\u7801"),(0,o.kt)("img",{src:(0,a.Z)("img/dbfirst8.png")}),(0,o.kt)("p",null,"\u6700\u7ec8\u811a\u672c\u5982\u4e0b\uff1a"),(0,o.kt)("pre",null,(0,o.kt)("code",{parentName:"pre",className:"language-shell",metastring:"showLineNumbers",showLineNumbers:!0},'PM> &"../tools/cli.ps1"\n// -----------------------------------------------------------------------------\n//   ______            _______          _\n//  |  ____|          |__   __|        | |\n//  | |__ _   _ _ __     | | ___   ___ | |___\n//  |  __| | | | \'__|    | |/ _ \\ / _ \\| / __|\n//  | |  | |_| | |       | | (_) | (_) | \\__ \\\n//  |_|   \\__,_|_|       |_|\\___/ \\___/|_|___/\n//\n// -----------------------------------------------------------------------------\nFurion Tools v1.0.0 \u542f\u52a8\u4e2d......\nFurion Tools v1.0.0 \u542f\u52a8\u6210\u529f\uff01\nFurion Tools v1.0.0 \u8bf7\u952e\u5165\u64cd\u4f5c\u7c7b\u578b\uff1a[G] \u754c\u9762\u64cd\u4f5c\uff0c[\u4efb\u610f\u5b57\u7b26] \u547d\u4ee4\u884c\u64cd\u4f5c\nFurion Tools v1.0.0 \u60a8\u7684\u8f93\u5165\u662f: G\nFurion Tools v1.0.0 \u6b63\u5728\u52a0\u8f7d\u6570\u636e\u5e93\u8868\u548c\u89c6\u56fe......\nFurion Tools v1.0.0 \u52a0\u8f7d\u6210\u529f\uff01\nFurion Tools v1.0.0 \u6b63\u5728\u7f16\u8bd1\u89e3\u51b3\u65b9\u6848\u4ee3\u7801......\nBuild started...\nBuild succeeded.\nFor foreign key FK_PersonDetail_Person_PersonId on table dbo.PersonDetail, unable to model the end of the foreign key on principal table dbo.Person. This is usually because the principal table was not included in the selection set.\nFurion Tools v1.0.0 \u7f16\u8bd1\u6210\u529f\uff01\nFurion Tools v1.0.0 \u5f00\u59cb\u751f\u6210\u5b9e\u4f53\u6587\u4ef6......\nFurion Tools v1.0.0 \u6b63\u5728\u751f\u6210 City.cs \u5b9e\u4f53\u4ee3\u7801......\nFurion Tools v1.0.0 \u6210\u529f\u751f\u6210 City.cs \u5b9e\u4f53\u4ee3\u7801\n// -----------------------------------------------------------------------------\n// \u4ee5\u4e0b\u4ee3\u7801\u7531 Furion Tools v1.0.0 \u751f\u6210\n// -----------------------------------------------------------------------------\n\nusing Furion.DatabaseAccessor;\nusing Microsoft.EntityFrameworkCore.Metadata.Builders;\nusing System;\nusing System.Collections.Generic;\n\n#nullable disable\n\nnamespace Furion.Core\n{\n    public partial class City : IEntity, IEntityTypeBuilder<City>\n    {\n\n        public City()\n        {\n            InverseParent = new HashSet<City>();\n        }\n\n        public int Id { get; set; }\n        public string Name { get; set; }\n        public int? ParentId { get; set; }\n        public DateTime CreatedTime { get; set; }\n        public DateTime? UpdatedTime { get; set; }\n        public bool IsDeleted { get; set; }\n\n        public virtual City Parent { get; set; }\n        public virtual ICollection<City> InverseParent { get; set; }\n\n        public void Configure(EntityTypeBuilder<City> entityBuilder, DbContext dbContext, Type dbContextLocator)\n        {\n                entityBuilder.HasIndex(e => e.ParentId, "IX_City_ParentId");\n\n                entityBuilder.HasOne(d => d.Parent)\n                    .WithMany(p => p.InverseParent)\n                    .HasForeignKey(d => d.ParentId);\n\n        }\n\n    }\n}\n\nFurion Tools v1.0.0 \u6b63\u5728\u751f\u6210 PersonDetail.cs \u5b9e\u4f53\u4ee3\u7801......\nFurion Tools v1.0.0 \u6210\u529f\u751f\u6210 PersonDetail.cs \u5b9e\u4f53\u4ee3\u7801\n// -----------------------------------------------------------------------------\n// \u4ee5\u4e0b\u4ee3\u7801\u7531 Furion Tools v1.0.0 \u751f\u6210\n// -----------------------------------------------------------------------------\n\nusing Furion.DatabaseAccessor;\nusing Microsoft.EntityFrameworkCore.Metadata.Builders;\nusing System;\nusing System.Collections.Generic;\n\n#nullable disable\n\nnamespace Furion.Core\n{\n    public partial class PersonDetail : IEntity, IEntityTypeBuilder<PersonDetail>\n    {\n\n        public int Id { get; set; }\n        public string PhoneNumber { get; set; }\n        public string Qq { get; set; }\n        public int PersonId { get; set; }\n\n        public void Configure(EntityTypeBuilder<PersonDetail> entityBuilder, DbContext dbContext, Type dbContextLocator)\n        {\n                entityBuilder.HasIndex(e => e.PersonId, "IX_PersonDetail_PersonId")\n                    .IsUnique();\n\n                entityBuilder.Property(e => e.Qq).HasColumnName("QQ");\n\n        }\n\n    }\n}\n\nFurion Tools v1.0.0 \u6b63\u5728\u751f\u6210 Post.cs \u5b9e\u4f53\u4ee3\u7801......\nFurion Tools v1.0.0 \u6210\u529f\u751f\u6210 Post.cs \u5b9e\u4f53\u4ee3\u7801\n// -----------------------------------------------------------------------------\n// \u4ee5\u4e0b\u4ee3\u7801\u7531 Furion Tools v1.0.0 \u751f\u6210\n// -----------------------------------------------------------------------------\n\nusing Furion.DatabaseAccessor;\nusing Microsoft.EntityFrameworkCore.Metadata.Builders;\nusing System;\nusing System.Collections.Generic;\n\n#nullable disable\n\nnamespace Furion.Core\n{\n    public partial class Post : IEntity\n    {\n\n        public int Id { get; set; }\n        public string Name { get; set; }\n        public DateTime CreatedTime { get; set; }\n        public DateTime? UpdatedTime { get; set; }\n        public bool IsDeleted { get; set; }\n\n    }\n}\n\nFurion Tools v1.0.0 \u6b63\u5728\u751f\u6210 VPerson.cs \u5b9e\u4f53\u4ee3\u7801......\nFurion Tools v1.0.0 \u6210\u529f\u751f\u6210 VPerson.cs \u5b9e\u4f53\u4ee3\u7801\n// -----------------------------------------------------------------------------\n// \u4ee5\u4e0b\u4ee3\u7801\u7531 Furion Tools v1.0.0 \u751f\u6210\n// -----------------------------------------------------------------------------\n\nusing Furion.DatabaseAccessor;\nusing Microsoft.EntityFrameworkCore.Metadata.Builders;\nusing System;\nusing System.Collections.Generic;\n\n#nullable disable\n\nnamespace Furion.Core\n{\n    public partial class VPerson : IEntity, IEntityTypeBuilder<VPerson>\n    {\n\n        public int Id { get; set; }\n        public string Name { get; set; }\n        public int Age { get; set; }\n        public string Address { get; set; }\n\n        public void Configure(EntityTypeBuilder<VPerson> entityBuilder, DbContext dbContext, Type dbContextLocator)\n        {\n                entityBuilder.HasNoKey();\n\n                entityBuilder.ToView("V_Person");\n\n                entityBuilder.Property(e => e.Id).ValueGeneratedOnAdd();\n\n                entityBuilder.Property(e => e.Name).HasMaxLength(32);\n\n        }\n\n    }\n}\n\nFurion Tools v1.0.0 \u5168\u90e8\u5b9e\u4f53\u751f\u6210\u6210\u529f\uff01\nPM>\n')),(0,o.kt)("h2",{id:"9194-\u547d\u4ee4\u53c2\u6570\u914d\u7f6e"},"9.19.4 \u547d\u4ee4\u53c2\u6570\u914d\u7f6e"),(0,o.kt)("p",null,(0,o.kt)("inlineCode",{parentName:"p"},"Furion Tools Cli")," \u652f\u6301\u591a\u4e2a\u53c2\u6570\u914d\u7f6e\uff0c\u4f7f\u7528\u65b9\u6cd5\u53ea\u9700\u8981\u5728\u547d\u4ee4\u540e\u9762\u6dfb\u52a0\u5373\u53ef\uff0c\u5982\uff1a"),(0,o.kt)("pre",null,(0,o.kt)("code",{parentName:"pre",className:"language-shell",metastring:"showLineNumbers",showLineNumbers:!0},' &"../tools/cli.ps1" -Context \u6570\u636e\u5e93\u4e0a\u4e0b\u6587\u540d -ConnectionName \u8fde\u63a5\u5b57\u7b26\u4e32Key\n')),(0,o.kt)("p",null,"\u652f\u6301\u53c2\u6570\u5982\u4e0b\uff1a"),(0,o.kt)("ul",null,(0,o.kt)("li",{parentName:"ul"},(0,o.kt)("inlineCode",{parentName:"li"},"-Tables"),"\uff1a\u914d\u7f6e\u8981\u751f\u6210\u7684\u6570\u636e\u5e93\u8868\uff0c\u6570\u7ec4\u7c7b\u578b\uff0c\u5982\u679c\u4e3a\u7a7a\uff0c\u5219\u751f\u6210\u6570\u636e\u5e93\u6240\u6709\u8868\u548c\u89c6\u56fe\u3002\u5982\uff1a",(0,o.kt)("inlineCode",{parentName:"li"},"-Tables Person,PersonDetails")),(0,o.kt)("li",{parentName:"ul"},(0,o.kt)("inlineCode",{parentName:"li"},"-Context"),"\uff1a\u914d\u7f6e\u6570\u636e\u5e93\u4e0a\u4e0b\u6587\uff0c\u9ed8\u8ba4 ",(0,o.kt)("inlineCode",{parentName:"li"},"FurionDbContext"),"\uff0c\u5982\u679c\u6709\u591a\u4e2a\u6570\u636e\u5e93\u4e0a\u4e0b\u6587\uff0c\u5219\u6b64\u53c2\u6570\u5fc5\u987b\u914d\u7f6e"),(0,o.kt)("li",{parentName:"ul"},(0,o.kt)("inlineCode",{parentName:"li"},"-ConnectionName"),"\uff1a\u914d\u7f6e\u6570\u636e\u5e93\u8fde\u63a5\u5b57\u7b26\u4e32\uff0c\u5bf9\u5e94 ",(0,o.kt)("inlineCode",{parentName:"li"},"appsetting.json")," \u4e2d\u7684 ",(0,o.kt)("inlineCode",{parentName:"li"},"ConnectionStrings")," \u5b9a\u4e49\u7684 ",(0,o.kt)("inlineCode",{parentName:"li"},"Key")),(0,o.kt)("li",{parentName:"ul"},(0,o.kt)("inlineCode",{parentName:"li"},"-OutputDir"),"\uff1a\u751f\u6210\u5b9e\u4f53\u4ee3\u7801\u8f93\u51fa\u76ee\u5f55\uff0c\u9ed8\u8ba4\u4e3a\uff1a",(0,o.kt)("inlineCode",{parentName:"li"},"./Furion.Core/Entities/")),(0,o.kt)("li",{parentName:"ul"},(0,o.kt)("inlineCode",{parentName:"li"},"-DbProvider"),"\uff1a\u6570\u636e\u5e93\u63d0\u4f9b\u5668\uff0c\u9ed8\u8ba4\u662f ",(0,o.kt)("inlineCode",{parentName:"li"},"Microsoft.EntityFrameworkCore.SqlServer"),"\uff0c\u5176\u4ed6\u6570\u636e\u5e93\u8bf7\u6307\u5b9a\u5bf9\u5e94\u7a0b\u5e8f\u96c6",(0,o.kt)("ul",{parentName:"li"},(0,o.kt)("li",{parentName:"ul"},(0,o.kt)("inlineCode",{parentName:"li"},"SqlServer"),"\uff1a",(0,o.kt)("inlineCode",{parentName:"li"},"Microsoft.EntityFrameworkCore.SqlServer")),(0,o.kt)("li",{parentName:"ul"},(0,o.kt)("inlineCode",{parentName:"li"},"Sqlite"),"\uff1a",(0,o.kt)("inlineCode",{parentName:"li"},"Microsoft.EntityFrameworkCore.Sqlite")),(0,o.kt)("li",{parentName:"ul"},(0,o.kt)("inlineCode",{parentName:"li"},"Cosmos"),"\uff1a",(0,o.kt)("inlineCode",{parentName:"li"},"Microsoft.EntityFrameworkCore.Cosmos")),(0,o.kt)("li",{parentName:"ul"},(0,o.kt)("inlineCode",{parentName:"li"},"InMemoryDatabase"),"\uff1a",(0,o.kt)("inlineCode",{parentName:"li"},"Microsoft.EntityFrameworkCore.InMemory")),(0,o.kt)("li",{parentName:"ul"},(0,o.kt)("inlineCode",{parentName:"li"},"MySql"),"\uff1a",(0,o.kt)("inlineCode",{parentName:"li"},"Pomelo.EntityFrameworkCore.MySql")," \u6216 ",(0,o.kt)("inlineCode",{parentName:"li"},"MySql.EntityFrameworkCore")),(0,o.kt)("li",{parentName:"ul"},(0,o.kt)("inlineCode",{parentName:"li"},"PostgreSQL"),"\uff1a",(0,o.kt)("inlineCode",{parentName:"li"},"Npgsql.EntityFrameworkCore.PostgreSQL")),(0,o.kt)("li",{parentName:"ul"},(0,o.kt)("inlineCode",{parentName:"li"},"Oracle"),"\uff1a",(0,o.kt)("inlineCode",{parentName:"li"},"Oracle.EntityFrameworkCore")),(0,o.kt)("li",{parentName:"ul"},(0,o.kt)("inlineCode",{parentName:"li"},"Dm"),"\uff1a",(0,o.kt)("inlineCode",{parentName:"li"},"Microsoft.EntityFrameworkCore.Dm")))),(0,o.kt)("li",{parentName:"ul"},(0,o.kt)("inlineCode",{parentName:"li"},"-EntryProject"),"\uff1aWeb \u542f\u7528\u9879\u76ee\u5c42\u540d\uff0c\u9ed8\u8ba4 ",(0,o.kt)("inlineCode",{parentName:"li"},"Furion.Web.Entry")),(0,o.kt)("li",{parentName:"ul"},(0,o.kt)("inlineCode",{parentName:"li"},"-CoreProject"),"\uff1a\u5b9e\u4f53\u9879\u76ee\u5c42\u540d\uff0c\u9ed8\u8ba4 ",(0,o.kt)("inlineCode",{parentName:"li"},"Furion.Core")),(0,o.kt)("li",{parentName:"ul"},(0,o.kt)("inlineCode",{parentName:"li"},"-DbContextLocators"),"\uff1a\u591a\u6570\u636e\u5e93\u4e0a\u4e0b\u6587\u5b9a\u4f4d\u5668\uff0c\u9ed8\u8ba4 ",(0,o.kt)("inlineCode",{parentName:"li"},"MasterDbContextLocator"),"\uff0c\u652f\u6301\u591a\u4e2a\uff0c\u5982\uff1a",(0,o.kt)("inlineCode",{parentName:"li"},"MasterDbContextLocator,MySqlDbContextLocator")),(0,o.kt)("li",{parentName:"ul"},(0,o.kt)("inlineCode",{parentName:"li"},"-Product"),"\uff1a\u89e3\u51b3\u65b9\u6848\u9ed8\u8ba4\u524d\u7f00\uff0c\u5982 ",(0,o.kt)("inlineCode",{parentName:"li"},"Furion")),(0,o.kt)("li",{parentName:"ul"},(0,o.kt)("inlineCode",{parentName:"li"},"-UseDatabaseNames"),"\uff1a\u662f\u5426\u4fdd\u6301\u751f\u6210\u548c\u6570\u636e\u5e93\u3001\u8868\u4e00\u81f4\u7684\u540d\u79f0"),(0,o.kt)("li",{parentName:"ul"},(0,o.kt)("inlineCode",{parentName:"li"},"-Namespace"),"\uff1a\u6307\u5b9a\u5b9e\u4f53\u547d\u540d\u7a7a\u95f4")),(0,o.kt)("h2",{id:"9195-\u53cd\u9988\u4e0e\u5efa\u8bae"},"9.19.5 \u53cd\u9988\u4e0e\u5efa\u8bae"),(0,o.kt)("admonition",{title:"\u4e0e\u6211\u4eec\u4ea4\u6d41",type:"note"},(0,o.kt)("p",{parentName:"admonition"},"\u7ed9 Furion \u63d0 ",(0,o.kt)("a",{parentName:"p",href:"https://gitee.com/dotnetchina/Furion/issues/new?issue"},"Issue"),"\u3002")))}k.isMDXComponent=!0}}]);