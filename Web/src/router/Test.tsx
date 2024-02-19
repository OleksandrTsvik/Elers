import { DatePicker } from 'antd';
import { useTranslation } from 'react-i18next';

export default function Test() {
  const { t } = useTranslation();

  return (
    <>
      <p>{t('header.avatar.logout')}</p>
      <DatePicker />
    </>
  );
}
