import TabNameCreateModal from '../modals/tab-name.create-modal';
import TabNameUpdateModal from '../modals/tab-name.update-modal';
import useCourseEditState from '../use-course-edit.state';

interface Props {
  courseId: string;
}

export default function TabModals({ courseId }: Props) {
  const { activeCourseTab } = useCourseEditState();

  return (
    <>
      <TabNameCreateModal courseId={courseId} />
      {activeCourseTab && <TabNameUpdateModal courseTab={activeCourseTab} />}
    </>
  );
}
