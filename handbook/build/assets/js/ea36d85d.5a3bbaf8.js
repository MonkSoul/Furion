(self.webpackChunkfurion=self.webpackChunkfurion||[]).push([[2690],{3905:function(e,t,n){"use strict";n.d(t,{Zo:function(){return u},kt:function(){return m}});var r=n(7294);function a(e,t,n){return t in e?Object.defineProperty(e,t,{value:n,enumerable:!0,configurable:!0,writable:!0}):e[t]=n,e}function i(e,t){var n=Object.keys(e);if(Object.getOwnPropertySymbols){var r=Object.getOwnPropertySymbols(e);t&&(r=r.filter((function(t){return Object.getOwnPropertyDescriptor(e,t).enumerable}))),n.push.apply(n,r)}return n}function o(e){for(var t=1;t<arguments.length;t++){var n=null!=arguments[t]?arguments[t]:{};t%2?i(Object(n),!0).forEach((function(t){a(e,t,n[t])})):Object.getOwnPropertyDescriptors?Object.defineProperties(e,Object.getOwnPropertyDescriptors(n)):i(Object(n)).forEach((function(t){Object.defineProperty(e,t,Object.getOwnPropertyDescriptor(n,t))}))}return e}function c(e,t){if(null==e)return{};var n,r,a=function(e,t){if(null==e)return{};var n,r,a={},i=Object.keys(e);for(r=0;r<i.length;r++)n=i[r],t.indexOf(n)>=0||(a[n]=e[n]);return a}(e,t);if(Object.getOwnPropertySymbols){var i=Object.getOwnPropertySymbols(e);for(r=0;r<i.length;r++)n=i[r],t.indexOf(n)>=0||Object.prototype.propertyIsEnumerable.call(e,n)&&(a[n]=e[n])}return a}var l=r.createContext({}),p=function(e){var t=r.useContext(l),n=t;return e&&(n="function"==typeof e?e(t):o(o({},t),e)),n},u=function(e){var t=p(e.components);return r.createElement(l.Provider,{value:t},e.children)},s={inlineCode:"code",wrapper:function(e){var t=e.children;return r.createElement(r.Fragment,{},t)}},d=r.forwardRef((function(e,t){var n=e.components,a=e.mdxType,i=e.originalType,l=e.parentName,u=c(e,["components","mdxType","originalType","parentName"]),d=p(n),m=a,f=d["".concat(l,".").concat(m)]||d[m]||s[m]||i;return n?r.createElement(f,o(o({ref:t},u),{},{components:n})):r.createElement(f,o({ref:t},u))}));function m(e,t){var n=arguments,a=t&&t.mdxType;if("string"==typeof e||a){var i=n.length,o=new Array(i);o[0]=d;var c={};for(var l in t)hasOwnProperty.call(t,l)&&(c[l]=t[l]);c.originalType=e,c.mdxType="string"==typeof e?e:a,o[1]=c;for(var p=2;p<i;p++)o[p]=n[p];return r.createElement.apply(null,o)}return r.createElement.apply(null,n)}d.displayName="MDXCreateElement"},1573:function(e,t,n){"use strict";n.r(t),n.d(t,{frontMatter:function(){return o},metadata:function(){return c},toc:function(){return l},default:function(){return u}});var r=n(4034),a=n(9973),i=(n(7294),n(3905)),o={id:"virtual-deploy",title:"31.4 \u4e8c\u7ea7\u865a\u62df\u76ee\u5f55\u90e8\u7f72",sidebar_label:"31.4 \u4e8c\u7ea7\u865a\u62df\u76ee\u5f55\u90e8\u7f72"},c={unversionedId:"virtual-deploy",id:"virtual-deploy",isDocsHomePage:!1,title:"31.4 \u4e8c\u7ea7\u865a\u62df\u76ee\u5f55\u90e8\u7f72",description:"31.4.1 \u5173\u4e8e\u4e8c\u7ea7\u865a\u62df\u76ee\u5f55",source:"@site/docs/virtual-deploy.mdx",sourceDirName:".",slug:"/virtual-deploy",permalink:"/furion/docs/virtual-deploy",editUrl:"https://gitee.com/dotnetchina/Furion/tree/master/handbook/docs/virtual-deploy.mdx",version:"current",lastUpdatedBy:"Monk",lastUpdatedAt:1619517411,formattedLastUpdatedAt:"4/27/2021",sidebar_label:"31.4 \u4e8c\u7ea7\u865a\u62df\u76ee\u5f55\u90e8\u7f72",frontMatter:{id:"virtual-deploy",title:"31.4 \u4e8c\u7ea7\u865a\u62df\u76ee\u5f55\u90e8\u7f72",sidebar_label:"31.4 \u4e8c\u7ea7\u865a\u62df\u76ee\u5f55\u90e8\u7f72"},sidebar:"docs",previous:{title:"31.3 \u5728 Nginx \u90e8\u7f72",permalink:"/furion/docs/deploy-nginx"},next:{title:"32.1 Docker \u73af\u5883\u6301\u7eed\u90e8\u7f72",permalink:"/furion/docs/deploy-docker-auto"}},l=[{value:"31.4.1 \u5173\u4e8e\u4e8c\u7ea7\u865a\u62df\u76ee\u5f55",id:"3141-\u5173\u4e8e\u4e8c\u7ea7\u865a\u62df\u76ee\u5f55",children:[{value:"31.4.1.1 \u914d\u7f6e <code>Startup.cs</code>",id:"31411-\u914d\u7f6e-startupcs",children:[]},{value:"31.4.1.2 \u542f\u7528 <code>Swagger</code> \u865a\u62df\u76ee\u5f55\u652f\u6301",id:"31412-\u542f\u7528-swagger-\u865a\u62df\u76ee\u5f55\u652f\u6301",children:[]}]},{value:"31.4.2 \u53cd\u9988\u4e0e\u5efa\u8bae",id:"3142-\u53cd\u9988\u4e0e\u5efa\u8bae",children:[]}],p={toc:l};function u(e){var t=e.components,n=(0,a.Z)(e,["components"]);return(0,i.kt)("wrapper",(0,r.Z)({},p,n,{components:t,mdxType:"MDXLayout"}),(0,i.kt)("h2",{id:"3141-\u5173\u4e8e\u4e8c\u7ea7\u865a\u62df\u76ee\u5f55"},"31.4.1 \u5173\u4e8e\u4e8c\u7ea7\u865a\u62df\u76ee\u5f55"),(0,i.kt)("p",null,"\u901a\u5e38\u6211\u4eec\u7684\u7ad9\u70b9\u90fd\u662f\u90e8\u7f72\u5728\u7f51\u7ad9\u7684\u6839\u76ee\u5f55\u4e0b\u7684\uff0c\u4f46\u662f\u6709\u4e9b\u65f6\u5019\uff0c\u6211\u4eec\u53ef\u80fd\u5c06\u7f51\u7ad9\u6839\u76ee\u5f55\u4e0b\u7684\u76ee\u5f55\u4f5c\u4e3a\u4e8c\u7ea7\u7ad9\u70b9\u6216\u5b50\u7ad9\u70b9\uff0c\u8fd9\u4e2a\u65f6\u5019\uff0c\u5c31\u4f1a\u51fa\u73b0 ",(0,i.kt)("inlineCode",{parentName:"p"},"404")," \u9519\u8bef\u4e86\u3002"),(0,i.kt)("p",null,"\u8fd9\u4e2a\u65f6\u5019\u5c31\u9700\u8981\u8fdb\u884c\u4e00\u4e9b\u7279\u6b8a\u914d\u7f6e\u4e86\u3002"),(0,i.kt)("h3",{id:"31411-\u914d\u7f6e-startupcs"},"31.4.1.1 \u914d\u7f6e ",(0,i.kt)("inlineCode",{parentName:"h3"},"Startup.cs")),(0,i.kt)("p",null,"\u9996\u5148\uff0c\u6211\u4eec\u9700\u8981\u914d\u7f6e ",(0,i.kt)("inlineCode",{parentName:"p"},"Startup.cs")," \u4e2d\u7684 ",(0,i.kt)("inlineCode",{parentName:"p"},"Configure"),"\uff0c\u5982\uff1a"),(0,i.kt)("pre",null,(0,i.kt)("code",{parentName:"pre",className:"language-cs",metastring:"{3}","{3}":!0},'public void Configure(IApplicationBuilder application, IWebHostEnvironment env)\n{\n    application.Map("/\u4e8c\u7ea7\u76ee\u5f55", app=>\n    {\n        // \u5176\u4ed6\u4e2d\u95f4\u4ef6\u4ee3\u7801\u5168\u90e8\u5728\u8fd9\u91cc\u9762\u5199\n    });\n}\n')),(0,i.kt)("h3",{id:"31412-\u542f\u7528-swagger-\u865a\u62df\u76ee\u5f55\u652f\u6301"},"31.4.1.2 \u542f\u7528 ",(0,i.kt)("inlineCode",{parentName:"h3"},"Swagger")," \u865a\u62df\u76ee\u5f55\u652f\u6301"),(0,i.kt)("p",null,"\u7b2c\u4e8c\u6b65\uff0c\u5728 ",(0,i.kt)("inlineCode",{parentName:"p"},"appsettings.json")," \u914d\u7f6e\u6587\u4ef6\uff08\u5176\u4ed6\u914d\u7f6e\u6587\u4ef6\u4e5f\u53ef\u4ee5\uff09\u4e2d\u6dfb\u52a0\u4ee5\u4e0b\u914d\u7f6e\uff1a"),(0,i.kt)("pre",null,(0,i.kt)("code",{parentName:"pre",className:"language-json",metastring:"{2-3}","{2-3}":!0},'{\n  "SpecificationDocumentSettings": {\n    "VirtualPath": "/\u4e8c\u7ea7\u76ee\u5f55"\n  }\n}\n')),(0,i.kt)("h2",{id:"3142-\u53cd\u9988\u4e0e\u5efa\u8bae"},"31.4.2 \u53cd\u9988\u4e0e\u5efa\u8bae"),(0,i.kt)("div",{className:"admonition admonition-note alert alert--secondary"},(0,i.kt)("div",{parentName:"div",className:"admonition-heading"},(0,i.kt)("h5",{parentName:"div"},(0,i.kt)("span",{parentName:"h5",className:"admonition-icon"},(0,i.kt)("svg",{parentName:"span",xmlns:"http://www.w3.org/2000/svg",width:"14",height:"16",viewBox:"0 0 14 16"},(0,i.kt)("path",{parentName:"svg",fillRule:"evenodd",d:"M6.3 5.69a.942.942 0 0 1-.28-.7c0-.28.09-.52.28-.7.19-.18.42-.28.7-.28.28 0 .52.09.7.28.18.19.28.42.28.7 0 .28-.09.52-.28.7a1 1 0 0 1-.7.3c-.28 0-.52-.11-.7-.3zM8 7.99c-.02-.25-.11-.48-.31-.69-.2-.19-.42-.3-.69-.31H6c-.27.02-.48.13-.69.31-.2.2-.3.44-.31.69h1v3c.02.27.11.5.31.69.2.2.42.31.69.31h1c.27 0 .48-.11.69-.31.2-.19.3-.42.31-.69H8V7.98v.01zM7 2.3c-3.14 0-5.7 2.54-5.7 5.68 0 3.14 2.56 5.7 5.7 5.7s5.7-2.55 5.7-5.7c0-3.15-2.56-5.69-5.7-5.69v.01zM7 .98c3.86 0 7 3.14 7 7s-3.14 7-7 7-7-3.12-7-7 3.14-7 7-7z"}))),"\u4e0e\u6211\u4eec\u4ea4\u6d41")),(0,i.kt)("div",{parentName:"div",className:"admonition-content"},(0,i.kt)("p",{parentName:"div"},"\u7ed9 Furion \u63d0 ",(0,i.kt)("a",{parentName:"p",href:"https://gitee.com/dotnetchina/Furion/issues/new?issue"},"Issue"),"\u3002"))))}u.isMDXComponent=!0}}]);