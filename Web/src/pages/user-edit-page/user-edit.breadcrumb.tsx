import { Breadcrumb } from 'antd';
import { useTranslation } from 'react-i18next';
import { Link } from 'react-router-dom';

export default function UserEditBreadcrumb() {
  const { t } = useTranslation();

  return (
    <Breadcrumb
      items={[
        {
          title: <Link to="/users">{t('users_page.head_title')}</Link>,
        },
        {
          title: t('user_edit_page.head_title'),
        },
      ]}
    />
  );
}
