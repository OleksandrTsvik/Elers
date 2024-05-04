import { Typography } from 'antd';
import { TitleProps } from 'antd/es/typography/Title';

import useBreakpointValue, {
  BreakpointValues,
} from '../../hooks/use-breakpoint-value';

type FontSizeByTitleLevel = {
  [key in Required<TitleProps>['level']]: BreakpointValues<number>;
};

const fontSizeByTitleLevel: FontSizeByTitleLevel = {
  1: { xl: 38, lg: 30, md: 24, xs: 20 },
  2: { lg: 30, md: 24, xs: 20 },
  3: { md: 24, xs: 20 },
  4: { xs: 20 },
  5: { xs: 16 },
};

export default function ResponsiveTitle({
  level = 1,
  style,
  children,
  ...props
}: TitleProps) {
  return (
    <Typography.Title
      style={{
        fontSize: useBreakpointValue(fontSizeByTitleLevel[level]),
        ...style,
      }}
      {...props}
    >
      {children}
    </Typography.Title>
  );
}
