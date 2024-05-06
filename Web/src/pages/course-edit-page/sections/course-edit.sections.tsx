import { Dispatch, SetStateAction } from 'react';

import SectionCreationContent from './section-creation.content';
import SectionListContent from './section-list.content';
import { CourseTab } from '../../../models/course.interface';

interface Props {
  courseId: string;
  sections: CourseTab[];
  onUpdateSections: Dispatch<SetStateAction<CourseTab[]>>;
}

export default function CourseEditSections({
  courseId,
  sections,
  onUpdateSections,
}: Props) {
  const handleCreationSection = (section: CourseTab) => {
    onUpdateSections((prevState) => [...prevState, section]);
  };

  const handleUpdateSection = (section: CourseTab) => {
    onUpdateSections((prevState) =>
      prevState.map((item) => (item.id === section.id ? section : item)),
    );
  };

  const handleDeleteSection = (section: CourseTab) => {
    onUpdateSections((prevState) =>
      prevState.filter((item) => item.id !== section.id),
    );
  };

  return (
    <>
      <SectionCreationContent
        courseId={courseId}
        onCreationSection={handleCreationSection}
      />
      <SectionListContent
        sections={sections}
        onUpdateSection={handleUpdateSection}
        onDeleteSection={handleDeleteSection}
      />
    </>
  );
}
