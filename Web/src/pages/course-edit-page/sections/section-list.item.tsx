import { Divider } from 'antd';

import SectionListItemTitle from './section-list.item-title';
import { CourseTab } from '../../../models/course.interface';
import CourseEditTabContent from '../tabs/course-edit.tab-content';

interface Props {
  section: CourseTab;
  onUpdateSection: (section: CourseTab) => void;
  onDeleteSection: (section: CourseTab) => void;
}

export default function SectionListItem({
  section,
  onDeleteSection,
  onUpdateSection,
}: Props) {
  return (
    <>
      <Divider />
      <SectionListItemTitle
        section={section}
        onUpdateSection={onUpdateSection}
        onDeleteSection={onDeleteSection}
      />
      <CourseEditTabContent tab={section} />
    </>
  );
}
