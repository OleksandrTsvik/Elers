import { blue } from '@ant-design/colors';
import { Tooltip } from 'antd';
import dayjs from 'dayjs';

import { ActiveGradeCell } from './course-grades.slice';
import useCourseGradesState from './use-course-grades.state';
import {
  AssessmentItem,
  GradeItemResponse,
  GradeType,
} from '../../models/grade.interface';
import { UserDto } from '../../models/user.interface';
import { DATE_FORMAT } from '../../utils/constants/app.constants';

const isEditedGrade = (gradeType: GradeType, grade?: GradeItemResponse) => {
  if (gradeType === GradeType.Test) {
    return false;
  }

  if (gradeType === GradeType.Assignment && !grade) {
    return false;
  }

  return true;
};

interface Props {
  student: UserDto;
  assessment: AssessmentItem;
  grade: GradeItemResponse | undefined;
}

export default function CourseGradesTdGrade({
  student,
  assessment,
  grade,
}: Props) {
  const { onOpenGradeModal } = useCourseGradesState();

  const handleClick = () => {
    if (!isEditedGrade(assessment.type, grade)) {
      return;
    }

    const activeGradeCell: ActiveGradeCell = {
      assessmentId: assessment.id,
      title: assessment.title,
      gradeType: assessment.type,
      student,
    };

    if (
      assessment.type === GradeType.Manual ||
      assessment.type === GradeType.Assignment
    ) {
      activeGradeCell.maxGrade = assessment.maxGrade;
    }

    if (grade) {
      activeGradeCell.gradeId = grade.gradeId;
      activeGradeCell.grade = grade.grade;
    }

    onOpenGradeModal(activeGradeCell);
  };

  return (
    <td
      onClick={handleClick}
      style={
        isEditedGrade(assessment.type, grade)
          ? {
              cursor: 'pointer',
              backgroundColor: blue[4],
            }
          : undefined
      }
    >
      {grade && (
        <Tooltip
          title={
            <>
              <div>{dayjs(grade.createdAt).format(DATE_FORMAT)}</div>
              {(grade.type === GradeType.Assignment ||
                grade.type === GradeType.Manual) &&
                grade.teacher && (
                  <div>
                    {grade.teacher.lastName} {grade.teacher.firstName}{' '}
                    {grade.teacher.patronymic}
                  </div>
                )}
            </>
          }
        >
          <span>{grade.grade}</span>
        </Tooltip>
      )}
    </td>
  );
}
