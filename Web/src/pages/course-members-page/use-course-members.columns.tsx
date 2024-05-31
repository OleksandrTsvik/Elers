import { UserOutlined } from '@ant-design/icons';
import { Avatar, TableColumnsType } from 'antd';
import { ColumnGroupType, ColumnType } from 'antd/es/table';
import { useTranslation } from 'react-i18next';

import {
  CoursePermissionType,
  PermissionType,
  useCoursePermission,
} from '../../auth';
import { CourseMember } from '../../models/course-member.interface';

type CourseMemberColumns = (
  | ColumnGroupType<CourseMember>
  | ColumnType<CourseMember>
) & {
  coursePermissions: CoursePermissionType[];
  userPermissions: PermissionType[];
};

export default function useCourseMembersColumns(
  courseId?: string,
): TableColumnsType<CourseMember> {
  const { t } = useTranslation();
  const { checkCoursePermission } = useCoursePermission(courseId);

  const columns: CourseMemberColumns[] = [
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
      render: (_, record) =>
        record.avatarUrl ? (
          <Avatar src={record.avatarUrl} />
        ) : (
          <Avatar icon={<UserOutlined />} />
        ),
      coursePermissions: [],
      userPermissions: [],
    },
    {
      key: 'firstName',
      dataIndex: 'firstName',
      title: t('course_members_page.first_name'),
      coursePermissions: [],
      userPermissions: [],
    },
    {
      key: 'lastName',
      dataIndex: 'lastName',
      title: t('course_members_page.last_name'),
      coursePermissions: [],
      userPermissions: [],
    },
    {
      key: 'patronymic',
      dataIndex: 'patronymic',
      title: t('course_members_page.patronymic'),
      coursePermissions: [],
      userPermissions: [],
    },
    {
      key: 'role',
      title: t('course_members_page.role'),
      render: (_, record) => record.courseRole?.description,
      coursePermissions: [CoursePermissionType.ChangeCourseMemberRole],
      userPermissions: [PermissionType.ManageCourse],
    },
  ];

  return (
    columns
      .filter(({ coursePermissions, userPermissions }) =>
        checkCoursePermission(coursePermissions, userPermissions),
      )
      // eslint-disable-next-line @typescript-eslint/no-unused-vars
      .map(({ coursePermissions, userPermissions, ...item }) => item)
  );
}
