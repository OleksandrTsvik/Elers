import { useParams } from 'react-router-dom';

import CourseMyGradesHead from './course-my-grades.head';
import { MyGradesTable } from '../../shared';

export default function CourseMyGradesPage() {
  const { courseId } = useParams();

  return (
    <>
      <CourseMyGradesHead />
      <MyGradesTable courseId={courseId} />
    </>
  );
}
