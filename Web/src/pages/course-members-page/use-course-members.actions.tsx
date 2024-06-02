import { SolutionOutlined, UserDeleteOutlined } from '@ant-design/icons';
import { App } from 'antd';
import { useTranslation } from 'react-i18next';

import { openRoleModal } from './course-members.slice';
import { useRemoveCourseMemberMutation } from '../../api/course-members.mutations.api';
import {
  CoursePermissionType,
  PermissionType,
  useCoursePermission,
} from '../../auth';
import { GetActionItems } from '../../common/types';
import { useAppDispatch } from '../../hooks/redux-hooks';
import useDisplayError from '../../hooks/use-display-error';
import { CourseMember } from '../../models/course-member.interface';

export default function useCourseMembersActions(courseId?: string) {
  const { t } = useTranslation();
  const appDispatch = useAppDispatch();
  const { filterActions } = useCoursePermission(courseId);

  const { modal } = App.useApp();
  const { displayError } = useDisplayError();

  const [removeCourseMember] = useRemoveCourseMemberMutation();

  const handleChangeRoleClick = (record: CourseMember) => {
    appDispatch(openRoleModal(record));
  };

  const handleRemoveMemberClick = async ({
    id,
    firstName,
    lastName,
    patronymic,
  }: CourseMember) => {
    await modal.confirm({
      title: t('course_members_page.confirm_remove_member', {
        firstName,
        lastName,
        patronymic,
      }),
      content: t('actions.confirm_delete'),
      okButtonProps: { danger: true },
      onOk: () =>
        removeCourseMember({ memberId: id })
          .unwrap()
          .catch((error) => displayError(error)),
    });
  };

  const getActionItems: GetActionItems<CourseMember> = (record) =>
    filterActions([
      {
        key: 'change-role',
        icon: <SolutionOutlined />,
        label: t('course_members_page.change_role'),
        onClick: () => handleChangeRoleClick(record),
        coursePermissions: [CoursePermissionType.ChangeCourseMemberRole],
        userPermissions: [PermissionType.ManageCourse],
      },
      {
        key: 'remove',
        icon: <UserDeleteOutlined />,
        label: t('course_members_page.remove_member'),
        onClick: () => handleRemoveMemberClick(record),
        coursePermissions: [CoursePermissionType.RemoveCourseMember],
        userPermissions: [PermissionType.ManageCourse],
      },
    ]);

  return { getActionItems };
}
