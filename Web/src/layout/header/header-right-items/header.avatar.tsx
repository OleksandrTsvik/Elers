import { LogoutOutlined, UserOutlined } from '@ant-design/icons';
import { Dropdown, MenuProps } from 'antd';
import { useTranslation } from 'react-i18next';
import { useNavigate } from 'react-router-dom';

import { useAuth, useLogout } from '../../../auth';
import { UserAvatar } from '../../../components';

export default function HeaderAvatar() {
  const { t } = useTranslation();
  const navigate = useNavigate();

  const { user } = useAuth();
  const { logout } = useLogout();

  const items: MenuProps['items'] = [
    {
      key: 'profile',
      icon: <UserOutlined />,
      label: t('header.avatar.profile'),
      onClick: () => navigate('profile'),
    },
    {
      type: 'divider',
    },
    {
      key: 'logout',
      icon: <LogoutOutlined />,
      label: t('header.avatar.logout'),
      onClick: logout,
    },
  ];

  return (
    <Dropdown trigger={['click']} menu={{ items }}>
      <UserAvatar url={user?.avatarUrl} style={{ cursor: 'pointer' }} />
    </Dropdown>
  );
}
