import {
  PictureOutlined,
  SolutionOutlined,
  TeamOutlined,
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
  useCoursePermission,
} from '../../auth';
import { DeleteIcon, EditIcon } from '../../components';
import useDisplayError from '../../hooks/use-display-error';

type CourseAction = ItemType & {
  show?: () => boolean;
  coursePermissions: CoursePermissionType[];
  userPermissions: PermissionType[];
};

export default function useCourseActions(
  courseId: string,
  title: string,
): ItemType[] {
  const { t } = useTranslation();
  const navigate = useNavigate();

  const { isCreator, isMember, checkCoursePermission } =
    useCoursePermission(courseId);

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
      show: () => !isCreator && !isMember,
      coursePermissions: [],
      userPermissions: [],
    },
    {
      key: 'unenroll',
      icon: <UserDeleteOutlined />,
      label: t('course.unenroll'),
      onClick: () => console.log('unenroll'),
      show: () => !isCreator && isMember,
      coursePermissions: [],
      userPermissions: [],
    },
    {
      key: 'members',
      icon: <TeamOutlined />,
      label: 'Учасники',
      onClick: () => console.log('members'),
      show: () => isCreator || isMember,
      coursePermissions: [],
      userPermissions: [],
    },
    {
      key: 'edit',
      icon: <EditIcon />,
      label: t('actions.edit'),
      onClick: () => navigate(`/courses/edit/${courseId}`),
      coursePermissions: [
        CoursePermissionType.CreateCourseMaterial,
        CoursePermissionType.CreateCourseTab,
        CoursePermissionType.DeleteCourseMaterial,
        CoursePermissionType.DeleteCourseTab,
        CoursePermissionType.UpdateCourse,
        CoursePermissionType.UpdateCourseMaterial,
        CoursePermissionType.UpdateCourseTab,
      ],
      userPermissions: [PermissionType.ManageCourse],
    },
    {
      key: 'roles',
      icon: <SolutionOutlined />,
      label: 'Ролі курсу',
      onClick: () => navigate(`/courses/roles/${courseId}`),
      coursePermissions: [
        CoursePermissionType.CreateCourseRole,
        CoursePermissionType.UpdateCourseRole,
        CoursePermissionType.DeleteCourseRole,
      ],
      userPermissions: [PermissionType.ManageCourse],
    },
    {
      key: 'change-image',
      icon: <PictureOutlined />,
      label: t('actions.change_image'),
      onClick: () => navigate(`/courses/change-image/${courseId}`),
      coursePermissions: [CoursePermissionType.UpdateCourseImage],
      userPermissions: [PermissionType.ManageCourse],
    },
    {
      key: 'delete',
      icon: <DeleteIcon />,
      label: t('actions.delete'),
      onClick: handleDeleteClick,
      coursePermissions: [CoursePermissionType.DeleteCourse],
      userPermissions: [PermissionType.ManageCourse],
    },
  ];

  return (
    courseActions
      .filter((item) => {
        if (item.show) {
          return item.show();
        }

        return checkCoursePermission(
          item.coursePermissions,
          item.userPermissions,
        );
      })
      // eslint-disable-next-line @typescript-eslint/no-unused-vars
      .map(({ coursePermissions, userPermissions, show, ...item }) => item)
  );
}
