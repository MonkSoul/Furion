(window.webpackJsonp=window.webpackJsonp||[]).push([[72],{147:function(t,e,r){"use strict";r.r(e),r.d(e,"frontMatter",(function(){return c})),r.d(e,"metadata",(function(){return u})),r.d(e,"toc",(function(){return l})),r.d(e,"default",(function(){return p}));var n=r(3),o=r(7),a=(r(0),r(191)),i=r(192),c={id:"shttp",title:"6. Http \u9759\u6001\u7c7b",sidebar_label:"6. Http \u9759\u6001\u7c7b"},u={unversionedId:"global/shttp",id:"global/shttp",isDocsHomePage:!1,title:"6. Http \u9759\u6001\u7c7b",description:"6.1 \u83b7\u53d6\u8fdc\u7a0b\u4ee3\u7406\u670d\u52a1",source:"@site/docs\\global\\shttp.mdx",slug:"/global/shttp",permalink:"/furion/docs/global/shttp",editUrl:"https://gitee.com/monksoul/Furion/tree/master/handbook/docs/global/shttp.mdx",version:"current",lastUpdatedBy:"\u767e\u5c0f\u50e7",lastUpdatedAt:1612020493,sidebar_label:"6. Http \u9759\u6001\u7c7b",sidebar:"global",previous:{title:"5. LinqExpression \u9759\u6001\u7c7b",permalink:"/furion/docs/global/linqexpression"},next:{title:"7. JsonSerializerUtility \u9759\u6001\u7c7b",permalink:"/furion/docs/global/jsonserializer"}},l=[{value:"6.1 \u83b7\u53d6\u8fdc\u7a0b\u4ee3\u7406\u670d\u52a1",id:"61-\u83b7\u53d6\u8fdc\u7a0b\u4ee3\u7406\u670d\u52a1",children:[]}],s={toc:l};function p(t){var e=t.components,r=Object(o.a)(t,["components"]);return Object(a.b)("wrapper",Object(n.a)({},s,r,{components:e,mdxType:"MDXLayout"}),Object(a.b)(i.a,{mdxType:"JoinGroup"}),Object(a.b)("h2",{id:"61-\u83b7\u53d6\u8fdc\u7a0b\u4ee3\u7406\u670d\u52a1"},"6.1 \u83b7\u53d6\u8fdc\u7a0b\u4ee3\u7406\u670d\u52a1"),Object(a.b)("pre",null,Object(a.b)("code",Object(n.a)({parentName:"pre"},{className:"language-cs"}),"var http = Http.GetHttpProxy<THttpDispatchProxy>();\n")))}p.isMDXComponent=!0},189:function(t,e,r){"use strict";r.d(e,"b",(function(){return a})),r.d(e,"a",(function(){return i}));var n=r(21),o=r(190);function a(){const{siteConfig:{baseUrl:t="/",url:e}={}}=Object(n.default)();return{withBaseUrl:(r,n)=>function(t,e,r,{forcePrependBaseUrl:n=!1,absolute:a=!1}={}){if(!r)return r;if(r.startsWith("#"))return r;if(Object(o.b)(r))return r;if(n)return e+r;const i=r.startsWith(e)?r:e+r.replace(/^\//,"");return a?t+i:i}(e,t,r,n)}}function i(t,e={}){const{withBaseUrl:r}=a();return r(t,e)}},190:function(t,e,r){"use strict";function n(t){return!0===/^(\w*:|\/\/)/.test(t)}function o(t){return void 0!==t&&!n(t)}r.d(e,"b",(function(){return n})),r.d(e,"a",(function(){return o}))},191:function(t,e,r){"use strict";r.d(e,"a",(function(){return p})),r.d(e,"b",(function(){return d}));var n=r(0),o=r.n(n);function a(t,e,r){return e in t?Object.defineProperty(t,e,{value:r,enumerable:!0,configurable:!0,writable:!0}):t[e]=r,t}function i(t,e){var r=Object.keys(t);if(Object.getOwnPropertySymbols){var n=Object.getOwnPropertySymbols(t);e&&(n=n.filter((function(e){return Object.getOwnPropertyDescriptor(t,e).enumerable}))),r.push.apply(r,n)}return r}function c(t){for(var e=1;e<arguments.length;e++){var r=null!=arguments[e]?arguments[e]:{};e%2?i(Object(r),!0).forEach((function(e){a(t,e,r[e])})):Object.getOwnPropertyDescriptors?Object.defineProperties(t,Object.getOwnPropertyDescriptors(r)):i(Object(r)).forEach((function(e){Object.defineProperty(t,e,Object.getOwnPropertyDescriptor(r,e))}))}return t}function u(t,e){if(null==t)return{};var r,n,o=function(t,e){if(null==t)return{};var r,n,o={},a=Object.keys(t);for(n=0;n<a.length;n++)r=a[n],e.indexOf(r)>=0||(o[r]=t[r]);return o}(t,e);if(Object.getOwnPropertySymbols){var a=Object.getOwnPropertySymbols(t);for(n=0;n<a.length;n++)r=a[n],e.indexOf(r)>=0||Object.prototype.propertyIsEnumerable.call(t,r)&&(o[r]=t[r])}return o}var l=o.a.createContext({}),s=function(t){var e=o.a.useContext(l),r=e;return t&&(r="function"==typeof t?t(e):c(c({},e),t)),r},p=function(t){var e=s(t.components);return o.a.createElement(l.Provider,{value:e},t.children)},f={inlineCode:"code",wrapper:function(t){var e=t.children;return o.a.createElement(o.a.Fragment,{},e)}},b=o.a.forwardRef((function(t,e){var r=t.components,n=t.mdxType,a=t.originalType,i=t.parentName,l=u(t,["components","mdxType","originalType","parentName"]),p=s(r),b=n,d=p["".concat(i,".").concat(b)]||p[b]||f[b]||a;return r?o.a.createElement(d,c(c({ref:e},l),{},{components:r})):o.a.createElement(d,c({ref:e},l))}));function d(t,e){var r=arguments,n=e&&e.mdxType;if("string"==typeof t||n){var a=r.length,i=new Array(a);i[0]=b;var c={};for(var u in e)hasOwnProperty.call(e,u)&&(c[u]=e[u]);c.originalType=t,c.mdxType="string"==typeof t?t:n,i[1]=c;for(var l=2;l<a;l++)i[l]=r[l];return o.a.createElement.apply(null,i)}return o.a.createElement.apply(null,r)}b.displayName="MDXCreateElement"},192:function(t,e,r){"use strict";r.d(e,"a",(function(){return i}));var n=r(0),o=r.n(n),a=r(189);r(55);function i(){const[t,e]=Object(n.useState)(!1);return o.a.createElement("div",{className:"furion-join-group"},t&&o.a.createElement("img",{src:Object(a.a)("img/dotnetchina2.jpg")}),o.a.createElement("button",{onClick:()=>e(!t)},"\u52a0\u5165QQ\u4ea4\u6d41\u7fa4"))}}}]);