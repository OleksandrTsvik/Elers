import SectionCreationModalButton from './sections/section-creation.button';
import SectionListContent from './sections/section-list.content';
import { CourseTab } from '../../models/course.interface';

interface Props {
  courseTabs: CourseTab[];
}

export default function CourseEditTabs({ courseTabs }: Props) {
  return (
    <>
      <SectionCreationModalButton />
      <SectionListContent sections={courseTabs} />
    </>
  );
}
