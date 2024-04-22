import { Breadcrumb } from 'antd';
import { useTranslation } from 'react-i18next';
import { Link } from 'react-router-dom';

export default function RoleCreationBreadcrumb() {
  const { t } = useTranslation();

  return (
    <Breadcrumb
      items={[
        {
          title: <Link to="/roles">{t('roles_page.head_title')}</Link>,
        },
        {
          title: t('role_creation_page.head_title'),
        },
      ]}
    />
  );
}
