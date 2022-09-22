/* eslint-disable */

import React from 'react';
import { getIconColor } from './helper';

const DEFAULT_STYLE = {
  display: 'block',
};

const IconXinzeng = ({ size, color, style: _style, ...rest }) => {
  const style = _style ? { ...DEFAULT_STYLE, ..._style } : DEFAULT_STYLE;

  return (
    <svg viewBox="0 0 1024 1024" width={size + 'px'} height={size + 'px'} style={style} {...rest}>
      <path
        d="M512 71.68c-242.688 0-440.32 197.632-440.32 440.32s197.632 440.32 440.32 440.32 440.32-197.632 440.32-440.32-197.632-440.32-440.32-440.32z m0 819.2c-208.896 0-378.88-169.984-378.88-378.88s169.984-378.88 378.88-378.88 378.88 169.984 378.88 378.88-169.984 378.88-378.88 378.88z"
        fill={getIconColor(color, 0, '#333333')}
      />
      <path
        d="M542.72 261.12H481.28v220.16H261.12v61.44h220.16v220.16h61.44v-220.16h220.16V481.28h-220.16z"
        fill={getIconColor(color, 1, '#333333')}
      />
    </svg>
  );
};

IconXinzeng.defaultProps = {
  size: 18,
};

export default IconXinzeng;
