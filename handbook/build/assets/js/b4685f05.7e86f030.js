"use strict";(self.webpackChunkfurion=self.webpackChunkfurion||[]).push([[9138],{3905:function(e,n,t){t.d(n,{Zo:function(){return p},kt:function(){return m}});var i=t(7294);function r(e,n,t){return n in e?Object.defineProperty(e,n,{value:t,enumerable:!0,configurable:!0,writable:!0}):e[n]=t,e}function a(e,n){var t=Object.keys(e);if(Object.getOwnPropertySymbols){var i=Object.getOwnPropertySymbols(e);n&&(i=i.filter((function(n){return Object.getOwnPropertyDescriptor(e,n).enumerable}))),t.push.apply(t,i)}return t}function o(e){for(var n=1;n<arguments.length;n++){var t=null!=arguments[n]?arguments[n]:{};n%2?a(Object(t),!0).forEach((function(n){r(e,n,t[n])})):Object.getOwnPropertyDescriptors?Object.defineProperties(e,Object.getOwnPropertyDescriptors(t)):a(Object(t)).forEach((function(n){Object.defineProperty(e,n,Object.getOwnPropertyDescriptor(t,n))}))}return e}function s(e,n){if(null==e)return{};var t,i,r=function(e,n){if(null==e)return{};var t,i,r={},a=Object.keys(e);for(i=0;i<a.length;i++)t=a[i],n.indexOf(t)>=0||(r[t]=e[t]);return r}(e,n);if(Object.getOwnPropertySymbols){var a=Object.getOwnPropertySymbols(e);for(i=0;i<a.length;i++)t=a[i],n.indexOf(t)>=0||Object.prototype.propertyIsEnumerable.call(e,t)&&(r[t]=e[t])}return r}var l=i.createContext({}),c=function(e){var n=i.useContext(l),t=n;return e&&(t="function"==typeof e?e(n):o(o({},n),e)),t},p=function(e){var n=c(e.components);return i.createElement(l.Provider,{value:n},e.children)},d={inlineCode:"code",wrapper:function(e){var n=e.children;return i.createElement(i.Fragment,{},n)}},u=i.forwardRef((function(e,n){var t=e.components,r=e.mdxType,a=e.originalType,l=e.parentName,p=s(e,["components","mdxType","originalType","parentName"]),u=c(t),m=r,v=u["".concat(l,".").concat(m)]||u[m]||d[m]||a;return t?i.createElement(v,o(o({ref:n},p),{},{components:t})):i.createElement(v,o({ref:n},p))}));function m(e,n){var t=arguments,r=n&&n.mdxType;if("string"==typeof e||r){var a=t.length,o=new Array(a);o[0]=u;var s={};for(var l in n)hasOwnProperty.call(n,l)&&(s[l]=n[l]);s.originalType=e,s.mdxType="string"==typeof e?e:r,o[1]=s;for(var c=2;c<a;c++)o[c]=t[c];return i.createElement.apply(null,o)}return i.createElement.apply(null,t)}u.displayName="MDXCreateElement"},2928:function(e,n,t){t.r(n),t.d(n,{assets:function(){return d},contentTitle:function(){return c},default:function(){return v},frontMatter:function(){return l},metadata:function(){return p},toc:function(){return u}});var i=t(7462),r=t(3366),a=(t(7294),t(3905)),o=t(4996),s=["components"],l={id:"sensitive-detection",title:"30. \u8131\u654f\u5904\u7406",sidebar_label:"30. \u8131\u654f\u5904\u7406"},c=void 0,p={unversionedId:"sensitive-detection",id:"sensitive-detection",title:"30. \u8131\u654f\u5904\u7406",description:"\u4ee5\u4e0b\u5185\u5bb9\u4ec5\u9650 Furion 2.4.4 + \u7248\u672c\u4f7f\u7528\u3002",source:"@site/docs/sensitive-detection.mdx",sourceDirName:".",slug:"/sensitive-detection",permalink:"/furion/docs/sensitive-detection",editUrl:"https://gitee.com/dotnetchina/Furion/tree/net6/handbook/docs/sensitive-detection.mdx",tags:[],version:"current",lastUpdatedBy:"\u767e\u5c0f\u50e7",lastUpdatedAt:1622342267,formattedLastUpdatedAt:"5/30/2021",frontMatter:{id:"sensitive-detection",title:"30. \u8131\u654f\u5904\u7406",sidebar_label:"30. \u8131\u654f\u5904\u7406"},sidebar:"docs",previous:{title:"29. \u7c98\u571f\u5bf9\u8c61",permalink:"/furion/docs/clayobj"},next:{title:"31. \u865a\u62df\u6587\u4ef6\u7cfb\u7edf\uff08\u4e0a\u4f20\u4e0b\u8f7d\uff09",permalink:"/furion/docs/file-provider"}},d={},u=[{value:"30.1 \u5173\u4e8e\u8131\u654f",id:"301-\u5173\u4e8e\u8131\u654f",level:2},{value:"30.2 \u5982\u4f55\u4f7f\u7528",id:"302-\u5982\u4f55\u4f7f\u7528",level:2},{value:"30.2.1 \u6ce8\u518c <code>\u8131\u654f\u8bcd\u6c47\u68c0\u6d4b</code> \u670d\u52a1",id:"3021-\u6ce8\u518c-\u8131\u654f\u8bcd\u6c47\u68c0\u6d4b-\u670d\u52a1",level:3},{value:"30.2.2 \u521b\u5efa <code>sensitive-words.txt</code> \u6587\u4ef6",id:"3022-\u521b\u5efa-sensitive-wordstxt-\u6587\u4ef6",level:3},{value:"30.2.3 \u4f7f\u7528\u8131\u654f\u68c0\u6d4b",id:"3023-\u4f7f\u7528\u8131\u654f\u68c0\u6d4b",level:3},{value:"30.2.3 \u8131\u654f\u8bcd\u6c47\u66ff\u6362",id:"3023-\u8131\u654f\u8bcd\u6c47\u66ff\u6362",level:3},{value:"30.3 \u81ea\u5b9a\u4e49\u8131\u654f\u8bcd\u6c47\u5904\u7406",id:"303-\u81ea\u5b9a\u4e49\u8131\u654f\u8bcd\u6c47\u5904\u7406",level:2},{value:"30.3.1 \u81ea\u5b9a\u4e49 <code>ISensitiveDetectionProvider</code> \u7a0b\u5e8f\uff0c\u5982\uff1a",id:"3031-\u81ea\u5b9a\u4e49-isensitivedetectionprovider-\u7a0b\u5e8f\u5982",level:3},{value:"30.3.2 \u6ce8\u518c\u81ea\u5b9a\u4e49\u8131\u654f\u63d0\u4f9b\u5668",id:"3032-\u6ce8\u518c\u81ea\u5b9a\u4e49\u8131\u654f\u63d0\u4f9b\u5668",level:3},{value:"30.4 \u53cd\u9988\u4e0e\u5efa\u8bae",id:"304-\u53cd\u9988\u4e0e\u5efa\u8bae",level:2}],m={toc:u};function v(e){var n=e.components,t=(0,r.Z)(e,s);return(0,a.kt)("wrapper",(0,i.Z)({},m,t,{components:n,mdxType:"MDXLayout"}),(0,a.kt)("div",{className:"admonition admonition-important alert alert--info"},(0,a.kt)("div",{parentName:"div",className:"admonition-heading"},(0,a.kt)("h5",{parentName:"div"},(0,a.kt)("span",{parentName:"h5",className:"admonition-icon"},(0,a.kt)("svg",{parentName:"span",xmlns:"http://www.w3.org/2000/svg",width:"14",height:"16",viewBox:"0 0 14 16"},(0,a.kt)("path",{parentName:"svg",fillRule:"evenodd",d:"M7 2.3c3.14 0 5.7 2.56 5.7 5.7s-2.56 5.7-5.7 5.7A5.71 5.71 0 0 1 1.3 8c0-3.14 2.56-5.7 5.7-5.7zM7 1C3.14 1 0 4.14 0 8s3.14 7 7 7 7-3.14 7-7-3.14-7-7-7zm1 3H6v5h2V4zm0 6H6v2h2v-2z"}))),"\u7248\u672c\u8bf4\u660e")),(0,a.kt)("div",{parentName:"div",className:"admonition-content"},(0,a.kt)("p",{parentName:"div"},"\u4ee5\u4e0b\u5185\u5bb9\u4ec5\u9650 ",(0,a.kt)("inlineCode",{parentName:"p"},"Furion 2.4.4 +")," \u7248\u672c\u4f7f\u7528\u3002"))),(0,a.kt)("h2",{id:"301-\u5173\u4e8e\u8131\u654f"},"30.1 \u5173\u4e8e\u8131\u654f"),(0,a.kt)("p",null,"\u5f15\u7528\u767e\u5ea6\u767e\u79d1\uff1a"),(0,a.kt)("blockquote",null,(0,a.kt)("p",{parentName:"blockquote"},"\u6570\u636e\u8131\u654f\u662f\u6307\u5bf9\u67d0\u4e9b\u654f\u611f\u4fe1\u606f\u901a\u8fc7\u8131\u654f\u89c4\u5219\u8fdb\u884c\u6570\u636e\u7684\u53d8\u5f62\uff0c\u5b9e\u73b0\u654f\u611f\u9690\u79c1\u6570\u636e\u7684\u53ef\u9760\u4fdd\u62a4\u3002\u5728\u6d89\u53ca\u5ba2\u6237\u5b89\u5168\u6570\u636e\u6216\u8005\u4e00\u4e9b\u5546\u4e1a\u6027\u654f\u611f\u6570\u636e\u7684\u60c5\u51b5\u4e0b\uff0c\u5728\u4e0d\u8fdd\u53cd\u7cfb\u7edf\u89c4\u5219\u6761\u4ef6\u4e0b\uff0c\u5bf9\u771f\u5b9e\u6570\u636e\u8fdb\u884c\u6539\u9020\u5e76\u63d0\u4f9b\u6d4b\u8bd5\u4f7f\u7528\uff0c\u5982\u8eab\u4efd\u8bc1\u53f7\u3001\u624b\u673a\u53f7\u3001\u5361\u53f7\u3001\u5ba2\u6237\u53f7\u7b49\u4e2a\u4eba\u4fe1\u606f\u90fd\u9700\u8981\u8fdb\u884c\u6570\u636e\u8131\u654f\u3002\u6570\u636e\u5b89\u5168\u6280\u672f\u4e4b\u4e00\uff0c\u6570\u636e\u5e93\u5b89\u5168\u6280\u672f\u4e3b\u8981\u5305\u62ec\uff1a\u6570\u636e\u5e93\u6f0f\u626b\u3001\u6570\u636e\u5e93\u52a0\u5bc6\u3001\u6570\u636e\u5e93\u9632\u706b\u5899\u3001\u6570\u636e\u8131\u654f\u3001\u6570\u636e\u5e93\u5b89\u5168\u5ba1\u8ba1\u7cfb\u7edf\u3002")),(0,a.kt)("p",null,"\u5728 ",(0,a.kt)("inlineCode",{parentName:"p"},"Furion")," \u7cfb\u7edf\u4e2d\uff0c",(0,a.kt)("inlineCode",{parentName:"p"},"\u8131\u654f\u5904\u7406")," \u6307\u7684\u662f\u5bf9\u4e0d\u7b26\u5408\u7cfb\u7edf\u5408\u6cd5\u8bcd\u6c47\u68c0\u6d4b\u9a8c\u8bc1\u3002"),(0,a.kt)("h2",{id:"302-\u5982\u4f55\u4f7f\u7528"},"30.2 \u5982\u4f55\u4f7f\u7528"),(0,a.kt)("p",null,(0,a.kt)("inlineCode",{parentName:"p"},"Furion")," \u6846\u67b6\u5185\u7f6e\u4e86\u4e00\u5957\u9ed8\u8ba4\u7684\u8131\u654f\u8bcd\u6c47\u8131\u654f\u5904\u7406\u673a\u5236\uff0c\u5e76\u4e14\u63d0\u4f9b\u81ea\u5b9a\u4e49\u64cd\u4f5c\u3002"),(0,a.kt)("h3",{id:"3021-\u6ce8\u518c-\u8131\u654f\u8bcd\u6c47\u68c0\u6d4b-\u670d\u52a1"},"30.2.1 \u6ce8\u518c ",(0,a.kt)("inlineCode",{parentName:"h3"},"\u8131\u654f\u8bcd\u6c47\u68c0\u6d4b")," \u670d\u52a1"),(0,a.kt)("pre",null,(0,a.kt)("code",{parentName:"pre",className:"language-cs",metastring:"{3}","{3}":!0},"public void ConfigureServices(IServiceCollection services)\n{\n    services.AddSensitiveDetection();\n}\n")),(0,a.kt)("h3",{id:"3022-\u521b\u5efa-sensitive-wordstxt-\u6587\u4ef6"},"30.2.2 \u521b\u5efa ",(0,a.kt)("inlineCode",{parentName:"h3"},"sensitive-words.txt")," \u6587\u4ef6"),(0,a.kt)("p",null,"\u5728 ",(0,a.kt)("inlineCode",{parentName:"p"},"Web")," \u542f\u52a8\u5c42\u9879\u76ee\u4e2d\u521b\u5efa ",(0,a.kt)("inlineCode",{parentName:"p"},"sensitive-words.txt")," \u6587\u4ef6\uff0c",(0,a.kt)("strong",{parentName:"p"},"\u786e\u4fdd\u91c7\u7528 ",(0,a.kt)("inlineCode",{parentName:"strong"},"UTF-8")," \u7f16\u7801\u683c\u5f0f\u4e14\u8bbe\u7f6e\u4e3a\u5d4c\u5165\u5f0f\u8d44\u6e90\uff01"),"\ud83c\udf83"),(0,a.kt)("p",null,(0,a.kt)("inlineCode",{parentName:"p"},"sensitive-words.txt")," \u5185\u5bb9\u683c\u5f0f\u4e3a\u6bcf\u4e00\u884c\u6807\u8bc6\u4e00\u4e2a\u8131\u654f\u8bcd\u6c47\uff1a"),(0,a.kt)("pre",null,(0,a.kt)("code",{parentName:"pre"},"\u574f\u4eba\n\u65e0\u8bed\n\u6eda\u5f00\n\u516b\u560e\n")),(0,a.kt)("p",null,"\u63a5\u4e0b\u6765\u8bbe\u7f6e\u4e3a\u5d4c\u5165\u5f0f\u8d44\u6e90\uff1a"),(0,a.kt)("img",{src:(0,o.Z)("img/tm.png")}),(0,a.kt)("h3",{id:"3023-\u4f7f\u7528\u8131\u654f\u68c0\u6d4b"},"30.2.3 \u4f7f\u7528\u8131\u654f\u68c0\u6d4b"),(0,a.kt)("ul",null,(0,a.kt)("li",{parentName:"ul"},(0,a.kt)("strong",{parentName:"li"},"\u5b9e\u73b0\u6570\u636e\u9a8c\u8bc1\u8131\u654f\u68c0\u6d4b ",(0,a.kt)("inlineCode",{parentName:"strong"},"[SensitiveDetection]")))),(0,a.kt)("p",null,(0,a.kt)("inlineCode",{parentName:"p"},"Furion")," \u6846\u67b6\u63d0\u4f9b\u4e86 ",(0,a.kt)("inlineCode",{parentName:"p"},"[SensitiveDetection]")," \u9a8c\u8bc1\u7279\u6027\uff0c\u53ef\u4ee5\u5bf9\u53c2\u6570\u3001\u5c5e\u6027\u8fdb\u884c\u8131\u654f\u9a8c\u8bc1\uff0c\u7528\u6cd5\u548c ",(0,a.kt)("inlineCode",{parentName:"p"},"[DataValidation]")," \u4e00\u81f4\uff0c\u5982\uff1a"),(0,a.kt)("pre",null,(0,a.kt)("code",{parentName:"pre",className:"language-cs",metastring:"{4,9}","{4,9}":!0},"// \u5728\u5c5e\u6027\u4e2d\u4f7f\u7528\npublic class Content\n{\n    [SensitiveDetection]\n    public string Text { get; set; }\n}\n\n// \u5728 \u52a8\u6001API/Controller \u4e2d\u4f7f\u7528\npublic void Test([SensitiveDetection] string text)\n{\n\n}\n")),(0,a.kt)("ul",null,(0,a.kt)("li",{parentName:"ul"},(0,a.kt)("strong",{parentName:"li"},"\u901a\u8fc7 ",(0,a.kt)("inlineCode",{parentName:"strong"},"ISensitiveDetectionProvider")," \u670d\u52a1\u4f7f\u7528"))),(0,a.kt)("p",null,(0,a.kt)("inlineCode",{parentName:"p"},"Furion")," \u6846\u67b6\u4e5f\u63d0\u4f9b\u4e86 ",(0,a.kt)("inlineCode",{parentName:"p"},"ISensitiveDetectionProvider")," \u670d\u52a1\u8fdb\u884c\u624b\u52a8\u8131\u654f\u9a8c\u8bc1\u5904\u7406\uff0c\u5982\uff1a"),(0,a.kt)("pre",null,(0,a.kt)("code",{parentName:"pre",className:"language-cs",metastring:"{4,15,25,35}","{4,15,25,35}":!0},'public class FurionService\n{\n    private readonly ISensitiveDetectionProvider _sensitiveDetectionProvider;\n    public FurionService(ISensitiveDetectionProvider sensitiveDetectionProvider)\n    {\n        _sensitiveDetectionProvider = sensitiveDetectionProvider;\n    }\n\n    /// <summary>\n    /// \u83b7\u53d6\u6240\u6709\u8131\u654f\u8bcd\u6c47\n    /// </summary>\n    /// <returns></returns>\n    public async Task<IEnumerable<string>> GetWordsAsync()\n    {\n        return await _sensitiveDetectionProvider.GetWordsAsync();\n    }\n\n    /// <summary>\n    /// \u5224\u65ad\u662f\u5426\u662f\u6b63\u5e38\u7684\u8bcd\u6c47\n    /// </summary>\n    /// <param name="text"></param>\n    /// <returns></returns>\n    public async Task<bool> VaildedAsync(string text)\n    {\n        return await _sensitiveDetectionProvider.VaildedAsync(text);\n    }\n\n    /// <summary>\n    /// \u66ff\u6362\u975e\u6b63\u5e38\u8bcd\u6c47\n    /// </summary>\n    /// <param name="text"></param>\n    /// <returns></returns>\n    public async Task<string> ReplaceAsync(string text)\n    {\n        return await _sensitiveDetectionProvider.ReplaceAsync(text, \'*\');\n    }\n}\n')),(0,a.kt)("h3",{id:"3023-\u8131\u654f\u8bcd\u6c47\u66ff\u6362"},"30.2.3 \u8131\u654f\u8bcd\u6c47\u66ff\u6362"),(0,a.kt)("p",null,(0,a.kt)("inlineCode",{parentName:"p"},"Furion")," \u6846\u67b6\u4e5f\u63d0\u4f9b\u4e86\u66ff\u6362\u8131\u654f\u8bcd\u6c47\u7684\u7279\u6027\u652f\u6301\uff0c\u5982\uff1a"),(0,a.kt)("pre",null,(0,a.kt)("code",{parentName:"pre",className:"language-cs",metastring:"{4}","{4}":!0},"// \u5728\u5c5e\u6027\u4e2d\u4f7f\u7528\npublic class Content\n{\n    [SensitiveDetection('*')]\n    public string Text { get; set; }\n}\n")),(0,a.kt)("div",{className:"admonition admonition-caution alert alert--warning"},(0,a.kt)("div",{parentName:"div",className:"admonition-heading"},(0,a.kt)("h5",{parentName:"div"},(0,a.kt)("span",{parentName:"h5",className:"admonition-icon"},(0,a.kt)("svg",{parentName:"span",xmlns:"http://www.w3.org/2000/svg",width:"16",height:"16",viewBox:"0 0 16 16"},(0,a.kt)("path",{parentName:"svg",fillRule:"evenodd",d:"M8.893 1.5c-.183-.31-.52-.5-.887-.5s-.703.19-.886.5L.138 13.499a.98.98 0 0 0 0 1.001c.193.31.53.501.886.501h13.964c.367 0 .704-.19.877-.5a1.03 1.03 0 0 0 .01-1.002L8.893 1.5zm.133 11.497H6.987v-2.003h2.039v2.003zm0-3.004H6.987V5.987h2.039v4.006z"}))),"\u7279\u522b\u6ce8\u610f")),(0,a.kt)("div",{parentName:"div",className:"admonition-content"},(0,a.kt)("p",{parentName:"div"},(0,a.kt)("inlineCode",{parentName:"p"},"Furion")," \u6846\u67b6\u76ee\u524d\u53ea\u63d0\u4f9b\u7c7b\u4e2d\u4f7f\u7528\u66ff\u6362\u7279\u6027\u652f\u6301\uff0c\u4f46\u672a\u63d0\u4f9b\u65b9\u6cd5\u5355\u4e2a\u503c\u66ff\u6362\u652f\u6301\uff0c\u5982",(0,a.kt)("strong",{parentName:"p"},"\u4ee5\u4e0b\u4ee3\u7801\u4e0d\u53d7\u652f\u6301"),"\uff1a"),(0,a.kt)("pre",{parentName:"div"},(0,a.kt)("code",{parentName:"pre",className:"language-cs",metastring:"{1}","{1}":!0},"public void Test([SensitiveDetection('*')] string text)\n{\n}\n")))),(0,a.kt)("h2",{id:"303-\u81ea\u5b9a\u4e49\u8131\u654f\u8bcd\u6c47\u5904\u7406"},"30.3 \u81ea\u5b9a\u4e49\u8131\u654f\u8bcd\u6c47\u5904\u7406"),(0,a.kt)("p",null,(0,a.kt)("inlineCode",{parentName:"p"},"Furion")," \u6846\u67b6\u9664\u4e86\u5185\u7f6e\u4e86\u4e00\u5957\u9ed8\u8ba4\u7684 ",(0,a.kt)("inlineCode",{parentName:"p"},"\u8131\u654f\u5904\u7406")," \u7a0b\u5e8f\uff0c\u4e5f\u652f\u6301\u81ea\u5b9a\u4e49\u8131\u654f\u5904\u7406\u7a0b\u5e8f\u3002"),(0,a.kt)("h3",{id:"3031-\u81ea\u5b9a\u4e49-isensitivedetectionprovider-\u7a0b\u5e8f\u5982"},"30.3.1 \u81ea\u5b9a\u4e49 ",(0,a.kt)("inlineCode",{parentName:"h3"},"ISensitiveDetectionProvider")," \u7a0b\u5e8f\uff0c\u5982\uff1a"),(0,a.kt)("pre",null,(0,a.kt)("code",{parentName:"pre",className:"language-cs",metastring:"{4,15,25,36}","{4,15,25,36}":!0},'/// <summary>\n/// \u81ea\u5b9a\u4e49\u8131\u654f\u8bcd\u6c47\u68c0\u6d4b\u5668\n/// </summary>\npublic class YourSensitiveDetectionProvider : ISensitiveDetectionProvider\n{\n    // \u652f\u6301\u6784\u9020\u51fd\u6570\u6ce8\u5165\n    public YourSensitiveDetectionProvider()\n    {\n    }\n\n    /// <summary>\n    /// \u8fd4\u56de\u6240\u6709\u8131\u654f\u8bcd\u6c47\n    /// </summary>\n    /// <returns></returns>\n    public async Task<IEnumerable<string>> GetWordsAsync()\n    {\n        // \u8fd9\u91cc\u5199\u4f60\u8131\u654f\u8bcd\u6c47\u6570\u636e\u7684\u6765\u6e90\uff08\u5982\u4ece\u6570\u636e\u5e93\u8bfb\u53d6\uff09\uff0c\u5efa\u8bae\u505a\u597d\u7f13\u5b58\u64cd\u4f5c\n    }\n\n    /// <summary>\n    /// \u5224\u65ad\u8131\u654f\u8bcd\u6c47\u662f\u5426\u6709\u6548\n    /// </summary>\n    /// <param name="text"></param>\n    /// <returns></returns>\n    public async Task<bool> VaildedAsync(string text)\n    {\n        // \u8fd9\u91cc\u5199\u4f60\u5982\u4f55\u5224\u65ad\u662f\u6b63\u5e38\u7684\u5b57\u7b26\uff0c\u8fd4\u56de true \u6b63\u5e38\uff0c\u8fd4\u56de false \u8868\u793a\u662f\u4e2a\u8131\u654f\u8bcd\u6c47\n    }\n\n    /// <summary>\n    /// \u66ff\u6362\u8131\u654f\u8bcd\u6c47\n    /// </summary>\n    /// <param name="text"></param>\n    /// <param name="transfer"></param>\n    /// <returns></returns>\n    public async Task<string> ReplaceAsync(string text, char transfer = \'*\')\n    {\n        // \u8fd9\u91cc\u5199\u4f60\u66ff\u6362\u975e\u6b63\u5e38\u5b57\u7b26\u4e3a\u6307\u5b9a\u5b57\u7b26\n    }\n}\n')),(0,a.kt)("h3",{id:"3032-\u6ce8\u518c\u81ea\u5b9a\u4e49\u8131\u654f\u63d0\u4f9b\u5668"},"30.3.2 \u6ce8\u518c\u81ea\u5b9a\u4e49\u8131\u654f\u63d0\u4f9b\u5668"),(0,a.kt)("pre",null,(0,a.kt)("code",{parentName:"pre",className:"language-cs",metastring:"{3}","{3}":!0},"public void ConfigureServices(IServiceCollection services)\n{\n    services.AddSensitiveDetection<YourSensitiveDetectionProvider>();\n}\n")),(0,a.kt)("p",null,"\u4e4b\u540e\u7cfb\u7edf\u5c06\u81ea\u52a8\u91c7\u7528\u81ea\u5b9a\u4e49\u7684\u65b9\u5f0f\u8fdb\u884c\u8131\u654f\u5904\u7406\u3002"),(0,a.kt)("h2",{id:"304-\u53cd\u9988\u4e0e\u5efa\u8bae"},"30.4 \u53cd\u9988\u4e0e\u5efa\u8bae"),(0,a.kt)("div",{className:"admonition admonition-note alert alert--secondary"},(0,a.kt)("div",{parentName:"div",className:"admonition-heading"},(0,a.kt)("h5",{parentName:"div"},(0,a.kt)("span",{parentName:"h5",className:"admonition-icon"},(0,a.kt)("svg",{parentName:"span",xmlns:"http://www.w3.org/2000/svg",width:"14",height:"16",viewBox:"0 0 14 16"},(0,a.kt)("path",{parentName:"svg",fillRule:"evenodd",d:"M6.3 5.69a.942.942 0 0 1-.28-.7c0-.28.09-.52.28-.7.19-.18.42-.28.7-.28.28 0 .52.09.7.28.18.19.28.42.28.7 0 .28-.09.52-.28.7a1 1 0 0 1-.7.3c-.28 0-.52-.11-.7-.3zM8 7.99c-.02-.25-.11-.48-.31-.69-.2-.19-.42-.3-.69-.31H6c-.27.02-.48.13-.69.31-.2.2-.3.44-.31.69h1v3c.02.27.11.5.31.69.2.2.42.31.69.31h1c.27 0 .48-.11.69-.31.2-.19.3-.42.31-.69H8V7.98v.01zM7 2.3c-3.14 0-5.7 2.54-5.7 5.68 0 3.14 2.56 5.7 5.7 5.7s5.7-2.55 5.7-5.7c0-3.15-2.56-5.69-5.7-5.69v.01zM7 .98c3.86 0 7 3.14 7 7s-3.14 7-7 7-7-3.12-7-7 3.14-7 7-7z"}))),"\u4e0e\u6211\u4eec\u4ea4\u6d41")),(0,a.kt)("div",{parentName:"div",className:"admonition-content"},(0,a.kt)("p",{parentName:"div"},"\u7ed9 Furion \u63d0 ",(0,a.kt)("a",{parentName:"p",href:"https://gitee.com/dotnetchina/Furion/issues/new?issue"},"Issue"),"\u3002"))),(0,a.kt)("hr",null))}v.isMDXComponent=!0}}]);