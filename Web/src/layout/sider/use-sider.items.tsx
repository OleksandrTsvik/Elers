import {
  BookOutlined,
  HomeOutlined,
  SafetyOutlined,
  SolutionOutlined,
} from '@ant-design/icons';
import { ItemType } from 'antd/es/menu/hooks/useItems';
import { useTranslation } from 'react-i18next';

import useAuth from '../../hooks/use-auth';
import { PermissionType } from '../../models/permission-type.enum';

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
      permissions: [PermissionType.ReadPermission],
    },
  ].filter((item) => checkPermission(item.permissions));
}
