import Icon from '@ant-design/icons';
import { Button } from 'antd';
import { useTranslation } from 'react-i18next';
import { ImSection } from 'react-icons/im';

import SectionCreationForm from './section-creation.form';
import useModal from '../../../hooks/use-modal';
import { CourseTab } from '../../../models/course.interface';
import classnames from '../../../utils/helpers/classnames.util';

import styles from '../course-edit.module.scss';

interface Props {
  courseId: string;
  onCreationSection: (section: CourseTab) => void;
}

export default function SectionCreationContent({
  courseId,
  onCreationSection,
}: Props) {
  const { t } = useTranslation();
  const { isOpen, onOpen, onClose } = useModal();

  return (
    <>
      {isOpen && (
        <SectionCreationForm
          courseId={courseId}
          onCreationSection={onCreationSection}
          onHide={onClose}
        />
      )}
      {!isOpen && (
        <Button
          className={classnames(styles.sectionButton, 'btn-success')}
          type="primary"
          icon={<Icon component={ImSection} />}
          onClick={onOpen}
        >
          {t('course_edit_page.add_section')}
        </Button>
      )}
    </>
  );
}
