import Icon, { DownOutlined } from '@ant-design/icons';
import { Button, Dropdown, Space } from 'antd';
import { useTranslation } from 'react-i18next';
import { FaBox } from 'react-icons/fa';

import useMaterialActions from './use-material.actions';

import styles from '../course-edit.module.scss';

interface Props {
  tabId: string;
}

export default function MaterialCreationButton({ tabId }: Props) {
  const { t } = useTranslation();

  const items = useMaterialActions(tabId);

  return (
    <Dropdown menu={{ items }} trigger={['click']}>
      <Button className={styles.materialButton} type="primary">
        <Space>
          <Icon component={FaBox} />
          {t('course_edit_page.add_material')}
          <DownOutlined />
        </Space>
      </Button>
    </Dropdown>
  );
}
