import { Tabs } from 'antd';
import { useTranslation } from 'react-i18next';

import CoursePermissionsTab from './course-permissions.tab';
import PermissionsTab from './permissions.tab';

export default function PermissionsTabs() {
  const { t } = useTranslation();

  return (
    <Tabs
      items={[
        {
          key: 'permissions',
          label: t('permissions_page.permissions'),
          children: <PermissionsTab />,
        },
        {
          key: 'course-permissions',
          label: t('permissions_page.course_permissions'),
          children: <CoursePermissionsTab />,
        },
      ]}
    />
  );
}
