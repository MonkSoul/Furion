/**
 * Copyright (c) Facebook, Inc. and its affiliates.
 *
 * This source code is licensed under the MIT license found in the
 * LICENSE file in the root directory of this source tree.
 */
import React from 'react';
import clsx from 'clsx';
import TOCItems from '@theme/TOCItems';
import styles from './styles.module.css'; // Using a custom className
// This prevents TOC highlighting to highlight TOCInline/TOCCollapsible by mistake
import sponsors from '../../data/sponsor';
import useBaseUrl from "@docusaurus/useBaseUrl";

const LINK_CLASS_NAME = 'table-of-contents__link toc-highlight';
const LINK_ACTIVE_CLASS_NAME = 'table-of-contents__link--active';

function TOC({ className, ...props }) {
  return (
    <div className={clsx(styles.tableOfContents, 'thin-scrollbar', className)}>
      {/* <Sponsor /> */}
      <TOCItems
        {...props}
        linkClassName={LINK_CLASS_NAME}
        linkActiveClassName={LINK_ACTIVE_CLASS_NAME}
      />
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
    <span style={{ position: 'absolute', display: 'block', right: 0, bottom: 0, zIndex: 10, fontSize: 12, backgroundColor: 'rgba(0,0,0,0.8)', padding: '0 5px' }}>广告</span>
  </a>
}
export default TOC;
