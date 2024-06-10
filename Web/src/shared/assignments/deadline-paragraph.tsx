import { Typography } from 'antd';
import dayjs from 'dayjs';
import { useTranslation } from 'react-i18next';

import { DATE_FORMAT } from '../../utils/constants/app.constants';

interface Props {
  deadline: Date | undefined;
  title?: string;
  noDeadlineText?: string;
}

export function DeadlineParagraph({ deadline, title, noDeadlineText }: Props) {
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
      {title ?? t('course_material.deadline')}:{' '}
      {deadline
        ? dayjs(deadline).format(DATE_FORMAT)
        : noDeadlineText ?? t('course_material.no_deadline')}
    </Typography.Paragraph>
  );
}
