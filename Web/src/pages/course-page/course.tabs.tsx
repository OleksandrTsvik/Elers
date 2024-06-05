import { ReadOutlined, TeamOutlined } from '@ant-design/icons';
import { Tabs } from 'antd';
import { useTranslation } from 'react-i18next';
import { Outlet, useLocation, useNavigate } from 'react-router-dom';

import CourseMainTab from './course.main-tab';
import {
  CoursePermissionType,
  PermissionType,
  useAuth,
  useCoursePermission,
} from '../../auth';
import { Course } from '../../models/course.interface';
import { CourseMaterialIcon, CourseMaterialType } from '../../shared';

interface Props {
  course: Course;
}

export default function CourseTabs({ course }: Props) {
  const { t } = useTranslation();

  const { isAuth } = useAuth();
  const { filterTabs } = useCoursePermission(course.id);

  const location = useLocation();
  const navigate = useNavigate();

  const tabItems = filterTabs([
    {
      key: `/courses/${course.id}`,
      icon: <ReadOutlined />,
      label: t('course.course'),
      children: <CourseMainTab course={course} />,
    },
    {
      key: `/courses/${course.id}/members`,
      icon: <TeamOutlined />,
      label: t('course.members'),
      children: <Outlet />,
      show: isAuth,
    },
    {
      key: `/courses/${course.id}/submitted-assignments`,
      icon: <CourseMaterialIcon type={CourseMaterialType.Assignment} />,
      label: t('course.submitted_assignment'),
      children: <Outlet />,
      coursePermissions: [CoursePermissionType.GradeCourseStudents],
      userPermissions: [PermissionType.GradeStudents],
    },
  ]);

  if (tabItems.length === 1) {
    return <CourseMainTab course={course} />;
  }

  return (
    <Tabs
      activeKey={location.pathname}
      items={tabItems}
      onChange={(activeKey) => navigate(activeKey)}
    />
  );
}
