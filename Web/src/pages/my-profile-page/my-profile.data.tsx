import { Typography } from 'antd';
import dayjs from 'dayjs';
import { useTranslation } from 'react-i18next';

import { UserProfile } from '../../models/user.interface';
import { DATE_FORMAT } from '../../utils/constants/app.constants';

interface Props {
  profile: UserProfile;
}

export default function MyProfileData({
  profile: {
    firstName,
    lastName,
    patronymic,
    email,
    birthDate,
    registrationDate,
  },
}: Props) {
  const { t } = useTranslation();

  return (
    <>
      <Typography.Title level={3}>
        {lastName} {firstName} {patronymic}
      </Typography.Title>

      <Typography.Paragraph type="secondary" copyable>
        {email}
      </Typography.Paragraph>

      {birthDate && (
        <Typography.Paragraph type="secondary">
          {t('my_profile_page.birth_date')}:{' '}
          <Typography.Text>
            {dayjs(birthDate).format(DATE_FORMAT)}
          </Typography.Text>
        </Typography.Paragraph>
      )}

      <Typography.Paragraph type="secondary">
        {t('my_profile_page.registration_date')}:{' '}
        {dayjs(registrationDate).format(DATE_FORMAT)}
      </Typography.Paragraph>
    </>
  );
}
