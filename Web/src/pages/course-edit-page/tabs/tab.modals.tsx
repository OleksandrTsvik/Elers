import { CourseTabType } from '../../../shared';
import TabColorUpdateModal from '../modals/tab-color.update-modal';
import TabNameCreateModal from '../modals/tab-name.create-modal';
import TabNameUpdateModal from '../modals/tab-name.update-modal';
import useCourseEditState from '../use-course-edit.state';

interface Props {
  courseId: string;
  tabType: CourseTabType;
}

export default function TabModals({ courseId, tabType }: Props) {
  const { activeCourseTab } = useCourseEditState();

  return (
    <>
      <TabNameCreateModal courseId={courseId} tabType={tabType} />

      {activeCourseTab && (
        <>
          <TabNameUpdateModal courseTab={activeCourseTab} />
          <TabColorUpdateModal courseTab={activeCourseTab} />
        </>
      )}
    </>
  );
}
