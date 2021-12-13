/**
 * Copyright (c) Facebook, Inc. and its affiliates.
 *
 * This source code is licensed under the MIT license found in the
 * LICENSE file in the root directory of this source tree.
 */
import React, { useState } from 'react';
import clsx from 'clsx';
import {
  useThemeConfig,
  useAnnouncementBar,
  MobileSecondaryMenuFiller,
  ThemeClassNames,
  useScrollPosition,
} from '@docusaurus/theme-common';
import useWindowSize from '@theme/hooks/useWindowSize';
import Logo from '@theme/Logo';
import IconArrow from '@theme/IconArrow';
import { translate } from '@docusaurus/Translate';
import { DocSidebarItems } from '@theme/DocSidebarItem';
import styles from './styles.module.css';
import useBaseUrl from "@docusaurus/useBaseUrl";
import sponsors from '../../data/sponsor';

function useShowAnnouncementBar() {
  const { isActive } = useAnnouncementBar();
  const [showAnnouncementBar, setShowAnnouncementBar] = useState(isActive);
  useScrollPosition(
    ({ scrollY }) => {
      if (isActive) {
        setShowAnnouncementBar(scrollY === 0);
      }
    },
    [isActive],
  );
  return isActive && showAnnouncementBar;
}

function HideableSidebarButton({ onClick }) {
  return (
    <button
      type="button"
      title={translate({
        id: 'theme.docs.sidebar.collapseButtonTitle',
        message: 'Collapse sidebar',
        description: 'The title attribute for collapse button of doc sidebar',
      })}
      aria-label={translate({
        id: 'theme.docs.sidebar.collapseButtonAriaLabel',
        message: 'Collapse sidebar',
        description: 'The title attribute for collapse button of doc sidebar',
      })}
      className={clsx(
        'button button--secondary button--outline',
        styles.collapseSidebarButton,
      )}
      onClick={onClick}>
      <IconArrow className={styles.collapseSidebarButtonIcon} />
    </button>
  );
}

function DocSidebarDesktop({ path, sidebar, onCollapse, isHidden }) {
  const showAnnouncementBar = useShowAnnouncementBar();
  const {
    navbar: { hideOnScroll },
    hideableSidebar,
  } = useThemeConfig();
  return (
    <div
      className={clsx(styles.sidebar, {
        [styles.sidebarWithHideableNavbar]: hideOnScroll,
        [styles.sidebarHidden]: isHidden,
      })}>
      {hideOnScroll && <Logo tabIndex={-1} className={styles.sidebarLogo} />}
      <nav
        className={clsx('menu thin-scrollbar', styles.menu, {
          [styles.menuWithAnnouncementBar]: showAnnouncementBar,
        })}>
        <Sponsor />
        <ul className={clsx(ThemeClassNames.docs.docSidebarMenu, 'menu__list')}>
          <DocSidebarItems items={sidebar} activePath={path} level={1} />
        </ul>
      </nav>
      {hideableSidebar && <HideableSidebarButton onClick={onCollapse} />}
    </div>
  );
}

function Sponsor() {
  return <div style={{ margin: '0.5em', marginTop: '0', display: 'block', textAlign: 'center', borderBottom: '1px solid #dedede', paddingBottom: '0.2em' }}>
    {sponsors.map(({ picture, url, title }, i) => <SponsorItem key={url} title={title} url={url} picture={picture} last={sponsors.length - 1 == i} />)}
    <div><a href="mailto:monksoul@outlook.com" style={{ color: '#723cff', fontSize: 13, fontWeight: 'bold' }} title="monksoul@outlook.com">成为赞助商</a></div>
  </div>
}

function SponsorItem({ picture, url, last, title }) {
  return <a href={url} target="_blank" title={title} style={{ display: 'block', marginBottom: last ? null : '0.5em', position: 'relative' }}><img src={useBaseUrl(picture)} style={{ display: 'block', width: '100%' }} />
    <span style={{ position: 'absolute', display: 'block', right: 0, bottom: 0, zIndex: 10, fontSize: 12, backgroundColor: 'rgba(0,0,0,0.8)', padding: '0 5px' }}>赞助商广告</span>
  </a>
}

const DocSidebarMobileSecondaryMenu = ({ toggleSidebar, sidebar, path }) => {
  return <>
    <Sponsor />
    <ul className={clsx(ThemeClassNames.docs.docSidebarMenu, 'menu__list')}>
      <DocSidebarItems
        items={sidebar}
        activePath={path}
        onItemClick={() => toggleSidebar()}
        level={1}
      />
    </ul>
  </>;
};

function DocSidebarMobile(props) {
  return (
    <MobileSecondaryMenuFiller
      component={DocSidebarMobileSecondaryMenu}
      props={props}
    />
  );
}

const DocSidebarDesktopMemo = React.memo(DocSidebarDesktop);
const DocSidebarMobileMemo = React.memo(DocSidebarMobile);
export default function DocSidebar(props) {
  const windowSize = useWindowSize(); // Desktop sidebar visible on hydration: need SSR rendering

  const shouldRenderSidebarDesktop =
    windowSize === 'desktop' || windowSize === 'ssr'; // Mobile sidebar not visible on hydration: can avoid SSR rendering

  const shouldRenderSidebarMobile = windowSize === 'mobile';
  return (
    <>
      {shouldRenderSidebarDesktop && <DocSidebarDesktopMemo {...props} />}
      {shouldRenderSidebarMobile && <DocSidebarMobileMemo {...props} />}
    </>
  );
}
