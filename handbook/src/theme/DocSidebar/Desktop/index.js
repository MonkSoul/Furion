import { useThemeConfig } from "@docusaurus/theme-common";
import CollapseButton from "@theme/DocSidebar/Desktop/CollapseButton";
import Content from "@theme/DocSidebar/Desktop/Content";
import Logo from "@theme/Logo";
import clsx from "clsx";
import React, { useState } from "react";
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
  const [show, setShow] = useState(true);
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
      {show && (
        <>
          <Sponsor />
          <span style={closeStyle} onClick={() => setShow((s) => !s)}>
            收
          </span>
        </>
      )}
      <Content path={path} sidebar={sidebar} />
      {show && sponsor && (
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
