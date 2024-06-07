import { TableColumnsType } from 'antd';
import { useTranslation } from 'react-i18next';

import useCourseMembersActions from './use-course-members.actions';
import { useGetListCourseRolesQuery } from '../../api/course-roles.api';
import {
  CoursePermissionType,
  PermissionType,
  useCoursePermission,
} from '../../auth';
import { AuthItemColumn } from '../../common/types';
import { ActionsDropdown, UserAvatar } from '../../components';
import { GetColumnSearchProps } from '../../hooks/use-table-search-props';
import { CourseMember } from '../../models/course-member.interface';

export default function useCourseMembersColumns(
  getColumnSearchProps: GetColumnSearchProps<CourseMember>,
  courseId?: string,
): TableColumnsType<CourseMember> {
  const { t } = useTranslation();
  const { filterColumns } = useCoursePermission(courseId);

  const { data: roles } = useGetListCourseRolesQuery({ courseId });
  const { getActionItems } = useCourseMembersActions(courseId);

  const columns: AuthItemColumn<CourseMember>[] = [
    {
      key: 'index',
      title: '#',
      width: 1,
      render: (_, __, index) => index + 1,
      coursePermissions: [],
      userPermissions: [],
    },
    {
      key: 'avatar',
      width: 1,
      render: (_, record) => <UserAvatar url={record.avatarUrl} />,
      coursePermissions: [],
      userPermissions: [],
    },
    {
      key: 'lastName',
      dataIndex: 'lastName',
      title: t('course_members_page.last_name'),
      sorter: true,
      ...getColumnSearchProps('lastName', t('course_members_page.last_name')),
      coursePermissions: [],
      userPermissions: [],
    },
    {
      key: 'firstName',
      dataIndex: 'firstName',
      title: t('course_members_page.first_name'),
      sorter: true,
      ...getColumnSearchProps('firstName', t('course_members_page.first_name')),
      coursePermissions: [],
      userPermissions: [],
    },
    {
      key: 'patronymic',
      dataIndex: 'patronymic',
      title: t('course_members_page.patronymic'),
      sorter: true,
      ...getColumnSearchProps(
        'patronymic',
        t('course_members_page.patronymic'),
      ),
      coursePermissions: [],
      userPermissions: [],
    },
    {
      key: 'role',
      title: t('course_members_page.role'),
      render: (_, record) => record.courseRole?.description,
      filters: roles?.map((item) => ({ text: item.name, value: item.id })),
      coursePermissions: [CoursePermissionType.ChangeCourseMemberRole],
      userPermissions: [PermissionType.ManageCourse],
    },
    {
      key: 'action',
      width: 1,
      render: (_, record) => <ActionsDropdown items={getActionItems(record)} />,
      coursePermissions: [
        CoursePermissionType.ChangeCourseMemberRole,
        CoursePermissionType.RemoveCourseMember,
      ],
      userPermissions: [PermissionType.ManageCourse],
    },
  ];

  return filterColumns(columns);
}
