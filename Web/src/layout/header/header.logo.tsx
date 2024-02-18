import { Grid, Typography } from 'antd';
import { Link } from 'react-router-dom';

export default function HeaderLogo() {
  const breakpoints = Grid.useBreakpoint();

  return (
    <Link
      to="/"
      style={{
        padding: '0 10px',
      }}
    >
      <Typography.Text>
        {breakpoints.sm ? 'Електронне навчання' : 'ЕН'}
      </Typography.Text>
    </Link>
  );
}
