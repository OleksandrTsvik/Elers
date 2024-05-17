import { CourseTab } from '../../../models/course-tab.interface';
import MaterialCreationButton from '../material-creation/material-creation.button';

interface Props {
  tab: CourseTab;
}

export default function TabContent({ tab }: Props) {
  return (
    <>
      <p>{tab.id}</p>
      <MaterialCreationButton tabId={tab.id} />
    </>
  );
}
