import { EllipsisOutlined } from '@ant-design/icons';
import { Button, Dropdown, GetProps } from 'antd';
import { ItemType } from 'antd/es/menu/hooks/useItems';

type DropdownProps = GetProps<typeof Dropdown>;

interface Props extends DropdownProps {
  items: ItemType[];
}

export default function ActionsDropdown({ items, ...props }: Props) {
  return (
    <Dropdown menu={{ items }} {...props}>
      <Button type="text" icon={<EllipsisOutlined />} />
    </Dropdown>
  );
}
