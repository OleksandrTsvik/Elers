import { Menu } from 'antd';
import { useLocation, useNavigate } from 'react-router-dom';

import useSiderItems from './use-sider.items';

export default function SiderMenu() {
  const location = useLocation();
  const navigate = useNavigate();

  const items = useSiderItems();

  return (
    <Menu
      mode="inline"
      items={items}
      defaultSelectedKeys={[location.pathname]}
      style={{ borderRight: 0 }}
      onSelect={({ key }) => navigate(key)}
    />
  );
}
