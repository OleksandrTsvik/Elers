import { Select } from 'antd';
import { useTranslation } from 'react-i18next';
import { useParams } from 'react-router-dom';

import { useGetListAssignmentTitlesQuery } from '../../api/assignments.api';

interface Props {
  value: string | undefined;
  onChange: (value: string) => void;
}

export default function SubmittedAssignmentsSelectAssignment({
  value,
  onChange,
}: Props) {
  const { courseId } = useParams();
  const { t } = useTranslation();

  const { data, isFetching } = useGetListAssignmentTitlesQuery({ courseId });

  const filterOption = (
    input: string,
    option?: { label: string; value: string },
  ) => (option?.label ?? '').toLowerCase().includes(input.toLowerCase());

  return (
    <Select
      className="w-100"
      allowClear
      showSearch
      loading={isFetching}
      placeholder={t('course.material.assignment')}
      value={value}
      options={data?.map(({ assignmentId, title }) => ({
        label: title,
        value: assignmentId,
      }))}
      filterOption={filterOption}
      onChange={onChange}
    />
  );
}
