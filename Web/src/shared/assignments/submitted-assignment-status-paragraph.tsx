import { Typography } from 'antd';
import { BaseType } from 'antd/es/typography/Base';
import { useTranslation } from 'react-i18next';

import { SubmittedAssignmentStatus } from '../../models/assignment.interface';

const paragraphTypeStatus: { [key in SubmittedAssignmentStatus]: BaseType } = {
  Submitted: 'warning',
  Graded: 'success',
  Resubmit: 'danger',
};

interface Props {
  status: SubmittedAssignmentStatus;
}

export function SubmittedAssignmentStatusParagraph({ status }: Props) {
  const { t } = useTranslation();

  return (
    <Typography.Paragraph type={paragraphTypeStatus[status]}>
      {t('assignment.status')}:{' '}
      {t([`assignment.submitted_status.${status}`, status])}
    </Typography.Paragraph>
  );
}
