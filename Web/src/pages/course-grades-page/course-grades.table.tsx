import { useTranslation } from 'react-i18next';

import CourseGradesTdContent from './course-grades.td-content';
import CourseGradesThContent from './course-grades.th-content';
import { TableContainer, UserAvatar } from '../../components';
import { GetCourseGradesResponse } from '../../models/grade.interface';

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
            <th>#</th>
            <th>{t('user.full_name')}</th>
            {data.assessments.map((item) => (
              <th key={item.id}>
                <CourseGradesThContent value={item} />
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
                <td key={item.id}>
                  <CourseGradesTdContent
                    value={grades.find(
                      (grade) => grade.assessmentId == item.id,
                    )}
                  />
                </td>
              ))}
            </tr>
          ))}
        </tbody>
      </table>
    </TableContainer>
  );
}
