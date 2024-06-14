import { blue } from '@ant-design/colors';
import { Tooltip } from 'antd';

import useCourseGradesState from './use-course-grades.state';
import { AssessmentItem, GradeType } from '../../models/grade.interface';

interface Props {
  value: AssessmentItem;
}

export default function CourseGradesTh({ value }: Props) {
  const { onOpenColumnModal } = useCourseGradesState();

  const handleClick = () => {
    if (value.type !== GradeType.Manual) {
      return;
    }

    onOpenColumnModal(value);
  };

  return (
    <th
      style={{
        cursor: value.type === GradeType.Manual ? 'pointer' : undefined,
        backgroundColor:
          value.type === GradeType.Manual ? blue.primary : undefined,
      }}
      onClick={handleClick}
    >
      <Tooltip title={value.title}>
        <span>{value.title}</span>
      </Tooltip>
    </th>
  );
}
