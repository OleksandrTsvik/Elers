import Icon, {
  BookOutlined,
  HomeOutlined,
  SafetyOutlined,
  SolutionOutlined,
  ThunderboltOutlined,
} from '@ant-design/icons';
import { ItemType } from 'antd/es/menu/hooks/useItems';
import { useTranslation } from 'react-i18next';
import { PiUsers } from 'react-icons/pi';

import { PermissionType, useAuth } from '../../auth';

export default function useSiderItems(): ItemType[] {
  const { t } = useTranslation();
  const { isStudent, filterMenu } = useAuth();

  return filterMenu([
    {
      key: '/',
      icon: <HomeOutlined />,
      label: t('sider_items.home'),
    },
    {
      key: '/courses',
      icon: <BookOutlined />,
      label: t('sider_items.courses'),
    },
    {
      key: '/my-progress',
      icon: <ThunderboltOutlined />,
      label: t('sider_items.my_progress'),
      show: isStudent,
    },
    {
      key: '/users',
      icon: <Icon component={PiUsers} />,
      label: t('sider_items.users'),
      userPermissions: [
        PermissionType.CreateUser,
        PermissionType.ReadUser,
        PermissionType.UpdateUser,
        PermissionType.DeleteUser,
      ],
    },
    {
      key: '/roles',
      icon: <SolutionOutlined />,
      label: t('sider_items.user_roles'),
      userPermissions: [
        PermissionType.CreateRole,
        PermissionType.ReadRole,
        PermissionType.UpdateRole,
        PermissionType.DeleteRole,
      ],
    },
    {
      key: '/permissions',
      icon: <SafetyOutlined />,
      label: t('sider_items.user_permissions'),
      userPermissions: [
        PermissionType.ReadPermission,
        PermissionType.CreateRole,
      ],
    },
  ]);
}
