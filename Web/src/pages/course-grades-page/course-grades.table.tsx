import { useTranslation } from 'react-i18next';

import CourseGradesTdGrade from './course-grades.td-grade';
import CourseGradesTh from './course-grades.th';
import { TableContainer, UserAvatar } from '../../components';
import { GetCourseGradesResponse } from '../../models/grade.interface';
import { GradeIcon } from '../../shared';

import styles from './course-grades.module.scss';

interface Props {
  data: GetCourseGradesResponse;
}

export default function CourseGradesTable({ data }: Props) {
  const { t } = useTranslation();

  return (
    <TableContainer>
      <table className={styles.table}>
        <thead>
          <tr>
            <th rowSpan={2}>#</th>
            <th rowSpan={2}>{t('user.full_name')}</th>
            {data.assessments.map((item) => (
              <CourseGradesTh key={item.id} value={item} />
            ))}
          </tr>
          <tr>
            {data.assessments.map(({ id, type }) => (
              <th key={id} style={{ padding: 2 }}>
                <GradeIcon type={type} />
              </th>
            ))}
          </tr>
        </thead>
        <tbody>
          {data.studentGrades.map(({ student, grades }, index) => (
            <tr key={student.id}>
              <td>{index + 1}</td>
              <td>
                <UserAvatar className="mr-avatar" url={student.avatarUrl} />
                {student.lastName} {student.firstName} {student.patronymic}
              </td>
              {data.assessments.map((item) => (
                <CourseGradesTdGrade
                  key={item.id}
                  student={student}
                  assessment={item}
                  grade={grades.find((grade) => grade.assessmentId == item.id)}
                />
              ))}
            </tr>
          ))}
        </tbody>
      </table>
    </TableContainer>
  );
}
