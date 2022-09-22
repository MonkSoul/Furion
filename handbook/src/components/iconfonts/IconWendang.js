/* eslint-disable */

import React from 'react';
import { getIconColor } from './helper';

const DEFAULT_STYLE = {
  display: 'block',
};

const IconWendang = ({ size, color, style: _style, ...rest }) => {
  const style = _style ? { ...DEFAULT_STYLE, ..._style } : DEFAULT_STYLE;

  return (
    <svg viewBox="0 0 1024 1024" width={size + 'px'} height={size + 'px'} style={style} {...rest}>
      <path
        d="M302 332a30 30 0 1 1 0-60h420a30 30 0 0 1 0 60H302zM302 542a30 30 0 0 1 0-60h420a30 30 0 0 1 0 60H302zM302 752a30 30 0 0 1 0-60h120a30 30 0 0 1 0 60H302z"
        fill={getIconColor(color, 0, '#333333')}
      />
      <path
        d="M789.47 784.1a30 30 0 0 1 39.36 45.3l-144.24 125.25a30 30 0 0 1-19.68 7.35H214.85C163.4 962 122 919.46 122 867.38V156.62C122 104.54 163.4 62 214.85 62h594.3C860.6 62 902 104.54 902 156.62v529.05a30 30 0 1 1-60 0V156.62C842 137.3 827.09 122 809.15 122H214.85C196.91 122 182 137.3 182 156.62v710.76C182 886.7 196.91 902 214.85 902h438.84l135.78-117.9z"
        fill={getIconColor(color, 1, '#333333')}
      />
      <path
        d="M692 931.19a30 30 0 1 1-60 0v-174.6C632 704.57 673.4 662 724.85 662h147.78a30 30 0 0 1 0 60h-147.78c-17.94 0-32.85 15.3-32.85 34.62v174.6z"
        fill={getIconColor(color, 2, '#333333')}
      />
    </svg>
  );
};

IconWendang.defaultProps = {
  size: 18,
};

export default IconWendang;
