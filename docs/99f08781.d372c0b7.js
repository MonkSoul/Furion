(window.webpackJsonp=window.webpackJsonp||[]).push([[63],{139:function(e,t,r){"use strict";r.r(t),r.d(t,"frontMatter",(function(){return c})),r.d(t,"metadata",(function(){return l})),r.d(t,"toc",(function(){return s})),r.d(t,"default",(function(){return p}));var n=r(3),i=r(7),o=(r(0),r(191)),a=r(192),c={id:"jsonserializer",title:"7. JsonSerializerUtility \u9759\u6001\u7c7b",sidebar_label:"7. JsonSerializerUtility \u9759\u6001\u7c7b"},l={unversionedId:"global/jsonserializer",id:"global/jsonserializer",isDocsHomePage:!1,title:"7. JsonSerializerUtility \u9759\u6001\u7c7b",description:"7.1 \u5e8f\u5217\u5316",source:"@site/docs\\global\\jsonserializer.mdx",slug:"/global/jsonserializer",permalink:"/docs/global/jsonserializer",editUrl:"https://gitee.com/monksoul/Furion/tree/master/handbook/docs/global/jsonserializer.mdx",version:"current",lastUpdatedBy:"\u767e\u5c0f\u50e7",lastUpdatedAt:1612020493,sidebar_label:"7. JsonSerializerUtility \u9759\u6001\u7c7b",sidebar:"global",previous:{title:"6. Http \u9759\u6001\u7c7b",permalink:"/docs/global/shttp"},next:{title:"8. L \u9759\u6001\u7c7b",permalink:"/docs/global/l"}},s=[{value:"7.1 \u5e8f\u5217\u5316",id:"71-\u5e8f\u5217\u5316",children:[]},{value:"7.3 \u53cd\u5e8f\u5217\u5316",id:"73-\u53cd\u5e8f\u5217\u5316",children:[]},{value:"7.4 \u83b7\u53d6\u9ed8\u8ba4\u5e8f\u5217\u5316\u914d\u7f6e",id:"74-\u83b7\u53d6\u9ed8\u8ba4\u5e8f\u5217\u5316\u914d\u7f6e",children:[]}],u={toc:s};function p(e){var t=e.components,r=Object(i.a)(e,["components"]);return Object(o.b)("wrapper",Object(n.a)({},u,r,{components:t,mdxType:"MDXLayout"}),Object(o.b)(a.a,{mdxType:"JoinGroup"}),Object(o.b)("h2",{id:"71-\u5e8f\u5217\u5316"},"7.1 \u5e8f\u5217\u5316"),Object(o.b)("pre",null,Object(o.b)("code",Object(n.a)({parentName:"pre"},{className:"language-cs"}),"var str = JsonSerializerUtility.Serialize(obj, [options]);\n")),Object(o.b)("h2",{id:"73-\u53cd\u5e8f\u5217\u5316"},"7.3 \u53cd\u5e8f\u5217\u5316"),Object(o.b)("pre",null,Object(o.b)("code",Object(n.a)({parentName:"pre"},{className:"language-cs"}),"var obj = JsonSerializerUtility.Deserialize<T>(str, [options]);\n")),Object(o.b)("h2",{id:"74-\u83b7\u53d6\u9ed8\u8ba4\u5e8f\u5217\u5316\u914d\u7f6e"},"7.4 \u83b7\u53d6\u9ed8\u8ba4\u5e8f\u5217\u5316\u914d\u7f6e"),Object(o.b)("pre",null,Object(o.b)("code",Object(n.a)({parentName:"pre"},{className:"language-cs"}),"var options = JsonSerializerUtility.GetDefaultJsonSerializerOptionss);\n")))}p.isMDXComponent=!0},189:function(e,t,r){"use strict";r.d(t,"b",(function(){return o})),r.d(t,"a",(function(){return a}));var n=r(21),i=r(190);function o(){const{siteConfig:{baseUrl:e="/",url:t}={}}=Object(n.default)();return{withBaseUrl:(r,n)=>function(e,t,r,{forcePrependBaseUrl:n=!1,absolute:o=!1}={}){if(!r)return r;if(r.startsWith("#"))return r;if(Object(i.b)(r))return r;if(n)return t+r;const a=r.startsWith(t)?r:t+r.replace(/^\//,"");return o?e+a:a}(t,e,r,n)}}function a(e,t={}){const{withBaseUrl:r}=o();return r(e,t)}},190:function(e,t,r){"use strict";function n(e){return!0===/^(\w*:|\/\/)/.test(e)}function i(e){return void 0!==e&&!n(e)}r.d(t,"b",(function(){return n})),r.d(t,"a",(function(){return i}))},191:function(e,t,r){"use strict";r.d(t,"a",(function(){return p})),r.d(t,"b",(function(){return d}));var n=r(0),i=r.n(n);function o(e,t,r){return t in e?Object.defineProperty(e,t,{value:r,enumerable:!0,configurable:!0,writable:!0}):e[t]=r,e}function a(e,t){var r=Object.keys(e);if(Object.getOwnPropertySymbols){var n=Object.getOwnPropertySymbols(e);t&&(n=n.filter((function(t){return Object.getOwnPropertyDescriptor(e,t).enumerable}))),r.push.apply(r,n)}return r}function c(e){for(var t=1;t<arguments.length;t++){var r=null!=arguments[t]?arguments[t]:{};t%2?a(Object(r),!0).forEach((function(t){o(e,t,r[t])})):Object.getOwnPropertyDescriptors?Object.defineProperties(e,Object.getOwnPropertyDescriptors(r)):a(Object(r)).forEach((function(t){Object.defineProperty(e,t,Object.getOwnPropertyDescriptor(r,t))}))}return e}function l(e,t){if(null==e)return{};var r,n,i=function(e,t){if(null==e)return{};var r,n,i={},o=Object.keys(e);for(n=0;n<o.length;n++)r=o[n],t.indexOf(r)>=0||(i[r]=e[r]);return i}(e,t);if(Object.getOwnPropertySymbols){var o=Object.getOwnPropertySymbols(e);for(n=0;n<o.length;n++)r=o[n],t.indexOf(r)>=0||Object.prototype.propertyIsEnumerable.call(e,r)&&(i[r]=e[r])}return i}var s=i.a.createContext({}),u=function(e){var t=i.a.useContext(s),r=t;return e&&(r="function"==typeof e?e(t):c(c({},t),e)),r},p=function(e){var t=u(e.components);return i.a.createElement(s.Provider,{value:t},e.children)},b={inlineCode:"code",wrapper:function(e){var t=e.children;return i.a.createElement(i.a.Fragment,{},t)}},f=i.a.forwardRef((function(e,t){var r=e.components,n=e.mdxType,o=e.originalType,a=e.parentName,s=l(e,["components","mdxType","originalType","parentName"]),p=u(r),f=n,d=p["".concat(a,".").concat(f)]||p[f]||b[f]||o;return r?i.a.createElement(d,c(c({ref:t},s),{},{components:r})):i.a.createElement(d,c({ref:t},s))}));function d(e,t){var r=arguments,n=t&&t.mdxType;if("string"==typeof e||n){var o=r.length,a=new Array(o);a[0]=f;var c={};for(var l in t)hasOwnProperty.call(t,l)&&(c[l]=t[l]);c.originalType=e,c.mdxType="string"==typeof e?e:n,a[1]=c;for(var s=2;s<o;s++)a[s]=r[s];return i.a.createElement.apply(null,a)}return i.a.createElement.apply(null,r)}f.displayName="MDXCreateElement"},192:function(e,t,r){"use strict";r.d(t,"a",(function(){return a}));var n=r(0),i=r.n(n),o=r(189);r(55);function a(){const[e,t]=Object(n.useState)(!1);return i.a.createElement("div",{className:"furion-join-group"},e&&i.a.createElement("img",{src:Object(o.a)("img/dotnetchina2.jpg")}),i.a.createElement("button",{onClick:()=>t(!e)},"\u52a0\u5165QQ\u4ea4\u6d41\u7fa4"))}}}]);