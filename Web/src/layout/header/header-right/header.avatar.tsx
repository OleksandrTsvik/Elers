import { LogoutOutlined, UserOutlined } from '@ant-design/icons';
import { Avatar, Dropdown, MenuProps } from 'antd';

const items: MenuProps['items'] = [
  {
    key: '1',
    icon: <UserOutlined />,
    label: 'Profile',
  },
  {
    type: 'divider',
  },
  {
    key: '2',
    icon: <LogoutOutlined />,
    label: 'Logout',
  },
];

export default function HeaderAvatar() {
  return (
    <Dropdown trigger={['click']} menu={{ items }}>
      <Avatar icon={<UserOutlined />} style={{ cursor: 'pointer' }} />
    </Dropdown>
  );
}
