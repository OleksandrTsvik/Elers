import Icon from '@ant-design/icons';
import { Button } from 'antd';
import { useTranslation } from 'react-i18next';
import { ImSection } from 'react-icons/im';

import { useAppDispatch } from '../../../hooks/redux-hooks';
import { classnames } from '../../../utils/helpers';
import { setModalMode } from '../course-edit.slice';
import { CourseTabModalMode } from '../modals/tab-modal-mode.enum';

import styles from '../course-edit.module.scss';

export default function SectionCreationModalButton() {
  const { t } = useTranslation();
  const appDispatch = useAppDispatch();

  const handleClick = () => {
    appDispatch(setModalMode(CourseTabModalMode.CreateTab));
  };

  return (
    <>
      <Button
        className={classnames(styles.sectionButton, 'btn-success')}
        type="primary"
        icon={<Icon component={ImSection} />}
        onClick={handleClick}
      >
        {t('course_edit_page.add_section')}
      </Button>
    </>
  );
}
