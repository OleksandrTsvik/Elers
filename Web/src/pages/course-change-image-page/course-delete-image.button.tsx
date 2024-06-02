import { App, Button } from 'antd';
import { useTranslation } from 'react-i18next';
import { useNavigate } from 'react-router-dom';

import { useDeleteCourseImageMutation } from '../../api/courses.api';
import useDisplayError from '../../hooks/use-display-error';
import { classnames } from '../../utils/helpers';

import styles from './course-change-image.module.scss';

interface Props {
  courseId: string;
  disabled: boolean;
}

export default function CourseDeleteImageButton({ courseId, disabled }: Props) {
  const { t } = useTranslation();
  const navigate = useNavigate();

  const { modal } = App.useApp();
  const { displayError } = useDisplayError();

  const [deleteCourseImage] = useDeleteCourseImageMutation();

  const handleDeleteCourseImage = async () => {
    await modal.confirm({
      title: t('course_change_image_page.confirm_delete'),
      content: t('actions.confirm_delete'),
      okButtonProps: { danger: true },
      onOk: () =>
        deleteCourseImage({ id: courseId })
          .unwrap()
          .then(() => navigate('/'))
          .catch((error) => displayError(error)),
    });
  };

  return (
    <Button
      className={classnames(styles.deleteImgBtn, 'right-btn')}
      danger
      disabled={disabled}
      onClick={handleDeleteCourseImage}
    >
      {t('course_change_image_page.delete_image')}
    </Button>
  );
}
