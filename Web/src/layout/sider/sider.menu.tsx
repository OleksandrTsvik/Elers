import { Menu } from 'antd';
import { SelectEventHandler } from 'rc-menu/es/interface';
import { useLocation, useNavigate } from 'react-router-dom';

import useSiderItems from './use-sider.items';

interface Props {
  onSelect?: SelectEventHandler;
}

export default function SiderMenu({ onSelect }: Props) {
  const location = useLocation();
  const navigate = useNavigate();

  const items = useSiderItems();

  return (
    <Menu
      mode="inline"
      items={items}
      defaultSelectedKeys={[location.pathname]}
      selectedKeys={[location.pathname]}
      style={{ borderRight: 0 }}
      onSelect={(info) => {
        navigate(info.key);
        onSelect && onSelect(info);
      }}
    />
  );
}
