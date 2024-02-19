import { LogoutOutlined, UserOutlined } from '@ant-design/icons';
import { Avatar, Dropdown, MenuProps } from 'antd';
import { useTranslation } from 'react-i18next';

export default function HeaderAvatar() {
  const { t } = useTranslation();

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
    },
  ];

  return (
    <Dropdown trigger={['click']} menu={{ items }}>
      <Avatar icon={<UserOutlined />} style={{ cursor: 'pointer' }} />
    </Dropdown>
  );
}
