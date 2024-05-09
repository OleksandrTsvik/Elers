import MaterialCreationButton from './material-creation.button';
import { CourseTab } from '../../../models/course-tab.interface';

interface Props {
  tab: CourseTab;
}

export default function TabContent({ tab }: Props) {
  return (
    <>
      <p>{tab.id}</p>
      <MaterialCreationButton />
    </>
  );
}
