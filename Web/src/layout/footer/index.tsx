import {
  GithubOutlined,
  LinkedinOutlined,
  MailOutlined,
} from '@ant-design/icons';
import { Flex, Layout } from 'antd';
import { Link } from 'react-router-dom';

import styles from './footer.module.scss';

export default function Footer() {
  const email = 'oleksandr.zwick@gmail.com';

  return (
    <Layout.Footer>
      <Flex
        className={styles.socialLinks}
        align="center"
        justify="center"
        gap="middle"
      >
        <Link to="https://linkedin.com/in/oleksandr-tsvik" target="_blank">
          <LinkedinOutlined />
        </Link>
        <Link to="https://github.com/OleksandrTsvik" target="_blank">
          <GithubOutlined />
        </Link>
        <Link to={`mailto:${email}`}>
          <MailOutlined />
        </Link>
      </Flex>
      <div className={styles.copyright}>
        {/* © 2024 - {new Date().getFullYear()}. */}© 2024. Copyright: {email}
      </div>
    </Layout.Footer>
  );
}
