import { Menu } from 'antd';

import { items } from './sider.items';

export default function SiderMenu() {
  return (
    <Menu
      mode="inline"
      defaultSelectedKeys={['1']}
      style={{ borderRight: 0 }}
      items={items}
    />
  );
}
