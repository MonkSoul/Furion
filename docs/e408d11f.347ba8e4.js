(window.webpackJsonp=window.webpackJsonp||[]).push([[97],{171:function(e,t,n){"use strict";n.r(t),n.d(t,"frontMatter",(function(){return a})),n.d(t,"metadata",(function(){return u})),n.d(t,"toc",(function(){return s})),n.d(t,"default",(function(){return l}));var r=n(3),o=n(7),c=(n(0),n(191)),i=(n(189),n(192)),a={id:"deploy-nginx",title:"27.3 \u5728 Nginx \u90e8\u7f72",sidebar_label:"27.3 \u5728 Nginx \u90e8\u7f72"},u={unversionedId:"deploy-nginx",id:"deploy-nginx",isDocsHomePage:!1,title:"27.3 \u5728 Nginx \u90e8\u7f72",description:"\u6587\u6863\u7d27\u6025\u7f16\u5199\u4e2d\uff0c\u53ef\u4ee5\u67e5\u770b\u5b98\u65b9\u6587\u6863 https://docs.microsoft.com/zh-cn/aspnet/core/host-and-deploy/linux-nginx?view=aspnetcore-5.0",source:"@site/docs\\docker-nginx.mdx",slug:"/deploy-nginx",permalink:"/docs/deploy-nginx",editUrl:"https://gitee.com/monksoul/Furion/tree/master/handbook/docs/docker-nginx.mdx",version:"current",lastUpdatedBy:"\u767e\u5c0f\u50e7",lastUpdatedAt:1612020493,sidebar_label:"27.3 \u5728 Nginx \u90e8\u7f72",sidebar:"docs",previous:{title:"27.2 \u5728 Docker \u90e8\u7f72",permalink:"/docs/deploy-docker"},next:{title:"27.4 \u4e8c\u7ea7\u865a\u62df\u76ee\u5f55\u90e8\u7f72",permalink:"/docs/virtual-deploy"}},s=[],p={toc:s};function l(e){var t=e.components,n=Object(o.a)(e,["components"]);return Object(c.b)("wrapper",Object(r.a)({},p,n,{components:t,mdxType:"MDXLayout"}),Object(c.b)(i.a,{mdxType:"JoinGroup"}),Object(c.b)("p",null,"\u6587\u6863\u7d27\u6025\u7f16\u5199\u4e2d\uff0c\u53ef\u4ee5\u67e5\u770b\u5b98\u65b9\u6587\u6863 ",Object(c.b)("a",Object(r.a)({parentName:"p"},{href:"https://docs.microsoft.com/zh-cn/aspnet/core/host-and-deploy/linux-nginx?view=aspnetcore-5.0"}),"https://docs.microsoft.com/zh-cn/aspnet/core/host-and-deploy/linux-nginx?view=aspnetcore-5.0")))}l.isMDXComponent=!0},189:function(e,t,n){"use strict";n.d(t,"b",(function(){return c})),n.d(t,"a",(function(){return i}));var r=n(21),o=n(190);function c(){const{siteConfig:{baseUrl:e="/",url:t}={}}=Object(r.default)();return{withBaseUrl:(n,r)=>function(e,t,n,{forcePrependBaseUrl:r=!1,absolute:c=!1}={}){if(!n)return n;if(n.startsWith("#"))return n;if(Object(o.b)(n))return n;if(r)return t+n;const i=n.startsWith(t)?n:t+n.replace(/^\//,"");return c?e+i:i}(t,e,n,r)}}function i(e,t={}){const{withBaseUrl:n}=c();return n(e,t)}},190:function(e,t,n){"use strict";function r(e){return!0===/^(\w*:|\/\/)/.test(e)}function o(e){return void 0!==e&&!r(e)}n.d(t,"b",(function(){return r})),n.d(t,"a",(function(){return o}))},191:function(e,t,n){"use strict";n.d(t,"a",(function(){return l})),n.d(t,"b",(function(){return b}));var r=n(0),o=n.n(r);function c(e,t,n){return t in e?Object.defineProperty(e,t,{value:n,enumerable:!0,configurable:!0,writable:!0}):e[t]=n,e}function i(e,t){var n=Object.keys(e);if(Object.getOwnPropertySymbols){var r=Object.getOwnPropertySymbols(e);t&&(r=r.filter((function(t){return Object.getOwnPropertyDescriptor(e,t).enumerable}))),n.push.apply(n,r)}return n}function a(e){for(var t=1;t<arguments.length;t++){var n=null!=arguments[t]?arguments[t]:{};t%2?i(Object(n),!0).forEach((function(t){c(e,t,n[t])})):Object.getOwnPropertyDescriptors?Object.defineProperties(e,Object.getOwnPropertyDescriptors(n)):i(Object(n)).forEach((function(t){Object.defineProperty(e,t,Object.getOwnPropertyDescriptor(n,t))}))}return e}function u(e,t){if(null==e)return{};var n,r,o=function(e,t){if(null==e)return{};var n,r,o={},c=Object.keys(e);for(r=0;r<c.length;r++)n=c[r],t.indexOf(n)>=0||(o[n]=e[n]);return o}(e,t);if(Object.getOwnPropertySymbols){var c=Object.getOwnPropertySymbols(e);for(r=0;r<c.length;r++)n=c[r],t.indexOf(n)>=0||Object.prototype.propertyIsEnumerable.call(e,n)&&(o[n]=e[n])}return o}var s=o.a.createContext({}),p=function(e){var t=o.a.useContext(s),n=t;return e&&(n="function"==typeof e?e(t):a(a({},t),e)),n},l=function(e){var t=p(e.components);return o.a.createElement(s.Provider,{value:t},e.children)},d={inlineCode:"code",wrapper:function(e){var t=e.children;return o.a.createElement(o.a.Fragment,{},t)}},f=o.a.forwardRef((function(e,t){var n=e.components,r=e.mdxType,c=e.originalType,i=e.parentName,s=u(e,["components","mdxType","originalType","parentName"]),l=p(n),f=r,b=l["".concat(i,".").concat(f)]||l[f]||d[f]||c;return n?o.a.createElement(b,a(a({ref:t},s),{},{components:n})):o.a.createElement(b,a({ref:t},s))}));function b(e,t){var n=arguments,r=t&&t.mdxType;if("string"==typeof e||r){var c=n.length,i=new Array(c);i[0]=f;var a={};for(var u in t)hasOwnProperty.call(t,u)&&(a[u]=t[u]);a.originalType=e,a.mdxType="string"==typeof e?e:r,i[1]=a;for(var s=2;s<c;s++)i[s]=n[s];return o.a.createElement.apply(null,i)}return o.a.createElement.apply(null,n)}f.displayName="MDXCreateElement"},192:function(e,t,n){"use strict";n.d(t,"a",(function(){return i}));var r=n(0),o=n.n(r),c=n(189);n(55);function i(){const[e,t]=Object(r.useState)(!1);return o.a.createElement("div",{className:"furion-join-group"},e&&o.a.createElement("img",{src:Object(c.a)("img/dotnetchina2.jpg")}),o.a.createElement("button",{onClick:()=>t(!e)},"\u52a0\u5165QQ\u4ea4\u6d41\u7fa4"))}}}]);