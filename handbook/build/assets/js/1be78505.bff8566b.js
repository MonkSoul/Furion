"use strict";(self.webpackChunkfurion=self.webpackChunkfurion||[]).push([[9514,4972],{4326:function(e,t,n){n.r(t),n.d(t,{default:function(){return Ae}});var a=n(7294),r=n(4334),l=n(833),o=n(5281),i=n(4575),c=n(3320),s=n(4477),d=n(1116),m=n(5571),u=n(5999),p=n(2466),b=n(5936);var f="backToTopButton_sjWU",v="backToTopButtonShow_xfvO";function h(){var e=function(e){var t=e.threshold,n=(0,a.useState)(!1),r=n[0],l=n[1],o=(0,a.useRef)(!1),i=(0,p.Ct)(),c=i.startScroll,s=i.cancelScroll;return(0,p.RF)((function(e,n){var a=e.scrollY,r=null==n?void 0:n.scrollY;r&&(o.current?o.current=!1:a>=r?(s(),l(!1)):a<t?l(!1):a+window.innerHeight<document.documentElement.scrollHeight&&l(!0))})),(0,b.S)((function(e){e.location.hash&&(o.current=!0,l(!1))})),{shown:r,scrollToTop:function(){return c(0)}}}({threshold:300}),t=e.shown,n=e.scrollToTop;return a.createElement("button",{"aria-label":(0,u.I)({id:"theme.BackToTopButton.buttonAriaLabel",message:"Scroll back to top",description:"The ARIA label for the back to top button"}),className:(0,r.Z)("clean-btn",o.k.common.backToTopButton,f,t&&v),type:"button",onClick:n})}var E=n(6775),g=n(7524),k=n(6668),_=n(4996),C=n(3117);function y(e){return a.createElement("svg",(0,C.Z)({width:"20",height:"20","aria-hidden":"true"},e),a.createElement("g",{fill:"#7a7a7a"},a.createElement("path",{d:"M9.992 10.023c0 .2-.062.399-.172.547l-4.996 7.492a.982.982 0 01-.828.454H1c-.55 0-1-.453-1-1 0-.2.059-.403.168-.551l4.629-6.942L.168 3.078A.939.939 0 010 2.528c0-.548.45-.997 1-.997h2.996c.352 0 .649.18.828.45L9.82 9.472c.11.148.172.347.172.55zm0 0"}),a.createElement("path",{d:"M19.98 10.023c0 .2-.058.399-.168.547l-4.996 7.492a.987.987 0 01-.828.454h-3c-.547 0-.996-.453-.996-1 0-.2.059-.403.168-.551l4.625-6.942-4.625-6.945a.939.939 0 01-.168-.55 1 1 0 01.996-.997h3c.348 0 .649.18.828.45l4.996 7.492c.11.148.168.347.168.55zm0 0"})))}var I="collapseSidebarButton_PEFL",S="collapseSidebarButtonIcon_kv0_";function N(e){var t=e.onClick;return a.createElement("button",{type:"button",title:(0,u.I)({id:"theme.docs.sidebar.collapseButtonTitle",message:"Collapse sidebar",description:"The title attribute for collapse button of doc sidebar"}),"aria-label":(0,u.I)({id:"theme.docs.sidebar.collapseButtonAriaLabel",message:"Collapse sidebar",description:"The title attribute for collapse button of doc sidebar"}),className:(0,r.Z)("button button--secondary button--outline",I),onClick:t},a.createElement(y,{className:S}))}var Z=n(9689),x=n(102),T=n(4700),L=Symbol("EmptyContext"),w=a.createContext(L);function B(e){var t=e.children,n=(0,a.useState)(null),r=n[0],l=n[1],o=(0,a.useMemo)((function(){return{expandedItem:r,setExpandedItem:l}}),[r]);return a.createElement(w.Provider,{value:o},t)}var F=n(6043),A=n(8596),P=n(9960),M=n(2389),H=["item","onItemClick","activePath","level","index"];function z(e){var t=e.categoryLabel,n=e.onClick;return a.createElement("button",{"aria-label":(0,u.I)({id:"theme.DocSidebarItem.toggleCollapsedCategoryAriaLabel",message:"Toggle the collapsible sidebar category '{label}'",description:"The ARIA label to toggle the collapsible sidebar category"},{label:t}),type:"button",className:"clean-btn menu__caret",onClick:n})}function W(e){var t=e.item,n=e.onItemClick,l=e.activePath,c=e.level,s=e.index,d=(0,x.Z)(e,H),m=t.items,u=t.label,p=t.collapsible,b=t.className,f=t.href,v=(0,k.L)().docs.sidebar.autoCollapseCategories,h=function(e){var t=(0,M.Z)();return(0,a.useMemo)((function(){return e.href?e.href:!t&&e.collapsible?(0,i.Wl)(e):void 0}),[e,t])}(t),E=(0,i._F)(t,l),g=(0,A.Mg)(f,l),_=(0,F.u)({initialState:function(){return!!p&&(!E&&t.collapsed)}}),y=_.collapsed,I=_.setCollapsed,S=function(){var e=(0,a.useContext)(w);if(e===L)throw new T.i6("DocSidebarItemsExpandedStateProvider");return e}(),N=S.expandedItem,Z=S.setExpandedItem,B=function(e){void 0===e&&(e=!y),Z(e?null:s),I(e)};return function(e){var t=e.isActive,n=e.collapsed,r=e.updateCollapsed,l=(0,T.D9)(t);(0,a.useEffect)((function(){t&&!l&&n&&r(!1)}),[t,l,n,r])}({isActive:E,collapsed:y,updateCollapsed:B}),(0,a.useEffect)((function(){p&&N&&N!==s&&v&&I(!0)}),[p,N,s,I,v]),a.createElement("li",{className:(0,r.Z)(o.k.docs.docSidebarItemCategory,o.k.docs.docSidebarItemCategoryLevel(c),"menu__list-item",{"menu__list-item--collapsed":y},b)},a.createElement("div",{className:(0,r.Z)("menu__list-item-collapsible",{"menu__list-item-collapsible--active":g})},a.createElement(P.Z,(0,C.Z)({className:(0,r.Z)("menu__link",{"menu__link--sublist":p,"menu__link--sublist-caret":!f&&p,"menu__link--active":E}),onClick:p?function(e){null==n||n(t),f?B(!1):(e.preventDefault(),B())}:function(){null==n||n(t)},"aria-current":g?"page":void 0,"aria-expanded":p?!y:void 0,href:p?null!=h?h:"#":h},d),u),f&&p&&a.createElement(z,{categoryLabel:u,onClick:function(e){e.preventDefault(),B()}})),a.createElement(F.z,{lazy:!0,as:"ul",className:"menu__list",collapsed:y},a.createElement(X,{items:m,tabIndex:y?-1:0,onItemClick:n,activePath:l,level:c+1})))}var D=n(3919),R=n(9471),j="menuExternalLink_NmtK",U=["item","onItemClick","activePath","level","index"];function V(e){var t=e.item,n=e.onItemClick,l=e.activePath,c=e.level,s=(e.index,(0,x.Z)(e,U)),d=t.href,m=t.label,u=t.className,p=(0,i._F)(t,l),b=(0,D.Z)(d);return a.createElement("li",{className:(0,r.Z)(o.k.docs.docSidebarItemLink,o.k.docs.docSidebarItemLinkLevel(c),"menu__list-item",u),key:m},a.createElement(P.Z,(0,C.Z)({className:(0,r.Z)("menu__link",!b&&j,{"menu__link--active":p}),"aria-current":p?"page":void 0,to:d},b&&{onClick:n?function(){return n(t)}:void 0},s),m,!b&&a.createElement(R.Z,null)))}var G="menuHtmlItem_M9Kj";function K(e){var t=e.item,n=e.level,l=e.index,i=t.value,c=t.defaultStyle,s=t.className;return a.createElement("li",{className:(0,r.Z)(o.k.docs.docSidebarItemLink,o.k.docs.docSidebarItemLinkLevel(n),c&&[G,"menu__list-item"],s),key:l,dangerouslySetInnerHTML:{__html:i}})}var Y=["item"];function q(e){var t=e.item,n=(0,x.Z)(e,Y);switch(t.type){case"category":return a.createElement(W,(0,C.Z)({item:t},n));case"html":return a.createElement(K,(0,C.Z)({item:t},n));default:return a.createElement(V,(0,C.Z)({item:t},n))}}var O=["items"];function J(e){var t=e.items,n=(0,x.Z)(e,O);return a.createElement(B,null,t.map((function(e,t){return a.createElement(q,(0,C.Z)({key:t,item:e,index:t},n))})))}var X=(0,a.memo)(J),Q="menu_SIkG",$="menuWithAnnouncementBar_GW3s";function ee(e){var t=e.path,n=e.sidebar,l=e.className,i=function(){var e=(0,Z.nT)().isActive,t=(0,a.useState)(e),n=t[0],r=t[1];return(0,p.RF)((function(t){var n=t.scrollY;e&&r(0===n)}),[e]),e&&n}();return a.createElement("nav",{className:(0,r.Z)("menu thin-scrollbar",Q,i&&$,l)},a.createElement("ul",{className:(0,r.Z)(o.k.docs.docSidebarMenu,"menu__list")},a.createElement(X,{items:n,activePath:t,level:1})))}var te=n(1327),ne=n(6010),ae=[{title:"JNPF\uff1a\u57fa\u4e8e\u4ee3\u7801\u751f\u6210\u5668\u7684 .NET \u6846\u67b6",picture:"img/jnpfsoft.png",url:"https://dotnet.jnpfsoft.com/login?from=furion"}],re="sidebar_mhZE",le="sidebarWithHideableNavbar__6UL",oe="sidebarHidden__LRd",ie="sidebarLogo_F_0z";function ce(e){var t=e.path,n=e.sidebar,r=e.onCollapse,l=e.isHidden,o=(0,k.L)(),i=o.navbar.hideOnScroll,c=o.hideableSidebar;return a.createElement("div",{className:(0,ne.Z)(re,i&&le,l&&oe)},i&&a.createElement(te.Z,{tabIndex:-1,className:ie}),a.createElement(se,null),a.createElement(ee,{path:t,sidebar:n}),c&&a.createElement(N,{onClick:r}))}function se(){return a.createElement("div",{style:{margin:"0.5em",display:"block",textAlign:"center",borderBottom:"1px solid #dedede",paddingBottom:"0.2em"}},ae.map((function(e,t){var n=e.picture,r=e.url,l=e.title;return a.createElement(de,{key:r,title:l,url:r,picture:n,last:ae.length-1==t})})),a.createElement("div",null,a.createElement("a",{href:"mailto:monksoul@outlook.com",style:{color:"#723cff",fontSize:13,fontWeight:"bold"},title:"monksoul@outlook.com"},"\u6210\u4e3a\u8d5e\u52a9\u5546")))}function de(e){var t=e.picture,n=e.url,r=e.last,l=e.title;return a.createElement("a",{href:n,target:"_blank",title:l,style:{display:"block",marginBottom:r?null:"0.5em",position:"relative"}},a.createElement("img",{src:(0,_.Z)(t),style:{display:"block",width:"100%"}}),a.createElement("span",{style:{position:"absolute",display:"block",right:0,bottom:0,zIndex:10,fontSize:12,backgroundColor:"rgba(0,0,0,0.8)",padding:"0 5px"}},"\u8d5e\u52a9\u5546\u5e7f\u544a"))}var me=a.memo(ce),ue=n(3102),pe=n(2961),be=function(e){var t=e.sidebar,n=e.path,r=(0,pe.e)();return a.createElement(a.Fragment,null,a.createElement(ve,null),a.createElement("ul",{className:(0,ne.Z)(o.k.docs.docSidebarMenu,"menu__list")},a.createElement(X,{items:t,activePath:n,onItemClick:function(e){"category"===e.type&&e.href&&r.toggle(),"link"===e.type&&r.toggle()},level:1})))};function fe(e){return a.createElement(ue.Zo,{component:be,props:e})}function ve(){return a.createElement("div",{style:{margin:"0.5em",marginTop:"0",display:"block",textAlign:"center",borderBottom:"1px solid #dedede",paddingBottom:"0.2em"}},ae.map((function(e,t){var n=e.picture,r=e.url,l=e.title;return a.createElement(he,{key:r,title:l,url:r,picture:n,last:ae.length-1==t})})),a.createElement("div",null,a.createElement("a",{href:"mailto:monksoul@outlook.com",style:{color:"#723cff",fontSize:13,fontWeight:"bold"},title:"monksoul@outlook.com"},"\u6210\u4e3a\u8d5e\u52a9\u5546")))}function he(e){var t=e.picture,n=e.url,r=e.last,l=e.title;return a.createElement("a",{href:n,target:"_blank",title:l,style:{display:"block",marginBottom:r?null:"0.5em",position:"relative"}},a.createElement("img",{src:(0,_.Z)(t),style:{display:"block",width:"100%"}}),a.createElement("span",{style:{position:"absolute",display:"block",right:0,bottom:0,zIndex:10,fontSize:12,backgroundColor:"rgba(0,0,0,0.8)",padding:"0 5px"}},"\u8d5e\u52a9\u5546\u5e7f\u544a"))}var Ee=a.memo(fe);function ge(e){var t=(0,g.i)(),n="desktop"===t||"ssr"===t,r="mobile"===t;return a.createElement(a.Fragment,null,n&&a.createElement(me,e),r&&a.createElement(Ee,e))}var ke="expandButton_m80_",_e="expandButtonIcon_BlDH";function Ce(e){var t=e.toggleSidebar;return a.createElement("div",{className:ke,title:(0,u.I)({id:"theme.docs.sidebar.expandButtonTitle",message:"Expand sidebar",description:"The ARIA label and title attribute for expand button of doc sidebar"}),"aria-label":(0,u.I)({id:"theme.docs.sidebar.expandButtonAriaLabel",message:"Expand sidebar",description:"The ARIA label and title attribute for expand button of doc sidebar"}),tabIndex:0,role:"button",onKeyDown:t,onClick:t},a.createElement(y,{className:_e}))}var ye="docSidebarContainer_b6E3",Ie="docSidebarContainerHidden_b3ry";function Se(e){var t,n=e.children,r=(0,d.V)();return a.createElement(a.Fragment,{key:null!=(t=null==r?void 0:r.name)?t:"noSidebar"},n)}function Ne(e){var t=e.sidebar,n=e.hiddenSidebarContainer,l=e.setHiddenSidebarContainer,i=(0,E.TH)().pathname,c=(0,a.useState)(!1),s=c[0],d=c[1],m=(0,a.useCallback)((function(){s&&d(!1),l((function(e){return!e}))}),[l,s]);return a.createElement("aside",{className:(0,r.Z)(o.k.docs.docSidebarContainer,ye,n&&Ie),onTransitionEnd:function(e){e.currentTarget.classList.contains(ye)&&n&&d(!0)}},a.createElement(Se,null,a.createElement(ge,{sidebar:t,path:i,onCollapse:m,isHidden:s})),s&&a.createElement(Ce,{toggleSidebar:m}))}var Ze={docMainContainer:"docMainContainer_gTbr",docMainContainerEnhanced:"docMainContainerEnhanced_Uz_u",docItemWrapperEnhanced:"docItemWrapperEnhanced_czyv"};function xe(e){var t=e.hiddenSidebarContainer,n=e.children,l=(0,d.V)();return a.createElement("main",{className:(0,r.Z)(Ze.docMainContainer,(t||!l)&&Ze.docMainContainerEnhanced)},a.createElement("div",{className:(0,r.Z)("container padding-top--md padding-bottom--lg",Ze.docItemWrapper,t&&Ze.docItemWrapperEnhanced)},n))}var Te="docPage__5DB",Le="docsWrapper_BCFX";function we(e){var t=e.children,n=(0,d.V)(),r=(0,a.useState)(!1),l=r[0],o=r[1];return a.createElement(m.Z,{wrapperClassName:Le},a.createElement(h,null),a.createElement("div",{className:Te},n&&a.createElement(Ne,{sidebar:n.items,hiddenSidebarContainer:l,setHiddenSidebarContainer:o}),a.createElement(xe,{hiddenSidebarContainer:l},t)))}var Be=n(4972),Fe=n(197);function Ae(e){var t=e.versionMetadata,n=(0,i.hI)(e);if(!n)return a.createElement(Be.default,null);var m=n.docElement,u=n.sidebarName,p=n.sidebarItems;return a.createElement(a.Fragment,null,a.createElement(Fe.Z,{version:t.version,tag:(0,c.os)(t.pluginId,t.version)}),a.createElement(l.FG,{className:(0,r.Z)(o.k.wrapper.docsPages,o.k.page.docsDocPage,e.versionMetadata.className)},a.createElement(s.q,{version:t},a.createElement(d.b,{name:u,items:p},a.createElement(we,null,m)))))}},4972:function(e,t,n){n.r(t),n.d(t,{default:function(){return i}});var a=n(7294),r=n(5999),l=n(833),o=n(5571);function i(){return a.createElement(a.Fragment,null,a.createElement(l.d,{title:(0,r.I)({id:"theme.NotFound.title",message:"Page Not Found"})}),a.createElement(o.Z,null,a.createElement("main",{className:"container margin-vert--xl"},a.createElement("div",{className:"row"},a.createElement("div",{className:"col col--6 col--offset-3"},a.createElement("h1",{className:"hero__title"},a.createElement(r.Z,{id:"theme.NotFound.title",description:"The title of the 404 page"},"Page Not Found")),a.createElement("p",null,a.createElement(r.Z,{id:"theme.NotFound.p1",description:"The first paragraph of the 404 page"},"We could not find what you were looking for.")),a.createElement("p",null,a.createElement(r.Z,{id:"theme.NotFound.p2",description:"The 2nd paragraph of the 404 page"},"Please contact the owner of the site that linked you to the original URL and let them know their link is broken.")))))))}},4477:function(e,t,n){n.d(t,{E:function(){return i},q:function(){return o}});var a=n(7294),r=n(4700),l=a.createContext(null);function o(e){var t=e.children,n=e.version;return a.createElement(l.Provider,{value:n},t)}function i(){var e=(0,a.useContext)(l);if(null===e)throw new r.i6("DocsVersionProvider");return e}}}]);