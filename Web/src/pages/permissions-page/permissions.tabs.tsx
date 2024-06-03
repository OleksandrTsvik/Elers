import { Tabs } from 'antd';
import { useTranslation } from 'react-i18next';
import { Outlet, useLocation, useNavigate } from 'react-router-dom';

import PermissionsTab from './permissions.tab';

export default function PermissionsTabs() {
  const { t } = useTranslation();

  const location = useLocation();
  const navigate = useNavigate();

  return (
    <Tabs
      activeKey={location.pathname}
      items={[
        {
          key: '/permissions',
          label: t('permissions_page.permissions'),
          children: <PermissionsTab />,
        },
        {
          key: '/permissions/course-permissions',
          label: t('permissions_page.course_permissions'),
          children: <Outlet />,
        },
      ]}
      onChange={(activeKey) => navigate(activeKey)}
    />
  );
}
