/* eslint-disable */

import React from 'react';
import { getIconColor } from './helper';

const DEFAULT_STYLE = {
  display: 'block',
};

const IconFuwu = ({ size, color, style: _style, ...rest }) => {
  const style = _style ? { ...DEFAULT_STYLE, ..._style } : DEFAULT_STYLE;

  return (
    <svg viewBox="0 0 1024 1024" width={size + 'px'} height={size + 'px'} style={style} {...rest}>
      <path
        d="M384 896h-64v-70.4c0-15.2-10.4-28-24.8-31.2C159.2 768 64 644.8 64 496v-32h64v32c0 118.4 73.6 215.2 179.2 236 44.8 8.8 76.8 48 76.8 94.4v69.6zM704 896h-64v-70.4c0-45.6 32-85.6 76.8-94.4C822.4 711.2 896 614.4 896 496v-32h64v32c0 148.8-95.2 272-231.2 298.4-14.4 3.2-24.8 16-24.8 31.2v70.4zM512.8 640l-41.6-37.6c-147.2-133.6-244-208-244-316.8 0-88 68.8-156.8 156.8-156.8 49.6 0 97.6 23.2 128.8 60C544 152 592 128.8 641.6 128.8c88 0 156.8 68.8 156.8 156.8 0 108-96.8 183.2-244 316.8L512.8 640z"
        fill={getIconColor(color, 0, '#333333')}
      />
    </svg>
  );
};

IconFuwu.defaultProps = {
  size: 18,
};

export default IconFuwu;
