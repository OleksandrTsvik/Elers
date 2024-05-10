import Icon from '@ant-design/icons';
import { Dropdown, Button, Space, GetProps } from 'antd';
import { ItemType } from 'antd/es/menu/hooks/useItems';
import { IoMdArrowDropdown } from 'react-icons/io';
import { IoSettingsSharp } from 'react-icons/io5';

import styles from './settings-dropdown.module.scss';

type DropdownProps = GetProps<typeof Dropdown>;

interface Props extends DropdownProps {
  items: ItemType[];
  loading?: boolean;
}

export default function SettingsDropdown({ items, loading, ...props }: Props) {
  return (
    <Dropdown menu={{ items }} trigger={['click']} {...props}>
      <Button className={styles.settingsButton} type="link" loading={loading}>
        <Space size={4}>
          <Icon component={IoSettingsSharp} />
          <Icon component={IoMdArrowDropdown} />
        </Space>
      </Button>
    </Dropdown>
  );
}
