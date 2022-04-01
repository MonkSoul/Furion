/* eslint-disable */

import React from 'react';
import IconFuwu from './IconFuwu';
import IconDown from './IconDown';
import IconUp from './IconUp';
export { default as IconFuwu } from './IconFuwu';
export { default as IconDown } from './IconDown';
export { default as IconUp } from './IconUp';

const IconFont = ({ name, ...rest }) => {
  switch (name) {
    case 'fuwu':
      return <IconFuwu {...rest} />;
    case 'down':
      return <IconDown {...rest} />;
    case 'up':
      return <IconUp {...rest} />;

  }

  return null;
};

export default IconFont;
