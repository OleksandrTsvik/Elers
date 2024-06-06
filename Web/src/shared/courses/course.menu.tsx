import { ReadOutlined, TeamOutlined } from '@ant-design/icons';
import { Menu, Skeleton } from 'antd';
import { Suspense } from 'react';
import { useTranslation } from 'react-i18next';
import { Outlet, useLocation, useNavigate, useParams } from 'react-router-dom';

import {
  CoursePermissionType,
  PermissionType,
  useAuth,
  useCoursePermission,
} from '../../auth';
import { CourseMaterialIcon, CourseMaterialType } from '../materials';

export default function CourseMenu() {
  const { courseId } = useParams();
  const { t } = useTranslation();

  const { isAuth } = useAuth();
  const { filterMenu } = useCoursePermission(courseId);

  const location = useLocation();
  const navigate = useNavigate();

  const menuItems = filterMenu([
    {
      key: `/courses/${courseId}`,
      icon: <ReadOutlined />,
      label: t('course.course'),
    },
    {
      key: `/courses/members/${courseId}`,
      icon: <TeamOutlined />,
      label: t('course.members'),
      show: isAuth,
    },
    {
      key: `/courses/submitted-assignments/${courseId}`,
      icon: <CourseMaterialIcon type={CourseMaterialType.Assignment} />,
      label: t('course.submitted_assignment'),
      coursePermissions: [CoursePermissionType.GradeCourseStudents],
      userPermissions: [PermissionType.GradeStudents],
    },
  ]);

  if (menuItems.length === 1) {
    return (
      <Suspense fallback={<Skeleton active />}>
        <Outlet />
      </Suspense>
    );
  }

  return (
    <>
      <Menu
        className="mb-field"
        mode="horizontal"
        selectedKeys={[location.pathname]}
        items={menuItems}
        onSelect={(info) => navigate(info.key)}
      />
      <Suspense fallback={<Skeleton active />}>
        <Outlet />
      </Suspense>
    </>
  );
}
