import useTabActions from './use-tab.actions';
import { SettingsDropdown } from '../../../components';
import { CourseTab } from '../../../models/course.interface';

interface Props {
  courseTab: CourseTab;
}

export default function TabSettingsDropdown({ courseTab }: Props) {
  const tabActions = useTabActions(courseTab);

  return <SettingsDropdown items={tabActions} />;
}
