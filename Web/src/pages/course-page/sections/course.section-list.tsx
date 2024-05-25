import CourseSectionListItem from './course.section-list-item';
import { CourseTab } from '../../../models/course-tab.interface';
import { CourseTabsEmpty } from '../../../shared';

interface Props {
  tabs: CourseTab[];
}

export default function CourseSectionList({ tabs }: Props) {
  if (!tabs.length) {
    return <CourseTabsEmpty />;
  }

  return tabs.map((item) => <CourseSectionListItem key={item.id} tab={item} />);
}
