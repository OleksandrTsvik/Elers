import { PictureOutlined } from '@ant-design/icons';
import { App } from 'antd';
import { ItemType } from 'antd/es/menu/hooks/useItems';
import { useTranslation } from 'react-i18next';
import { useNavigate } from 'react-router-dom';

import { useDeleteCourseMutation } from '../../api/courses.api';
import { DeleteIcon, EditIcon } from '../../components';
import useDisplayError from '../../hooks/use-display-error';

export default function useCourseActions(
  courseId: string,
  title: string,
): ItemType[] {
  const { t } = useTranslation();
  const navigate = useNavigate();

  const { modal } = App.useApp();
  const { displayError } = useDisplayError();

  const [deleteCourse] = useDeleteCourseMutation();

  const handleDeleteClick = async () => {
    await modal.confirm({
      title: t('course_page.confirm_delete', { title }),
      content: t('actions.confirm_delete'),
      okButtonProps: { danger: true },
      onOk: () =>
        deleteCourse({ id: courseId })
          .unwrap()
          .then(() => navigate('/'))
          .catch((error) => displayError(error)),
    });
  };

  return [
    {
      key: 'edit',
      icon: <EditIcon />,
      label: t('actions.edit'),
      onClick: () => navigate(`/courses/edit/${courseId}`),
    },
    {
      key: 'change-image',
      icon: <PictureOutlined />,
      label: t('actions.change_image'),
      onClick: () => navigate(`/courses/change-image/${courseId}`),
    },
    {
      key: 'delete',
      icon: <DeleteIcon />,
      label: t('actions.delete'),
      onClick: handleDeleteClick,
    },
  ];
}
