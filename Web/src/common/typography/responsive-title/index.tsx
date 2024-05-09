import { Typography } from 'antd';
import { TitleProps } from 'antd/es/typography/Title';

import { fontSizeByTitleLevel } from './responsive-title.constants';
import useBreakpointValue from '../../../hooks/use-breakpoint-value';

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
