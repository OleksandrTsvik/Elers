import { Typography } from 'antd';
import { Link } from 'react-router-dom';

import useBreakpointValue from '../../hooks/use-breakpoint-value';

export default function HeaderLogo() {
  return (
    <Link
      to="/"
      style={{
        padding: '0 10px',
      }}
    >
      <Typography.Text>
        {useBreakpointValue({ sm: 'Електронне навчання', xs: 'ЕН' })}
      </Typography.Text>
    </Link>
  );
}
