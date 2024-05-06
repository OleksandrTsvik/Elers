import Icon from '@ant-design/icons';
import { Button } from 'antd';
import { FaBox } from 'react-icons/fa';

import styles from '../course-edit.module.scss';

export default function MaterialCreationContent() {
  return (
    <>
      <Button
        className={styles.materialButton}
        type="primary"
        icon={<Icon component={FaBox} />}
      >
        Додати матеріал
      </Button>
    </>
  );
}
