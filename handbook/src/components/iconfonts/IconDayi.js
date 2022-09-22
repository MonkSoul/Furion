/* eslint-disable */

import React from 'react';
import { getIconColor } from './helper';

const DEFAULT_STYLE = {
  display: 'block',
};

const IconDayi = ({ size, color, style: _style, ...rest }) => {
  const style = _style ? { ...DEFAULT_STYLE, ..._style } : DEFAULT_STYLE;

  return (
    <svg viewBox="0 0 1024 1024" width={size + 'px'} height={size + 'px'} style={style} {...rest}>
      <path
        d="M143.872 768a51.2 51.2 0 0 1-15.36-2.56 51.2 51.2 0 0 1-35.328-51.2V283.136a148.992 148.992 0 0 1 141.824-153.6h450.56a148.992 148.992 0 0 1 141.824 153.6V512a148.992 148.992 0 0 1-141.824 153.6H244.224l-60.928 80.896a51.2 51.2 0 0 1-39.424 21.504zM235.008 180.224a97.792 97.792 0 0 0-90.624 102.4v430.592L218.624 614.4h466.944a97.792 97.792 0 0 0 90.624-102.4V283.136a97.792 97.792 0 0 0-90.624-102.4z"
        fill={getIconColor(color, 0, '#333333')}
      />
      <path
        d="M880.128 875.52a51.2 51.2 0 0 1-39.424-20.48l-60.928-80.896h-243.2a25.6 25.6 0 0 1 0-51.2h268.8l76.288 102.4v-295.936a25.6 25.6 0 0 1 25.6-25.6 25.6 25.6 0 0 1 25.6 25.6v293.888a51.2 51.2 0 0 1-51.2 51.2z"
        fill={getIconColor(color, 1, '#333333')}
      />
    </svg>
  );
};

IconDayi.defaultProps = {
  size: 18,
};

export default IconDayi;
