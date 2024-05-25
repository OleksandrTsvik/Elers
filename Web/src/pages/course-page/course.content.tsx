import CourseSectionList from './sections/course.section-list';
import CourseTabList from './tabs/course.tab-list';
import { Course } from '../../models/course.interface';
import { CourseTabType } from '../../shared';

interface Props {
  course: Course;
}

export default function CourseContent({ course }: Props) {
  switch (course.tabType) {
    case CourseTabType.Sections:
      return <CourseSectionList tabs={course.courseTabs} />;
    default:
      return <CourseTabList tabs={course.courseTabs} />;
  }
}
