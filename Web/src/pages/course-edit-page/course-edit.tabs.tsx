import SectionCreationModalButton from './sections/section-creation.button';
import SectionListContent from './sections/section-list.content';
import TabListContent from './tabs/tab-list.content';
import { CourseTab } from '../../models/course-tab.interface';
import { CourseTabType } from '../../shared';

interface Props {
  tabType?: string;
  courseTabs: CourseTab[];
}

export default function CourseEditTabs({ tabType, courseTabs }: Props) {
  switch (tabType) {
    case CourseTabType.Sections:
      return (
        <>
          <SectionCreationModalButton />
          <SectionListContent sections={courseTabs} />
        </>
      );
    default:
      return <TabListContent tabs={courseTabs} />;
  }
}
