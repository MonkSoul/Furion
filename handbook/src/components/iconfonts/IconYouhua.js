/* eslint-disable */

import React from 'react';
import { getIconColor } from './helper';

const DEFAULT_STYLE = {
  display: 'block',
};

const IconYouhua = ({ size, color, style: _style, ...rest }) => {
  const style = _style ? { ...DEFAULT_STYLE, ..._style } : DEFAULT_STYLE;

  return (
    <svg viewBox="0 0 1024 1024" width={size + 'px'} height={size + 'px'} style={style} {...rest}>
      <path
        d="M856.4 292.8c-63.3-63.6-126.6-127.1-190.2-190.3-15.3-15.2-32.7-16.1-48.1-0.8-64.3 63.6-128.1 127.6-191.8 191.9-14 14.2-16.3 31.6-1.7 46 14.8 14.7 31.5 10.6 46.1-2.7 5.1-4.6 9.8-9.7 14.7-14.7 39.2-39.7 78.5-79.5 122.8-124.4 0 170 3 332.2-1.1 494-2.4 96.4-91.2 174.6-187.4 176.6-110.6 2.3-198.6-84.4-199-197.4-0.6-136.3-0.2-272.6-0.1-408.9 0-21.8-7.9-37.4-31.2-39.9-18.9-2-33.2 13.2-33.1 37.5 0 145.8-3.4 291.7 2.4 437.2 6 152.1 160.4 263.5 309.5 230.5C591.8 900 672.8 797.2 673.6 664.6c0.8-144 0.2-288.1 0.2-432.1v-33.3c11.2 10.2 17.6 15.4 23.3 21.3 38.5 38.4 76.7 77 115.3 115.2 14.8 14.6 32.2 19.2 47.8 2.9 13.8-14.8 10.3-31.7-3.8-45.8z"
        fill={getIconColor(color, 0, '#333333')}
      />
    </svg>
  );
};

IconYouhua.defaultProps = {
  size: 18,
};

export default IconYouhua;
