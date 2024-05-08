import Icon from '@ant-design/icons';
import { Dropdown, Button, Space, GetProps } from 'antd';
import { ItemType } from 'antd/es/menu/hooks/useItems';
import { IoMdArrowDropdown } from 'react-icons/io';
import { IoSettingsSharp } from 'react-icons/io5';

type DropdownProps = GetProps<typeof Dropdown>;

interface Props extends DropdownProps {
  items: ItemType[];
}

export default function SettingsDropdown({ items, ...props }: Props) {
  return (
    <Dropdown menu={{ items }} trigger={['click']} {...props}>
      <Button type="link">
        <Space size={4}>
          <Icon component={IoSettingsSharp} />
          <Icon component={IoMdArrowDropdown} />
        </Space>
      </Button>
    </Dropdown>
  );
}
