import { App } from 'antd';
import { ItemType } from 'antd/es/menu/hooks/useItems';
import { useTranslation } from 'react-i18next';

import { CourseRolesModalMode } from './course-roles.modal-mode.enum';
import { useDeleteCourseRoleMutation } from '../../api/course-roles.api';
import { EditIcon, DeleteIcon } from '../../components';
import useDisplayError from '../../hooks/use-display-error';
import { CourseRoleListItem } from '../../models/course-role.interface';

export default function useCourseRolesActions(
  updateModalMode: (modalMode: CourseRolesModalMode) => void,
  updateCurrentCourseRole: (courseRole: CourseRoleListItem) => void,
) {
  const { t } = useTranslation();

  const { modal } = App.useApp();
  const { displayError } = useDisplayError();

  const [deleteCourseRole] = useDeleteCourseRoleMutation();

  const handleEditClick = (record: CourseRoleListItem) => {
    updateModalMode(CourseRolesModalMode.Edit);
    updateCurrentCourseRole(record);
  };

  const handleDeleteClick = async (roleId: string, role: string) => {
    await modal.confirm({
      title: t('course_roles_page.confirm_delete_title', { role }),
      content: t('actions.confirm_delete'),
      okButtonProps: { danger: true },
      onOk: () =>
        deleteCourseRole({ roleId })
          .unwrap()
          .catch((error) => displayError(error)),
    });
  };

  const getActionItems = (record: CourseRoleListItem): ItemType[] => [
    {
      key: 'edit',
      icon: <EditIcon />,
      label: t('actions.edit'),
      onClick: () => handleEditClick(record),
    },
    {
      key: 'delete',
      icon: <DeleteIcon />,
      label: t('actions.delete'),
      onClick: () => handleDeleteClick(record.id, record.name),
    },
  ];

  return { getActionItems };
}
