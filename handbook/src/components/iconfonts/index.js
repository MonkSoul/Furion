/* eslint-disable */

import React from 'react';
import IconYouhua from './IconYouhua';
import IconDayi from './IconDayi';
import IconShengji from './IconShengji';
import IconTiaozheng from './IconTiaozheng';
import IconGengxin from './IconGengxin';
import IconWendang from './IconWendang';
import IconShanchu from './IconShanchu';
import IconBug from './IconBug';
import IconXinzeng from './IconXinzeng';
import IconFuwu from './IconFuwu';
import IconDown from './IconDown';
import IconUp from './IconUp';
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

const IconFont = ({ name, ...rest }) => {
  switch (name) {
    case 'youhua':
      return <IconYouhua {...rest} />;
    case 'dayi':
      return <IconDayi {...rest} />;
    case 'shengji':
      return <IconShengji {...rest} />;
    case 'tiaozheng':
      return <IconTiaozheng {...rest} />;
    case 'gengxin':
      return <IconGengxin {...rest} />;
    case 'wendang':
      return <IconWendang {...rest} />;
    case 'shanchu':
      return <IconShanchu {...rest} />;
    case 'bug':
      return <IconBug {...rest} />;
    case 'xinzeng':
      return <IconXinzeng {...rest} />;
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
