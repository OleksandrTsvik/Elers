import { Skeleton } from 'antd';
import { useTranslation } from 'react-i18next';
import { useParams } from 'react-router-dom';

import CourseGradesHead from './course-grades.head';
import CourseGradesTdContent from './course-grades.td-content';
import CourseGradesThContent from './course-grades.th-content';
import { useGetCourseGradesQuery } from '../../api/grades.api';
import { NavigateToError, NavigateToNotFound } from '../../common/navigate';
import { TableContainer, UserAvatar } from '../../components';

import styles from './course-grades.module.scss';

export default function CourseGradesPage() {
  const { courseId } = useParams();
  const { t } = useTranslation();

  const { data, isFetching, error } = useGetCourseGradesQuery({ courseId });

  if (isFetching) {
    return <Skeleton active />;
  }

  if (error) {
    return <NavigateToError error={error} />;
  }

  if (!data) {
    return <NavigateToNotFound />;
  }

  return (
    <>
      <CourseGradesHead />
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
    </>
  );
}
