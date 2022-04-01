/* eslint-disable */

import { SVGAttributes, FunctionComponent } from 'react';
export { default as IconFuwu } from './IconFuwu';
export { default as IconDown } from './IconDown';
export { default as IconUp } from './IconUp';

interface Props extends Omit<SVGAttributes<SVGElement>, 'color'> {
  name: 'fuwu' | 'down' | 'up';
  size?: number;
  color?: string | string[];
}

declare const IconFont: FunctionComponent<Props>;

export default IconFont;
