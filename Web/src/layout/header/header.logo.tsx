import { Typography } from 'antd';
import { Link } from 'react-router-dom';

import useBreakpointValue from '../../hooks/use-breakpoint-value';

import logo from '../../assets/logo.svg';

import styles from './header.module.scss';

export default function HeaderLogo() {
  return (
    <Link to="/" className={styles.logo}>
      <img src={logo} alt="logo" />
      {useBreakpointValue({ sm: <Typography.Text>Elers</Typography.Text> })}
    </Link>
  );
}
