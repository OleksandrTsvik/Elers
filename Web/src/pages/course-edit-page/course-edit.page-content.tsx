import CourseEditBreadcrumb from './course-edit.breadcrumb';
import CourseEditDescription from './course-edit.description';
import CourseEditHead from './course-edit.head';
import CourseEditTabs from './course-edit.tabs';
import CourseEditTitle from './course-edit.title';
import TabModals from './tabs/tab.modals';
import { Course } from '../../models/course.interface';

interface Props {
  course: Course;
}

export default function CourseEditPageContent({ course }: Props) {
  return (
    <>
      <CourseEditHead title={course.title} />
      <CourseEditBreadcrumb courseId={course.id} title={course.title} />
      <CourseEditTitle courseId={course.id} title={course.title} />
      <CourseEditDescription
        courseId={course.id}
        description={course.description}
      />
      <CourseEditTabs courseTabs={course.courseTabs} />
      <TabModals courseId={course.id} />
    </>
  );
}
