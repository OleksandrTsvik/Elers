import { Path } from 'react-router-dom';

import { CourseTabType } from '../course-tabs/course-tab.enum';

export function getCourseEditPagePath(
  courseId: string,
  tabId: string,
  courseTabType: CourseTabType,
): Partial<Path> {
  const path: Partial<Path> = {
    pathname: `/courses/edit/${courseId}`,
  };

  switch (courseTabType) {
    case CourseTabType.Tabs:
      path.search = `tab=${tabId}`;
      break;
  }

  return path;
}
