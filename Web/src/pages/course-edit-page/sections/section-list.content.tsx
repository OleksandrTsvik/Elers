import SectionListItem from './section-list.item';
import { CourseTab } from '../../../models/course-tab.interface';
import { CourseTabsEmpty } from '../../../shared';

interface Props {
  sections: CourseTab[];
}

export default function SectionListContent({ sections }: Props) {
  if (!sections.length) {
    return <CourseTabsEmpty />;
  }

  return sections.map((item) => (
    <SectionListItem key={item.id} section={item} />
  ));
}
