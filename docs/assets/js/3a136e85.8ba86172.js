(self.webpackChunkfurion=self.webpackChunkfurion||[]).push([[1254],{3905:function(e,t,r){"use strict";r.d(t,{Zo:function(){return d},kt:function(){return b}});var n=r(7294);function o(e,t,r){return t in e?Object.defineProperty(e,t,{value:r,enumerable:!0,configurable:!0,writable:!0}):e[t]=r,e}function a(e,t){var r=Object.keys(e);if(Object.getOwnPropertySymbols){var n=Object.getOwnPropertySymbols(e);t&&(n=n.filter((function(t){return Object.getOwnPropertyDescriptor(e,t).enumerable}))),r.push.apply(r,n)}return r}function l(e){for(var t=1;t<arguments.length;t++){var r=null!=arguments[t]?arguments[t]:{};t%2?a(Object(r),!0).forEach((function(t){o(e,t,r[t])})):Object.getOwnPropertyDescriptors?Object.defineProperties(e,Object.getOwnPropertyDescriptors(r)):a(Object(r)).forEach((function(t){Object.defineProperty(e,t,Object.getOwnPropertyDescriptor(r,t))}))}return e}function c(e,t){if(null==e)return{};var r,n,o=function(e,t){if(null==e)return{};var r,n,o={},a=Object.keys(e);for(n=0;n<a.length;n++)r=a[n],t.indexOf(r)>=0||(o[r]=e[r]);return o}(e,t);if(Object.getOwnPropertySymbols){var a=Object.getOwnPropertySymbols(e);for(n=0;n<a.length;n++)r=a[n],t.indexOf(r)>=0||Object.prototype.propertyIsEnumerable.call(e,r)&&(o[r]=e[r])}return o}var i=n.createContext({}),p=function(e){var t=n.useContext(i),r=t;return e&&(r="function"==typeof e?e(t):l(l({},t),e)),r},d=function(e){var t=p(e.components);return n.createElement(i.Provider,{value:t},e.children)},s={inlineCode:"code",wrapper:function(e){var t=e.children;return n.createElement(n.Fragment,{},t)}},u=n.forwardRef((function(e,t){var r=e.components,o=e.mdxType,a=e.originalType,i=e.parentName,d=c(e,["components","mdxType","originalType","parentName"]),u=p(r),b=o,m=u["".concat(i,".").concat(b)]||u[b]||s[b]||a;return r?n.createElement(m,l(l({ref:t},d),{},{components:r})):n.createElement(m,l({ref:t},d))}));function b(e,t){var r=arguments,o=t&&t.mdxType;if("string"==typeof e||o){var a=r.length,l=new Array(a);l[0]=u;var c={};for(var i in t)hasOwnProperty.call(t,i)&&(c[i]=t[i]);c.originalType=e,c.mdxType="string"==typeof e?e:o,l[1]=c;for(var p=2;p<a;p++)l[p]=r[p];return n.createElement.apply(null,l)}return n.createElement.apply(null,r)}u.displayName="MDXCreateElement"},4785:function(e,t,r){"use strict";r.r(t),r.d(t,{frontMatter:function(){return l},metadata:function(){return c},toc:function(){return i},default:function(){return d}});var n=r(4034),o=r(9973),a=(r(7294),r(3905)),l={id:"db",title:"2. Db \u9759\u6001\u7c7b",sidebar_label:"2. Db \u9759\u6001\u7c7b"},c={unversionedId:"global/db",id:"global/db",isDocsHomePage:!1,title:"2. Db \u9759\u6001\u7c7b",description:"2.1 \u83b7\u53d6\u975e\u6cdb\u578b\u4ed3\u50a8",source:"@site/docs/global/db.mdx",sourceDirName:"global",slug:"/global/db",permalink:"/docs/global/db",editUrl:"https://gitee.com/dotnetchina/Furion/tree/master/handbook/docs/global/db.mdx",version:"current",lastUpdatedBy:"sourcehome",lastUpdatedAt:1618583895,formattedLastUpdatedAt:"4/16/2021",sidebar_label:"2. Db \u9759\u6001\u7c7b",frontMatter:{id:"db",title:"2. Db \u9759\u6001\u7c7b",sidebar_label:"2. Db \u9759\u6001\u7c7b"},sidebar:"global",previous:{title:"1. App \u9759\u6001\u7c7b",permalink:"/docs/global/app"},next:{title:"3. DataValidator \u9759\u6001\u7c7b",permalink:"/docs/global/datavalidator"}},i=[{value:"2.1 \u83b7\u53d6\u975e\u6cdb\u578b\u4ed3\u50a8",id:"21-\u83b7\u53d6\u975e\u6cdb\u578b\u4ed3\u50a8",children:[]},{value:"2.2 \u83b7\u53d6\u6cdb\u578b\u4ed3\u50a8",id:"22-\u83b7\u53d6\u6cdb\u578b\u4ed3\u50a8",children:[]},{value:"2.3 \u83b7\u53d6\u5e26\u5b9a\u4f4d\u5668\u6cdb\u578b\u4ed3\u50a8",id:"23-\u83b7\u53d6\u5e26\u5b9a\u4f4d\u5668\u6cdb\u578b\u4ed3\u50a8",children:[]},{value:"2.4 \u83b7\u53d6 <code>Sql</code> \u4ed3\u50a8",id:"24-\u83b7\u53d6-sql-\u4ed3\u50a8",children:[]},{value:"2.5 \u83b7\u53d6 <code>Sql</code> \u5b9a\u4f4d\u5668\u4ed3\u50a8",id:"25-\u83b7\u53d6-sql-\u5b9a\u4f4d\u5668\u4ed3\u50a8",children:[]},{value:"2.6 \u83b7\u53d6 <code>Sql</code> \u4ee3\u7406\u5bf9\u8c61",id:"26-\u83b7\u53d6-sql-\u4ee3\u7406\u5bf9\u8c61",children:[]},{value:"2.7 \u83b7\u53d6\u9ed8\u8ba4\u6570\u636e\u5e93\u4e0a\u4e0b\u6587",id:"27-\u83b7\u53d6\u9ed8\u8ba4\u6570\u636e\u5e93\u4e0a\u4e0b\u6587",children:[]},{value:"2.8 \u83b7\u53d6\u5b9a\u4f4d\u5668\u6570\u636e\u5e93\u4e0a\u4e0b\u6587",id:"28-\u83b7\u53d6\u5b9a\u4f4d\u5668\u6570\u636e\u5e93\u4e0a\u4e0b\u6587",children:[]},{value:"2.9 \u521b\u5efa\u65b0\u7684\u9ed8\u8ba4\u6570\u636e\u5e93\u4e0a\u4e0b\u6587",id:"29-\u521b\u5efa\u65b0\u7684\u9ed8\u8ba4\u6570\u636e\u5e93\u4e0a\u4e0b\u6587",children:[]},{value:"2.10 \u521b\u5efa\u65b0\u7684\u5b9a\u4f4d\u5668\u6570\u636e\u5e93\u4e0a\u4e0b\u6587",id:"210-\u521b\u5efa\u65b0\u7684\u5b9a\u4f4d\u5668\u6570\u636e\u5e93\u4e0a\u4e0b\u6587",children:[]}],p={toc:i};function d(e){var t=e.components,r=(0,o.Z)(e,["components"]);return(0,a.kt)("wrapper",(0,n.Z)({},p,r,{components:t,mdxType:"MDXLayout"}),(0,a.kt)("h2",{id:"21-\u83b7\u53d6\u975e\u6cdb\u578b\u4ed3\u50a8"},"2.1 \u83b7\u53d6\u975e\u6cdb\u578b\u4ed3\u50a8"),(0,a.kt)("pre",null,(0,a.kt)("code",{parentName:"pre",className:"language-cs"},"var repository = Db.GetRepository();\n")),(0,a.kt)("h2",{id:"22-\u83b7\u53d6\u6cdb\u578b\u4ed3\u50a8"},"2.2 \u83b7\u53d6\u6cdb\u578b\u4ed3\u50a8"),(0,a.kt)("pre",null,(0,a.kt)("code",{parentName:"pre",className:"language-cs"},"var entityRepository = Db.GetRepository<TEntity>();\n")),(0,a.kt)("h2",{id:"23-\u83b7\u53d6\u5e26\u5b9a\u4f4d\u5668\u6cdb\u578b\u4ed3\u50a8"},"2.3 \u83b7\u53d6\u5e26\u5b9a\u4f4d\u5668\u6cdb\u578b\u4ed3\u50a8"),(0,a.kt)("pre",null,(0,a.kt)("code",{parentName:"pre",className:"language-cs"},"var locatorRepository = Db.GetRepository<TEntity, TDbContextLocator>();\n")),(0,a.kt)("h2",{id:"24-\u83b7\u53d6-sql-\u4ed3\u50a8"},"2.4 \u83b7\u53d6 ",(0,a.kt)("inlineCode",{parentName:"h2"},"Sql")," \u4ed3\u50a8"),(0,a.kt)("pre",null,(0,a.kt)("code",{parentName:"pre",className:"language-cs"},"var sqlRepository = Db.GetSqlRepository();\n")),(0,a.kt)("h2",{id:"25-\u83b7\u53d6-sql-\u5b9a\u4f4d\u5668\u4ed3\u50a8"},"2.5 \u83b7\u53d6 ",(0,a.kt)("inlineCode",{parentName:"h2"},"Sql")," \u5b9a\u4f4d\u5668\u4ed3\u50a8"),(0,a.kt)("pre",null,(0,a.kt)("code",{parentName:"pre",className:"language-cs"},"var sqlLocatorRepository = Db.GetSqlRepository<TDbContextLocator>();\n")),(0,a.kt)("h2",{id:"26-\u83b7\u53d6-sql-\u4ee3\u7406\u5bf9\u8c61"},"2.6 \u83b7\u53d6 ",(0,a.kt)("inlineCode",{parentName:"h2"},"Sql")," \u4ee3\u7406\u5bf9\u8c61"),(0,a.kt)("pre",null,(0,a.kt)("code",{parentName:"pre",className:"language-cs"},"var sqlProxy= Db.GetSqlProxy<TSqlDispatchProxy>();\n")),(0,a.kt)("h2",{id:"27-\u83b7\u53d6\u9ed8\u8ba4\u6570\u636e\u5e93\u4e0a\u4e0b\u6587"},"2.7 \u83b7\u53d6\u9ed8\u8ba4\u6570\u636e\u5e93\u4e0a\u4e0b\u6587"),(0,a.kt)("pre",null,(0,a.kt)("code",{parentName:"pre",className:"language-cs"},"var dbContext = Db.GetDbContext();\n")),(0,a.kt)("h2",{id:"28-\u83b7\u53d6\u5b9a\u4f4d\u5668\u6570\u636e\u5e93\u4e0a\u4e0b\u6587"},"2.8 \u83b7\u53d6\u5b9a\u4f4d\u5668\u6570\u636e\u5e93\u4e0a\u4e0b\u6587"),(0,a.kt)("pre",null,(0,a.kt)("code",{parentName:"pre",className:"language-cs"},"var locatorDbContext = Db.GetDbContext<TDbContextLocator>();\nvar locatorDbContext2 = Db.GetDbContext(typeof(TDbContextLocator));\n")),(0,a.kt)("h2",{id:"29-\u521b\u5efa\u65b0\u7684\u9ed8\u8ba4\u6570\u636e\u5e93\u4e0a\u4e0b\u6587"},"2.9 \u521b\u5efa\u65b0\u7684\u9ed8\u8ba4\u6570\u636e\u5e93\u4e0a\u4e0b\u6587"),(0,a.kt)("pre",null,(0,a.kt)("code",{parentName:"pre",className:"language-cs"},"var dbContext = Db.GetNewDbContext();\n")),(0,a.kt)("h2",{id:"210-\u521b\u5efa\u65b0\u7684\u5b9a\u4f4d\u5668\u6570\u636e\u5e93\u4e0a\u4e0b\u6587"},"2.10 \u521b\u5efa\u65b0\u7684\u5b9a\u4f4d\u5668\u6570\u636e\u5e93\u4e0a\u4e0b\u6587"),(0,a.kt)("pre",null,(0,a.kt)("code",{parentName:"pre",className:"language-cs"},"var locatorDbContext = Db.GetNewDbContext<TDbContextLocator>();\nvar locatorDbContext2 = Db.GetNewDbContext(typeof(TDbContextLocator));\n")))}d.isMDXComponent=!0}}]);