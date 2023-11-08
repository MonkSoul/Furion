import { useThemeConfig } from "@docusaurus/theme-common";
import CollapseButton from "@theme/DocSidebar/Desktop/CollapseButton";
import Content from "@theme/DocSidebar/Desktop/Content";
import Logo from "@theme/Logo";
import clsx from "clsx";
import React, { useContext } from "react";
import Assistance from "../../../components/Assistance";
import Donate from "../../../components/Donate";
import GlobalContext from "../../../components/GlobalContext";
import { Sponsor, SponsorItem, closeStyle } from "../../../components/Sponsor";
import sponsors from "../../../data/sponsor";
import styles from "./styles.module.css";

function DocSidebarDesktop({ path, sidebar, onCollapse, isHidden }) {
  const {
    navbar: { hideOnScroll },
    docs: {
      sidebar: { hideable },
    },
  } = useThemeConfig();
  const { adv, setAdv } = useContext(GlobalContext);
  const sponsor = sponsors.find((u) => u.id == 100);

  return (
    <div
      className={clsx(
        styles.sidebar,
        hideOnScroll && styles.sidebarWithHideableNavbar,
        isHidden && styles.sidebarHidden
      )}
    >
      {hideOnScroll && <Logo tabIndex={-1} className={styles.sidebarLogo} />}
      <Assistance />
      {adv ? (
        <>
          <Sponsor />
          <span
            style={closeStyle}
            onClick={() => setAdv((s) => !s)}
            title="关闭所有赞助商广告"
          >
            收
          </span>
        </>
      ) : (
        <Donate style={{ margin: "0.5em", border: "2px solid #ffb02e" }} />
      )}

      <Content path={path} sidebar={sidebar} />
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
      {hideable && <CollapseButton onClick={onCollapse} />}
    </div>
  );
}

export default React.memo(DocSidebarDesktop);
