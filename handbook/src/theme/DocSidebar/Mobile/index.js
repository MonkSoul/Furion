import {
  NavbarSecondaryMenuFiller,
  ThemeClassNames,
} from "@docusaurus/theme-common";
import { useNavbarMobileSidebar } from "@docusaurus/theme-common/internal";
import DocSidebarItems from "@theme/DocSidebarItems";
import clsx from "clsx";
import React, { useContext } from "react";
import Assistance from "../../../components/Assistance";
import Donate from "../../../components/Donate";
import GlobalContext from "../../../components/GlobalContext";
import { Sponsor, SponsorItem, closeStyle } from "../../../components/Sponsor";
import sponsors from "../../../data/sponsor";

// eslint-disable-next-line react/function-component-definition
const DocSidebarMobileSecondaryMenu = ({ sidebar, path }) => {
  const mobileSidebar = useNavbarMobileSidebar();
  const { adv, setAdv } = useContext(GlobalContext);
  const sponsor = sponsors.find((u) => u.id == 100);

  return (
    <>
      <Assistance />
      {adv ? (
        <>
          <Sponsor />
          <span style={closeStyle} onClick={() => setAdv((s) => !s)}>
            收
          </span>
        </>
      ) : (
        <Donate style={{ margin: "0.5em", border: "2px solid #ffb02e" }} />
      )}
      <ul className={clsx(ThemeClassNames.docs.docSidebarMenu, "menu__list")}>
        <DocSidebarItems
          items={sidebar}
          activePath={path}
          onItemClick={(item) => {
            // Mobile sidebar should only be closed if the category has a link
            if (item.type === "category" && item.href) {
              mobileSidebar.toggle();
            }
            if (item.type === "link") {
              mobileSidebar.toggle();
            }
          }}
          level={1}
        />
      </ul>
      {adv && sponsor && (
        <div>
          <SponsorItem
            key={sponsor.url}
            title={sponsor.title}
            url={sponsor.url}
            picture={"img/xxyd2.jpeg"}
            top={true}
            last={false}
            tag={sponsor.tag}
            style={{ marginBottom: 0 }}
          />
        </div>
      )}
    </>
  );
};
function DocSidebarMobile(props) {
  return (
    <NavbarSecondaryMenuFiller
      component={DocSidebarMobileSecondaryMenu}
      props={props}
    />
  );
}

export default React.memo(DocSidebarMobile);
