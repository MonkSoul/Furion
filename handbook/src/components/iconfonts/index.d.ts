/* eslint-disable */

import { SVGAttributes, FunctionComponent } from 'react';
export { default as IconYouhua } from './IconYouhua';
export { default as IconDayi } from './IconDayi';
export { default as IconShengji } from './IconShengji';
export { default as IconTiaozheng } from './IconTiaozheng';
export { default as IconGengxin } from './IconGengxin';
export { default as IconWendang } from './IconWendang';
export { default as IconShanchu } from './IconShanchu';
export { default as IconBug } from './IconBug';
export { default as IconXinzeng } from './IconXinzeng';
export { default as IconFuwu } from './IconFuwu';
export { default as IconDown } from './IconDown';
export { default as IconUp } from './IconUp';

interface Props extends Omit<SVGAttributes<SVGElement>, 'color'> {
  name: 'youhua' | 'dayi' | 'shengji' | 'tiaozheng' | 'gengxin' | 'wendang' | 'shanchu' | 'bug' | 'xinzeng' | 'fuwu' | 'down' | 'up';
  size?: number;
  color?: string | string[];
}

declare const IconFont: FunctionComponent<Props>;

export default IconFont;
