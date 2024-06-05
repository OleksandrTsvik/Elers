import { Select } from 'antd';
import { useTranslation } from 'react-i18next';
import { useParams } from 'react-router-dom';

import { useGetCourseStudentsQuery } from '../../api/students.api';

interface Props {
  value: string | undefined;
  onChange: (value: string) => void;
}

export default function SubmittedAssignmentsSelectStudent({
  value,
  onChange,
}: Props) {
  const { courseId } = useParams();
  const { t } = useTranslation();

  const { data, isFetching } = useGetCourseStudentsQuery({ courseId });

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
      placeholder={t('course.student')}
      value={value}
      options={data?.map(({ id, firstName, lastName, patronymic }) => ({
        label: `${lastName} ${firstName} ${patronymic}`,
        value: id,
      }))}
      filterOption={filterOption}
      onChange={onChange}
    />
  );
}
