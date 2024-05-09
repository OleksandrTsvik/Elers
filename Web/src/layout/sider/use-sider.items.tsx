import Icon, {
  BookOutlined,
  HomeOutlined,
  SafetyOutlined,
  SolutionOutlined,
} from '@ant-design/icons';
import { ItemType } from 'antd/es/menu/hooks/useItems';
import { useTranslation } from 'react-i18next';
import { PiUsers } from 'react-icons/pi';

import { PermissionType } from '../../auth/permission-type.enum';
import useAuth from '../../auth/use-auth';

type SiderItem = ItemType & {
  permissions: PermissionType[];
};

export default function useSiderItems(): SiderItem[] {
  const { t } = useTranslation();
  const { checkPermission } = useAuth();

  return [
    {
      key: '/',
      icon: <HomeOutlined />,
      label: t('sider_items.home'),
      permissions: [],
    },
    {
      key: '/courses',
      icon: <BookOutlined />,
      label: t('sider_items.courses'),
      permissions: [],
    },
    {
      key: '/users',
      icon: <Icon component={PiUsers} />,
      label: t('sider_items.users'),
      permissions: [
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
      permissions: [
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
      permissions: [PermissionType.ReadPermission, PermissionType.CreateRole],
    },
  ].filter((item) => checkPermission(item.permissions));
}
