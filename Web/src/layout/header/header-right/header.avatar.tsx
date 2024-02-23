import { LogoutOutlined, UserOutlined } from '@ant-design/icons';
import { Avatar, Dropdown, MenuProps } from 'antd';
import { useTranslation } from 'react-i18next';

import useLogout from '../../../auth/use-logout';

export default function HeaderAvatar() {
  const { t } = useTranslation();
  const { logout } = useLogout();

  const items: MenuProps['items'] = [
    {
      key: '1',
      icon: <UserOutlined />,
      label: t('header.avatar.profile'),
    },
    {
      type: 'divider',
    },
    {
      key: '2',
      icon: <LogoutOutlined />,
      label: t('header.avatar.logout'),
      onClick: logout,
    },
  ];

  return (
    <Dropdown trigger={['click']} menu={{ items }}>
      <Avatar icon={<UserOutlined />} style={{ cursor: 'pointer' }} />
    </Dropdown>
  );
}
