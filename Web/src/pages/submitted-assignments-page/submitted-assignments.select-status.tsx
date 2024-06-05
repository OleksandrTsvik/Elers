import { Segmented } from 'antd';
import { useTranslation } from 'react-i18next';

import { SubmittedAssignmentStatus } from '../../models/assignment.interface';

interface Props {
  status: SubmittedAssignmentStatus;
  onChange: (value: SubmittedAssignmentStatus) => void;
}

export default function SubmittedAssignmentsSelectStatus({
  status,
  onChange,
}: Props) {
  const { t } = useTranslation();

  return (
    <Segmented
      block
      value={status}
      options={[
        {
          label: t('assignment.submitted_select_status.submitted'),
          value: SubmittedAssignmentStatus.Submitted,
        },
        {
          label: t('assignment.submitted_select_status.resubmit'),
          value: SubmittedAssignmentStatus.Resubmit,
        },
        {
          label: t('assignment.submitted_select_status.graded'),
          value: SubmittedAssignmentStatus.Graded,
        },
      ]}
      onChange={onChange}
    />
  );
}
