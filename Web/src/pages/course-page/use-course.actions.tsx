import {
  PictureOutlined,
  UserAddOutlined,
  UserDeleteOutlined,
} from '@ant-design/icons';
import { App } from 'antd';
import { ItemType } from 'antd/es/menu/hooks/useItems';
import { useTranslation } from 'react-i18next';
import { useNavigate } from 'react-router-dom';

import { useDeleteCourseMutation } from '../../api/courses.api';
import {
  CoursePermissionType,
  PermissionType,
  hasCoursePermission,
  useAuth,
} from '../../auth';
import { DeleteIcon, EditIcon } from '../../components';
import useDisplayError from '../../hooks/use-display-error';

type CourseAction = ItemType & {
  show?: () => boolean;
  permissions: PermissionType[];
  coursePermissions: CoursePermissionType[];
};

export default function useCourseActions(
  courseId: string,
  title: string,
  memberPermissions: CoursePermissionType[],
): ItemType[] {
  const { t } = useTranslation();
  const navigate = useNavigate();

  const { checkPermission } = useAuth();
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

  const courseActions: CourseAction[] = [
    {
      key: 'enroll',
      icon: <UserAddOutlined />,
      label: t('course.enroll'),
      onClick: () => console.log('enroll'),
      show: () => false,
      permissions: [],
      coursePermissions: [],
    },
    {
      key: 'unenroll',
      icon: <UserDeleteOutlined />,
      label: t('course.unenroll'),
      onClick: () => console.log('unenroll'),
      show: () => true,
      permissions: [],
      coursePermissions: [],
    },
    {
      key: 'edit',
      icon: <EditIcon />,
      label: t('actions.edit'),
      onClick: () => navigate(`/courses/edit/${courseId}`),
      permissions: [PermissionType.ManageCourse],
      coursePermissions: [
        CoursePermissionType.CreateCourseMaterial,
        CoursePermissionType.CreateCourseTab,
        CoursePermissionType.DeleteCourseMaterial,
        CoursePermissionType.DeleteCourseTab,
        CoursePermissionType.UpdateCourse,
        CoursePermissionType.UpdateCourseMaterial,
        CoursePermissionType.UpdateCourseTab,
      ],
    },
    {
      key: 'change-image',
      icon: <PictureOutlined />,
      label: t('actions.change_image'),
      onClick: () => navigate(`/courses/change-image/${courseId}`),
      permissions: [PermissionType.ManageCourse],
      coursePermissions: [CoursePermissionType.UpdateCourseImage],
    },
    {
      key: 'delete',
      icon: <DeleteIcon />,
      label: t('actions.delete'),
      onClick: handleDeleteClick,
      permissions: [PermissionType.ManageCourse],
      coursePermissions: [CoursePermissionType.DeleteCourse],
    },
  ];

  return (
    courseActions
      .filter(
        (item) =>
          item.show?.() ??
          (checkPermission(item.permissions) ||
            hasCoursePermission(memberPermissions, item.coursePermissions)),
      )
      // eslint-disable-next-line @typescript-eslint/no-unused-vars
      .map(({ coursePermissions, permissions, show, ...item }) => item)
  );
}
