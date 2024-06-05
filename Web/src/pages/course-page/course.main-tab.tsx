import CourseContent from './course.content';
import CourseHead from './course.head';
import CourseHeader from './course.header';
import { Course } from '../../models/course.interface';

interface Props {
  course: Course;
}

export default function CourseMainTab({ course }: Props) {
  return (
    <>
      <CourseHead title={course.title} />
      <CourseHeader
        courseId={course.id}
        title={course.title}
        description={course.description}
      />
      <CourseContent course={course} />
    </>
  );
}
