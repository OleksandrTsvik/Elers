import Icon from '@ant-design/icons';
import { Button } from 'antd';
import { useTranslation } from 'react-i18next';
import { FaBox } from 'react-icons/fa';

import styles from '../course-edit.module.scss';

export default function MaterialCreationButton() {
  const { t } = useTranslation();

  return (
    <Button
      className={styles.materialButton}
      type="primary"
      icon={<Icon component={FaBox} />}
    >
      {t('course_edit_page.add_material')}
    </Button>
  );
}
