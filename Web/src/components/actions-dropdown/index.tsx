import { EllipsisOutlined } from '@ant-design/icons';
import { Button, Dropdown } from 'antd';
import { ItemType } from 'antd/es/menu/hooks/useItems';

interface Props {
  items: ItemType[];
}

export default function ActionsDropdown({ items }: Props) {
  return (
    <Dropdown menu={{ items }}>
      <Button type="text" icon={<EllipsisOutlined />} />
    </Dropdown>
  );
}
