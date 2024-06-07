import { Typography } from 'antd';
import dayjs from 'dayjs';
import { useTranslation } from 'react-i18next';

import { DATE_FORMAT } from '../../utils/constants/app.constants';

interface Props {
  deadline: Date | undefined;
}

export function DeadlineParagraph({ deadline }: Props) {
  const { t } = useTranslation();

  return (
    <Typography.Paragraph
      type={
        deadline &&
        dayjs(deadline).isBefore(new Date(), 'date') &&
        !dayjs(deadline).isSame(new Date(), 'date')
          ? 'danger'
          : 'secondary'
      }
    >
      {t('course_material.deadline')}:{' '}
      {deadline
        ? dayjs(deadline).format(DATE_FORMAT)
        : t('course_material.no_deadline')}
    </Typography.Paragraph>
  );
}
