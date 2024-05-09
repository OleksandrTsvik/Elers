import { TitleProps } from 'antd/es/typography/Title';

import { BreakpointValues } from '../../../hooks/use-breakpoint-value';

type FontSizeByTitleLevel = {
  [key in Required<TitleProps>['level']]: BreakpointValues<number>;
};

export const fontSizeByTitleLevel: FontSizeByTitleLevel = {
  1: { xl: 38, lg: 30, md: 24, xs: 20 },
  2: { lg: 30, md: 24, xs: 20 },
  3: { md: 24, xs: 20 },
  4: { xs: 20 },
  5: { xs: 16 },
};
