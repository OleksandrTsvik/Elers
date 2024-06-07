import { Tooltip } from 'antd';

import { AssessmentItem } from '../../models/grade.interface';

interface Props {
  value: AssessmentItem;
}

export default function CourseGradesThContent({ value }: Props) {
  return (
    <Tooltip title={value.title}>
      <span>{value.title}</span>
    </Tooltip>
  );
}
