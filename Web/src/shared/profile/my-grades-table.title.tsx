import { Space } from 'antd';
import { Link } from 'react-router-dom';

import { AssessmentItem, GradeType } from '../../models/grade.interface';
import { GradeIcon } from '../courses';

interface Props {
  courseId: string | undefined;
  assessment: AssessmentItem;
}

export default function MyGradesTableTitle({ courseId, assessment }: Props) {
  switch (assessment.type) {
    case GradeType.Assignment:
      return (
        <Link
          className="d-block"
          to={`/courses/${courseId}/assignment/${assessment.id}`}
        >
          <Space align="start">
            <GradeIcon type={assessment.type} />
            {assessment.title}
          </Space>
        </Link>
      );
    case GradeType.Test:
      return (
        <Link
          className="d-block"
          to={`/courses/${courseId}/test/${assessment.id}`}
        >
          <Space align="start">
            <GradeIcon type={assessment.type} />
            {assessment.title}
          </Space>
        </Link>
      );
    case GradeType.Manual:
      return (
        <Space align="start">
          <GradeIcon type={assessment.type} />
          {assessment.title}
        </Space>
      );
    default:
      return null;
  }
}
